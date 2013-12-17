namespace VideoSearch
{
    partial class ExploreForm
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
            //base.Dispose(disposing);
            base.Hide();
        }
        
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExploreForm));
            this.backBtn = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.urlText = new System.Windows.Forms.TextBox();
            this.downloadBtn = new System.Windows.Forms.PictureBox();
            this.forwardBtn = new System.Windows.Forms.PictureBox();
            this.webView = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.backBtn)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.downloadBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.forwardBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // backBtn
            // 
            this.backBtn.Image = ((System.Drawing.Image)(resources.GetObject("backBtn.Image")));
            this.backBtn.Location = new System.Drawing.Point(18, 0);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(25, 50);
            this.backBtn.TabIndex = 0;
            this.backBtn.TabStop = false;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(119)))), ((int)(((byte)(239)))));
            this.panel1.Controls.Add(this.urlText);
            this.panel1.Controls.Add(this.downloadBtn);
            this.panel1.Controls.Add(this.forwardBtn);
            this.panel1.Controls.Add(this.backBtn);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(676, 50);
            this.panel1.TabIndex = 1;
            // 
            // urlText
            // 
            this.urlText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.urlText.Location = new System.Drawing.Point(86, 15);
            this.urlText.Name = "urlText";
            this.urlText.Size = new System.Drawing.Size(521, 21);
            this.urlText.TabIndex = 3;
            this.urlText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.urlText_KeyDown);
            // 
            // downloadBtn
            // 
            this.downloadBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadBtn.Image = ((System.Drawing.Image)(resources.GetObject("downloadBtn.Image")));
            this.downloadBtn.Location = new System.Drawing.Point(613, 0);
            this.downloadBtn.Name = "downloadBtn";
            this.downloadBtn.Size = new System.Drawing.Size(60, 50);
            this.downloadBtn.TabIndex = 2;
            this.downloadBtn.TabStop = false;
            this.downloadBtn.Click += new System.EventHandler(this.downloadBtn_Click);
            // 
            // forwardBtn
            // 
            this.forwardBtn.Image = ((System.Drawing.Image)(resources.GetObject("forwardBtn.Image")));
            this.forwardBtn.Location = new System.Drawing.Point(49, 0);
            this.forwardBtn.Name = "forwardBtn";
            this.forwardBtn.Size = new System.Drawing.Size(31, 50);
            this.forwardBtn.TabIndex = 1;
            this.forwardBtn.TabStop = false;
            this.forwardBtn.Click += new System.EventHandler(this.forwardBtn_Click);
            // 
            // webView
            // 
            this.webView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webView.Location = new System.Drawing.Point(0, 52);
            this.webView.MinimumSize = new System.Drawing.Size(20, 20);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(676, 361);
            this.webView.TabIndex = 2;
            this.webView.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webView_Navigated);
            // 
            // ExploreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(676, 415);
            this.Controls.Add(this.webView);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExploreForm";
            this.Text = "网页浏览模式";
            ((System.ComponentModel.ISupportInitialize)(this.backBtn)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.downloadBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.forwardBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox backBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox urlText;
        private System.Windows.Forms.PictureBox downloadBtn;
        private System.Windows.Forms.PictureBox forwardBtn;
        private System.Windows.Forms.WebBrowser webView;

    }
}