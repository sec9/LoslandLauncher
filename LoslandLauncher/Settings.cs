using LoslandLauncher.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoslandLauncher
{
    public partial class Settings : Form
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        bool inital = false;

        public Settings()
        {
            InitializeComponent();
            LoadFonts();
            SetFonts();
            inital = true;
            checkBox1.Checked = Convert.ToBoolean(Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\loslauncher").GetValue("settings_1"));
            checkBox2.Checked = Convert.ToBoolean(Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\loslauncher").GetValue("settings_2"));
            inital = false;
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
            checkBox1.Font = new Font(fonts.Families[0], 10, FontStyle.Regular);
            checkBox2.Font = new Font(fonts.Families[0], 10, FontStyle.Regular);
            label1.Font = new Font(fonts.Families[0], 12, FontStyle.Bold);
        }


        private void closeForm_MouseEnter(object sender, EventArgs e)
        {
            closeForm.Image = Properties.Resources.althover;
        }

        private void closeForm_MouseLeave(object sender, EventArgs e)
        {
            closeForm.Image = Properties.Resources.alt;
        }

        private void closeForm_Click(object sender, EventArgs e)
        {
            Main main = (Main)Application.OpenForms["Main"];
            main.CloseSettingsForm();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (inital) return;
            Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\loslauncher", true).SetValue("settings_1", Convert.ToInt32(checkBox1.Checked));
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (inital) return;
            if (!Directory.Exists(Globals.GamePath + "\\modloader"))
            {
                MessageBox.Show("Oyun klasöründe modloader yazılımı bulunamadı.", "Losland Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\loslauncher", true).SetValue("settings_2", Convert.ToInt32(checkBox2.Checked));
            Application.Restart();
            Environment.Exit(0);
        }
    }
}
