using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using rclone;
using rclone.Remote;
using System.Diagnostics;
using System.IO;

namespace Toolbox
{
    public partial class Form1 : Form
    {
        private string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string FolderLocation = @"C:\temp\";
        public WebClient client = new WebClient();
        public bool Done = false;

        public Form1()
        {
            InitializeComponent();
            Directory.CreateDirectory(FolderLocation + @"");
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
        }

        private void nightButton1_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\TestFile.bin"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/TestFile.bin", FolderLocation);
        }

        

        private void DownloadFile(string drivePath, string folder)
        {
            if (Directory.Exists(userPath + "\\.config\\rclone"))
            {
                client.DownloadFile("https://raw.githubusercontent.com/NotJansel/Jansel-s-SE/master/rclone.conf", userPath + "\\.config\\rclone\\rclone.conf");
            }
            else
            {
                Directory.CreateDirectory(userPath + "\\.config\\rclone");
                client.DownloadFile("https://raw.githubusercontent.com/NotJansel/Jansel-s-SE/master/rclone.conf", userPath + "\\.config\\rclone\\rclone.conf");
            }
            if (!File.Exists(FolderLocation + "\\downloader.exe"))
            {
                client.DownloadFile("https://picteon.dev/files/rclone.exe", FolderLocation + "\\downloader.exe");
            }

            Process process = new Process();
            process.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(process_OutputDataReceived);
            process.StartInfo.FileName = FolderLocation + "\\downloader.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.Arguments = "copy -P \"" + drivePath + "\" " + folder + "";
            process.Exited += new EventHandler(p_Exited);
            process.EnableRaisingEvents = true;
            process.Start();
            nightPanel2.Visible = true;
            process.BeginOutputReadLine();
            //Process.Start(FolderLocation + "\\downloader.exe", " copy -P \"OneDrive5TB:" + drivePath + "\" \"" + folder + "\" ");

        }

        private void p_Exited(object sender, EventArgs e)
        {
            
        }

        void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (Process.GetProcessesByName("downloader").Length > 0)
            {
                if (e.Data.Contains("ETA"))
                {
                    string string1 = e.Data;
                    string string2 = string1.Substring(string1.IndexOf("Transferred:"));
                    int progress = 0;
                    int index = string2.IndexOf('%');

                    FilenameLB.Invoke(new Action(() => FilenameLB.Text = string2));

                    if (index >= 0)
                    {
                        string sub = string2.Substring(0, index);
                        string sub2 = sub.Substring(sub.IndexOf(",") + 1);
                        int.TryParse(sub2, out progress);
                    }
                    poisonProgressBar1.Invoke(new Action(() => poisonProgressBar1.Value = progress));
                }

                if (e.Data.Contains("Elapsed"))
                {
                    SpeedLB.Invoke(new Action(() => SpeedLB.Text = e.Data));
                }

                //var lines = FilesizeLB.Lines.Length;
                //
                //if (e.Data.Contains("*") && !e.Data.Contains("ETA") && lines < 4)
                //{
                //    FilesizeLB.Invoke(new Action(() => FilesizeLB.AppendText(Environment.NewLine)));
                //    FilesizeLB.Invoke(new Action(() => FilesizeLB.AppendText(e.Data)));
                //}
                //else if (e.Data.Contains("*") && !e.Data.Contains("ETA") && lines >= 4)
                //{
                //    FilesizeLB.Invoke(new Action(() => FilesizeLB.Text.Remove(0, form3.FilesizeLB.Text.Length)));
                //    FilesizeLB.Invoke(new Action(() => FilesizeLB.AppendText(Environment.NewLine)));
                //    FilesizeLB.Invoke(new Action(() => FilesizeLB.AppendText(e.Data)));
                //}
            }

        }

        private void nightButton2_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\ChromeSetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/ChromeSetup.exe", FolderLocation);
        }

        private void nightButton3_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\FirefoxSetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/FirefoxSetup.exe", FolderLocation);
        }

        private void nightButton4_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\SteamSetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/SteamSetup.exe", FolderLocation);
        }

        private void nightButton5_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\MinecraftInstaller.msi"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/MinecraftInstaller.msi", FolderLocation);
        }

        private void nightButton6_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\SpotifySetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/SpotifySetup.exe", FolderLocation);
        }

        private void nightButton7_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\BraveBrowserSetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/BraveBrowserSetup.exe", FolderLocation);
        }

        private void nightButton8_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\DiscordSetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/DiscordSetup.exe", FolderLocation);
        }

        private void nightButton9_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\7ZipSetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/7ZipSetup.exe", FolderLocation);
        }

        private void nightButton10_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\NPPSetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/NPPSetup.exe", FolderLocation);
        }

        private void nightButton11_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\osuSetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/osuSetup.exe", FolderLocation);
        }
    }
}
