namespace Matjazev.Tcp
{
  partial class MainForm
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
      this.twFolderMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.miCreateFolder = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.miDeleteFolder = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.miRefreshFolder = new System.Windows.Forms.ToolStripMenuItem();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.panel2 = new System.Windows.Forms.Panel();
      this.lvFiles = new System.Windows.Forms.ListView();
      this.lvFilesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.miUpload = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.miRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.lblFolder = new System.Windows.Forms.Label();
      this.panel3 = new System.Windows.Forms.Panel();
      this.cbLog = new System.Windows.Forms.CheckBox();
      this.btnShowPict = new System.Windows.Forms.Button();
      this.btnShowFldrs = new System.Windows.Forms.Button();
      this.tbScanerIP = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.twFolder = new System.Windows.Forms.TreeView();
      this.panel1 = new System.Windows.Forms.Panel();
      this.panel4 = new System.Windows.Forms.Panel();
      this.panel5 = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.twFolderMenu.SuspendLayout();
      this.panel2.SuspendLayout();
      this.lvFilesMenu.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel1.SuspendLayout();
      this.panel4.SuspendLayout();
      this.panel5.SuspendLayout();
      this.SuspendLayout();
      // 
      // twFolderMenu
      // 
      this.twFolderMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCreateFolder,
            this.toolStripSeparator2,
            this.miDeleteFolder,
            this.toolStripSeparator4,
            this.miRefreshFolder});
      this.twFolderMenu.Name = "lvFilesMenu";
      this.twFolderMenu.Size = new System.Drawing.Size(151, 82);
      // 
      // miCreateFolder
      // 
      this.miCreateFolder.Name = "miCreateFolder";
      this.miCreateFolder.Size = new System.Drawing.Size(150, 22);
      this.miCreateFolder.Text = "CreateFolder";
      this.miCreateFolder.Click += new System.EventHandler(this.miCreateFolder_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(147, 6);
      // 
      // miDeleteFolder
      // 
      this.miDeleteFolder.Name = "miDeleteFolder";
      this.miDeleteFolder.Size = new System.Drawing.Size(150, 22);
      this.miDeleteFolder.Text = "DeleteFolder";
      this.miDeleteFolder.Click += new System.EventHandler(this.miDeleteFolder_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(147, 6);
      // 
      // miRefreshFolder
      // 
      this.miRefreshFolder.Name = "miRefreshFolder";
      this.miRefreshFolder.Size = new System.Drawing.Size(150, 22);
      this.miRefreshFolder.Text = "refreshFolder";
      this.miRefreshFolder.Click += new System.EventHandler(this.miRefreshFolder_Click);
      // 
      // splitter1
      // 
      this.splitter1.Location = new System.Drawing.Point(302, 0);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(3, 471);
      this.splitter1.TabIndex = 1;
      this.splitter1.TabStop = false;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.lvFiles);
      this.panel2.Controls.Add(this.lblFolder);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(305, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(531, 471);
      this.panel2.TabIndex = 2;
      // 
      // lvFiles
      // 
      this.lvFiles.AllowDrop = true;
      this.lvFiles.ContextMenuStrip = this.lvFilesMenu;
      this.lvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvFiles.FullRowSelect = true;
      this.lvFiles.Location = new System.Drawing.Point(0, 27);
      this.lvFiles.Name = "lvFiles";
      this.lvFiles.Size = new System.Drawing.Size(531, 444);
      this.lvFiles.TabIndex = 10;
      this.lvFiles.UseCompatibleStateImageBehavior = false;
      this.lvFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.FilesDragDrop);
      this.lvFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.FilesDragEnter);
      this.lvFiles.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.FilesItemDrag);
      // 
      // lvFilesMenu
      // 
      this.lvFilesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miUpload,
            this.toolStripSeparator1,
            this.miDelete,
            this.toolStripSeparator3,
            this.miRefresh});
      this.lvFilesMenu.Name = "lvFilesMenu";
      this.lvFilesMenu.Size = new System.Drawing.Size(124, 82);
      // 
      // miUpload
      // 
      this.miUpload.Name = "miUpload";
      this.miUpload.Size = new System.Drawing.Size(123, 22);
      this.miUpload.Text = "Upload";
      this.miUpload.Click += new System.EventHandler(this.miUpload_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(120, 6);
      // 
      // miDelete
      // 
      this.miDelete.Name = "miDelete";
      this.miDelete.Size = new System.Drawing.Size(123, 22);
      this.miDelete.Text = "Delete";
      this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(120, 6);
      // 
      // miRefresh
      // 
      this.miRefresh.Name = "miRefresh";
      this.miRefresh.Size = new System.Drawing.Size(123, 22);
      this.miRefresh.Text = "Refresh";
      this.miRefresh.Click += new System.EventHandler(this.miRefresh_Click);
      // 
      // lblFolder
      // 
      this.lblFolder.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblFolder.Location = new System.Drawing.Point(0, 0);
      this.lblFolder.Name = "lblFolder";
      this.lblFolder.Padding = new System.Windows.Forms.Padding(5);
      this.lblFolder.Size = new System.Drawing.Size(531, 27);
      this.lblFolder.TabIndex = 0;
      // 
      // panel3
      // 
      this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel3.Controls.Add(this.cbLog);
      this.panel3.Controls.Add(this.btnShowPict);
      this.panel3.Controls.Add(this.btnShowFldrs);
      this.panel3.Controls.Add(this.tbScanerIP);
      this.panel3.Controls.Add(this.label1);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(302, 81);
      this.panel3.TabIndex = 0;
      // 
      // cbLog
      // 
      this.cbLog.AutoSize = true;
      this.cbLog.Location = new System.Drawing.Point(201, 49);
      this.cbLog.Name = "cbLog";
      this.cbLog.Size = new System.Drawing.Size(93, 20);
      this.cbLog.TabIndex = 4;
      this.cbLog.Text = "Write LOG";
      this.cbLog.UseVisualStyleBackColor = true;
      this.cbLog.CheckedChanged += new System.EventHandler(this.cbLog_CheckedChanged);
      // 
      // btnShowPict
      // 
      this.btnShowPict.Location = new System.Drawing.Point(95, 47);
      this.btnShowPict.Name = "btnShowPict";
      this.btnShowPict.Size = new System.Drawing.Size(79, 23);
      this.btnShowPict.TabIndex = 3;
      this.btnShowPict.Text = "Screen";
      this.btnShowPict.UseVisualStyleBackColor = true;
      this.btnShowPict.Click += new System.EventHandler(this.btnShowPict_Click);
      // 
      // btnShowFldrs
      // 
      this.btnShowFldrs.Location = new System.Drawing.Point(10, 47);
      this.btnShowFldrs.Name = "btnShowFldrs";
      this.btnShowFldrs.Size = new System.Drawing.Size(79, 23);
      this.btnShowFldrs.TabIndex = 2;
      this.btnShowFldrs.Text = "Folders";
      this.btnShowFldrs.UseVisualStyleBackColor = true;
      this.btnShowFldrs.Click += new System.EventHandler(this.btnShow_Click);
      // 
      // tbScanerIP
      // 
      this.tbScanerIP.Location = new System.Drawing.Point(95, 9);
      this.tbScanerIP.Name = "tbScanerIP";
      this.tbScanerIP.Size = new System.Drawing.Size(185, 23);
      this.tbScanerIP.TabIndex = 1;
      this.tbScanerIP.Text = "localhost";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(11, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(78, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "Scaner IP:";
      // 
      // twFolder
      // 
      this.twFolder.ContextMenuStrip = this.twFolderMenu;
      this.twFolder.Dock = System.Windows.Forms.DockStyle.Fill;
      this.twFolder.Location = new System.Drawing.Point(0, 81);
      this.twFolder.Name = "twFolder";
      this.twFolder.Size = new System.Drawing.Size(302, 390);
      this.twFolder.TabIndex = 7;
      this.twFolder.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.twDir_Select);
      this.twFolder.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.twDir_Select);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.twFolder);
      this.panel1.Controls.Add(this.panel3);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(302, 471);
      this.panel1.TabIndex = 0;
      // 
      // panel4
      // 
      this.panel4.Controls.Add(this.panel2);
      this.panel4.Controls.Add(this.splitter1);
      this.panel4.Controls.Add(this.panel1);
      this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel4.Location = new System.Drawing.Point(0, 32);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(836, 471);
      this.panel4.TabIndex = 3;
      // 
      // panel5
      // 
      this.panel5.Controls.Add(this.label2);
      this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel5.Location = new System.Drawing.Point(0, 0);
      this.panel5.Name = "panel5";
      this.panel5.Size = new System.Drawing.Size(836, 32);
      this.panel5.TabIndex = 4;
      // 
      // label2
      // 
      this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.label2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.label2.Location = new System.Drawing.Point(0, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(836, 32);
      this.label2.TabIndex = 0;
      this.label2.Text = "This program is just for testing and is not complete!";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // MainForm
      // 
      this.ClientSize = new System.Drawing.Size(836, 503);
      this.Controls.Add(this.panel5);
      this.Controls.Add(this.panel4);
      this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.Name = "MainForm";
      this.Text = "Scaner TCP client...";
      this.twFolderMenu.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.lvFilesMenu.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel4.ResumeLayout(false);
      this.panel5.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.ListView lvFiles;
    private System.Windows.Forms.Label lblFolder;
    private System.Windows.Forms.ContextMenuStrip lvFilesMenu;
    private System.Windows.Forms.ToolStripMenuItem miUpload;
    private System.Windows.Forms.ToolStripMenuItem miDelete;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ContextMenuStrip twFolderMenu;
    private System.Windows.Forms.ToolStripMenuItem miCreateFolder;
    private System.Windows.Forms.ToolStripMenuItem miDeleteFolder;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem miRefresh;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripMenuItem miRefreshFolder;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.CheckBox cbLog;
    private System.Windows.Forms.Button btnShowPict;
    private System.Windows.Forms.Button btnShowFldrs;
    private System.Windows.Forms.TextBox tbScanerIP;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TreeView twFolder;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.Label label2;

  }
}