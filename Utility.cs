using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toolbox
{
    public partial class Utility : Form
    {
        private string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string FolderLocation = @"C:\temp\";
        public WebClient client = new WebClient();

        public Utility()
        {
            InitializeComponent();
        }

        private void nightButton8_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\DiscordSetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/DiscordSetup.exe", FolderLocation);
            StartInstall("DiscordSetup.exe");
        }

        private void nightButton9_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\7ZipSetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/7ZipSetup.exe", FolderLocation);
            StartInstall("7ZipSetup.exe");
        }

        private void nightButton10_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\NPPSetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/NPPSetup.exe", FolderLocation);
            StartInstall("NPPSetup.exe");
        }

        private void nightButton4_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\SteamSetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/SteamSetup.exe", FolderLocation);
            StartInstall("SteamSetup.exe");
        }

        private void nightButton6_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\SpotifySetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/SpotifySetup.exe", FolderLocation);
            StartInstall("SpotifySetup.exe");
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
            process.BeginOutputReadLine();
            //Process.Start(FolderLocation + "\\downloader.exe", " copy -P \"OneDrive5TB:" + drivePath + "\" \"" + folder + "\" ");

        }

        private void p_Exited(object sender, EventArgs e)
        {

        }

        private void StartInstall(string filename)
        {
            while (true)
            {
                if (Process.GetProcessesByName("downloader").Length > 0)
                {

                }
                else
                {
                    Process process = new Process();
                    process.StartInfo.FileName = FolderLocation + filename;
                    process.Exited += (sender, e) => Process_Exited(sender, e, filename);
                    process.EnableRaisingEvents = true;
                    process.Start();
                    return;
                }
            }
        }

        void Process_Exited(object sender, EventArgs e, string filename)
        {
            File.Delete(FolderLocation + filename);
        }
    }
}
