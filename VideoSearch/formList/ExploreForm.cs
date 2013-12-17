using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace VideoSearch
{
    public sealed partial class ExploreForm : Form
    {
        private ExploreForm()
        {
            InitializeComponent();
            this.navigate(Constant.SERVICE_ADDRESS + "2010index.html");
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

        private void navigate(object url)
        {
            Thread.Sleep(100);
            this.webViewNavigate((string)url);
        }
        public void navigate(string url)
        {
            new Thread(this.navigate).Start((object)url);
            this.urlText.Text = url;
        }

        private void downloadBtn_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(MainForm.getInterface().analyzeKeyValue);
            th.Start((object)this.webView.Url.OriginalString);
            MainForm.getInterface().Focus();
        }

        private void webView_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (!this.webView.Url.OriginalString.StartsWith(Constant.SERVICE_ADDRESS))
            {
                this.urlText.Text = Regex.Replace(this.webView.Url.OriginalString, Constant.IS_USE_HTTPS ? "https://(.*?)/" : "http://(.*?)/", Constant.SERVICE_ADDRESS);
                this.webView.Navigate(this.urlText.Text);
            }
        }

        private void urlText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.webView.Navigate(Regex.Replace(this.urlText.Text, Constant.IS_USE_HTTPS ? "https://(.*?)/" : "http://(.*?)/", Constant.SERVICE_ADDRESS));
        }

        private delegate void WEBVIEWNAVIGATE(string url);
        private void webViewNavigate(string url)
        {
            if (this.webView.InvokeRequired)
            {
                WEBVIEWNAVIGATE set = new WEBVIEWNAVIGATE(webViewNavigate);
                this.Invoke(set, new object[] { url });
            }
            else
            {
                this.webView.Navigate(url);
            }
        }

    }
}
