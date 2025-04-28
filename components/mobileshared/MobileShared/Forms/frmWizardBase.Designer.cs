namespace MobileShared
{
    partial class frmWizardBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItemLeft = new System.Windows.Forms.MenuItem();
            this.menuItemRight = new System.Windows.Forms.MenuItem();
            this.lblTop = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.Label();
            this.GPSTimer = new System.Windows.Forms.Timer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblWizard = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemLeft);
            this.mainMenu1.MenuItems.Add(this.menuItemRight);
            // 
            // menuItemLeft
            // 
            this.menuItemLeft.Text = "OK";
            this.menuItemLeft.Click += new System.EventHandler(this.menuItemLeft_Click);
            // 
            // menuItemRight
            // 
            this.menuItemRight.Text = "Cancel";
            this.menuItemRight.Click += new System.EventHandler(this.menuItemRight_Click);
            // 
            // lblTop
            // 
            this.lblTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTop.Font = new System.Drawing.Font("Segoe Condensed", 12F, System.Drawing.FontStyle.Bold);
            this.lblTop.Location = new System.Drawing.Point(3, 19);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(150, 20);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Font = new System.Drawing.Font("Segoe Condensed", 8F, System.Drawing.FontStyle.Regular);
            this.txtDescription.Location = new System.Drawing.Point(3, 39);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(157, 130);
            // 
            // GPSTimer
            // 
            this.GPSTimer.Interval = 1000;
            this.GPSTimer.Tick += new System.EventHandler(this.GPSTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Location = new System.Drawing.Point(0, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(173, 1);
            // 
            // lblWizard
            // 
            this.lblWizard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWizard.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblWizard.Location = new System.Drawing.Point(3, 2);
            this.lblWizard.Name = "lblWizard";
            this.lblWizard.Size = new System.Drawing.Size(166, 18);
            this.lblWizard.Text = "Wizard";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(5, 77);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(164, 21);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Visible = false;
            // 
            // lblUsername
            // 
            this.lblUsername.Location = new System.Drawing.Point(4, 63);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(172, 18);
            this.lblUsername.Text = "Email";
            this.lblUsername.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(4, 115);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(165, 21);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Visible = false;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(3, 101);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(173, 18);
            this.lblPassword.Text = "Password";
            this.lblPassword.Visible = false;
            // 
            // frmWizardBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(176, 180);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblWizard);
            this.Controls.Add(this.lblTop);
            this.Controls.Add(this.txtDescription);
            this.Menu = this.mainMenu1;
            this.Name = "frmWizardBase";
            this.Text = "iFind Mobile";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItemLeft;
        private System.Windows.Forms.MenuItem menuItemRight;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.Label txtDescription;
        private System.Windows.Forms.Timer GPSTimer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblWizard;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
    }
}