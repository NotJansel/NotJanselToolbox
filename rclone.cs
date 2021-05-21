using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toolbox
{
    public class rclone
    {
        private string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string FolderLocation = @"C:\temp\";
        public WebClient client = new WebClient();
        Form1 form1 = new Form1();

        public void DownloadFile(string drivePath, string folder)
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
    }
}
