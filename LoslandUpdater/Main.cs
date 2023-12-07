using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoslandUpdater
{
    public partial class Main : Form
    {
        RegistryKey _reg = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\loslauncher", true);
        public static string _path = "";
        public Main()
        {
            InitializeComponent();
            if (_reg == null)
            {
                Environment.Exit(0);
            }
            _path = (string)_reg.GetValue("app_path");
            Random random = new Random();
            WebClient _down = new WebClient();
            Uri _uri = new Uri("http://fast.sec-nine.com/secac/losland/app/files/LoslandLauncher.exe" + "?r=" + random.Next().ToString());
            _down.DownloadFileCompleted += new AsyncCompletedEventHandler(FileDownloaded);
            _down.DownloadFileAsync(_uri, Path.GetDirectoryName(_path) + "\\" + Path.GetFileName(_path));
        }

        public static void FileDownloaded(object sender, AsyncCompletedEventArgs e)
        {
            System.Diagnostics.Process.Start(Path.GetDirectoryName(_path) + "\\" + Path.GetFileName(_path));
            Environment.Exit(0);
        }
    }
}
