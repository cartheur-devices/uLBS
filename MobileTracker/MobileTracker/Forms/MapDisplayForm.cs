using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MobileTracker
{
    public partial class MapDisplayForm : Form
    {
        public MapDisplayForm()
        {
            InitializeComponent();
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}