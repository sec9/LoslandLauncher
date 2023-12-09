using Microsoft.Win32;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoslandUpdater
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            foreach (Process process in Process.GetProcesses("."))
            {
                try
                {
                    if (process.MainWindowTitle.Length > 0 && (process.MainWindowTitle.Contains("GTA: San Andreas") || process.MainWindowTitle.Contains("GTA:SA:MP")))
                    {
                        process.Kill();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string _GamePath = Registry.CurrentUser.OpenSubKey(@"Software\\SAMP").GetValue("gta_sa_exe").ToString();
                string GamePath = _GamePath = _GamePath.Substring(0, _GamePath.LastIndexOf(@"\") + 1);

                string[] replaceFiles = Directory.GetFiles(GamePath, "*.acreplace", SearchOption.AllDirectories);
                foreach (var file in replaceFiles)
                {
                    FileInfo f = new FileInfo(file);
                    string fileName = f.FullName;
                    string originalFile = f.FullName;
                    originalFile = originalFile.Replace(".acreplace", "");
                    if (File.Exists(originalFile)) File.Delete(originalFile);
                    File.Move(fileName, originalFile);
                    if (File.Exists(fileName)) File.Delete(fileName);
                }

                RegistryKey _reg = Registry.CurrentUser.OpenSubKey(@"Software\\SAMP", true);
                string username = (string)_reg.GetValue("PlayerName");

                Process game = new Process();
                game.StartInfo.FileName = GamePath + "\\samp.exe";
                game.StartInfo.Arguments = "samp.losland-rp.com -n" + username;
                game.StartInfo.UseShellExecute = true;
                game.Start();

                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
