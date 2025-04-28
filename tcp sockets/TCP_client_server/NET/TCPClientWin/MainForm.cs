//-----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using InputBoxSample;
using Matjazev.Tcp.Plugin;
using TCPClientWin;

namespace Matjazev.Tcp
{
  public partial class MainForm : Form
  {
    private PictForm pictForm = null;

    public MainForm()
    {
      this.InitializeComponent();

      string programPath = Path.GetDirectoryName(Application.ExecutablePath);
      PluginsManager.Inst.LoadPlugins(programPath);
    }

    public static void DoShow()
    {
      using (MainForm frm = new MainForm())
      {
        frm.prepareListView();
        frm.ShowDialog();
      }
    }

    private string createJobsXML(string jobs)
    {
      return string.Format(@"<?xml version='1.0' encoding='utf-8'?><jobs>{0}</jobs>", jobs);
    }

    private void btnShow_Click(object sender, EventArgs e)
    {
      this.beginBrowse();
    }

    private void beginBrowse()
    {
      this.twFolder.BeginUpdate();
      this.twFolder.SelectedNode = null;
      this.twFolder.Nodes.Clear();
      this.twFolder.EndUpdate();

      TreeNode mainNode = new TreeNode();
      mainNode.Name = "main";
      mainNode.Text = this.tbScanerIP.Text;
      this.twFolder.Nodes.Add(mainNode);

      this.prepareListView();
      this.addSubNode(mainNode, @"\"); 
      mainNode.Expand();
    }

    private void prepareListView()
    {
      this.lvFiles.View = View.Details;

      this.lvFiles.Columns.Clear();
      this.lvFiles.Columns.Add("Ime");
      this.lvFiles.Columns.Add("Konènica");
      this.lvFiles.Columns.Add("Velikost", 20, HorizontalAlignment.Right);
      this.lvFiles.Columns.Add("Datum");

      this.lblFolder.Text = string.Empty;
    }

    private void addSubNode(TreeNode root, string dir)
    {
      string ip = this.tbScanerIP.Text;

      string parentPath = string.Empty;
      if (root.Parent != null) parentPath = (root.Parent.Tag as FSRep.Directory).Path;
      dir = Path.Combine(parentPath, dir);

      XmlDocument xmlDoc = ClientUtils.GetDirData(ip, 15555, dir, "dir", LogDir.Instance.Get);
      if (xmlDoc == null) return;

      XmlElement job = ClientUtils.GetJob(xmlDoc, "dir");
      if ((job != null) && (job.FirstChild != null) && (job.FirstChild.FirstChild != null))
      {
        if (Utils.IsErrorJob(job, true)) return;

        FSRep.Directory currentDir = new FSRep.Directory(dir);
        XmlNode value = job.SelectSingleNode("value");
        if (value == null) return;

        XmlNode element = value.FirstChild.FirstChild;
        while (element != null)
        {
          if (element.Name == "Dir")
          {
            FSRep.Directory newDir = new FSRep.Directory(Path.Combine(dir, element.Attributes["name"].Value));

            TreeNode node = new TreeNode(newDir.Name);
            node.Tag = newDir;
            node.Nodes.Add(new TreeNode());
            root.Nodes.Add(node);
          }

          if (element.Name == "File")
          {
            FSRep.File f = new FSRep.File(element);
            currentDir.Files.Add(f);
          }

          element = element.NextSibling;
        }

        currentDir.Readed = true;
        root.Tag = currentDir;
      }
    }

    private void twDir_Select(object sender, TreeViewCancelEventArgs e)
    {
      if ((e.Node == null) || (e.Node.Tag == null)) return;

      this.updateNode(e.Node, false);
    }

    private void updateNode(TreeNode node, bool forceRefresh)
    {
      FSRep.Directory dir = (FSRep.Directory)node.Tag;
      if ((!dir.Readed) || (forceRefresh))
      {
        node.Nodes.Clear();
        this.addSubNode(node, node.Text);
        dir = (FSRep.Directory)node.Tag;
      }

      this.updateListView(dir);
    }

    private void updateListView(FSRep.Directory currentDir)
    {
      this.lblFolder.Text = currentDir.Path;
      this.lvFiles.BeginUpdate();
      this.lvFiles.Items.Clear();
      foreach (FSRep.File f in currentDir.Files)
      {
        ListViewItem itm = new ListViewItem(f.Name);
        itm.SubItems.Add(f.Ext);
        itm.SubItems.Add(f.Size.ToString("#,###"));
        itm.SubItems.Add(f.LastWrite.ToString("dd.MM.yyyy hh:mm:ss"));
        this.lvFiles.Items.Add(itm);
      }

      this.lvFiles.Tag = currentDir.Path;
      this.lvFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
      this.lvFiles.EndUpdate();
    }

    private void btnShowPict_Click(object sender, EventArgs e)
    {
      if (this.pictForm == null)
      {
        this.pictForm = new PictForm();
        this.pictForm.FormClosed += new FormClosedEventHandler(this.PictFormClosed);
        this.pictForm.Text = "Scaner: " + this.tbScanerIP.Text;
        this.pictForm.Show(this);
      }

      this.pictForm.GetPicture(this.tbScanerIP.Text);
    }

    private void PictFormClosed(object sender, FormClosedEventArgs e)
    {
      this.pictForm.Dispose();
      this.pictForm = null;
    }

    private void FilesDragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
        e.Effect = DragDropEffects.Copy;
      else
        e.Effect = DragDropEffects.None;
    }

