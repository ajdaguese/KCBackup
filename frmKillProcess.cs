using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KCBackup
{
    public partial class frmKillProcess : Form
    {
        string browser;
        public static btnResponse response;
        public frmKillProcess(string browser)
        {
            InitializeComponent();
            this.browser = browser;
            //response is automatically set to cancel, that way if they close this string with the x, it will be the same as clicking cancel
            response = btnResponse.cancel;
        }

        private void frmKillBrowser_Load(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder(lblText.Text);
            builder.Replace("[browser]", browser);
            lblText.Text = builder.ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            response = btnResponse.ok;
            this.Close();
        }

        private void btnFStop_Click(object sender, EventArgs e)
        {
            response = btnResponse.fStop;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            response = btnResponse.cancel;
            this.Close();
        }
    }
    public enum btnResponse
    {
        cancel,
        fStop,
        ok
    }
}
