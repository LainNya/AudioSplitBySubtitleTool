namespace 按字幕截取音频
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnSelectSub = new System.Windows.Forms.Button();
            this.BtnSelectVideo = new System.Windows.Forms.Button();
            this.LBFiles = new System.Windows.Forms.ListBox();
            this.LBSubCount = new System.Windows.Forms.Label();
            this.BtnSplit = new System.Windows.Forms.Button();
            this.BtnGetAudio = new System.Windows.Forms.Button();
            this.TxtFileIndex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtSubTimeSync = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CMBlanguage = new System.Windows.Forms.ComboBox();
            this.CKIsMutiSub = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // BtnSelectSub
            // 
            this.BtnSelectSub.Location = new System.Drawing.Point(12, 12);
            this.BtnSelectSub.Name = "BtnSelectSub";
            this.BtnSelectSub.Size = new System.Drawing.Size(94, 29);
            this.BtnSelectSub.TabIndex = 0;
            this.BtnSelectSub.Text = "选择字幕";
            this.BtnSelectSub.UseVisualStyleBackColor = true;
            this.BtnSelectSub.Click += new System.EventHandler(this.BtnSelectSub_Click);
            // 
            // BtnSelectVideo
            // 
            this.BtnSelectVideo.Location = new System.Drawing.Point(12, 47);
            this.BtnSelectVideo.Name = "BtnSelectVideo";
            this.BtnSelectVideo.Size = new System.Drawing.Size(94, 29);
            this.BtnSelectVideo.TabIndex = 1;
            this.BtnSelectVideo.Text = "选择文件";
            this.BtnSelectVideo.UseVisualStyleBackColor = true;
            this.BtnSelectVideo.Click += new System.EventHandler(this.BtnSelectVideo_Click);
            // 
            // LBFiles
            // 
            this.LBFiles.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LBFiles.FormattingEnabled = true;
            this.LBFiles.ItemHeight = 20;
            this.LBFiles.Location = new System.Drawing.Point(0, 86);
            this.LBFiles.Name = "LBFiles";
            this.LBFiles.Size = new System.Drawing.Size(800, 364);
            this.LBFiles.TabIndex = 4;
            // 
            // LBSubCount
            // 
            this.LBSubCount.AutoSize = true;
            this.LBSubCount.Location = new System.Drawing.Point(123, 16);
            this.LBSubCount.Name = "LBSubCount";
            this.LBSubCount.Size = new System.Drawing.Size(0, 20);
            this.LBSubCount.TabIndex = 5;
            // 
            // BtnSplit
            // 
            this.BtnSplit.Location = new System.Drawing.Point(112, 47);
            this.BtnSplit.Name = "BtnSplit";
            this.BtnSplit.Size = new System.Drawing.Size(94, 29);
            this.BtnSplit.TabIndex = 6;
            this.BtnSplit.Text = "分割";
            this.BtnSplit.UseVisualStyleBackColor = true;
            this.BtnSplit.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // BtnGetAudio
            // 
            this.BtnGetAudio.Location = new System.Drawing.Point(212, 47);
            this.BtnGetAudio.Name = "BtnGetAudio";
            this.BtnGetAudio.Size = new System.Drawing.Size(94, 29);
            this.BtnGetAudio.TabIndex = 7;
            this.BtnGetAudio.Text = "提取音频";
            this.BtnGetAudio.UseVisualStyleBackColor = true;
            this.BtnGetAudio.Click += new System.EventHandler(this.BtnGetAudio_ClickAsync);
            // 
            // TxtFileIndex
            // 
            this.TxtFileIndex.Location = new System.Drawing.Point(557, 49);
            this.TxtFileIndex.Name = "TxtFileIndex";
            this.TxtFileIndex.Size = new System.Drawing.Size(125, 27);
            this.TxtFileIndex.TabIndex = 9;
            this.TxtFileIndex.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(467, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "文件开始值";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(437, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "字幕时间轴偏移";
            // 
            // TxtSubTimeSync
            // 
            this.TxtSubTimeSync.Location = new System.Drawing.Point(557, 9);
            this.TxtSubTimeSync.Name = "TxtSubTimeSync";
            this.TxtSubTimeSync.Size = new System.Drawing.Size(125, 27);
            this.TxtSubTimeSync.TabIndex = 11;
            this.TxtSubTimeSync.Text = "0:00:00:000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(688, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "0:00:00:000";
            // 
            // CMBlanguage
            // 
            this.CMBlanguage.FormattingEnabled = true;
            this.CMBlanguage.Location = new System.Drawing.Point(312, 49);
            this.CMBlanguage.Name = "CMBlanguage";
            this.CMBlanguage.Size = new System.Drawing.Size(130, 28);
            this.CMBlanguage.TabIndex = 14;
            // 
            // CKIsMutiSub
            // 
            this.CKIsMutiSub.AutoSize = true;
            this.CKIsMutiSub.Location = new System.Drawing.Point(312, 8);
            this.CKIsMutiSub.Name = "CKIsMutiSub";
            this.CKIsMutiSub.Size = new System.Drawing.Size(106, 24);
            this.CKIsMutiSub.TabIndex = 15;
            this.CKIsMutiSub.Text = "是否多语言";
            this.CKIsMutiSub.UseVisualStyleBackColor = true;
            this.CKIsMutiSub.CheckedChanged += new System.EventHandler(this.CKIsMutiSub_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CKIsMutiSub);
            this.Controls.Add(this.CMBlanguage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtSubTimeSync);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtFileIndex);
            this.Controls.Add(this.BtnGetAudio);
            this.Controls.Add(this.BtnSplit);
            this.Controls.Add(this.LBSubCount);
            this.Controls.Add(this.LBFiles);
            this.Controls.Add(this.BtnSelectVideo);
            this.Controls.Add(this.BtnSelectSub);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "根据字幕分割音频";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button BtnSelectSub;
        private Button BtnSelectVideo;
        private ListBox LBFiles;
        private Label LBSubCount;
        private Button BtnSplit;
        private Button BtnGetAudio;
        private TextBox TxtFileIndex;
        private Label label1;
        private Label label2;
        private TextBox TxtSubTimeSync;
        private Label label3;
        private ComboBox CMBlanguage;
        private CheckBox CKIsMutiSub;
    }
}