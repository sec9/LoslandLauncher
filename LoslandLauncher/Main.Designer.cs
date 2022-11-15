
namespace LoslandLauncher
{
    partial class Main
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.userNameBox = new System.Windows.Forms.TextBox();
            this.hostName = new System.Windows.Forms.Label();
            this.serverInfo = new System.Windows.Forms.Timer(this.components);
            this.serverInfoWorker = new System.ComponentModel.BackgroundWorker();
            this.ipAddress = new System.Windows.Forms.Label();
            this.playerCount = new System.Windows.Forms.Label();
            this.pingCount = new System.Windows.Forms.Label();
            this.closeApp = new System.Windows.Forms.PictureBox();
            this.minimizeApp = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeApp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeApp)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::LoslandLauncher.Properties.Resources.login;
            this.pictureBox1.Location = new System.Drawing.Point(796, 483);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(145, 56);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            // 
            // userNameBox
            // 
            this.userNameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(133)))), ((int)(((byte)(133)))));
            this.userNameBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userNameBox.Location = new System.Drawing.Point(576, 495);
            this.userNameBox.MaxLength = 16;
            this.userNameBox.Name = "userNameBox";
            this.userNameBox.Size = new System.Drawing.Size(170, 13);
            this.userNameBox.TabIndex = 3;
            this.userNameBox.Text = "LRP_";
            // 
            // hostName
            // 
            this.hostName.AutoSize = true;
            this.hostName.BackColor = System.Drawing.Color.Transparent;
            this.hostName.ForeColor = System.Drawing.Color.White;
            this.hostName.Location = new System.Drawing.Point(65, 145);
            this.hostName.Name = "hostName";
            this.hostName.Size = new System.Drawing.Size(16, 13);
            this.hostName.TabIndex = 4;
            this.hostName.Text = "...";
            // 
            // serverInfo
            // 
            this.serverInfo.Enabled = true;
            this.serverInfo.Interval = 8000;
            this.serverInfo.Tick += new System.EventHandler(this.serverInfo_Tick);
            // 
            // serverInfoWorker
            // 
            this.serverInfoWorker.WorkerReportsProgress = true;
            this.serverInfoWorker.WorkerSupportsCancellation = true;
            this.serverInfoWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.serverInfoWorker_DoWork);
            // 
            // ipAddress
            // 
            this.ipAddress.AutoSize = true;
            this.ipAddress.BackColor = System.Drawing.Color.Transparent;
            this.ipAddress.ForeColor = System.Drawing.Color.White;
            this.ipAddress.Location = new System.Drawing.Point(65, 184);
            this.ipAddress.Name = "ipAddress";
            this.ipAddress.Size = new System.Drawing.Size(16, 13);
            this.ipAddress.TabIndex = 5;
            this.ipAddress.Text = "...";
            // 
            // playerCount
            // 
            this.playerCount.AutoSize = true;
            this.playerCount.BackColor = System.Drawing.Color.Transparent;
            this.playerCount.ForeColor = System.Drawing.Color.White;
            this.playerCount.Location = new System.Drawing.Point(65, 225);
            this.playerCount.Name = "playerCount";
            this.playerCount.Size = new System.Drawing.Size(16, 13);
            this.playerCount.TabIndex = 6;
            this.playerCount.Text = "...";
            // 
            // pingCount
            // 
            this.pingCount.AutoSize = true;
            this.pingCount.BackColor = System.Drawing.Color.Transparent;
            this.pingCount.ForeColor = System.Drawing.Color.White;
            this.pingCount.Location = new System.Drawing.Point(65, 265);
            this.pingCount.Name = "pingCount";
            this.pingCount.Size = new System.Drawing.Size(16, 13);
            this.pingCount.TabIndex = 7;
            this.pingCount.Text = "...";
            // 
            // closeApp
            // 
            this.closeApp.BackColor = System.Drawing.Color.Transparent;
            this.closeApp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeApp.Image = global::LoslandLauncher.Properties.Resources.x;
            this.closeApp.Location = new System.Drawing.Point(955, 28);
            this.closeApp.Name = "closeApp";
            this.closeApp.Size = new System.Drawing.Size(30, 30);
            this.closeApp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.closeApp.TabIndex = 8;
            this.closeApp.TabStop = false;
            this.closeApp.Click += new System.EventHandler(this.closeApp_Click);
            this.closeApp.MouseEnter += new System.EventHandler(this.closeApp_MouseEnter);
            this.closeApp.MouseLeave += new System.EventHandler(this.closeApp_MouseLeave);
            // 
            // minimizeApp
            // 
            this.minimizeApp.BackColor = System.Drawing.Color.Transparent;
            this.minimizeApp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimizeApp.Image = global::LoslandLauncher.Properties.Resources.alt;
            this.minimizeApp.Location = new System.Drawing.Point(906, 28);
            this.minimizeApp.Name = "minimizeApp";
            this.minimizeApp.Size = new System.Drawing.Size(30, 30);
            this.minimizeApp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.minimizeApp.TabIndex = 9;
            this.minimizeApp.TabStop = false;
            this.minimizeApp.Click += new System.EventHandler(this.minimizeApp_Click);
            this.minimizeApp.MouseEnter += new System.EventHandler(this.minimizeApp_MouseEnter);
            this.minimizeApp.MouseLeave += new System.EventHandler(this.minimizeApp_MouseLeave);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LoslandLauncher.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1024, 560);
            this.Controls.Add(this.minimizeApp);
            this.Controls.Add(this.closeApp);
            this.Controls.Add(this.pingCount);
            this.Controls.Add(this.playerCount);
            this.Controls.Add(this.ipAddress);
            this.Controls.Add(this.hostName);
            this.Controls.Add(this.userNameBox);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Losland Launcher";
            this.Load += new System.EventHandler(this.Main_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Main_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeApp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeApp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox userNameBox;
        private System.Windows.Forms.Label hostName;
        private System.Windows.Forms.Timer serverInfo;
        private System.ComponentModel.BackgroundWorker serverInfoWorker;
        private System.Windows.Forms.Label ipAddress;
        private System.Windows.Forms.Label playerCount;
        private System.Windows.Forms.Label pingCount;
        private System.Windows.Forms.PictureBox closeApp;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox minimizeApp;
    }
}

