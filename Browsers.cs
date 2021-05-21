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
    public partial class Browsers : Form
    {
        private string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string FolderLocation = @"C:\temp\";
        public WebClient client = new WebClient();

        public Browsers()
        {
            InitializeComponent();
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
            process.EnableRaisingEvents = true;
            process.Start();
            process.BeginOutputReadLine();

        }

        private void nightButton2_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\ChromeSetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/ChromeSetup.exe", FolderLocation);
            StartInstall("ChromeSetup.exe");
        }

        private void nightButton3_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\FirefoxSetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/FirefoxSetup.exe", FolderLocation);
            StartInstall("FirefoxSetup.exe");
        }

        private void nightButton7_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\BraveBrowserSetup.exe"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/BraveBrowserSetup.exe", FolderLocation);
            StartInstall("BraveBrowserSetup.exe");
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
