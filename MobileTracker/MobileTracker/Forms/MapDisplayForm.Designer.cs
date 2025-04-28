namespace MobileTracker
{
    partial class MapDisplayForm
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
            this.mapPanel = new System.Windows.Forms.Panel();
            this.zoomMenuItem = new System.Windows.Forms.MenuItem();
            this.closeMenuItem = new System.Windows.Forms.MenuItem();
            this.mapStatusBar = new System.Windows.Forms.StatusBar();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.zoomMenuItem);
            this.mainMenu1.MenuItems.Add(this.closeMenuItem);
            // 
            // mapPanel
            // 
            this.mapPanel.Location = new System.Drawing.Point(3, 3);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(234, 236);
            // 
            // zoomMenuItem
            // 
            this.zoomMenuItem.Text = "Zoom";
            // 
            // closeMenuItem
            // 
            this.closeMenuItem.Text = "Close";
            this.closeMenuItem.Click += new System.EventHandler(this.closeMenuItem_Click);
            // 
            // mapStatusBar
            // 
            this.mapStatusBar.Location = new System.Drawing.Point(0, 246);
            this.mapStatusBar.Name = "mapStatusBar";
            this.mapStatusBar.Size = new System.Drawing.Size(240, 22);
            // 
            // MapDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.mapStatusBar);
            this.Controls.Add(this.mapPanel);
            this.Menu = this.mainMenu1;
            this.Name = "MapDisplayForm";
            this.Text = "MapDisplayForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mapPanel;
        private System.Windows.Forms.MenuItem zoomMenuItem;
        private System.Windows.Forms.MenuItem closeMenuItem;
        private System.Windows.Forms.StatusBar mapStatusBar;
    }
}