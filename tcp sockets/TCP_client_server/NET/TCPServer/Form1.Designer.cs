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
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.lblInfo = new System.Windows.Forms.Label();
      this.lblIP = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(12, 93);
      this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(99, 27);
      this.button1.TabIndex = 0;
      this.button1.Text = "Start";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(195, 93);
      this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(99, 27);
      this.button2.TabIndex = 1;
      this.button2.Text = "End";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // lblInfo
      // 
      this.lblInfo.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.lblInfo.Location = new System.Drawing.Point(16, 9);
      this.lblInfo.Name = "lblInfo";
      this.lblInfo.Size = new System.Drawing.Size(278, 27);
      this.lblInfo.TabIndex = 2;
      this.lblInfo.Text = "label1";
      this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lblInfo.Click += new System.EventHandler(this.lblIP_Click);
      // 
      // lblIP
      // 
      this.lblIP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.lblIP.Location = new System.Drawing.Point(16, 36);
      this.lblIP.Name = "lblIP";
      this.lblIP.Size = new System.Drawing.Size(278, 27);
      this.lblIP.TabIndex = 3;
      this.lblIP.Text = "label1";
      this.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lblIP.Click += new System.EventHandler(this.lblIP_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(310, 133);
      this.Controls.Add(this.lblIP);
      this.Controls.Add(this.lblInfo);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "Form1";
      this.Text = "Server form";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Label lblInfo;
    private System.Windows.Forms.Label lblIP;
  }
}

