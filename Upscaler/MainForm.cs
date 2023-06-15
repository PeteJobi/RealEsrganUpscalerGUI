using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Upscaler
{
    public partial class MainForm : Form
    {
        const string realEsrganPath = @"realesrgan-ncnn-vulkan.exe";
        const string ffmpegPath = @"ffmpeg.exe";
        const int progressMax = 1_000_000;
        const int breakMergeSegmentFactor = 10; //The parts of the overall progress dedicated to breaking videos and merging frames. Assuming progressMax is 100, 10 means 10-80-10 and 5 means 5-90-5
        const int breakMergeProgressMax = progressMax / breakMergeSegmentFactor;
        static readonly string[] imageExts = new[] { ".jpg", ".jpeg", ".png" };
        static readonly string[] videoExts = new[] { ".mkv", ".mp4" };
        readonly string[] allowedExts = imageExts;
        bool videoSupported;
        bool hasBeenKilled;
        bool isPaused;
        string pathForView;
        FrameFolders? frameFolders;
        Process? currentProcess;
        bool isProcessingFolder;
        const string UPSCALED_PREFIX = "_UPSCALED";
        public MainForm()
        {
            InitializeComponent();
            realisticRadioButton.Checked = true;
            x2radioButton.Checked = true;
            overallProgressBar.Maximum = currentActionProgressBar.Maximum = videoBreakProgressBar.Maximum = videoMergeProgressBar.Maximum = progressMax;
            if (File.Exists(ffmpegPath))
            {
                videoSupported = true;
                allowedExts = allowedExts.Concat(videoExts).ToArray();
            }
            Reset(null, EventArgs.Empty);
        }

        private async void SelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Select one or multiple images and/or videos";
            openFileDialog.Filter = $"Image {(videoSupported ? "and Video " : "")}Files|" + string.Join(";", allowedExts.Select(e => $"*{e}"));
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            await PrepareFiles(openFileDialog.FileNames, false);
        }

        private async void SelectFolderButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.Description = "Select a folder that contains images and/or videos";
            folderBrowserDialog.UseDescriptionForTitle = true;
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
            await PrepareFiles(new string[] { folderBrowserDialog.SelectedPath }, true);
        }

        async Task PrepareFiles(string[] paths, bool folder)
        {
            string[] filePaths;
            if (folder)
            {
                filePaths = Directory.GetFiles(paths[0]).Where(p => allowedExts.Contains(Path.GetExtension(p))).ToArray();
                if (filePaths.Length == 0)
                {
                    MessageBox.Show("The selected folder does not contain any supported files");
                    return;
                }
            }
            else
            {
                filePaths = paths.Where(p => allowedExts.Contains(Path.GetExtension(p))).ToArray();
                if(filePaths.Length == 0)
                {
                    MessageBox.Show("None of the dropped files are supported");
                    return;
                }
            }

            fileDialogPanel.Hide();
            selectLabel.Hide();
            fileNameLabel.Show();
            mediaTypePanel.Enabled = false;
            scaleLevelPanel.Enabled = false;
            Height = 380;

            isProcessingFolder = folder;
            pathForView = isProcessingFolder ? paths[0] + UPSCALED_PREFIX : GetOutputName(paths[0]);
            fileNameLabel.Text = Path.GetFileName(paths[0]);
            if (paths.Length > 1) fileNameLabel.Text += $" and {paths.Length - 1} others";
            ProcessFiles(filePaths);
        }

        async void ProcessFiles(string[] fileNames)
        {
            bool canceled = false;
            for (int i = 0; i < fileNames.Length; i++)
            {
                UpdateTotalFileCountLabel(i, fileNames.Length);
                UpdateCurrentFileLabel(Path.GetFileName(fileNames[i]));
                if (!await UpscaleFile(fileNames[i], i, fileNames.Length))
                {
                    canceled = true;
                    break;
                }
            }

            if (!canceled) AllDone(fileNames.Length);
        }

        async Task<bool> UpscaleFile(string fileName, int currentFileIndex, int totalFilesCount)
        {
            string extension = Path.GetExtension(fileName);
            if (videoExts.Contains(extension))
            {
                ResetVideoUpscaleUI(true);
                #region Break video into frames
                frameFolders = GetFrameFolders(fileName);
                TimeSpan duration = TimeSpan.MinValue;
                string? fps = null;
                await StartProcess(ffmpegPath, $"-i \"{fileName}\" -qscale:v 1 -qmin 1 -qmax 1 -vsync 0 \"{frameFolders.InputFolder}/frame%08d.png\"", null, (sender, args) =>
                {
                    if (string.IsNullOrWhiteSpace(args.Data)) return;
                    if (duration == TimeSpan.MinValue)
                    {
                        MatchCollection matchCollection = Regex.Matches(args.Data, @"\s+?Duration:\s(\d{2}:\d{2}:\d{2}\.\d{2}).+");
                        if (matchCollection.Count == 0) return;
                        duration = TimeSpan.Parse(matchCollection[0].Groups[1].Value);
                    }
                    else if (fps == null)
                    {
                        MatchCollection matchCollection = Regex.Matches(args.Data, @"(\d+.?\d+)\stbr");
                        if (matchCollection.Count == 0) return;
                        fps = matchCollection[0].Groups[1].Value;
                    }
                    else
                    {
                        MatchCollection matchCollection = Regex.Matches(args.Data, @"^frame=\s+\d+\s.+?time=(\d{2}:\d{2}:\d{2}\.\d{2}).+");
                        if (matchCollection.Count == 0) return;
                        IncrementBreakMergeProgress(TimeSpan.Parse(matchCollection[0].Groups[1].Value), duration, currentFileIndex, totalFilesCount, false);
                    }
                });
                if (HasBeenKilled()) return false;
                IncrementBreakMergeProgress(duration, duration, currentFileIndex, totalFilesCount, false);
                #endregion

                #region Upscale video frames
                int numFrames = Directory.GetFiles(frameFolders.InputFolder).Length;
                int c = -1;
                await StartProcess(realEsrganPath, $"-i \"{frameFolders.InputFolder}\" -o \"{frameFolders.OutputFolder}\" -n {GetModel(true)} -s {GetScale()} -f png", null, (sender, args) =>
                {
                    if (string.IsNullOrWhiteSpace(args.Data)) return;
                    if (Regex.IsMatch(args.Data, @"\d+.\d+%"))
                    {
                        if (args.Data == "0.00%") c++;
                        IncrementUpscaleProgress(args.Data[0..^1], c, numFrames, currentFileIndex, totalFilesCount, true);
                    }
                });
                if (HasBeenKilled()) return false;
                IncrementUpscaleProgress("100", numFrames, numFrames, currentFileIndex, totalFilesCount, true);
                #endregion

                #region Merge frames into video
                string outputName = GetOutputName(fileName);
                File.Delete(outputName);
                await StartProcess(ffmpegPath, $"-r {fps} -i \"{frameFolders.OutputFolder}/frame%08d.png\" -i \"{fileName}\" -map 0:v:0 -map 1 -map -1:v -c:a copy -c:v libx264 -r {fps} -pix_fmt yuv420p \"{outputName}\"", null, (sender, args) =>
                {
                    if (string.IsNullOrWhiteSpace(args.Data)) return;
                    MatchCollection matchCollection = Regex.Matches(args.Data, @"^frame=\s+\d+\s.+?time=(\d{2}:\d{2}:\d{2}\.\d{2}).+");
                    if (matchCollection.Count == 0) return;
                    IncrementBreakMergeProgress(TimeSpan.Parse(matchCollection[0].Groups[1].Value), duration, currentFileIndex, totalFilesCount, true);
                });
                if (HasBeenKilled()) return false;

                IncrementBreakMergeProgress(duration, duration, currentFileIndex, totalFilesCount, true);
                Directory.Delete(frameFolders.InputFolder, true);
                Directory.Delete(frameFolders.OutputFolder, true);
                frameFolders = null;
                #endregion
            }
            else
            {
                ResetVideoUpscaleUI(false);
                await StartProcess(realEsrganPath, $"-i \"{fileName}\" -o \"{GetOutputName(fileName)}\" -n {GetModel(false)} -f png", null, (sender, args) =>
                {
                    if (string.IsNullOrWhiteSpace(args.Data)) return;
                    if (Regex.IsMatch(args.Data, @"\d+.\d+%"))
                    {
                        IncrementUpscaleProgress(args.Data[0..^1], 0, 1, currentFileIndex, totalFilesCount, false);
                    }
                });
                if(HasBeenKilled()) return false;
                IncrementUpscaleProgress("100", 0, 1, currentFileIndex, totalFilesCount, false);
            }

            return true;
        }

        void IncrementUpscaleProgress(string percentString, int currentFrame, int totalFrames, int currentFileIndex, int totalFilesCount, bool isVideo)
        {
            Invoke(() =>
            {
                if(!double.TryParse(percentString, out double percent))
                {
                    MessageBox.Show($"Something went wrong with the operation\n\n{percentString}");
                    Cancel();
                    return;
                }
                progressLabel.Text = $"{percent}%";
                if (isVideo) progressLabel.Text = $"{currentFrame}/{totalFrames} frames: " + progressLabel.Text;
                currentActionLabel.Text = isVideo ? "Upscaling video frames..." : "Upscaling...";
                if(isVideo) videoUpscaleProgresslabel.Text = $"{Math.Round((double)currentFrame / totalFrames * 100, 2)} %";
                int unusedSegmentForOverall = isVideo ? (int)(progressMax / (totalFilesCount * breakMergeSegmentFactor)) : 0;
                int usedSegmentForOverall = progressMax / totalFilesCount - (unusedSegmentForOverall * 2);
                currentActionProgressBar.Value = Math.Min(progressMax, (int)((double)currentFrame / totalFrames * progressMax) + (int)(percent / 100 * (progressMax / totalFrames)));
                overallProgressBar.Value = (int)((double)currentFileIndex / totalFilesCount * progressMax)
                    + unusedSegmentForOverall
                    + (int)((double)currentFrame / totalFrames * usedSegmentForOverall)
                    + (int)(percent / 100 * (usedSegmentForOverall / totalFrames));
            });
        }

        void IncrementBreakMergeProgress(TimeSpan currentTime, TimeSpan totalDuration, int currentFileIndex, int totalFilesCount, bool isMerging)
        {
            Invoke(() =>
            {
                progressLabel.Text = $"{currentTime:hh\\:mm\\:ss}/{totalDuration:hh\\:mm\\:ss}: {Math.Round(currentTime / totalDuration * 100, 2)} %";
                currentActionLabel.Text = isMerging ? "Merging frames into video..." : "Breaking video into frames...";
                int breakMergeProgressMaxForOverall = (int)((double)progressMax / (totalFilesCount * breakMergeSegmentFactor));
                //currentActionProgressBar.Value = (isMerging ? (progressMax - breakMergeProgressMax) : 0) + (int)(currentTime / totalDuration * breakMergeProgressMax);
                ProgressBar progressBarToEdit = isMerging ? videoMergeProgressBar : videoBreakProgressBar;
                progressBarToEdit.Value = (int)(currentTime / totalDuration * progressMax);
                overallProgressBar.Value = (int)((double)currentFileIndex / totalFilesCount * progressMax) 
                    + (isMerging ? ((progressMax / totalFilesCount) - breakMergeProgressMaxForOverall) : 0) 
                    + (int)(currentTime / totalDuration * breakMergeProgressMaxForOverall);
            });
        }

        void AllDone(int fileCount)
        {
            currentProcess = null;
            UpdateTotalFileCountLabel(fileCount, fileCount);
            currentActionLabel.Text = "Done";
            cancelButton.Text = "Retry";
            cancelButton.Click -= CancelButton_Click;
            cancelButton.Click += Reset;
            pauseButton.Text = "View";
            pauseButton.Click -= pauseButton_Click;
            pauseButton.Click += ViewFiles;
        }

        private void Reset(object? sender, EventArgs e)
        {
            Height = 230;
            totalFileCountLabel.Text = string.Empty;
            currentFileLabel.Text = string.Empty;
            currentActionLabel.Text = string.Empty;
            progressLabel.Text = string.Empty;
            cancelButton.Text = "Cancel";
            pauseButton.Text = "Pause";
            cancelButton.Click += CancelButton_Click;
            cancelButton.Click -= Reset;
            pauseButton.Click += pauseButton_Click;
            pauseButton.Click -= ViewFiles;
            fileDialogPanel.Show();
            selectLabel.Show();
            fileNameLabel.Hide();
            overallProgressBar.Value = 0;
            currentActionProgressBar.Value = 0;
            mediaTypePanel.Enabled = true;
            scaleLevelPanel.Enabled = true;
            ResetVideoUpscaleUI(true);
        }

        void ResetVideoUpscaleUI(bool show)
        {
            currentActionProgressBar.Left = show ? 80 : overallProgressBar.Left;
            currentActionProgressBar.Width = show ? 532 : overallProgressBar.Width;
            videoUpscaleProgresslabel.Visible = show;
            videoBreakProgressBar.Visible = show;
            videoMergeProgressBar.Visible = show;
            videoBreakProgressBar.Value = 0;
            videoMergeProgressBar.Value = 0;
            videoUpscaleProgresslabel.Text = string.Empty;
        }

        private void CancelButton_Click(object? sender, EventArgs e)
        {
            if (ConfirmCancel()) Cancel();
        }

        void Cancel()
        {
            currentProcess.Kill();
            hasBeenKilled = true;
            cancelButton.Click -= CancelButton_Click;
            Reset(null, EventArgs.Empty);
            currentProcess = null;
            if(frameFolders != null)
            {
                Directory.Delete(frameFolders.InputFolder, true);
                Directory.Delete(frameFolders.OutputFolder, true);
                frameFolders = null;
            }
        }

        bool HasBeenKilled()
        {
            if (!hasBeenKilled) return false;
            hasBeenKilled = false;
            return true;
        }

        bool ConfirmCancel()
        {
            const string message = "Are you sure that you would like to cancel the process?";
            const string caption = "Cancel upscale task";
            if (!isPaused) SuspendProcess(currentProcess);
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            bool confirm = result == DialogResult.Yes;
            if (!confirm && !isPaused) ResumeProcess(currentProcess);
            return confirm;
        }

        private void ViewFiles(object? sender, EventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "explorer";
            info.Arguments = $"/e, /select, \"{pathForView}\"";
            Process.Start(info);
        }

        private void pauseButton_Click(object? sender, EventArgs e)
        {
            if (isPaused)
            {
                ResumeProcess(currentProcess);
                pauseButton.Text = "Pause";
                isPaused = false;
            }
            else
            {
                SuspendProcess(currentProcess);
                pauseButton.Text = "Resume";
                isPaused = true;
            }
        }

        void UpdateTotalFileCountLabel(int current, int total)
        {
            totalFileCountLabel.Text = $"{current}/{total} files";
        }

        void UpdateCurrentFileLabel(string fileName)
        {
            currentFileLabel.Text = fileName;
        }

        string GetModel(bool isVideo)
        {
            if (animeRadioButton.Checked && isVideo) return "realesr-animevideov3";
            if (x2radioButton.Checked) return animeRadioButton.Checked ? "realesrgan-x2plus-anime" : "realesrgan-x2plus";
            if (x3radioButton.Checked) return animeRadioButton.Checked ? "realesrgan-x3plus-anime" : "realesrgan-x3plus";
            return animeRadioButton.Checked ? "realesrgan-x4plus-anime" : "realesrgan-x4plus";
        }

        int GetScale()
        {
            if (!animeRadioButton.Checked) return 4;
            if(x2radioButton.Checked) return 2;
            if(x3radioButton.Checked) return 3;
            return 4;
        }

        async Task StartProcess(string processFileName, string arguments, DataReceivedEventHandler? outputEventHandler, DataReceivedEventHandler? errorEventHandler)
        {
            Process esrgan = new ()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = processFileName,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                },
                EnableRaisingEvents = true
            };
            esrgan.OutputDataReceived += outputEventHandler;
            esrgan.ErrorDataReceived += errorEventHandler;
            esrgan.Start();
            esrgan.BeginErrorReadLine();
            esrgan.BeginOutputReadLine();
            currentProcess = esrgan;
            await esrgan.WaitForExitAsync();
            esrgan.Dispose();
        }

        string GetOutputName(string path)
        {
            string inputName = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);
            string parentFolder = Path.GetDirectoryName(path) ?? throw new FileNotFoundException($"The specified path does not exist: {path}");
            return isProcessingFolder ? Path.Combine(parentFolder + UPSCALED_PREFIX, $"{inputName}{extension}") :
                Path.Combine(parentFolder, $"{inputName}{UPSCALED_PREFIX}{extension}");
        }

        FrameFolders GetFrameFolders(string path)
        {
            string inputName = Path.GetFileNameWithoutExtension(path);
            string parentFolder = Path.GetDirectoryName(path) ?? throw new FileNotFoundException($"The specified path does not exist: {path}");
            if(isProcessingFolder) parentFolder += UPSCALED_PREFIX;
            FrameFolders frameFolders = new(Path.Combine(parentFolder, $"{inputName}_InputFrames"), Path.Combine(parentFolder, $"{inputName}_OutputFrames"));
            Directory.CreateDirectory(frameFolders.InputFolder);
            Directory.CreateDirectory(frameFolders.OutputFolder);
            return frameFolders;
        }

        [Flags]
        public enum ThreadAccess : int
        {
            SUSPEND_RESUME = (0x0002)
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);

        private static void SuspendProcess(Process process)
        {
            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                SuspendThread(pOpenThread);

                CloseHandle(pOpenThread);
            }
        }

        public static void ResumeProcess(Process process)
        {
            if (process.ProcessName == string.Empty)
                return;

            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                int suspendCount;
                do
                {
                    suspendCount = ResumeThread(pOpenThread);
                } while (suspendCount > 0);

                CloseHandle(pOpenThread);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentProcess == null) return;

            if (ConfirmCancel()) Cancel();
            else e.Cancel = true;
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (!fileDialogPanel.Visible) return;
            e.Effect = DragDropEffects.All;
        }

        private async void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (!fileDialogPanel.Visible) return;
            string[]? files = ((string[]?)e.Data?.GetData(DataFormats.FileDrop, false));
            if(files?.Length < 1) return;
            if (Path.GetExtension(files[0]) == string.Empty) await PrepareFiles(files[0..1], true);
            else await PrepareFiles(files, false);
        }
    }

    record ProcessOutput(string Output, string Error);
    record FrameFolders(string InputFolder, string OutputFolder);
}