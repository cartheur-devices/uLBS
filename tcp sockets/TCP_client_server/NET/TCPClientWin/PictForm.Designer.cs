namespace Matjazev.Tcp
{
  partial class PictForm
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.lblWork = new System.Windows.Forms.Label();
      this.tbSendKeys = new System.Windows.Forms.TextBox();
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.panel2 = new System.Windows.Forms.Panel();
      this.pictureBox = new System.Windows.Forms.PictureBox();
      this.mouseTimer = new System.Windows.Forms.Timer(this.components);
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.lblWork);
      this.panel1.Controls.Add(this.tbSendKeys);
      this.panel1.Controls.Add(this.button2);
      this.panel1.Controls.Add(this.button1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(292, 71);
      this.panel1.TabIndex = 6;
      // 
      // lblWork
      // 
      this.lblWork.BackColor = System.Drawing.Color.Lime;
      this.lblWork.Location = new System.Drawing.Point(12, 41);
      this.lblWork.Name = "lblWork";
      this.lblWork.Size = new System.Drawing.Size(111, 23);
      this.lblWork.TabIndex = 3;
      // 
      // tbSendKeys
      // 
      this.tbSendKeys.Location = new System.Drawing.Point(12, 15);
      this.tbSendKeys.Name = "tbSendKeys";
      this.tbSendKeys.Size = new System.Drawing.Size(111, 20);
      this.tbSendKeys.TabIndex = 2;
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(133, 12);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(75, 23);
      this.button2.TabIndex = 1;
      this.button2.Text = "SendKeys";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(133, 41);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "Refresh";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // panel2
      // 
      this.panel2.AutoScroll = true;
      this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
      this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel2.Controls.Add(this.pictureBox);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 71);
      this.panel2.Name = "panel2";
      this.panel2.Padding = new System.Windows.Forms.Padding(5);
      this.panel2.Size = new System.Drawing.Size(292, 195);
      this.panel2.TabIndex = 7;
      // 
      // pictureBox
      // 
      this.pictureBox.Location = new System.Drawing.Point(5, 5);
      this.pictureBox.Name = "pictureBox";
      this.pictureBox.Size = new System.Drawing.Size(232, 151);
      this.pictureBox.TabIndex = 5;
      this.pictureBox.TabStop = false;
      this.pictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDoubleClick);
      this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
      this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
      // 
      // mouseTimer
      // 
      this.mouseTimer.Interval = 500;
      this.mouseTimer.Tick += new System.EventHandler(this.mouseTimer_Tick);
      // 
      // PictForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(292, 266);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.Name = "PictForm";
      this.Text = "Scaner";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.PictureBox pictureBox;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.TextBox tbSendKeys;
    private System.Windows.Forms.Label lblWork;
    private System.Windows.Forms.Timer mouseTimer;

  }
}