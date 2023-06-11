namespace Upscaler
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.fileDialogPanel = new System.Windows.Forms.Panel();
            this.selectFolderButton = new System.Windows.Forms.Button();
            this.selectFilesButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.overallProgressBar = new System.Windows.Forms.ProgressBar();
            this.cancelButton = new System.Windows.Forms.Button();
            this.currentActionProgressBar = new System.Windows.Forms.ProgressBar();
            this.pauseButton = new System.Windows.Forms.Button();
            this.totalFileCountLabel = new System.Windows.Forms.Label();
            this.currentFileLabel = new System.Windows.Forms.Label();
            this.progressLabel = new System.Windows.Forms.Label();
            this.currentActionLabel = new System.Windows.Forms.Label();
            this.selectLabel = new System.Windows.Forms.Label();
            this.animeRadioButton = new System.Windows.Forms.RadioButton();
            this.mediaTypePanel = new System.Windows.Forms.Panel();
            this.realisticRadioButton = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.scaleLevelPanel = new System.Windows.Forms.Panel();
            this.x4radioButton = new System.Windows.Forms.RadioButton();
            this.x3radioButton = new System.Windows.Forms.RadioButton();
            this.x2radioButton = new System.Windows.Forms.RadioButton();
            this.scaleLevelLabel = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.videoBreakProgressBar = new System.Windows.Forms.ProgressBar();
            this.videoMergeProgressBar = new System.Windows.Forms.ProgressBar();
            this.videoUpscaleProgresslabel = new System.Windows.Forms.Label();
            this.fileDialogPanel.SuspendLayout();
            this.mediaTypePanel.SuspendLayout();
            this.scaleLevelPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileDialogPanel
            // 
            this.fileDialogPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.fileDialogPanel.Controls.Add(this.selectFolderButton);
            this.fileDialogPanel.Controls.Add(this.selectFilesButton);
            this.fileDialogPanel.Location = new System.Drawing.Point(290, 12);
            this.fileDialogPanel.Name = "fileDialogPanel";
            this.fileDialogPanel.Size = new System.Drawing.Size(213, 30);
            this.fileDialogPanel.TabIndex = 0;
            // 
            // selectFolderButton
            // 
            this.selectFolderButton.Location = new System.Drawing.Point(109, 3);
            this.selectFolderButton.Name = "selectFolderButton";
            this.selectFolderButton.Size = new System.Drawing.Size(100, 23);
            this.selectFolderButton.TabIndex = 1;
            this.selectFolderButton.Text = "Select folder";
            this.selectFolderButton.UseVisualStyleBackColor = true;
            this.selectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // selectFilesButton
            // 
            this.selectFilesButton.Location = new System.Drawing.Point(3, 3);
            this.selectFilesButton.Name = "selectFilesButton";
            this.selectFilesButton.Size = new System.Drawing.Size(100, 23);
            this.selectFilesButton.TabIndex = 0;
            this.selectFilesButton.Text = "Select files";
            this.selectFilesButton.UseVisualStyleBackColor = true;
            this.selectFilesButton.Click += new System.EventHandler(this.SelectFile_Click);
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileNameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fileNameLabel.Location = new System.Drawing.Point(12, 16);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(760, 22);
            this.fileNameLabel.TabIndex = 1;
            this.fileNameLabel.Text = "File Name";
            this.fileNameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.fileNameLabel.Visible = false;
            // 
            // overallProgressBar
            // 
            this.overallProgressBar.Location = new System.Drawing.Point(12, 229);
            this.overallProgressBar.Name = "overallProgressBar";
            this.overallProgressBar.Size = new System.Drawing.Size(668, 21);
            this.overallProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.overallProgressBar.TabIndex = 2;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(697, 229);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 21);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // currentActionProgressBar
            // 
            this.currentActionProgressBar.Location = new System.Drawing.Point(12, 301);
            this.currentActionProgressBar.Name = "currentActionProgressBar";
            this.currentActionProgressBar.Size = new System.Drawing.Size(668, 21);
            this.currentActionProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.currentActionProgressBar.TabIndex = 2;
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(697, 301);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(75, 21);
            this.pauseButton.TabIndex = 8;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            // 
            // totalFileCountLabel
            // 
            this.totalFileCountLabel.AutoSize = true;
            this.totalFileCountLabel.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.totalFileCountLabel.Location = new System.Drawing.Point(12, 211);
            this.totalFileCountLabel.Name = "totalFileCountLabel";
            this.totalFileCountLabel.Size = new System.Drawing.Size(34, 12);
            this.totalFileCountLabel.TabIndex = 4;
            this.totalFileCountLabel.Text = "21/181";
            this.totalFileCountLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // currentFileLabel
            // 
            this.currentFileLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.currentFileLabel.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.currentFileLabel.Location = new System.Drawing.Point(360, 204);
            this.currentFileLabel.Name = "currentFileLabel";
            this.currentFileLabel.Size = new System.Drawing.Size(320, 20);
            this.currentFileLabel.TabIndex = 4;
            this.currentFileLabel.Text = "spiderman.png";
            this.currentFileLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.progressLabel.Location = new System.Drawing.Point(12, 286);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(34, 12);
            this.progressLabel.TabIndex = 4;
            this.progressLabel.Text = "21/181";
            this.progressLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // currentActionLabel
            // 
            this.currentActionLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.currentActionLabel.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.currentActionLabel.Location = new System.Drawing.Point(440, 278);
            this.currentActionLabel.Name = "currentActionLabel";
            this.currentActionLabel.Size = new System.Drawing.Size(240, 20);
            this.currentActionLabel.TabIndex = 4;
            this.currentActionLabel.Text = "Breaking video into frames";
            this.currentActionLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // selectLabel
            // 
            this.selectLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.selectLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.selectLabel.Location = new System.Drawing.Point(12, 40);
            this.selectLabel.Name = "selectLabel";
            this.selectLabel.Size = new System.Drawing.Size(760, 15);
            this.selectLabel.TabIndex = 1;
            this.selectLabel.Text = "Selecting a folder will upscale every image/video in that folder";
            this.selectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // animeRadioButton
            // 
            this.animeRadioButton.AutoSize = true;
            this.animeRadioButton.Location = new System.Drawing.Point(3, 3);
            this.animeRadioButton.Name = "animeRadioButton";
            this.animeRadioButton.Size = new System.Drawing.Size(81, 19);
            this.animeRadioButton.TabIndex = 2;
            this.animeRadioButton.TabStop = true;
            this.animeRadioButton.Text = "Animation";
            this.animeRadioButton.UseVisualStyleBackColor = true;
            // 
            // mediaTypePanel
            // 
            this.mediaTypePanel.Controls.Add(this.realisticRadioButton);
            this.mediaTypePanel.Controls.Add(this.animeRadioButton);
            this.mediaTypePanel.Location = new System.Drawing.Point(310, 71);
            this.mediaTypePanel.Name = "mediaTypePanel";
            this.mediaTypePanel.Size = new System.Drawing.Size(174, 27);
            this.mediaTypePanel.TabIndex = 6;
            // 
            // realisticRadioButton
            // 
            this.realisticRadioButton.AutoSize = true;
            this.realisticRadioButton.Location = new System.Drawing.Point(103, 3);
            this.realisticRadioButton.Name = "realisticRadioButton";
            this.realisticRadioButton.Size = new System.Drawing.Size(68, 19);
            this.realisticRadioButton.TabIndex = 3;
            this.realisticRadioButton.TabStop = true;
            this.realisticRadioButton.Text = "Realistic";
            this.realisticRadioButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(12, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(760, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "What kind of image/video are you upscaling?";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scaleLevelPanel
            // 
            this.scaleLevelPanel.Controls.Add(this.x4radioButton);
            this.scaleLevelPanel.Controls.Add(this.x3radioButton);
            this.scaleLevelPanel.Controls.Add(this.x2radioButton);
            this.scaleLevelPanel.Location = new System.Drawing.Point(327, 130);
            this.scaleLevelPanel.Name = "scaleLevelPanel";
            this.scaleLevelPanel.Size = new System.Drawing.Size(132, 27);
            this.scaleLevelPanel.TabIndex = 6;
            // 
            // x4radioButton
            // 
            this.x4radioButton.AutoSize = true;
            this.x4radioButton.Location = new System.Drawing.Point(89, 3);
            this.x4radioButton.Name = "x4radioButton";
            this.x4radioButton.Size = new System.Drawing.Size(38, 19);
            this.x4radioButton.TabIndex = 6;
            this.x4radioButton.TabStop = true;
            this.x4radioButton.Text = "X4";
            this.x4radioButton.UseVisualStyleBackColor = true;
            // 
            // x3radioButton
            // 
            this.x3radioButton.AutoSize = true;
            this.x3radioButton.Location = new System.Drawing.Point(48, 3);
            this.x3radioButton.Name = "x3radioButton";
            this.x3radioButton.Size = new System.Drawing.Size(38, 19);
            this.x3radioButton.TabIndex = 5;
            this.x3radioButton.TabStop = true;
            this.x3radioButton.Text = "X3";
            this.x3radioButton.UseVisualStyleBackColor = true;
            // 
            // x2radioButton
            // 
            this.x2radioButton.AutoSize = true;
            this.x2radioButton.Location = new System.Drawing.Point(3, 3);
            this.x2radioButton.Name = "x2radioButton";
            this.x2radioButton.Size = new System.Drawing.Size(38, 19);
            this.x2radioButton.TabIndex = 4;
            this.x2radioButton.TabStop = true;
            this.x2radioButton.Text = "X2";
            this.x2radioButton.UseVisualStyleBackColor = true;
            // 
            // scaleLevelLabel
            // 
            this.scaleLevelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scaleLevelLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.scaleLevelLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.scaleLevelLabel.Location = new System.Drawing.Point(12, 155);
            this.scaleLevelLabel.Name = "scaleLevelLabel";
            this.scaleLevelLabel.Size = new System.Drawing.Size(760, 15);
            this.scaleLevelLabel.TabIndex = 1;
            this.scaleLevelLabel.Text = "What level of upscaling would you like to use? X4 will upscale it to 4 times its " +
    "resolution, but will take much longer than x2";
            this.scaleLevelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // videoBreakProgressBar
            // 
            this.videoBreakProgressBar.Location = new System.Drawing.Point(12, 301);
            this.videoBreakProgressBar.Name = "videoBreakProgressBar";
            this.videoBreakProgressBar.Size = new System.Drawing.Size(67, 21);
            this.videoBreakProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.videoBreakProgressBar.TabIndex = 2;
            // 
            // videoMergeProgressBar
            // 
            this.videoMergeProgressBar.Location = new System.Drawing.Point(613, 301);
            this.videoMergeProgressBar.Name = "videoMergeProgressBar";
            this.videoMergeProgressBar.Size = new System.Drawing.Size(67, 21);
            this.videoMergeProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.videoMergeProgressBar.TabIndex = 2;
            // 
            // videoUpscaleProgresslabel
            // 
            this.videoUpscaleProgresslabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.videoUpscaleProgresslabel.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.videoUpscaleProgresslabel.Location = new System.Drawing.Point(204, 278);
            this.videoUpscaleProgresslabel.Name = "videoUpscaleProgresslabel";
            this.videoUpscaleProgresslabel.Size = new System.Drawing.Size(295, 20);
            this.videoUpscaleProgresslabel.TabIndex = 4;
            this.videoUpscaleProgresslabel.Text = "33.33%";
            this.videoUpscaleProgresslabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 341);
            this.Controls.Add(this.scaleLevelPanel);
            this.Controls.Add(this.mediaTypePanel);
            this.Controls.Add(this.videoUpscaleProgresslabel);
            this.Controls.Add(this.currentActionLabel);
            this.Controls.Add(this.currentFileLabel);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.totalFileCountLabel);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.videoMergeProgressBar);
            this.Controls.Add(this.videoBreakProgressBar);
            this.Controls.Add(this.currentActionProgressBar);
            this.Controls.Add(this.overallProgressBar);
            this.Controls.Add(this.scaleLevelLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectLabel);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.fileDialogPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Real-ESRGAN Upscaler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.fileDialogPanel.ResumeLayout(false);
            this.mediaTypePanel.ResumeLayout(false);
            this.mediaTypePanel.PerformLayout();
            this.scaleLevelPanel.ResumeLayout(false);
            this.scaleLevelPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel fileDialogPanel;
        private Button selectFolderButton;
        private Button selectFilesButton;
        private OpenFileDialog openFileDialog;
        private Label fileNameLabel;
        private ProgressBar overallProgressBar;
        private Button cancelButton;
        private ProgressBar currentActionProgressBar;
        private Button pauseButton;
        private Label totalFileCountLabel;
        private Label currentFileLabel;
        private Label progressLabel;
        private Label currentActionLabel;
        private Label selectLabel;
        private RadioButton animeRadioButton;
        private Panel mediaTypePanel;
        private RadioButton realisticRadioButton;
        private Label label2;
        private Panel scaleLevelPanel;
        private RadioButton x4radioButton;
        private RadioButton x3radioButton;
        private RadioButton x2radioButton;
        private Label scaleLevelLabel;
        private FolderBrowserDialog folderBrowserDialog;
        private ProgressBar videoBreakProgressBar;
        private ProgressBar videoMergeProgressBar;
        private Label videoUpscaleProgresslabel;
    }
}