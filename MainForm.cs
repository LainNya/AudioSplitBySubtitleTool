using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xabe.FFmpeg;

namespace 按字幕截取音频
{
    public partial class MainForm : Form
    {
        private Task LoadSubTask;
        private bool IsMutiSub = false;
        public MainForm()
        {
            InitializeComponent();
        }

        public class SubTimeLine
        {
            public string Language { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public string SubStr { get; set; }
        }

        List<string> FirstList;
        List<SubTimeLine> subTimeLines;
        string VideoPath = string.Empty;
        private void BtnSelectSub_Click(object sender, EventArgs e)
        {
            FirstList = new List<string>();
            subTimeLines = new List<SubTimeLine>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "ass字幕文件|*.ass|srt字幕文件|*.srt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                if (openFileDialog.FileName.Contains(".ass"))
                {
                    LoadSubTask = Task.Factory.StartNew(() =>
                    {
                        SplitAss(openFileDialog.FileName);
                    });
                }
                else if (openFileDialog.FileName.Contains(".srt"))
                {
                    LoadSubTask = Task.Factory.StartNew(() =>
                    {
                        SplitSrt(openFileDialog.FileName);
                    });
                }
            }
        }

        private void SplitAss(string Path)
        {
            BeginInvoke(() =>
            {
                LBSubCount.Text = "载入中";
            });
            string[] ReadText = File.ReadAllLines(Path, Encoding.UTF8);
            string SubF = "Dialogue:";
            foreach (var item in ReadText)
            {
                if (item.ToUpper().Contains(SubF.ToUpper()))
                {
                    string TempItem = item.Replace(SubF, string.Empty);
                    TempItem = TempItem.Replace("\\N", string.Empty);
                    FirstList.Add(TempItem.Trim());
                }
            }

            foreach (var item in FirstList)
            {
                string[] textSplit = item.Split(',');
                if (IsMutiSub)
                {
                    if (!textSplit[3].ToUpper().Contains("JP"))
                    {
                        continue;
                    }
                }
                string StartTime = textSplit[1].Contains(".") ? textSplit[1].Replace(".", ":") : textSplit[1].Replace(",", ":");
                string EndTime = textSplit[2].Contains(".") ? textSplit[2].Replace(".", ":") : textSplit[2].Replace(",", ":");
                SubTimeLine subTimeLine = new SubTimeLine
                {
                    Language = textSplit[3].Trim(),
                    StartTime = ConvertToTimeSpan(StartTime.Trim()),
                    EndTime = ConvertToTimeSpan(EndTime.Trim()),
                    SubStr = textSplit[9]
                };
                subTimeLines.Add(subTimeLine);
            }

            if (subTimeLines.Count > 0 && IsMutiSub)
            {
                BeginInvoke(() =>
                {
                    CMBlanguage.Items.Clear();
                    foreach (var item in subTimeLines.DistinctBy(i => i.Language).ToList())
                    {
                        CMBlanguage.Items.Add(item.Language);
                    }
                });

            }
            BeginInvoke(() =>
            {
                LBSubCount.Text = "载入完成";
            });
        }

        private void SplitSrt(string Path)
        {
            BeginInvoke(() =>
            {
                LBSubCount.Text = "载入中";
            });
            string[] ReadText = File.ReadAllLines(Path, Encoding.UTF8);
            List<string> NewStrList = new List<string>();
            foreach (var item in ReadText)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string TempStr = item.Replace("\r", string.Empty);
                    TempStr = TempStr.Replace("\n", string.Empty);
                    NewStrList.Add(TempStr);
                }
            }
            bool IsFirst = true;
            List<string> lineList = new List<string>();
            List<string> TimeLineList = new List<string>();
            foreach (var item in NewStrList)
            {
                if (item.All(char.IsDigit))
                {
                    if (IsFirst == false)
                    {
                        TimeLineList.Add(string.Join("|", lineList));
                        lineList.Clear();
                        IsFirst = true;
                    }
                    lineList.Add(item);
                }
                else
                {
                    IsFirst = false;
                    lineList.Add(item);
                }
            }
            if (lineList.Count > 0)
            {
                TimeLineList.Add(string.Join("|", lineList));
                lineList.Clear();
            }
            foreach (var item in TimeLineList)
            {
                string[] items = item.Split('|');
                string TimeLine = items[1].Replace("-->", "|");
                string[] times = TimeLine.Split('|');
                SubTimeLine subTimeLine = new SubTimeLine
                {
                    StartTime = ConvertToTimeSpan(times[0].Replace(",", ":").Trim()),
                    EndTime = ConvertToTimeSpan(times[1].Replace(",", ":").Trim()),
                    SubStr = items[2]
                };
                subTimeLines.Add(subTimeLine);

            }

