using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoslandLauncher.Classes
{
    class Functions
    {
        public static int CheckNetwork()
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public static string ReadTextFromUrl(string url, bool nocache = false)
        {
            if(nocache)
            {
                Random random = new Random();
                url = url + "?r=" + random.Next().ToString();
            }
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.UserAgent = "SECAC/1.0";
            httpWebRequest.CookieContainer = cookieContainer;
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            return new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding(httpWebResponse.CharacterSet)).ReadToEnd();
        }

        public static bool IsRunning(string name) => Process.GetProcessesByName(name).Length > 0;

        public static void UpdateApp()
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\loslauncher", true);
            if (registryKey == null)
            {
                registryKey = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\loslauncher");
            }
            registryKey.SetValue("app_path", System.Reflection.Assembly.GetExecutingAssembly().Location);
            Random random = new Random();
            WebClient indir = new WebClient();
            Uri yol = new Uri(Globals.WEBAPI + "files/LoslandUpdater.exe" + "?r=" + random.Next().ToString());
            indir.DownloadFileCompleted += new AsyncCompletedEventHandler(FileDownloaded);
            indir.DownloadFileAsync(yol, Path.GetTempPath() + "LoslandUpdater.exe");
        }

        public static void FileDownloaded(object sender, AsyncCompletedEventArgs e)
        {
            System.Diagnostics.Process.Start(Path.GetTempPath() + "LoslandUpdater.exe");
            Environment.Exit(0);
        }

        public static long GetDirectorySize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += GetDirectorySize(di);
            }
            return size;
        }

        public static long GetDirectorySize(string p)
        {
            // Get array of all file names.
            string[] a = Directory.GetFiles(p, "*.*");

            // Calculate total bytes of all files in a loop.
            long b = 0;
            foreach (string name in a)
            {
                // Use FileInfo to get length of each file.
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }
            // Return total size
            return b;
        }
    }
}
