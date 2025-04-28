namespace PositionSenderMobile
{
    partial class PositionSenderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenuStrip;

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
            this.mainMenuStrip = new System.Windows.Forms.MainMenu();
            this.sendMenuItem = new System.Windows.Forms.MenuItem();
            this.menuMenuItem = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.configurationMenuItem = new System.Windows.Forms.MenuItem();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.positionDetailsTab = new System.Windows.Forms.TabPage();
            this.portLabel = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.HostLabel = new System.Windows.Forms.Label();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.satellitesInViewLabel = new System.Windows.Forms.Label();
            this.remarksLabel = new System.Windows.Forms.Label();
            this.coordinatesLabel = new System.Windows.Forms.Label();
            this.remarksTextBox = new System.Windows.Forms.TextBox();
            this.positionMapTab = new System.Windows.Forms.TabPage();
            this.getMapButton = new System.Windows.Forms.Button();
            this.mapPictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.googleAPIKey = new System.Windows.Forms.TextBox();
            this.mainTabControl.SuspendLayout();
            this.positionDetailsTab.SuspendLayout();
            this.positionMapTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.MenuItems.Add(this.sendMenuItem);
            this.mainMenuStrip.MenuItems.Add(this.menuMenuItem);
            // 
            // sendMenuItem
            // 
            this.sendMenuItem.Text = "Send!";
            this.sendMenuItem.Click += new System.EventHandler(this.sendMenuItem_Click);
            // 
            // menuMenuItem
            // 
            this.menuMenuItem.MenuItems.Add(this.exitMenuItem);
            this.menuMenuItem.MenuItems.Add(this.configurationMenuItem);
            this.menuMenuItem.Text = "Menu";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // configurationMenuItem
            // 
            this.configurationMenuItem.Text = "Save Configuration";
            this.configurationMenuItem.Click += new System.EventHandler(this.configurationMenuItem_Click);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.positionDetailsTab);
            this.mainTabControl.Controls.Add(this.positionMapTab);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(240, 268);
            this.mainTabControl.TabIndex = 0;
            // 
            // positionDetailsTab
            // 
            this.positionDetailsTab.Controls.Add(this.googleAPIKey);
            this.positionDetailsTab.Controls.Add(this.label1);
            this.positionDetailsTab.Controls.Add(this.portLabel);
            this.positionDetailsTab.Controls.Add(this.portTextBox);
            this.positionDetailsTab.Controls.Add(this.HostLabel);
            this.positionDetailsTab.Controls.Add(this.hostTextBox);
            this.positionDetailsTab.Controls.Add(this.satellitesInViewLabel);
            this.positionDetailsTab.Controls.Add(this.remarksLabel);
            this.positionDetailsTab.Controls.Add(this.coordinatesLabel);
            this.positionDetailsTab.Controls.Add(this.remarksTextBox);
            this.positionDetailsTab.Location = new System.Drawing.Point(0, 0);
            this.positionDetailsTab.Name = "positionDetailsTab";
            this.positionDetailsTab.Size = new System.Drawing.Size(240, 245);
            this.positionDetailsTab.Text = "Position Details";
            // 
            // portLabel
            // 
            this.portLabel.Location = new System.Drawing.Point(7, 35);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(76, 20);
            this.portLabel.Text = "Port";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(89, 34);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(37, 21);
            this.portTextBox.TabIndex = 6;
            // 
            // HostLabel
            // 
            this.HostLabel.Location = new System.Drawing.Point(7, 8);
            this.HostLabel.Name = "HostLabel";
            this.HostLabel.Size = new System.Drawing.Size(76, 20);
            this.HostLabel.Text = "Host";
            // 
            // hostTextBox
            // 
            this.hostTextBox.Location = new System.Drawing.Point(89, 7);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(148, 21);
            this.hostTextBox.TabIndex = 3;
            // 
            // satellitesInViewLabel
            // 
            this.satellitesInViewLabel.Location = new System.Drawing.Point(7, 104);
            this.satellitesInViewLabel.Name = "satellitesInViewLabel";
            this.satellitesInViewLabel.Size = new System.Drawing.Size(226, 20);
            this.satellitesInViewLabel.Text = "# Satellites unknown";
            // 
            // remarksLabel
            // 
            this.remarksLabel.Location = new System.Drawing.Point(7, 144);
            this.remarksLabel.Name = "remarksLabel";
            this.remarksLabel.Size = new System.Drawing.Size(226, 20);
            this.remarksLabel.Text = "Enter position remarks";
            // 
            // coordinatesLabel
            // 
            this.coordinatesLabel.Location = new System.Drawing.Point(7, 124);
            this.coordinatesLabel.Name = "coordinatesLabel";
            this.coordinatesLabel.Size = new System.Drawing.Size(226, 20);
            this.coordinatesLabel.Text = "Position not known";
            // 
            // remarksTextBox
            // 
            this.remarksTextBox.Location = new System.Drawing.Point(7, 167);
            this.remarksTextBox.Multiline = true;
            this.remarksTextBox.Name = "remarksTextBox";
            this.remarksTextBox.Size = new System.Drawing.Size(226, 75);
            this.remarksTextBox.TabIndex = 0;
            // 
            // positionMapTab
            // 
            this.positionMapTab.Controls.Add(this.getMapButton);
            this.positionMapTab.Controls.Add(this.mapPictureBox);
            this.positionMapTab.Location = new System.Drawing.Point(0, 0);
            this.positionMapTab.Name = "positionMapTab";
            this.positionMapTab.Size = new System.Drawing.Size(240, 245);
            this.positionMapTab.Text = "Position Map";
            // 
            // getMapButton
            // 
            this.getMapButton.Location = new System.Drawing.Point(7, 222);
            this.getMapButton.Name = "getMapButton";
            this.getMapButton.Size = new System.Drawing.Size(226, 20);
            this.getMapButton.TabIndex = 1;
            this.getMapButton.Text = "Get Map";
            this.getMapButton.Click += new System.EventHandler(this.getMapButton_Click);
            // 
            // mapPictureBox
            // 
            this.mapPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.mapPictureBox.Location = new System.Drawing.Point(0, 0);
            this.mapPictureBox.Name = "mapPictureBox";
            this.mapPictureBox.Size = new System.Drawing.Size(240, 220);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.Text = "Google Key";
            // 
            // googleAPIKey
            // 
            this.googleAPIKey.Location = new System.Drawing.Point(89, 61);
            this.googleAPIKey.Name = "googleAPIKey";
            this.googleAPIKey.Size = new System.Drawing.Size(148, 21);
            this.googleAPIKey.TabIndex = 12;
            // 
            // PositionSenderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.mainTabControl);
            this.Menu = this.mainMenuStrip;
            this.Name = "PositionSenderForm";
            this.Text = "Position Sender";
            this.Load += new System.EventHandler(this.PositionSenderForm_Load);
            this.mainTabControl.ResumeLayout(false);
            this.positionDetailsTab.ResumeLayout(false);
            this.positionMapTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem sendMenuItem;
        private System.Windows.Forms.MenuItem menuMenuItem;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage positionDetailsTab;
        private System.Windows.Forms.Label remarksLabel;
        private System.Windows.Forms.Label coordinatesLabel;
        private System.Windows.Forms.TextBox remarksTextBox;
        private System.Windows.Forms.TabPage positionMapTab;
        private System.Windows.Forms.PictureBox mapPictureBox;
        private System.Windows.Forms.Label satellitesInViewLabel;
        private System.Windows.Forms.Button getMapButton;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.MenuItem configurationMenuItem;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Label HostLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.TextBox googleAPIKey;
        private System.Windows.Forms.Label label1;
    }
}

