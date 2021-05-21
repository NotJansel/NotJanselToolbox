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
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace Toolbox
{
    

    public partial class Form1 : Form
    {
        private string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string FolderLocation = @"C:\temp\";
        public WebClient client = new WebClient();
        public bool Done = false;
        //rclone Rclone = new rclone();
        Browsers browsers = new Browsers();
        Games games = new Games();
        Utility utility = new Utility();

        public Form1()
        {
            InitializeComponent();
            Directory.CreateDirectory(FolderLocation);
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            MessageBox.Show("This Message is shown on every startup\n\nWhen you Close this program, the temporary Installationfiles will get deleted.\n\nwith kind regards, NotJansel (Developer)");
            client.DownloadFile("https://drive.google.com/uc?export=download&id=1nlgH6J82UKk9oosZ3dWHGJ586araMLvC", FolderLocation + "config.json");
            config config = JsonConvert.DeserializeObject<config>(File.ReadAllText(FolderLocation + "config.json"));
            nightLabel2.Text = "Serverstatus: Online";
            if (config.reachable == false)
            {
                MessageBox.Show("Servers are not available right now. Please be patient. More information on the Discord");
                nightLabel2.Text = "Serverstatus: Offline";
                nightButton1.Enabled = false;
                nightButton2.Enabled = false;
                nightButton3.Enabled = false;
                nightButton4.Enabled = false;
            }
            utility.TopLevel = false;
            panel1.Controls.Add(utility);
            games.TopLevel = false;
            panel1.Controls.Add(games);
            browsers.TopLevel = false;
            panel1.Controls.Add(browsers);
        }

        private void nightButton1_Click(object sender, EventArgs e)
        {
            if (File.Exists(FolderLocation + @"\test.bin"))
            {
                return;
            }
            DownloadFile("OneDrive5TB:Software/test.bin", FolderLocation);
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
            process.BeginOutputReadLine();
            //Process.Start(FolderLocation + "\\downloader.exe", " copy -P \"OneDrive5TB:" + drivePath + "\" \"" + folder + "\" ");
        
        }

        private void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (Process.GetProcessesByName("downloader").Length > 0)
            {
                if (e.Data.Contains("ETA"))
                {
                    string string1 = e.Data;
                    string string2 = string1.Substring(string1.IndexOf("Transferred:"));
                    int progress = 0;
                    int index = string2.IndexOf('%');


                    if (index >= 0)
                    {
                        string sub = string2.Substring(0, index);
                        string sub2 = sub.Substring(sub.IndexOf(",") + 1);
                        int.TryParse(sub2, out progress);
                    }
                    poisonProgressBar1.Invoke(new Action(() => poisonProgressBar1.Value = progress));
                }

            }
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


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Directory.Delete(FolderLocation, true);
        }

        private void nightButton2_Click(object sender, EventArgs e) //Browsers
        {
            utility.Hide();
            games.Hide();
            browsers.Show();
        }

        private void nightButton3_Click(object sender, EventArgs e) //Utility/Software
        {
            browsers.Hide();
            games.Hide();
            utility.Show();
        }

        private void nightButton4_Click(object sender, EventArgs e) //Games
        {
            utility.Hide();
            browsers.Hide();
            games.Show();
        }

        private void nightLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://discord.gg/SwnMsRr36y");
        }
    }

    public class config
    {
        public bool reachable;
    }
}
