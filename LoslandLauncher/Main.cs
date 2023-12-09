using LoslandLauncher.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LoslandLauncher
{
    public partial class Main : Form
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private SAMPQuery sampquery = new SAMPQuery(Globals.GameIP, 7777);
        public Main()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(21, 21, 21);
            this.TransparencyKey = Color.FromArgb(21, 21, 21);
            LoadFonts();
            SetFonts();
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\loslauncher", true);
                if (registryKey.GetValue("username") == null)
                {
                    RegistryKey _reg = Registry.CurrentUser.OpenSubKey(@"Software\\SAMP", true);
                    if (_reg != null)
                    {
                        userNameBox.Text = (string)_reg.GetValue("PlayerName");
                        registryKey.SetValue("username", userNameBox.Text);
                    }
                    else userNameBox.Text = "LRP_";
                }
                else
                {
                    userNameBox.Text = (string)registryKey.GetValue("username");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            userNameBox.Select(userNameBox.Text.Length, userNameBox.Text.Length);
            ipAddress.Text = Globals.GameIP + ":7777";
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
            userNameBox.Font = new Font(fonts.Families[0], 12, FontStyle.Regular);
            hostName.Font = new Font(fonts.Families[0], 8, FontStyle.Bold);
            ipAddress.Font = new Font(fonts.Families[0], 8, FontStyle.Bold);
            playerCount.Font = new Font(fonts.Families[0], 8, FontStyle.Bold);
            pingCount.Font = new Font(fonts.Families[0], 8, FontStyle.Bold);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            serverInfoWorker.RunWorkerAsync();
        }

        public void CloseDownloaderForm()
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Name == "Downloader")
                {
                    frm.Hide();
                    return;
                }
            }
        }

        public void CloseACForm()
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Name == "ACLauncher")
                {
                    frm.Close();
                    return;
                }
            }
        }

        public void CloseSettingsForm()
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if(frm.Name == "Settings")
                {
                    frm.Close();
                    return;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Functions.CheckNetwork() == 0)
            {
                MessageBox.Show("İnternet bağlantısı yok.", "Losland Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (userNameBox.Text.Length < 4 || userNameBox.Text.Length > 23)
            {
                MessageBox.Show("Kullanıcı adı en az 4, en fazla 24 karakter olabilir.", "Losland Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            /*
            else if (userNameBox.Text[0] != 'L' || userNameBox.Text[1] != 'R' || userNameBox.Text[2] != 'P' || userNameBox.Text[3] != '_')
            {
                MessageBox.Show("Geçersiz LRP_ID formatı. (örnek: LRP_1)", "Losland Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            */
            else if (Globals.Version != Functions.ReadTextFromUrl(Globals.WEBAPI + "version.php"))
            {
                Functions.UpdateApp();
                return;
            }
            Globals.Username = userNameBox.Text;
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\loslauncher", true);
            registryKey.SetValue("username", userNameBox.Text);
            this.Hide();
            ACLauncher launcher = new ACLauncher();
            launcher.Show();
            launcher.TopLevel = true;
            launcher.TopMost = true;
            launcher.WindowState = FormWindowState.Minimized;
            launcher.WindowState = FormWindowState.Normal;
       }

        private void serverInfo_Tick(object sender, EventArgs e)
        {
            if (!serverInfoWorker.IsBusy) serverInfoWorker.RunWorkerAsync();
        }

        private void serverInfoWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.sampquery.Send('p');
            int receiver = this.sampquery.Receive();
            if (receiver > 0)
            {
                string[] array = this.sampquery.Store(receiver);
                int serverping = int.Parse(array[0]);
                this.sampquery.Send('i');
                receiver = this.sampquery.Receive();
                array = this.sampquery.Store(receiver);
                if (receiver > 0)
                {
                    hostName.Text = array[3];
                    playerCount.Text = array[1] + "/" + array[2];
                    pingCount.Text = serverping + "ms";
                }
            }
            else
            {
                hostName.Text = playerCount.Text = pingCount.Text = "N/A";
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.loginhover;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.login;
        }

        private void closeApp_MouseEnter(object sender, EventArgs e)
        {
            closeApp.Image = Properties.Resources.xhover;
        }

        private void closeApp_MouseLeave(object sender, EventArgs e)
        {
            closeApp.Image = Properties.Resources.x;
        }

        private void minimizeApp_MouseEnter(object sender, EventArgs e)
        {
            minimizeApp.Image = Properties.Resources.althover;
        }

        private void minimizeApp_MouseLeave(object sender, EventArgs e)
        {
            minimizeApp.Image = Properties.Resources.alt;
        }

        private void closeApp_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void minimizeApp_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        int fMove;
        int fMouse_X;
        int fMouse_Y;

        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            fMove = 0;
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            fMove = 1;
            fMouse_X = e.X;
            fMouse_Y = e.Y;
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (fMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - fMouse_X, MousePosition.Y - fMouse_Y);
            }
        }

        private void settings_MouseEnter(object sender, EventArgs e)
        {
            settings.Image = Properties.Resources.settingshover;
        }

        private void settings_MouseLeave(object sender, EventArgs e)
        {
            settings.Image = Properties.Resources.settings;
        }

        private void settings_Click(object sender, EventArgs e)
        {
            CloseSettingsForm();
            Settings settingsForm = new Settings();
            settingsForm.Show();
        }
    }
}
