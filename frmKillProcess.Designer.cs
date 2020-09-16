namespace KCBackup
{
    partial class frmKillProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKillProcess));
            this.btnOk = new System.Windows.Forms.Button();
            this.lblText = new System.Windows.Forms.Label();
            this.btnFStop = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(13, 221);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblText
            // 
            this.lblText.Location = new System.Drawing.Point(12, 9);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(427, 180);
            this.lblText.TabIndex = 1;
            this.lblText.Text = resources.GetString("lblText.Text");
            // 
            // btnFStop
            // 
            this.btnFStop.Location = new System.Drawing.Point(187, 221);
            this.btnFStop.Name = "btnFStop";
            this.btnFStop.Size = new System.Drawing.Size(75, 23);
            this.btnFStop.TabIndex = 2;
            this.btnFStop.Text = "Force Stop";
            this.btnFStop.UseVisualStyleBackColor = true;
            this.btnFStop.Click += new System.EventHandler(this.btnFStop_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(364, 221);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmKillBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 256);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFStop);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.btnOk);
            this.Name = "frmKillBrowser";
            this.Text = "frmKillBrowser";
            this.Load += new System.EventHandler(this.frmKillBrowser_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Button btnFStop;
        private System.Windows.Forms.Button btnCancel;
    }
}