namespace VideoSearch
{
    partial class ShareForm
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
            base.Hide();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShareForm));
            this.sourceDirText = new System.Windows.Forms.TextBox();
            this.allowIpText = new System.Windows.Forms.TextBox();
            this.explanationLabel = new System.Windows.Forms.Label();
            this.startService = new System.Windows.Forms.Label();
            this.stopServiceLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sourceDirText
            // 
            this.sourceDirText.BackColor = System.Drawing.Color.White;
            this.sourceDirText.Location = new System.Drawing.Point(121, 112);
            this.sourceDirText.Name = "sourceDirText";
            this.sourceDirText.ReadOnly = true;
            this.sourceDirText.Size = new System.Drawing.Size(314, 21);
            this.sourceDirText.TabIndex = 0;
            this.sourceDirText.Click += new System.EventHandler(this.sourceDirText_Click);
            // 
            // allowIpText
            // 
            this.allowIpText.Location = new System.Drawing.Point(121, 151);
            this.allowIpText.Name = "allowIpText";
            this.allowIpText.Size = new System.Drawing.Size(314, 21);
            this.allowIpText.TabIndex = 1;
            // 
            // explanationLabel
            // 
            this.explanationLabel.AutoSize = true;
            this.explanationLabel.BackColor = System.Drawing.Color.Transparent;
            this.explanationLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.explanationLabel.ForeColor = System.Drawing.Color.White;
            this.explanationLabel.Location = new System.Drawing.Point(44, 213);
            this.explanationLabel.Name = "explanationLabel";
            this.explanationLabel.Size = new System.Drawing.Size(74, 22);
            this.explanationLabel.TabIndex = 2;
            this.explanationLabel.Text = "服务说明";
            this.explanationLabel.Click += new System.EventHandler(this.explanationLabel_Click);
            // 
            // startService
            // 
            this.startService.AutoSize = true;
            this.startService.BackColor = System.Drawing.Color.Transparent;
            this.startService.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.startService.ForeColor = System.Drawing.Color.White;
            this.startService.Location = new System.Drawing.Point(185, 214);
            this.startService.Name = "startService";
            this.startService.Size = new System.Drawing.Size(82, 22);
            this.startService.TabIndex = 3;
            this.startService.Text = "    启动    ";
            this.startService.Click += new System.EventHandler(this.startService_Click);
            // 
            // stopServiceLabel
            // 
            this.stopServiceLabel.AutoSize = true;
            this.stopServiceLabel.BackColor = System.Drawing.Color.Transparent;
            this.stopServiceLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stopServiceLabel.ForeColor = System.Drawing.Color.White;
            this.stopServiceLabel.Location = new System.Drawing.Point(330, 214);
            this.stopServiceLabel.Name = "stopServiceLabel";
            this.stopServiceLabel.Size = new System.Drawing.Size(82, 22);
            this.stopServiceLabel.TabIndex = 4;
            this.stopServiceLabel.Text = "    停止    ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(271, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "*多个资源目录和ip请已 | 分割";
            // 
            // ShareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(451, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stopServiceLabel);
            this.Controls.Add(this.startService);
            this.Controls.Add(this.explanationLabel);
            this.Controls.Add(this.allowIpText);
            this.Controls.Add(this.sourceDirText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ShareForm";
            this.Text = "共享给其他设备";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sourceDirText;
        private System.Windows.Forms.TextBox allowIpText;
        private System.Windows.Forms.Label explanationLabel;
        private System.Windows.Forms.Label startService;
        private System.Windows.Forms.Label stopServiceLabel;
        private System.Windows.Forms.Label label1;
    }
}