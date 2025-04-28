namespace TCPServer
{
  partial class Form1
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
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.lblInfo = new System.Windows.Forms.LinkLabel();
      this.lblIP = new System.Windows.Forms.LinkLabel();
      this.SuspendLayout();
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(128, 100);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(63, 27);
      this.button2.TabIndex = 8;
      this.button2.Text = "End";
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(12, 100);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(63, 27);
      this.button1.TabIndex = 7;
      this.button1.Text = "Start";
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // lblInfo
      // 
      this.lblInfo.BackColor = System.Drawing.Color.White;
      this.lblInfo.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
      this.lblInfo.ForeColor = System.Drawing.Color.Black;
      this.lblInfo.Location = new System.Drawing.Point(12, 17);
      this.lblInfo.Name = "lblInfo";
      this.lblInfo.Size = new System.Drawing.Size(179, 20);
      this.lblInfo.TabIndex = 11;
      this.lblInfo.Text = "linkLabel1";
      this.lblInfo.Click += new System.EventHandler(this.lblInfo_Click);
      // 
      // lblIP
      // 
      this.lblIP.BackColor = System.Drawing.Color.White;
      this.lblIP.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
      this.lblIP.ForeColor = System.Drawing.Color.Black;
      this.lblIP.Location = new System.Drawing.Point(12, 37);
      this.lblIP.Name = "lblIP";
      this.lblIP.Size = new System.Drawing.Size(179, 40);
      this.lblIP.TabIndex = 12;
      this.lblIP.Text = "linkLabel1";
      this.lblIP.Click += new System.EventHandler(this.lblInfo_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.ClientSize = new System.Drawing.Size(198, 147);
      this.Controls.Add(this.lblIP);
      this.Controls.Add(this.lblInfo);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
      this.Name = "Form1";
      this.Text = "Form1";
      this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.LinkLabel lblInfo;
    private System.Windows.Forms.LinkLabel lblIP;
  }
}