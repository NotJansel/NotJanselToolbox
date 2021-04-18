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

        private void metroSwitch1_SwitchedChanged(object sender)
        {
            if (metroSwitch1.Switched == true)
            {
                LightMode(); 
            }
            if (metroSwitch1.Switched == false)
            {
                DarkMode();
            }
        }

        private void DarkMode()
        {
            //Form
            this.nightForm1.HeadColor = Color.FromArgb(50, 58, 61);
            this.nightForm1.BackColor = Color.FromArgb(40, 48, 51);
            this.nightForm1.TitleBarTextColor = Color.Gainsboro;
            //Panels
            this.nightPanel12.ForeColor = Color.FromArgb(41, 44, 61);
            this.nightPanel11.ForeColor = Color.FromArgb(41, 44, 61);
            this.nightPanel10.ForeColor = Color.FromArgb(41, 44, 61);
            this.nightPanel9.ForeColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel8.ForeColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel7.ForeColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel6.ForeColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel5.ForeColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel4.ForeColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel3.ForeColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel2.ForeColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel1.ForeColor =  Color.FromArgb(41, 44, 61);
            //Panel Side Colors
            this.nightPanel12.LeftSideColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel11.LeftSideColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel10.LeftSideColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel9.LeftSideColor =   Color.FromArgb(41, 44, 61);
            this.nightPanel8.LeftSideColor =   Color.FromArgb(41, 44, 61);
            this.nightPanel7.LeftSideColor =   Color.FromArgb(41, 44, 61);
            this.nightPanel6.LeftSideColor =   Color.FromArgb(41, 44, 61);
            this.nightPanel5.LeftSideColor =   Color.FromArgb(41, 44, 61);
            this.nightPanel4.LeftSideColor =   Color.FromArgb(41, 44, 61);
            this.nightPanel3.LeftSideColor =   Color.FromArgb(41, 44, 61);
            this.nightPanel2.LeftSideColor =   Color.FromArgb(41, 44, 61);
            this.nightPanel1.LeftSideColor =   Color.FromArgb(41, 44, 61);
            this.nightPanel12.RightSideColor = Color.FromArgb(41, 44, 61);
            this.nightPanel11.RightSideColor = Color.FromArgb(41, 44, 61);
            this.nightPanel10.RightSideColor = Color.FromArgb(41, 44, 61);
            this.nightPanel9.RightSideColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel8.RightSideColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel7.RightSideColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel6.RightSideColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel5.RightSideColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel4.RightSideColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel3.RightSideColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel2.RightSideColor =  Color.FromArgb(41, 44, 61);
            this.nightPanel1.RightSideColor =  Color.FromArgb(41, 44, 61);
            //Panel Side Change
            this.nightPanel12.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel11.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel10.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel9.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel8.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel7.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel6.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel5.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel4.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel3.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel2.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel1.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            //Buttons
            this.nightButton11.ForeColor =       Color.FromArgb(242, 93, 89);
            this.nightButton10.ForeColor =       Color.FromArgb(242, 93, 89);
            this.nightButton9.ForeColor =        Color.FromArgb(242, 93, 89);
            this.nightButton8.ForeColor =        Color.FromArgb(242, 93, 89);
            this.nightButton7.ForeColor =        Color.FromArgb(242, 93, 89);
            this.nightButton6.ForeColor =        Color.FromArgb(242, 93, 89);
            this.nightButton5.ForeColor =        Color.FromArgb(242, 93, 89);
            this.nightButton4.ForeColor =        Color.FromArgb(242, 93, 89);
            this.nightButton3.ForeColor =        Color.FromArgb(242, 93, 89);
            this.nightButton2.ForeColor =        Color.FromArgb(242, 93, 89);
            this.nightButton1.ForeColor =        Color.FromArgb(242, 93, 89);
            this.nightButton11.NormalBackColor = Color.FromArgb(242, 93, 89);
            this.nightButton10.NormalBackColor = Color.FromArgb(242, 93, 89);
            this.nightButton9.NormalBackColor =  Color.FromArgb(242, 93, 89);
            this.nightButton8.NormalBackColor =  Color.FromArgb(242, 93, 89);
            this.nightButton7.NormalBackColor =  Color.FromArgb(242, 93, 89);
            this.nightButton6.NormalBackColor =  Color.FromArgb(242, 93, 89);
            this.nightButton5.NormalBackColor =  Color.FromArgb(242, 93, 89);
            this.nightButton4.NormalBackColor =  Color.FromArgb(242, 93, 89);
            this.nightButton3.NormalBackColor =  Color.FromArgb(242, 93, 89);
            this.nightButton2.NormalBackColor =  Color.FromArgb(242, 93, 89);
            this.nightButton1.NormalBackColor =  Color.FromArgb(242, 93, 89);
            

        }

        private void LightMode()
        {
            //Form
            this.nightForm1.HeadColor = Color.Gainsboro;
            this.nightForm1.BackColor = Color.WhiteSmoke;
            this.nightForm1.TitleBarTextColor = Color.Black;
            //Panels
            this.nightPanel12.ForeColor = Color.LightGray;
            this.nightPanel11.ForeColor = Color.LightGray;
            this.nightPanel10.ForeColor = Color.LightGray;
            this.nightPanel9.ForeColor =  Color.LightGray;
            this.nightPanel8.ForeColor =  Color.LightGray;
            this.nightPanel7.ForeColor =  Color.LightGray;
            this.nightPanel6.ForeColor =  Color.LightGray;
            this.nightPanel5.ForeColor =  Color.LightGray;
            this.nightPanel4.ForeColor =  Color.LightGray;
            this.nightPanel3.ForeColor =  Color.LightGray;
            this.nightPanel2.ForeColor =  Color.LightGray;
            this.nightPanel1.ForeColor =  Color.LightGray;
            //Panel Side Colors
            this.nightPanel12.LeftSideColor =  Color.LightGray;
            this.nightPanel11.LeftSideColor =  Color.LightGray;
            this.nightPanel10.LeftSideColor =  Color.LightGray;
            this.nightPanel9.LeftSideColor =   Color.LightGray;
            this.nightPanel8.LeftSideColor =   Color.LightGray;
            this.nightPanel7.LeftSideColor =   Color.LightGray;
            this.nightPanel6.LeftSideColor =   Color.LightGray;
            this.nightPanel5.LeftSideColor =   Color.LightGray;
            this.nightPanel4.LeftSideColor =   Color.LightGray;
            this.nightPanel3.LeftSideColor =   Color.LightGray;
            this.nightPanel2.LeftSideColor =   Color.LightGray;
            this.nightPanel1.LeftSideColor =   Color.LightGray;
            this.nightPanel12.RightSideColor = Color.LightGray;
            this.nightPanel11.RightSideColor = Color.LightGray;
            this.nightPanel10.RightSideColor = Color.LightGray;
            this.nightPanel9.RightSideColor =  Color.LightGray;
            this.nightPanel8.RightSideColor =  Color.LightGray;
            this.nightPanel7.RightSideColor =  Color.LightGray;
            this.nightPanel6.RightSideColor =  Color.LightGray;
            this.nightPanel5.RightSideColor =  Color.LightGray;
            this.nightPanel4.RightSideColor =  Color.LightGray;
            this.nightPanel3.RightSideColor =  Color.LightGray;
            this.nightPanel2.RightSideColor =  Color.LightGray;
            this.nightPanel1.RightSideColor =  Color.LightGray;
            //Panel Side Change
            this.nightPanel12.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel11.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel10.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel9.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel8.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel7.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel6.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel5.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel4.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel3.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel2.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            this.nightPanel1.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Right;
            //Buttons
            this.nightButton11.ForeColor =       Color.DeepSkyBlue;
            this.nightButton10.ForeColor =       Color.DeepSkyBlue;
            this.nightButton9.ForeColor =        Color.DeepSkyBlue;
            this.nightButton8.ForeColor =        Color.DeepSkyBlue;
            this.nightButton7.ForeColor =        Color.DeepSkyBlue;
            this.nightButton6.ForeColor =        Color.DeepSkyBlue;
            this.nightButton5.ForeColor =        Color.DeepSkyBlue;
            this.nightButton4.ForeColor =        Color.DeepSkyBlue;
            this.nightButton3.ForeColor =        Color.DeepSkyBlue;
            this.nightButton2.ForeColor =        Color.DeepSkyBlue;
            this.nightButton1.ForeColor =        Color.DeepSkyBlue;
            this.nightButton11.NormalBackColor = Color.DeepSkyBlue;
            this.nightButton10.NormalBackColor = Color.DeepSkyBlue;
            this.nightButton9.NormalBackColor =  Color.DeepSkyBlue;
            this.nightButton8.NormalBackColor =  Color.DeepSkyBlue;
            this.nightButton7.NormalBackColor =  Color.DeepSkyBlue;
            this.nightButton6.NormalBackColor =  Color.DeepSkyBlue;
            this.nightButton5.NormalBackColor =  Color.DeepSkyBlue;
            this.nightButton4.NormalBackColor =  Color.DeepSkyBlue;
            this.nightButton3.NormalBackColor =  Color.DeepSkyBlue;
            this.nightButton2.NormalBackColor =  Color.DeepSkyBlue;
            this.nightButton1.NormalBackColor =  Color.DeepSkyBlue;
            
        }
    }
}
