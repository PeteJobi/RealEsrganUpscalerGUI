using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Upscaler
{
    public partial class MainForm : Form
    {
        string realEsrganPath = @"realesrgan-ncnn-vulkan.exe";
        string ffmpegPath = @"ffmpeg.exe";
        const int progressMax = 1_000_000;
        const int breakMergeSegmentFactor = 10; //The parts of the overall progress dedicated to breaking videos and merging frames. Assuming progressMax is 100, 10 means 10-80-10 and 5 means 5-90-5
        const int breakMergeProgressMax = progressMax / breakMergeSegmentFactor;
        string[] allowedExts = new[] { ".mkv", ".mp4", ".jpg", ".jpeg", ".png" };
        bool hasBeenKilled = false;
        bool isPaused = false;
        FrameFolders? frameFolders;
        Process? currentProcess;
        public MainForm()
        {
            InitializeComponent();
            realisticRadioButton.Checked = true;
            x2radioButton.Checked = true;
            overallProgressBar.Maximum = currentActionProgressBar.Maximum = progressMax;
            Reset(null, EventArgs.Empty);
        }

        private void AnimeRadioButton_CheckedChanged(object? sender, EventArgs e)
        {
            animeScaleLevelLabel.Visible = animeScaleLevelPanel.Visible = animeRadioButton.Checked;
            Size = new Size(Size.Width, animeRadioButton.Checked ? 230 : 170);
        }

        private void SelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Select one or multiple images and/or videos";
            openFileDialog.Filter = "Image and Video Files|*.jpg;*.jpeg;*.png;*.mkv;*.mp4";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            PrepareUI();
            fileNameLabel.Text = Path.GetFileName(openFileDialog.FileName);
            if (openFileDialog.FileNames.Length > 1) 
                fileNameLabel.Text += $" and {openFileDialog.FileNames.Length - 1} others";
            ProcessFiles(openFileDialog.FileNames);
        }

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.Description = "Select a folder that contains images and/or videos";
            folderBrowserDialog.UseDescriptionForTitle = true;
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
            PrepareUI();
            fileNameLabel.Text = Path.GetFileName(folderBrowserDialog.SelectedPath);
            string[] filePaths = Directory.GetFiles(folderBrowserDialog.SelectedPath).Where(p => allowedExts.Contains(Path.GetExtension(p))).ToArray();
            if(filePaths.Length == 0)
            {
                MessageBox.Show("The selected folder does not contain any supported files");
                return;
            }
            ProcessFiles(filePaths);
        }

        void PrepareUI()
        {
            fileDialogPanel.Hide();
            selectLabel.Hide();
            fileNameLabel.Show();
            mediaTypePanel.Enabled = false;
            animeScaleLevelPanel.Enabled = false;
        }

        void ProcessFiles(string[] fileNames)
        {
            Size = new Size(Size.Width, 380);
            void RecursivelyProcessFile(string fileName, int i, int total)
            {
                Invoke(() =>
                {
                    UpdateTotalFileCountLabel(i, total);
                    UpdateCurrentFileLabel(Path.GetFileName(fileName));
                });
                UpscaleFile(fileName, i, total, () =>
                {
                    i++;
                    if (i < fileNames.Length) RecursivelyProcessFile(fileNames[i], i, total);
                    else AllDone(i);
                });
            }

            RecursivelyProcessFile(fileNames[0], 0, fileNames.Length);
        }

        void UpscaleFile(string fileName, int currentFileIndex, int totalFilesCount, Action done)
        {
            string extension = Path.GetExtension(fileName);
            if (new string[] { ".mkv", ".mp4" }.Contains(extension))
            {
                frameFolders = GetFrameFolders(fileName);
                TimeSpan duration = TimeSpan.MinValue;
                string? fps = null;
                StartProcess(ffmpegPath, $"-i \"{fileName}\" -qscale:v 1 -qmin 1 -qmax 1 -vsync 0 \"{frameFolders.InputFolder}/frame%08d.png\"", null, (sender, args) =>
                {
                    if (string.IsNullOrWhiteSpace(args.Data)) return;
                    if (duration == TimeSpan.MinValue)
                    {
                        MatchCollection matchCollection = Regex.Matches(args.Data, @"\s+?Duration:\s(\d{2}:\d{2}:\d{2}\.\d{2}).+");
                        if (matchCollection.Count == 0) return;
                        duration = TimeSpan.Parse(matchCollection[0].Groups[1].Value);
                    }
                    else if (fps == null) //fps information should come immediately after the duration line
                    {
                        MatchCollection matchCollection = Regex.Matches(args.Data, @"(\d+.\d+)\sfps");
                        if (matchCollection.Count == 0) throw new ArgumentException("FPS information not found");
                        fps = matchCollection[0].Groups[1].Value;
                    }
                    else
                    {
                        MatchCollection matchCollection = Regex.Matches(args.Data, @"^frame=\s+\d+\s.+?time=(\d{2}:\d{2}:\d{2}\.\d{2}).+");
                        if (matchCollection.Count == 0) return;
                        IncrementBreakMergeProgress(TimeSpan.Parse(matchCollection[0].Groups[1].Value), duration, currentFileIndex, totalFilesCount, false);
                    }
                }, (sender, args) =>
                {
                    IncrementBreakMergeProgress(duration, duration, currentFileIndex, totalFilesCount, false);

                    int numFrames = Directory.GetFiles(frameFolders.InputFolder).Length;
                    int c = -1;
                    StartProcess(realEsrganPath, $"-i \"{frameFolders.InputFolder}\" -o \"{frameFolders.OutputFolder}\" -n {GetModel(true)} -s {GetScale()} -f png", null, (sender, args) =>
                    {
                        if (string.IsNullOrWhiteSpace(args.Data)) return;
                        if (Regex.IsMatch(args.Data, @"\d+.\d+%"))
                        {
                            if (args.Data == "0.00%")
                            {
                                c++;
                                //Console.WriteLine((double)c / numFrames * 100);
                            }
                            IncrementUpscaleProgress(args.Data[0..^1], c, numFrames, currentFileIndex, totalFilesCount, true);
                        }
                    }, (sender, args) =>
                    {
                        IncrementUpscaleProgress("100", c, numFrames, currentFileIndex, totalFilesCount, true);

                        string outputName = GetOutputName(fileName);
                        File.Delete(outputName);
                        StartProcess(ffmpegPath, $"-r {fps} -i \"{frameFolders.OutputFolder}/frame%08d.png\" -i \"{fileName}\" -map 0:v:0 -map 1:a:0 -c:a copy -c:v libx264 -r 23.98 -pix_fmt yuv420p \"{outputName}\"", null, (sender, args) =>
                        {
                            if (string.IsNullOrWhiteSpace(args.Data)) return;
                            MatchCollection matchCollection = Regex.Matches(args.Data, @"^frame=\s+\d+\s.+?time=(\d{2}:\d{2}:\d{2}\.\d{2}).+");
                            if (matchCollection.Count == 0) return;
                            IncrementBreakMergeProgress(TimeSpan.Parse(matchCollection[0].Groups[1].Value), duration, currentFileIndex, totalFilesCount, true);
                        }, (sender, args) =>
                        {
                            IncrementBreakMergeProgress(duration, duration, currentFileIndex, totalFilesCount, true);
                            Directory.Delete(frameFolders.InputFolder, true);
                            Directory.Delete(frameFolders.OutputFolder, true);
                            frameFolders = null;
                            done();
                        });
                    });
                });
            }
            else
            {
                StartProcess(realEsrganPath, $"-i \"{fileName}\" -o \"{GetOutputName(fileName)}\" -n {GetModel(false)}", null, (sender, args) =>
                {
                    if (string.IsNullOrWhiteSpace(args.Data)) return;
                    if (Regex.IsMatch(args.Data, @"\d+.\d+%"))
                    {
                        IncrementUpscaleProgress(args.Data[0..^1], 0, 1, currentFileIndex, totalFilesCount, false);
                    }
                }, (sender, args) =>
                {
                    IncrementUpscaleProgress("100", 0, 1, currentFileIndex, totalFilesCount, false);
                    done();
                });
            }
        }

        void IncrementUpscaleProgress(string percent, int currentFrame, int totalFrames, int currentFileIndex, int totalFilesCount, bool isVideo)
        {
            Invoke(() =>
            {
                progressLabel.Text = $"{percent}%";
                if (isVideo) progressLabel.Text = $"{currentFrame}/{totalFrames} frames: " + progressLabel.Text;
                currentActionLabel.Text = isVideo ? "Upscaling video frames..." : "Upscaling...";
                int unusedSegment = isVideo ? breakMergeProgressMax : 0;
                int usedSegment = progressMax - (unusedSegment * 2);
                int unusedSegmentForOverall = isVideo ? (int)(progressMax / (totalFilesCount * breakMergeSegmentFactor)) : 0;
                int usedSegmentForOverall = progressMax / totalFilesCount - (unusedSegmentForOverall * 2);
                currentActionProgressBar.Value = unusedSegment + (int)((double)currentFrame / totalFrames * usedSegment) + (int)(double.Parse(percent) / 100 * (usedSegment / totalFrames));
                overallProgressBar.Value = (int)((double)currentFileIndex / totalFilesCount * progressMax)
                    + unusedSegmentForOverall
                    + (int)((double)currentFrame / totalFrames * usedSegmentForOverall)
                    + (int)(double.Parse(percent) / 100 * (usedSegmentForOverall / totalFrames));
            });
        }

        void IncrementBreakMergeProgress(TimeSpan currentTime, TimeSpan totalDuration, int currentFileIndex, int totalFilesCount, bool isMerging)
        {
            Invoke(() =>
            {
                progressLabel.Text = $"{currentTime:hh\\:mm\\:ss}/{totalDuration:hh\\:mm\\:ss}: {Math.Round(currentTime / totalDuration * 100, 2)} %";
                currentActionLabel.Text = isMerging ? "Merging frames into video" : "Breaking video into frames";
                int breakMergeProgressMaxForOverall = (int)((double)progressMax / (totalFilesCount * breakMergeSegmentFactor));
                currentActionProgressBar.Value = (isMerging ? (progressMax - breakMergeProgressMax) : 0) + (int)(currentTime / totalDuration * breakMergeProgressMax);
                overallProgressBar.Value = (int)((double)currentFileIndex / totalFilesCount * progressMax) 
                    + (isMerging ? ((progressMax / totalFilesCount) - breakMergeProgressMaxForOverall) : 0) 
                    + (int)(currentTime / totalDuration * breakMergeProgressMaxForOverall);
            });
        }

        void AllDone(int fileCount)
        {
            currentProcess = null;
            Invoke(() =>
            {
                UpdateTotalFileCountLabel(fileCount, fileCount);
                currentActionLabel.Text = "Done";
                cancelButton.Text = "Retry";
                cancelButton.Click -= CancelButton_Click;
                cancelButton.Click += Reset;
                pauseButton.Enabled = false;
            });
        }

        private void Reset(object? sender, EventArgs e)
        {
            AnimeRadioButton_CheckedChanged(sender, e);
            totalFileCountLabel.Text = string.Empty;
            currentFileLabel.Text = string.Empty;
            currentActionLabel.Text = string.Empty;
            progressLabel.Text = string.Empty;
            cancelButton.Text = "Cancel";
            pauseButton.Text = "Pause";
            cancelButton.Click += CancelButton_Click;
            cancelButton.Click -= Reset;
            fileDialogPanel.Show();
            selectLabel.Show();
            fileNameLabel.Hide();
            overallProgressBar.Value = 0;
            currentActionProgressBar.Value = 0;
            mediaTypePanel.Enabled = true;
            animeScaleLevelPanel.Enabled = true;
        }

        private async void CancelButton_Click(object? sender, EventArgs e)
        {
            currentProcess.Kill();
            hasBeenKilled = true;
            cancelButton.Click -= CancelButton_Click;
            Reset(sender, e);
            currentProcess = null;
            await Task.Delay(1000);
            Directory.Delete(frameFolders.InputFolder, true);
            Directory.Delete(frameFolders.OutputFolder, true);
            hasBeenKilled = false;
            frameFolders = null;
        }

        private void pauseButton_Click(object sender, EventArgs e)
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
            if (!animeRadioButton.Checked) return "realesrgan-x4plus";
            return isVideo ? "realesr-animevideov3" : "realesrgan-x4plus-anime";
        }

        int GetScale()
        {
            if (!animeRadioButton.Checked) return 4;
            if(x2radioButton.Checked) return 2;
            if(x3radioButton.Checked) return 3;
            return 4;
        }

        void StartProcess(string processFileName, string arguments, DataReceivedEventHandler? outputEventHandler, DataReceivedEventHandler? errorEventHandler, EventHandler? exitedEventHandler)
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
            esrgan.Exited += (s, e) =>
            {
                esrgan.Dispose();
                if(!hasBeenKilled) exitedEventHandler?.Invoke(s, e);
            };
            esrgan.Start();
            esrgan.BeginErrorReadLine();
            esrgan.BeginOutputReadLine();
            currentProcess = esrgan;
        }

        string GetOutputName(string path)
        {
            string inputName = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);
            string parentFolder = Path.GetDirectoryName(path) ?? throw new FileNotFoundException($"The specified path does not exist: {path}");
            return Path.Combine(parentFolder, $"{inputName}_UPSCALED{extension}");
        }

        FrameFolders GetFrameFolders(string path)
        {
            string inputName = Path.GetFileNameWithoutExtension(path);
            string parentFolder = Path.GetDirectoryName(path) ?? throw new FileNotFoundException($"The specified path does not exist: {path}");
            FrameFolders frameFolders = new(Path.Combine(parentFolder, $"{inputName}_InputFrames"), Path.Combine(parentFolder, $"{inputName}_OutputFrames"));
            Directory.CreateDirectory(frameFolders.InputFolder);
            Directory.CreateDirectory(frameFolders.OutputFolder);
            return frameFolders;
        }

        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
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
            //var process = Process.GetProcessById(pid); // throws exception if process does not exist

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
            //var process = Process.GetProcessById(pid);

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
            if (currentProcess == null || frameFolders == null) return;

            const string message = "Are you sure that you would like to cancel the process?";
            const string caption = "Cancel upscale task";
            SuspendProcess(currentProcess);
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                e.Cancel = true;
                ResumeProcess(currentProcess);
            }
            else
            {
                CancelButton_Click(null, EventArgs.Empty);
                Directory.Delete(frameFolders.InputFolder, true);
                Directory.Delete(frameFolders.OutputFolder, true);
            }
        }
    }

    record ProcessOutput(string Output, string Error);
    record FrameFolders(string InputFolder, string OutputFolder);
}