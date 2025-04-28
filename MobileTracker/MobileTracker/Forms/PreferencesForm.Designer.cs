namespace MobileTracker
{
    partial class PreferencesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.lblLocExchange = new System.Windows.Forms.Label();
            this.bindingSourceDomestic = new System.Windows.Forms.BindingSource(this.components);
            this.lblPassword = new System.Windows.Forms.Label();
            this.inputPanel = new Microsoft.WindowsCE.Forms.InputPanel();
            this.lblDisconnectAfterPlot = new System.Windows.Forms.Label();
            this.lblDomesticInt = new System.Windows.Forms.Label();
            this.cbDisconnectAfterPlot = new System.Windows.Forms.ComboBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.cbDomesticInt = new System.Windows.Forms.ComboBox();
            this.cbAutoStartup = new System.Windows.Forms.ComboBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.bindingSourceMovingDelay = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbLocExchange = new System.Windows.Forms.ComboBox();
            this.bindingSourceRoaming = new System.Windows.Forms.BindingSource(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblAutoStartup = new System.Windows.Forms.Label();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.cancelMenuItem = new System.Windows.Forms.MenuItem();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuItemLeft = new System.Windows.Forms.MenuItem();
            this.menuItemRight = new System.Windows.Forms.MenuItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.preferencesStatusBar = new System.Windows.Forms.StatusBar();
            this.lblSettings = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLocExchange
            // 
            this.lblLocExchange.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblLocExchange.Location = new System.Drawing.Point(5, 22);
            this.lblLocExchange.Name = "lblLocExchange";
            this.lblLocExchange.Size = new System.Drawing.Size(181, 17);
            this.lblLocExchange.Text = "Location exchange";
            // 
            // lblPassword
            // 
            this.lblPassword.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblPassword.Location = new System.Drawing.Point(6, 238);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(181, 18);
            this.lblPassword.Text = "Password";
            // 
            // lblDisconnectAfterPlot
            // 
            this.lblDisconnectAfterPlot.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblDisconnectAfterPlot.Location = new System.Drawing.Point(5, 101);
            this.lblDisconnectAfterPlot.Name = "lblDisconnectAfterPlot";
            this.lblDisconnectAfterPlot.Size = new System.Drawing.Size(181, 18);
            this.lblDisconnectAfterPlot.Text = "Data connection";
            // 
            // lblDomesticInt
            // 
            this.lblDomesticInt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblDomesticInt.Location = new System.Drawing.Point(6, 60);
            this.lblDomesticInt.Name = "lblDomesticInt";
            this.lblDomesticInt.Size = new System.Drawing.Size(180, 16);
            this.lblDomesticInt.Text = "Interval";
            // 
            // cbDisconnectAfterPlot
            // 
            this.cbDisconnectAfterPlot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDisconnectAfterPlot.Items.Add("Stay connected");
            this.cbDisconnectAfterPlot.Items.Add("Disconnect @ report");
            this.cbDisconnectAfterPlot.Location = new System.Drawing.Point(6, 122);
            this.cbDisconnectAfterPlot.Name = "cbDisconnectAfterPlot";
            this.cbDisconnectAfterPlot.Size = new System.Drawing.Size(123, 22);
            this.cbDisconnectAfterPlot.TabIndex = 107;
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsername.Location = new System.Drawing.Point(6, 214);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(71, 21);
            this.txtUsername.TabIndex = 109;
            // 
            // cbDomesticInt
            // 
            this.cbDomesticInt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDomesticInt.DataSource = this.bindingSourceDomestic;
            this.cbDomesticInt.DisplayMember = "Display";
            this.cbDomesticInt.Location = new System.Drawing.Point(6, 76);
            this.cbDomesticInt.Name = "cbDomesticInt";
            this.cbDomesticInt.Size = new System.Drawing.Size(108, 22);
            this.cbDomesticInt.TabIndex = 104;
            this.cbDomesticInt.ValueMember = "Value";
            // 
            // cbAutoStartup
            // 
            this.cbAutoStartup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAutoStartup.Items.Add("On");
            this.cbAutoStartup.Items.Add("Off");
            this.cbAutoStartup.Location = new System.Drawing.Point(5, 168);
            this.cbAutoStartup.Name = "cbAutoStartup";
            this.cbAutoStartup.Size = new System.Drawing.Size(108, 22);
            this.cbAutoStartup.TabIndex = 108;
            // 
            // lblUsername
            // 
            this.lblUsername.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblUsername.Location = new System.Drawing.Point(6, 193);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(181, 18);
            this.lblUsername.Text = "Contact Alias";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel2.Location = new System.Drawing.Point(0, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(178, 1);
            // 
            // cbLocExchange
            // 
            this.cbLocExchange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLocExchange.Items.Add("On");
            this.cbLocExchange.Items.Add("Off");
            this.cbLocExchange.Location = new System.Drawing.Point(6, 36);
            this.cbLocExchange.Name = "cbLocExchange";
            this.cbLocExchange.Size = new System.Drawing.Size(108, 22);
            this.cbLocExchange.TabIndex = 103;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.AutoScrollMargin = new System.Drawing.Size(0, 10);
            this.panel3.Controls.Add(this.preferencesStatusBar);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.cbLocExchange);
            this.panel3.Controls.Add(this.txtPassword);
            this.panel3.Controls.Add(this.lblSettings);
            this.panel3.Controls.Add(this.lblLocExchange);
            this.panel3.Controls.Add(this.lblAutoStartup);
            this.panel3.Controls.Add(this.lblPassword);
            this.panel3.Controls.Add(this.lblDisconnectAfterPlot);
            this.panel3.Controls.Add(this.lblDomesticInt);
            this.panel3.Controls.Add(this.cbDisconnectAfterPlot);
            this.panel3.Controls.Add(this.txtUsername);
            this.panel3.Controls.Add(this.cbDomesticInt);
            this.panel3.Controls.Add(this.cbAutoStartup);
            this.panel3.Controls.Add(this.lblUsername);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(240, 268);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(6, 259);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(71, 21);
            this.txtPassword.TabIndex = 110;
            // 
            // lblAutoStartup
            // 
            this.lblAutoStartup.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblAutoStartup.Location = new System.Drawing.Point(5, 147);
            this.lblAutoStartup.Name = "lblAutoStartup";
            this.lblAutoStartup.Size = new System.Drawing.Size(181, 18);
            this.lblAutoStartup.Text = "Auto start application";
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Text = "Save";
            this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // cancelMenuItem
            // 
            this.cancelMenuItem.Text = "Cancel";
            this.cancelMenuItem.Click += new System.EventHandler(this.cancelMenuItem_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.cancelMenuItem);
            this.mainMenu.MenuItems.Add(this.saveMenuItem);
            // 
            // menuItemLeft
            // 
            this.menuItemLeft.Text = "Cancel";
            // 
            // menuItemRight
            // 
            this.menuItemRight.Text = "Save";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemLeft);
            this.mainMenu1.MenuItems.Add(this.menuItemRight);
            // 
            // preferencesStatusBar
            // 
            this.preferencesStatusBar.Location = new System.Drawing.Point(0, 290);
            this.preferencesStatusBar.Name = "preferencesStatusBar";
            this.preferencesStatusBar.Size = new System.Drawing.Size(227, 22);
            // 
            // lblSettings
            // 
            this.lblSettings.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblSettings.Location = new System.Drawing.Point(5, 4);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Size = new System.Drawing.Size(112, 16);
            this.lblSettings.Text = "Settings";
            // 
            // PreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panel3);
            this.Menu = this.mainMenu;
            this.Name = "PreferencesForm";
            this.Text = "Preferences";
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLocExchange;
        private System.Windows.Forms.BindingSource bindingSourceDomestic;
        private System.Windows.Forms.Label lblPassword;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel;
        private System.Windows.Forms.Label lblDisconnectAfterPlot;
        private System.Windows.Forms.Label lblDomesticInt;
        private System.Windows.Forms.ComboBox cbDisconnectAfterPlot;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.ComboBox cbDomesticInt;
        private System.Windows.Forms.ComboBox cbAutoStartup;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.BindingSource bindingSourceMovingDelay;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbLocExchange;
        private System.Windows.Forms.BindingSource bindingSourceRoaming;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblAutoStartup;
        private System.Windows.Forms.MenuItem saveMenuItem;
        private System.Windows.Forms.MenuItem cancelMenuItem;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItemLeft;
        private System.Windows.Forms.MenuItem menuItemRight;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.StatusBar preferencesStatusBar;
        private System.Windows.Forms.Label lblSettings;
    }
}