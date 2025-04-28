namespace MobileTracker
{
    partial class DebugForm
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
            this.lblDebugGPS = new System.Windows.Forms.Label();
            this.lblDebugCell = new System.Windows.Forms.Label();
            this.lblDebugGPSState = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDebugGPS
            // 
            this.lblDebugGPS.Location = new System.Drawing.Point(4, 49);
            this.lblDebugGPS.Name = "lblDebugGPS";
            this.lblDebugGPS.Size = new System.Drawing.Size(100, 20);
            this.lblDebugGPS.Text = "debugGPS";
            // 
            // lblDebugCell
            // 
            this.lblDebugCell.Location = new System.Drawing.Point(4, 103);
            this.lblDebugCell.Name = "lblDebugCell";
            this.lblDebugCell.Size = new System.Drawing.Size(100, 20);
            this.lblDebugCell.Text = "debugCell";
            // 
            // lblDebugGPSState
            // 
            this.lblDebugGPSState.Location = new System.Drawing.Point(4, 145);
            this.lblDebugGPSState.Name = "lblDebugGPSState";
            this.lblDebugGPSState.Size = new System.Drawing.Size(100, 20);
            this.lblDebugGPSState.Text = "debugGPSState";
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblDebugGPSState);
            this.Controls.Add(this.lblDebugCell);
            this.Controls.Add(this.lblDebugGPS);
            this.Menu = this.mainMenu1;
            this.Name = "DebugForm";
            this.Text = "Debugging";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblDebugGPS;
        public System.Windows.Forms.Label lblDebugCell;
        public System.Windows.Forms.Label lblDebugGPSState;


    }
}