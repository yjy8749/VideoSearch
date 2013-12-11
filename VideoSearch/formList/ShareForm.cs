using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VideoSearch
{
    public partial class ShareForm : Form
    {
        private static readonly ShareForm INTERFACE = new ShareForm();
        private ShareForm()
        {
            InitializeComponent();
        }
        public static ShareForm getInterface()
        {
            return ShareForm.INTERFACE;
        }

        private void explanationLabel_Click(object sender, EventArgs e)
        {
            Process.Start(Constant.SHARE_SERVICE_EXPLAN_URL);
        }

        private void sourceDirText_Click(object sender, EventArgs e)
        {
            Constant.folderBrowserDialog.ShowDialog();
            if (this.sourceDirText.Text == null || this.sourceDirText.Text.Equals(""))
            {
                this.sourceDirText.Text = Constant.folderBrowserDialog.SelectedPath;
            }
            else
            {
                this.sourceDirText.Text = this.sourceDirText.Text +"|"+ Constant.folderBrowserDialog.SelectedPath;
            }
        }
       
    }
}