    private void FilesDragDrop(object sender, DragEventArgs e)
    {
      string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

      int cntr = 0;
      string jobs = string.Empty;
      string path = this.lblFolder.Text;
      foreach (string file in fileList)
      {
        string fileName = Path.GetFileName(file);
        string outFileName = Path.Combine(path, fileName);
        jobs += string.Format("<job id='put{0}' action='putFile' inFile='{1}' outFile='{2}' />", ++cntr, file, outFileName);
      }

      // execute
      Utils.ExecuteWithMessageBox(this.tbScanerIP.Text, 15555, this.createJobsXML(jobs), LogDir.Instance.Get);

      // refresh screen
      this.updateNode(this.twFolder.SelectedNode, true);
    }

    private void FilesItemDrag(object sender, ItemDragEventArgs e)
    {
    }

    private void miUpload_Click(object sender, EventArgs e)
    {
      if (this.lvFiles.SelectedItems.Count == 0) return;

      string destFolder = Utils.SelectFolder();
      if (destFolder == string.Empty) return;

      int cntr = 0;
      string jobs = string.Empty;
      string path = this.lblFolder.Text;
      foreach (ListViewItem item in this.lvFiles.SelectedItems)
      {
        string inFileName = Path.Combine(path, item.Text);
        string outFileName = Path.Combine(destFolder, item.Text);
        jobs += string.Format("<job id='get{0}' action='getFile' inFile='{1}' outFile='{2}' />", ++cntr, inFileName, outFileName);
      }

      Utils.ExecuteWithMessageBox(this.tbScanerIP.Text, 15555, this.createJobsXML(jobs), LogDir.Instance.Get);

      MessageBox.Show("OK!");
    }

    private void miDelete_Click(object sender, EventArgs e)
    {
      if (this.lvFiles.SelectedItems.Count == 0) return;

      int cntr = 0;
      string jobs = string.Empty;
      string path = this.lblFolder.Text;
      foreach (ListViewItem item in this.lvFiles.SelectedItems)
      {
        string inFileName = Path.Combine(path, item.Text);
        jobs += string.Format("<job id='delete{0}' action='deleteFile' file='{1}' />", ++cntr, inFileName);
      }

      // execute
      Utils.ExecuteWithMessageBox(this.tbScanerIP.Text, 15555, this.createJobsXML(jobs), LogDir.Instance.Get);

      // refresh screen
      this.updateNode(this.twFolder.SelectedNode, true);
    }

    private void miCreateFolder_Click(object sender, EventArgs e)
    {
      InputBoxResult result = InputBox.Show("Folder name:", "Folder...", string.Empty, null);
      if (!result.OK) return;

      FSRep.Directory curretPath = (FSRep.Directory)this.twFolder.SelectedNode.Tag;
      string fullPath = Path.Combine(curretPath.Path, result.Text);

      string job = string.Format(@"<job id='create' action='createFolder' folder='{0}' />", fullPath);

      // execute
      Utils.ExecuteWithMessageBox(this.tbScanerIP.Text, 15555, this.createJobsXML(job), LogDir.Instance.Get);

      // refresh screen
      this.updateNode(this.twFolder.SelectedNode, true);
      this.twFolder.SelectedNode.Expand();
    }

    private void miDeleteFolder_Click(object sender, EventArgs e)
    {
      FSRep.Directory curretPath = (FSRep.Directory)this.twFolder.SelectedNode.Tag;

      if (MessageBox.Show(string.Format("Delete folder {0}?", curretPath.Path), "Info", MessageBoxButtons.YesNo) == DialogResult.No)
        return;

      string job = string.Format(@"<job id='delete' action='removeFolder' recursive='1' folder='{0}' />", curretPath.Path);

      // execute
      XmlDocument retValue = Utils.ExecuteWithMessageBox(this.tbScanerIP.Text, 15555, this.createJobsXML(job), LogDir.Instance.Get).XmlDocument;

      // refresh screen
      TreeNode node = this.twFolder.SelectedNode.Parent;
      if (node == null) return;
      this.updateNode(node, true);
      node.Expand();
    }

    private void cbLog_CheckedChanged(object sender, EventArgs e)
    {
      LogDir.Instance.DoLog = this.cbLog.Checked;
    }

    private void miRefresh_Click(object sender, EventArgs e)
    {
      // refresh screen
      this.updateNode(this.twFolder.SelectedNode, true);
    }

    private void miRefreshFolder_Click(object sender, EventArgs e)
    {
      // refresh screen
      this.updateNode(this.twFolder.SelectedNode, true);
    }
  }
}