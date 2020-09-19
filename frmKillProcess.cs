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
        /**
         * on load replace all instances of "[program]" in the form text with the program that needs closed
         */
        private void frmKillBrowser_Load(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder(lblText.Text);
            builder.Replace("[program]", browser);
            lblText.Text = builder.ToString();
        }
        /**
         * tells the software you have closed the program that is being backed up or restored
         */
        private void btnOk_Click(object sender, EventArgs e)
        {
            response = btnResponse.ok;
            this.Close();
        }
        /**
         * closes the program being backed up or restored for the user
         */
        private void btnFStop_Click(object sender, EventArgs e)
        {
            response = btnResponse.fStop;
            this.Close();
        }
        /**
         * tells the code to cancel backing up or restoring this program
         */
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
