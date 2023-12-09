using LoslandLauncher.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoslandLauncher
{
    public partial class Downloader : Form
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        string[] downloadArray;
        string[] downloadTempArray;
        bool downloadingZip = false;
        public Downloader()
        {
            InitializeComponent();
            LoadFonts();
            SetFonts();
        }

        private void Downloader_Load(object sender, EventArgs e)
        {
            try
            {
                bool check = Functions.IsRunning("gta_sa");
                if(check)
                {
                    MessageBox.Show("Launcheri çalıştırabilmek için öncelikle GTA:SA'yı kapatmanız gerekiyor.", "Losland Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                    return;
                }
                if (!Directory.Exists(Globals.GamePath))
                {
                    MessageBox.Show("Cihazınızda GTA:SA veya SA-MP yazılımı bulunamadı.", "Losland Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                    return;
                }
                RegistryKey _reg = Registry.CurrentUser.OpenSubKey(@"Software\\SAMP", true);
                if (_reg == null)
                {
                    MessageBox.Show("Cihazınızda GTA:SA veya SA-MP yazılımı bulunamadı.", "Losland Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                    return;
                }
                if (Globals.Version != Functions.ReadTextFromUrl(Globals.WEBAPI + "version.php", true))
                {
                    Functions.UpdateApp();
                    return;
                }
                if (!Directory.Exists(Globals.GamePath + "\\secac\\data"))
                {
                    Directory.CreateDirectory(Globals.GamePath + "\\secac\\data");
                }
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\loslauncher", true);
                if (registryKey == null)
                {
                    registryKey = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\loslauncher");
                }
                if (registryKey.GetValue("settings_1") == null) registryKey.SetValue("settings_1", 0);
                if (registryKey.GetValue("settings_2") == null) registryKey.SetValue("settings_2", 0);
                downloadArray = Functions.ReadTextFromUrl(Globals.WEBAPI + "patch.php", true).Split('|');
                CheckUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private PrivateFontCollection fonts = new PrivateFontCollection();
        private void LoadFonts()
        {
            byte[] fontData = Properties.Resources.jostreg;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.jostreg.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.jostreg.Length, IntPtr.Zero, ref dummy);
            Marshal.FreeCoTaskMem(fontPtr);
        }
        private void SetFonts()
        {
            label1.Font = new Font(fonts.Families[0], 10, FontStyle.Regular);
            label2.Font = new Font(fonts.Families[0], 8, FontStyle.Regular);
            label3.Font = new Font(fonts.Families[0], 8, FontStyle.Regular);
        }

        private void CheckUpdate()
        {
            int founded = 0;
            for (int i = 0; i < downloadArray.Length; i++)
            {
                downloadTempArray = downloadArray[i].Split(',');
                if (!File.Exists(Globals.GamePath + "\\" + downloadTempArray[0]))
                {
                    if (downloadTempArray[0] == "samp.asi") continue;
                    founded = 1;
                    DownloadFile(Globals.WEBAPI + "files/" + downloadTempArray[0], Globals.GamePath + "\\" + downloadTempArray[0]);
                    label1.Text = "Oyun dosyaları güncelleniyor...";
                    label2.Text = downloadTempArray[0] + " indiriliyor...";
                    break;
                }
                else
                {
                    long filesize = new FileInfo(Globals.GamePath + "\\" + downloadTempArray[0]).Length;
                    if (filesize != Convert.ToInt32(downloadTempArray[1]))
                    {
                        founded = 1;
                        DownloadFile(Globals.WEBAPI + "files/" + downloadTempArray[0], Globals.GamePath + "\\" + downloadTempArray[0]);
                        label1.Text = "Oyun dosyaları güncelleniyor...";
                        label2.Text = downloadTempArray[0] + " indiriliyor...";
                        break;
                    }
                }
            }
            if (founded == 0)
            {
                if (Convert.ToBoolean(Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\loslauncher").GetValue("settings_2")))
                {
                    if (Directory.Exists(Globals.GamePath + "\\modloader"))
                    {
                        int needdownload = 0;
                        if (File.Exists(Globals.GamePath + "\\modloader\\mod_loslandlauncher.zip"))
                        {
                            label1.Text = "Mod dosyaları güncelleniyor...";
                            label2.Text = "mod_loslandlauncher.zip çıkartılıyor...";
                            label3.Text = "Bu işlem disk hızına göre biraz zaman alabilir.";
                            zipExtractor.RunWorkerAsync();
                            return;
                        }
                        else if (Directory.Exists(Globals.GamePath + "\\modloader\\mod_loslandlauncher"))
                        {
                            long filesize = Functions.GetDirectorySize(new DirectoryInfo(Globals.GamePath + "\\modloader\\mod_loslandlauncher"));
                            long neededfilesize = Convert.ToInt64(Functions.ReadTextFromUrl(Globals.WEBAPI + "modpatch.php", true));
                            if(filesize != neededfilesize)
                            {
                                needdownload = 1;
                            }
                        }
                        else
                        {
                            needdownload = 1;
                        }
                        if(needdownload == 1)
                        {
                            downloadingZip = true;
                            DownloadFile(Globals.WEBAPI + "files/mod_loslandlauncher.zip", Globals.GamePath + "\\modloader\\mod_loslandlauncher_crc.zip");
                            label1.Text = "Mod dosyaları güncelleniyor...";
                            label2.Text = "mod_loslandlauncher.zip indiriliyor...";
                            return;
                        }
                    }
                }
                else
                {
                    if (Directory.Exists(Globals.GamePath + "\\modloader\\mod_loslandlauncher"))
                    {
                        Directory.Delete(Globals.GamePath + "\\modloader\\mod_loslandlauncher", true);
                    }
                }
                timer1.Start();
            }
        }

        private static Stopwatch stopWatch = new Stopwatch();
        private static long lastBytes;
        private static long currentBytes;

        public void DownloadFile(string url, string path)
        {
            Random random = new Random();
            url = url + "?r=" + random.Next().ToString();

            WebClient down = new WebClient();
            Uri link = new Uri(url);
            down.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadUpdate);
            down.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFinish);
            stopWatch.Start();
            down.DownloadFileAsync(link, path);
        }

        public void DownloadUpdate(object sender, DownloadProgressChangedEventArgs e)
        {
            panel3.Width = Convert.ToInt32(Math.Round(4.45 * e.ProgressPercentage));
            currentBytes = lastBytes + e.BytesReceived;
            label3.Text = "Boyut: " + ComputeDownloadSize((double)e.BytesReceived).ToString("0.00") + "MB / " + ComputeDownloadSize((double)e.TotalBytesToReceive).ToString("0.00") + "MB" + ", Hız: " + ComputeDownloadSpeed((double)e.BytesReceived, stopWatch).ToString("0,00") + "kb/sn";
        }

        private void DownloadFinish(object sender, AsyncCompletedEventArgs e)
        {
            if(downloadingZip)
            {
                if (File.Exists(Globals.GamePath + "\\modloader\\mod_loslandlauncher.zip")) File.Delete(Globals.GamePath + "\\modloader\\mod_loslandlauncher.zip");
                File.Move(Globals.GamePath + "\\modloader\\mod_loslandlauncher_crc.zip", Globals.GamePath + "\\modloader\\mod_loslandlauncher.zip");
                File.Delete(Globals.GamePath + "\\modloader\\mod_loslandlauncher_crc.zip");
                downloadingZip = false;
            }
            stopWatch.Reset();
            CheckUpdate();
        }

        public static double ComputeDownloadSize(double Size)
        {
            return Size / 1024.0 / 1024.0;
        }

        public static double ComputeDownloadSpeed(double Size, Stopwatch stopWatch)
        {
            return Size / 1024.0 / stopWatch.Elapsed.TotalSeconds;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Main main = new Main();
            main.Show();
            main.WindowState = FormWindowState.Minimized;
            main.WindowState = FormWindowState.Normal;
            main.CloseDownloaderForm();
        }

        private void zipExtractor_DoWork(object sender, DoWorkEventArgs e)
        {
            if(Directory.Exists(Globals.GamePath + "\\modloader\\mod_loslandlauncher")) Directory.Delete(Globals.GamePath + "\\modloader\\mod_loslandlauncher", true);
            Directory.CreateDirectory(Globals.GamePath + "\\modloader\\mod_loslandlauncher");

            ZipFile.ExtractToDirectory(Globals.GamePath + "\\modloader\\mod_loslandlauncher.zip", Globals.GamePath + "\\modloader\\mod_loslandlauncher");
            ZipArchive archive = ZipFile.OpenRead(Globals.GamePath + "\\modloader\\mod_loslandlauncher.zip");
            archive.Dispose();

            File.Delete(Globals.GamePath + "\\modloader\\mod_loslandlauncher.zip");
        }

        private void zipExtractor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CheckUpdate();
        }
    }
}