            BeginInvoke(() =>
            {
                LBSubCount.Text = "载入完成";
            });
        }

        private TimeSpan ConvertToTimeSpan(string text)
        {
            string cadena = text;
            string[] partes = cadena.Split(new char[] { ':' }).Select(x => x.ToString()).ToArray();
            string MiiSec = partes[3].ToString().PadRight(3, '0');
            TimeSpan returnSpan = new TimeSpan(0, Convert.ToInt32(partes[0]), Convert.ToInt32(partes[1]), Convert.ToInt32(partes[2]), Convert.ToInt32(MiiSec));
            return returnSpan;
        }

        private async Task<bool> SplitAudioAsync(string Path)
        {
            bool IsSucc = true;
            int index = Convert.ToInt32(TxtFileIndex.Text);
            try
            {
                IMediaInfo info = await FFmpeg.GetMediaInfo(Path);
                if (IsMutiSub && !string.IsNullOrEmpty(CMBlanguage.Text))
                {
                    var ItemList = subTimeLines.Where(x => x.Language == CMBlanguage.Text);
                    foreach (var item in ItemList)
                    {
                        index = index + 1;
                        int FileIndex = index;
                        TimeSpan timeSpan = ConvertToTimeSpan(TxtSubTimeSync.Text);
                        TimeSpan NewStartTime = item.StartTime.Add(timeSpan);
                        TimeSpan NewEndTime = item.EndTime.Add(timeSpan);
                        TimeSpan duration = NewEndTime - NewStartTime;
                        IAudioStream? audio = info.AudioStreams.FirstOrDefault()?.Split(NewStartTime, duration);
                        string OutPath = Environment.CurrentDirectory.ToString() + "\\" + "SplitWavs\\" + FileIndex.ToString() + ".wav";
                        await FFmpeg.Conversions.New()
                  .AddStream(audio)
                  .SetOutput(OutPath)
                  .Start();
                        LBFiles.Items.Add(OutPath);
                        LBFiles.SelectedIndex = LBFiles.Items.Count - 1;
                        string result1 = Environment.CurrentDirectory.ToString() + "\\" + "SplitWavs\\" + "list.txt";//
                        FileStream fs = new FileStream(result1, FileMode.Append);
                        StreamWriter wr = wr = new StreamWriter(fs);
                        wr.WriteLine("wavs/" + FileIndex + ".wav" + "|" + item.SubStr.Trim());
                        wr.Close();
                    }
                }
                else
                {
                    foreach (var item in subTimeLines)
                    {
                        index = index + 1;
                        int FileIndex = index;
                        TimeSpan timeSpan = ConvertToTimeSpan(TxtSubTimeSync.Text);
                        TimeSpan NewStartTime = item.StartTime.Add(timeSpan);
                        TimeSpan NewEndTime = item.EndTime.Add(timeSpan);
                        TimeSpan duration = NewEndTime - NewStartTime;
                        IAudioStream? audio = info.AudioStreams.FirstOrDefault()?.Split(NewStartTime, duration);
                        string OutPath = Environment.CurrentDirectory.ToString() + "\\" + "SplitWavs\\" + FileIndex.ToString() + ".wav";
                        await FFmpeg.Conversions.New()
                  .AddStream(audio)
                  .SetOutput(OutPath)
                  .Start();
                        LBFiles.Items.Add(OutPath);
                        LBFiles.SelectedIndex = LBFiles.Items.Count - 1;
                        string result1 = Environment.CurrentDirectory.ToString() + "\\" + "SplitWavs\\" + "list.txt";//
                        FileStream fs = new FileStream(result1, FileMode.Append);
                        StreamWriter wr = wr = new StreamWriter(fs);
                        wr.WriteLine("wavs/" + FileIndex + ".wav" + "|" + item.SubStr.Trim());
                        wr.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                LBFiles.Items.Add(index + ":" + ex.Message);
                MessageBox.Show(ex.Message);
                IsSucc = false;
            }
            return IsSucc;
        }

        private void BtnSelectVideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MKV视频文件|*.mkv|MP4视频文件|*.mp4|WAV音频|*.wav";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                VideoPath = openFileDialog.FileName;
            }
        }

        private async void BtnStart_Click(object sender, EventArgs e)
        {
            if (subTimeLines.Count > 0)
            {
                if (!LoadSubTask.IsCompleted)
                {
                    MessageBox.Show("请等待字幕载入完毕!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请载入字幕!");
                return;
            }
            if (string.IsNullOrEmpty(VideoPath))
            {
                return;
            }
            if (MessageBox.Show("是否开始分割?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (await SplitAudioAsync(VideoPath))
                {
                    MessageBox.Show("分割成功!");
                }
                else
                {
                    MessageBox.Show("分割失败!");
                }
            }
        }

        private async void BtnGetAudio_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(VideoPath))
                {
                    return;
                }
                IMediaInfo info = await FFmpeg.GetMediaInfo(VideoPath);
                IAudioStream? audio = info.AudioStreams.FirstOrDefault();
                string OutPath = Environment.CurrentDirectory.ToString() + "\\" + "wavs\\" + "audio" + ".wav";
                await FFmpeg.Conversions.New()
          .AddStream(audio)
          .SetOutput(OutPath)
          .Start();
                LBFiles.Items.Add(OutPath);
                LBFiles.SelectedIndex = LBFiles.Items.Count - 1;
            }
            catch (Exception)
            {
                MessageBox.Show("提取错误");
            }
        }

        private void CKIsMutiSub_CheckedChanged(object sender, EventArgs e)
        {
            IsMutiSub = CKIsMutiSub.Checked;
        }
    }
}
