namespace MobileTracker
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.sendMenuItem = new System.Windows.Forms.MenuItem();
            this.alertMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItemStartSending = new System.Windows.Forms.MenuItem();
            this.menuItemStopSending = new System.Windows.Forms.MenuItem();
            this.settingsMenuItem = new System.Windows.Forms.MenuItem();
            this.addContactMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.trailContactMenuItem = new System.Windows.Forms.MenuItem();
            this.trailAllmenuItem = new System.Windows.Forms.MenuItem();
            this.preferencesMenuItem = new System.Windows.Forms.MenuItem();
            this.viewMenuItem = new System.Windows.Forms.MenuItem();
            this.viewMapMenuItem = new System.Windows.Forms.MenuItem();
            this.debugMenuItem = new System.Windows.Forms.MenuItem();
            this.mainStatusBar = new System.Windows.Forms.StatusBar();
            this.positionsTimer = new System.Windows.Forms.Timer();
            this.lblNextSendTime = new System.Windows.Forms.Label();
            this.lblStatusDetails = new System.Windows.Forms.Label();
            this.lblLastSentTime = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.imageList2 = new System.Windows.Forms.ImageList();
            this.pbStatusSmall = new System.Windows.Forms.PictureBox();
            this.pbStatus = new System.Windows.Forms.PictureBox();
            this.quitMenuItem = new System.Windows.Forms.MenuItem();
            this.minimizeMenuItem = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.sendMenuItem);
            this.mainMenu.MenuItems.Add(this.settingsMenuItem);
            // 
            // sendMenuItem
            // 
            this.sendMenuItem.MenuItems.Add(this.alertMenuItem);
            this.sendMenuItem.MenuItems.Add(this.menuItemStartSending);
            this.sendMenuItem.MenuItems.Add(this.menuItemStopSending);
            this.sendMenuItem.MenuItems.Add(this.quitMenuItem);
            this.sendMenuItem.Text = "Send";
            // 
            // alertMenuItem
            // 
            this.alertMenuItem.Text = "Alert";
            // 
            // menuItemStartSending
            // 
            this.menuItemStartSending.Text = "Send Location";
            // 
            // menuItemStopSending
            // 
            this.menuItemStopSending.Text = "Stop Sending";
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.MenuItems.Add(this.addContactMenuItem);
            this.settingsMenuItem.MenuItems.Add(this.menuItem6);
            this.settingsMenuItem.MenuItems.Add(this.preferencesMenuItem);
            this.settingsMenuItem.MenuItems.Add(this.viewMenuItem);
            this.settingsMenuItem.MenuItems.Add(this.debugMenuItem);
            this.settingsMenuItem.Text = "Settings";
            // 
            // addContactMenuItem
            // 
            this.addContactMenuItem.Text = "Add Contact";
            this.addContactMenuItem.Click += new System.EventHandler(this.addContactMenuItem_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.MenuItems.Add(this.trailContactMenuItem);
            this.menuItem6.MenuItems.Add(this.trailAllmenuItem);
            this.menuItem6.Text = "Trail";
            // 
            // trailContactMenuItem
            // 
            this.trailContactMenuItem.Text = "Contact";
            // 
            // trailAllmenuItem
            // 
            this.trailAllmenuItem.Text = "All";
            // 
            // preferencesMenuItem
            // 
            this.preferencesMenuItem.Text = "Preferences...";
            this.preferencesMenuItem.Click += new System.EventHandler(this.preferencesMenuItem_Click);
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.MenuItems.Add(this.viewMapMenuItem);
            this.viewMenuItem.Text = "View";
            // 
            // viewMapMenuItem
            // 
            this.viewMapMenuItem.Text = "Map";
            this.viewMapMenuItem.Click += new System.EventHandler(this.viewMapMenuItem_Click);
            // 
            // debugMenuItem
            // 
            this.debugMenuItem.Text = "Debug";
            this.debugMenuItem.Click += new System.EventHandler(this.debugMenuItem_Click);
            // 
            // mainStatusBar
            // 
            this.mainStatusBar.Location = new System.Drawing.Point(0, 246);
            this.mainStatusBar.Name = "mainStatusBar";
            this.mainStatusBar.Size = new System.Drawing.Size(240, 22);
            // 
            // lblNextSendTime
            // 
            this.lblNextSendTime.Location = new System.Drawing.Point(4, 203);
            this.lblNextSendTime.Name = "lblNextSendTime";
            this.lblNextSendTime.Size = new System.Drawing.Size(233, 20);
            this.lblNextSendTime.Text = "(y)";
            // 
            // lblStatusDetails
            // 
            this.lblStatusDetails.Location = new System.Drawing.Point(4, 122);
            this.lblStatusDetails.Name = "lblStatusDetails";
            this.lblStatusDetails.Size = new System.Drawing.Size(233, 20);
            this.lblStatusDetails.Text = "(y)";
            // 
            // lblLastSentTime
            // 
            this.lblLastSentTime.Location = new System.Drawing.Point(4, 33);
            this.lblLastSentTime.Name = "lblLastSentTime";
            this.lblLastSentTime.Size = new System.Drawing.Size(233, 20);
            this.lblLastSentTime.Text = "(x)";
            // 
            // imageList1
            // 
            this.imageList1.ImageSize = new System.Drawing.Size(41, 55);
            this.imageList2.Images.Clear();
            this.imageList2.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource"))));
            // 
            // pbStatusSmall
            // 
            this.pbStatusSmall.Location = new System.Drawing.Point(201, 3);
            this.pbStatusSmall.Name = "pbStatusSmall";
            this.pbStatusSmall.Size = new System.Drawing.Size(36, 50);
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(29, 145);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(187, 50);
            // 
            // quitMenuItem
            // 
            this.quitMenuItem.MenuItems.Add(this.minimizeMenuItem);
            this.quitMenuItem.MenuItems.Add(this.exitMenuItem);
            this.quitMenuItem.Text = "Quit";
            // 
            // minimizeMenuItem
            // 
            this.minimizeMenuItem.Text = "Minimize";
            this.minimizeMenuItem.Click += new System.EventHandler(this.minimizeMenuItem_Click);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.pbStatusSmall);
            this.Controls.Add(this.lblLastSentTime);
            this.Controls.Add(this.lblStatusDetails);
            this.Controls.Add(this.lblNextSendTime);
            this.Controls.Add(this.mainStatusBar);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBar mainStatusBar;
        private System.Windows.Forms.MenuItem sendMenuItem;
        private System.Windows.Forms.MenuItem alertMenuItem;
        private System.Windows.Forms.MenuItem menuItemStartSending;
        private System.Windows.Forms.MenuItem settingsMenuItem;
        private System.Windows.Forms.MenuItem addContactMenuItem;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem trailContactMenuItem;
        private System.Windows.Forms.MenuItem trailAllmenuItem;
        private System.Windows.Forms.MenuItem preferencesMenuItem;
        private System.Windows.Forms.MenuItem viewMenuItem;
        private System.Windows.Forms.MenuItem viewMapMenuItem;
        private System.Windows.Forms.Timer positionsTimer;
        private System.Windows.Forms.MenuItem menuItemStopSending;
        private System.Windows.Forms.Label lblNextSendTime;
        private System.Windows.Forms.Label lblStatusDetails;
        private System.Windows.Forms.Label lblLastSentTime;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.PictureBox pbStatusSmall;
        private System.Windows.Forms.MenuItem debugMenuItem;
        private System.Windows.Forms.PictureBox pbStatus;
        private System.Windows.Forms.MenuItem quitMenuItem;
        private System.Windows.Forms.MenuItem minimizeMenuItem;
        private System.Windows.Forms.MenuItem exitMenuItem;
    }
}

