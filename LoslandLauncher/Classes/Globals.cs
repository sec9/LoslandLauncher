using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoslandLauncher.Classes
{
    class Globals
    {
        public static string WEBAPI = "http://fast.sec-nine.com/secac/losland/app/";
        public static string Version = "0.2";
        public static string GameIP = "51.195.39.72";
        public static string _GamePath = Registry.CurrentUser.OpenSubKey(@"Software\\SAMP").GetValue("gta_sa_exe").ToString();
        public static string GamePath = _GamePath = _GamePath.Substring(0, _GamePath.LastIndexOf(@"\") + 1);
        public static string Username = "";
    }
}
