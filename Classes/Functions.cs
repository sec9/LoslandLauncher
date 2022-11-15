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
        public static string ReadTextFromUrl(string url)
        {
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
            WebClient indir = new WebClient();
            Uri yol = new Uri(Globals.WEBAPI + "files/LoslandUpdater.exe");
            indir.DownloadFileCompleted += new AsyncCompletedEventHandler(FileDownloaded);
            indir.DownloadFileAsync(yol, Path.GetTempPath() + "LoslandUpdater.exe");
        }

        public static void FileDownloaded(object sender, AsyncCompletedEventArgs e)
        {
            System.Diagnostics.Process.Start(Path.GetTempPath() + "LoslandUpdater.exe");
            Environment.Exit(0);
        }
    }
}
