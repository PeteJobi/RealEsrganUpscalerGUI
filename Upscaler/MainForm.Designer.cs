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
            fileDialogPanel = new Panel();
            selectFolderButton = new Button();
            selectFilesButton = new Button();
            openFileDialog = new OpenFileDialog();
            fileNameLabel = new Label();
            overallProgressBar = new ProgressBar();
            cancelButton = new Button();
            currentActionProgressBar = new ProgressBar();
            pauseButton = new Button();
            totalFileCountLabel = new Label();
            currentFileLabel = new Label();
            progressLabel = new Label();
            currentActionLabel = new Label();
            selectLabel = new Label();
            animeRadioButton = new RadioButton();
            mediaTypePanel = new Panel();
            realisticRadioButton = new RadioButton();
            label2 = new Label();
            scaleLevelPanel = new Panel();
            x4radioButton = new RadioButton();
            x3radioButton = new RadioButton();
            x2radioButton = new RadioButton();
            scaleLevelLabel = new Label();
            folderBrowserDialog = new FolderBrowserDialog();
            videoBreakProgressBar = new ProgressBar();
            videoMergeProgressBar = new ProgressBar();
            videoUpscaleProgresslabel = new Label();
            fps24checkBox = new CheckBox();
            fps24Label = new Label();
            fileDialogPanel.SuspendLayout();
            mediaTypePanel.SuspendLayout();
            scaleLevelPanel.SuspendLayout();
            SuspendLayout();
            // 
            // fileDialogPanel
            // 
            fileDialogPanel.Anchor = AnchorStyles.Top;
            fileDialogPanel.Controls.Add(selectFolderButton);
            fileDialogPanel.Controls.Add(selectFilesButton);
            fileDialogPanel.Location = new Point(290, 12);
            fileDialogPanel.Name = "fileDialogPanel";
            fileDialogPanel.Size = new Size(213, 30);
            fileDialogPanel.TabIndex = 0;
            // 
            // selectFolderButton
            // 
            selectFolderButton.Location = new Point(109, 3);
            selectFolderButton.Name = "selectFolderButton";
            selectFolderButton.Size = new Size(100, 23);
            selectFolderButton.TabIndex = 1;
            selectFolderButton.Text = "Select folder";
            selectFolderButton.UseVisualStyleBackColor = true;
            selectFolderButton.Click += SelectFolderButton_Click;
            // 
            // selectFilesButton
            // 
            selectFilesButton.Location = new Point(3, 3);
            selectFilesButton.Name = "selectFilesButton";
            selectFilesButton.Size = new Size(100, 23);
            selectFilesButton.TabIndex = 0;
            selectFilesButton.Text = "Select files";
            selectFilesButton.UseVisualStyleBackColor = true;
            selectFilesButton.Click += SelectFile_Click;
            // 
            // fileNameLabel
            // 
            fileNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            fileNameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            fileNameLabel.Location = new Point(12, 16);
            fileNameLabel.Name = "fileNameLabel";
            fileNameLabel.Size = new Size(760, 22);
            fileNameLabel.TabIndex = 1;
            fileNameLabel.Text = "File Name";
            fileNameLabel.TextAlign = ContentAlignment.TopCenter;
            fileNameLabel.Visible = false;
            // 
            // overallProgressBar
            // 
            overallProgressBar.Location = new Point(12, 229);
            overallProgressBar.Name = "overallProgressBar";
            overallProgressBar.Size = new Size(668, 21);
            overallProgressBar.Style = ProgressBarStyle.Continuous;
            overallProgressBar.TabIndex = 2;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(697, 229);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 21);
            cancelButton.TabIndex = 7;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // currentActionProgressBar
            // 
            currentActionProgressBar.Location = new Point(12, 301);
            currentActionProgressBar.Name = "currentActionProgressBar";
            currentActionProgressBar.Size = new Size(668, 21);
            currentActionProgressBar.Style = ProgressBarStyle.Continuous;
            currentActionProgressBar.TabIndex = 2;
            // 
            // pauseButton
            // 
            pauseButton.Location = new Point(697, 301);
            pauseButton.Name = "pauseButton";
            pauseButton.Size = new Size(75, 21);
            pauseButton.TabIndex = 8;
            pauseButton.Text = "Pause";
            pauseButton.UseVisualStyleBackColor = true;
            // 
            // totalFileCountLabel
            // 
            totalFileCountLabel.AutoSize = true;
            totalFileCountLabel.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point);
            totalFileCountLabel.Location = new Point(12, 211);
            totalFileCountLabel.Name = "totalFileCountLabel";
            totalFileCountLabel.Size = new Size(34, 12);
            totalFileCountLabel.TabIndex = 4;
            totalFileCountLabel.Text = "21/181";
            totalFileCountLabel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // currentFileLabel
            // 
            currentFileLabel.Anchor = AnchorStyles.Top;
            currentFileLabel.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point);
            currentFileLabel.Location = new Point(360, 204);
            currentFileLabel.Name = "currentFileLabel";
            currentFileLabel.Size = new Size(320, 20);
            currentFileLabel.TabIndex = 4;
            currentFileLabel.Text = "spiderman.png";
            currentFileLabel.TextAlign = ContentAlignment.BottomRight;
            // 
            // progressLabel
            // 
            progressLabel.AutoSize = true;
            progressLabel.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point);
            progressLabel.Location = new Point(12, 286);
            progressLabel.Name = "progressLabel";
            progressLabel.Size = new Size(34, 12);
            progressLabel.TabIndex = 4;
            progressLabel.Text = "21/181";
            progressLabel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // currentActionLabel
            // 
            currentActionLabel.Anchor = AnchorStyles.Top;
            currentActionLabel.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point);
            currentActionLabel.Location = new Point(440, 278);
            currentActionLabel.Name = "currentActionLabel";
            currentActionLabel.Size = new Size(240, 20);
            currentActionLabel.TabIndex = 4;
            currentActionLabel.Text = "Breaking video into frames";
            currentActionLabel.TextAlign = ContentAlignment.BottomRight;
            // 
            // selectLabel
            // 
            selectLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            selectLabel.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            selectLabel.ForeColor = SystemColors.ControlDarkDark;
            selectLabel.Location = new Point(12, 40);
            selectLabel.Name = "selectLabel";
            selectLabel.Size = new Size(760, 15);
            selectLabel.TabIndex = 1;
            selectLabel.Text = "Selecting a folder will upscale every image/video in that folder";
            selectLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // animeRadioButton
            // 
            animeRadioButton.AutoSize = true;
            animeRadioButton.Location = new Point(3, 3);
            animeRadioButton.Name = "animeRadioButton";
            animeRadioButton.Size = new Size(81, 19);
            animeRadioButton.TabIndex = 2;
            animeRadioButton.TabStop = true;
            animeRadioButton.Text = "Animation";
            animeRadioButton.UseVisualStyleBackColor = true;
            // 
            // mediaTypePanel
            // 
            mediaTypePanel.Controls.Add(realisticRadioButton);
            mediaTypePanel.Controls.Add(animeRadioButton);
            mediaTypePanel.Location = new Point(310, 71);
            mediaTypePanel.Name = "mediaTypePanel";
            mediaTypePanel.Size = new Size(174, 27);
            mediaTypePanel.TabIndex = 6;
            // 
            // realisticRadioButton
            // 
            realisticRadioButton.AutoSize = true;
            realisticRadioButton.Location = new Point(103, 3);
            realisticRadioButton.Name = "realisticRadioButton";
            realisticRadioButton.Size = new Size(72, 19);
            realisticRadioButton.TabIndex = 3;
            realisticRadioButton.TabStop = true;
            realisticRadioButton.Text = "Standard";
            realisticRadioButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(12, 95);
            label2.Name = "label2";
            label2.Size = new Size(760, 15);
            label2.TabIndex = 1;
            label2.Text = "What kind of image/video are you upscaling?";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // scaleLevelPanel
            // 
            scaleLevelPanel.Controls.Add(x4radioButton);
            scaleLevelPanel.Controls.Add(x3radioButton);
            scaleLevelPanel.Controls.Add(x2radioButton);
            scaleLevelPanel.Location = new Point(327, 130);
            scaleLevelPanel.Name = "scaleLevelPanel";
            scaleLevelPanel.Size = new Size(132, 27);
            scaleLevelPanel.TabIndex = 6;
            // 
            // x4radioButton
            // 
            x4radioButton.AutoSize = true;
            x4radioButton.Location = new Point(89, 3);
            x4radioButton.Name = "x4radioButton";
            x4radioButton.Size = new Size(38, 19);
            x4radioButton.TabIndex = 6;
            x4radioButton.TabStop = true;
            x4radioButton.Text = "X4";
            x4radioButton.UseVisualStyleBackColor = true;
            // 
            // x3radioButton
            // 
            x3radioButton.AutoSize = true;
            x3radioButton.Location = new Point(48, 3);
            x3radioButton.Name = "x3radioButton";
            x3radioButton.Size = new Size(38, 19);
            x3radioButton.TabIndex = 5;
            x3radioButton.TabStop = true;
            x3radioButton.Text = "X3";
            x3radioButton.UseVisualStyleBackColor = true;
            // 
            // x2radioButton
            // 
            x2radioButton.AutoSize = true;
            x2radioButton.Location = new Point(3, 3);
            x2radioButton.Name = "x2radioButton";
            x2radioButton.Size = new Size(38, 19);
            x2radioButton.TabIndex = 4;
            x2radioButton.TabStop = true;
            x2radioButton.Text = "X2";
            x2radioButton.UseVisualStyleBackColor = true;
            // 
            // scaleLevelLabel
            // 
            scaleLevelLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            scaleLevelLabel.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            scaleLevelLabel.ForeColor = SystemColors.ControlDarkDark;
            scaleLevelLabel.Location = new Point(12, 155);
            scaleLevelLabel.Name = "scaleLevelLabel";
            scaleLevelLabel.Size = new Size(760, 15);
            scaleLevelLabel.TabIndex = 1;
            scaleLevelLabel.Text = "What level of upscaling would you like to use? X2 will upscale it to 2 times its resolution, X4 to 4 times";
            scaleLevelLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // videoBreakProgressBar
            // 
            videoBreakProgressBar.Location = new Point(12, 301);
            videoBreakProgressBar.Name = "videoBreakProgressBar";
            videoBreakProgressBar.Size = new Size(67, 21);
            videoBreakProgressBar.Style = ProgressBarStyle.Continuous;
            videoBreakProgressBar.TabIndex = 2;
            // 
            // videoMergeProgressBar
            // 
            videoMergeProgressBar.Location = new Point(613, 301);
            videoMergeProgressBar.Name = "videoMergeProgressBar";
            videoMergeProgressBar.Size = new Size(67, 21);
            videoMergeProgressBar.Style = ProgressBarStyle.Continuous;
            videoMergeProgressBar.TabIndex = 2;
            // 
            // videoUpscaleProgresslabel
            // 
            videoUpscaleProgresslabel.Anchor = AnchorStyles.Top;
            videoUpscaleProgresslabel.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point);
            videoUpscaleProgresslabel.Location = new Point(204, 278);
            videoUpscaleProgresslabel.Name = "videoUpscaleProgresslabel";
            videoUpscaleProgresslabel.Size = new Size(295, 20);
            videoUpscaleProgresslabel.TabIndex = 4;
            videoUpscaleProgresslabel.Text = "33.33%";
            videoUpscaleProgresslabel.TextAlign = ContentAlignment.BottomCenter;
            // 
            // fps24checkBox
            // 
            fps24checkBox.AutoSize = true;
            fps24checkBox.Location = new Point(12, 335);
            fps24checkBox.Name = "fps24checkBox";
            fps24checkBox.Size = new Size(115, 19);
            fps24checkBox.TabIndex = 9;
            fps24checkBox.Text = "Encode in 24 FPS";
            fps24checkBox.UseVisualStyleBackColor = true;
            // 
            // fps24Label
            // 
            fps24Label.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            fps24Label.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            fps24Label.ForeColor = SystemColors.ControlDarkDark;
            fps24Label.Location = new Point(126, 335);
            fps24Label.Name = "fps24Label";
            fps24Label.Size = new Size(554, 15);
            fps24Label.TabIndex = 1;
            fps24Label.Text = "If the upscaled video is out of sync with the audio, try it again and check this box before it merges.";
            fps24Label.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 361);
            Controls.Add(fps24checkBox);
            Controls.Add(scaleLevelPanel);
            Controls.Add(mediaTypePanel);
            Controls.Add(videoUpscaleProgresslabel);
            Controls.Add(currentActionLabel);
            Controls.Add(currentFileLabel);
            Controls.Add(progressLabel);
            Controls.Add(totalFileCountLabel);
            Controls.Add(pauseButton);
            Controls.Add(cancelButton);
            Controls.Add(videoMergeProgressBar);
            Controls.Add(videoBreakProgressBar);
            Controls.Add(currentActionProgressBar);
            Controls.Add(overallProgressBar);
            Controls.Add(fps24Label);
            Controls.Add(scaleLevelLabel);
            Controls.Add(label2);
            Controls.Add(selectLabel);
            Controls.Add(fileNameLabel);
            Controls.Add(fileDialogPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Real-ESRGAN Upscaler";
            FormClosing += MainForm_FormClosing;
            DragDrop += MainForm_DragDrop;
            DragEnter += MainForm_DragEnter;
            fileDialogPanel.ResumeLayout(false);
            mediaTypePanel.ResumeLayout(false);
            mediaTypePanel.PerformLayout();
            scaleLevelPanel.ResumeLayout(false);
            scaleLevelPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private CheckBox fps24checkBox;
        private Label fps24Label;
    }
}