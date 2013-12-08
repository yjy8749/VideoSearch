using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VideoSearch
{
    public sealed partial class SetForm : Form
    {
        private SetForm()
        {
            InitializeComponent();
        }
        private static readonly SetForm INTERFACE = new SetForm();
        public static SetForm getInterface()
        {
            return SetForm.INTERFACE;
        }

        private void aboutSoftWare_Click(object sender, EventArgs e)
        {
            new AboutSoftWareForm().ShowDialog();
        }

        private void setFormSureBtn_Click(object sender, EventArgs e)
        {
            string serviceIp = this.serviceAddressCombo.Text;
            if (System.Text.RegularExpressions.Regex.IsMatch(serviceIp,Constant.SERVIC_CHECK_REGEX))
            {
                Hashtable configTable = new Hashtable();
                configTable.Add("serviceIp", serviceIp);
                configTable.Add("defaultDir", this.defaultDownloadDirTextBox.Text);
                XMLService.saveConfigXMl(configTable);
                MessageBox.Show("配置成功");
                this.Hide();
            }else{
                MessageBox.Show(MsgString.SERVICE_IP_NOT_RIGHT);
            }
        }
        
    }
}
