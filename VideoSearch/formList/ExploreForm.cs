using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VideoSearch
{
    public sealed partial class ExploreForm : Form
    {
        private ExploreForm()
        {
            InitializeComponent();
            this.webView.Navigate(Constant.SERVICE_ADDRESS + "2010index.html");
        }
        private static readonly ExploreForm INTERFACE = new ExploreForm();
        public static ExploreForm getInterface()
        {
            return ExploreForm.INTERFACE;
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            this.webView.GoBack();
        }

        private void forwardBtn_Click(object sender, EventArgs e)
        {
            this.webView.GoForward();
        }

        private void downloadBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.webView.Url.ToString());
        }

        private void webView_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (!this.webView.Url.OriginalString.StartsWith(Constant.SERVICE_ADDRESS))
            {
                this.webView.Navigate(Regex.Replace(this.webView.Url.OriginalString, Constant.IS_USE_HTTPS ? "https://(.*?)/" : "http://(.*?)/", Constant.SERVICE_ADDRESS));
            }
        }



    }
}
