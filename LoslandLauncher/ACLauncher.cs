using LoslandLauncher.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoslandLauncher
{
    public partial class ACLauncher : Form
    {
        bool gtaRunning = false;
        bool waitGTA = false;
        public ACLauncher()
        {
            InitializeComponent();
        }

        private void ACLauncher_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Random rand = new Random();
            int randint = rand.Next(0, 2);
            switch (randint)
            {
                case 0:
                    this.BackgroundImage = Properties.Resources.acbg;
                    break;
                case 1:
                    this.BackgroundImage = Properties.Resources.acbg2;
                    break;
                default:
                    this.BackgroundImage = Properties.Resources.acbg;
                    break;
            }
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 4;
            if(panel2.Width >= 444)
            {
                Process game = new Process();
                game.StartInfo.FileName = Globals.GamePath + "\\samp.exe";
                game.StartInfo.Arguments = Globals.GameIP + " -n" + Globals.Username;
                game.StartInfo.UseShellExecute = true;
                game.StartInfo.Verb = "runas";
                game.Start();
                timer1.Stop();
                this.Hide();
                waitGTA = true;
                timer2.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            bool check = Functions.IsRunning("gta_sa");
            if (waitGTA)
            {
                if(!check)
                {
                    return;
                }
                waitGTA = false;
                gtaRunning = true;
            }
            if (!check)
            {
                timer2.Stop();
                Main main = (Main)Application.OpenForms["Main"];
                main.Show();
                main.WindowState = FormWindowState.Minimized;
                main.WindowState = FormWindowState.Normal;
                main.CloseACForm();
            }
        }
    }
}
