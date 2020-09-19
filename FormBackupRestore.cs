using System;
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
    public partial class FormBackupRestore : Form
    {
        public static string strLocation = "";

        public FormBackupRestore(bool bolMode)
        {
            InitializeComponent();

            //Set the initial DialogResult to Cancel in case close button or "X" is clicked
            this.DialogResult = DialogResult.Cancel;

            #region Initialization by mode

            if (bolMode)//It's in Load Restore mode
            {
                this.Text = "Choose backup to be restored";
            }
            else//It's in backup mode
            {
                this.Text = "Choose location to save the backup";
            }

            #endregion Initialization by mode
        }
        /**
         * brings up a browse screen to browse for a backup location folder
         */
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //IF run in Backup mode
            if (this.Text.EndsWith("backup"))
            {
                //Run folder browser
                FolderBrowserDialog fbdBrowse = new FolderBrowserDialog();

                if (fbdBrowse.ShowDialog() == DialogResult.OK)
                {
                    txtPathFile.Text = fbdBrowse.SelectedPath;
                }
            }
            else//Run in Restore mode
            {
                //Run file browser instead
                OpenFileDialog ofdBrowse = new OpenFileDialog();

                //Set parameters for file search
                ofdBrowse.Filter = "Restore|*.rstr";

                if (ofdBrowse.ShowDialog() == DialogResult.OK)
                {
                    txtPathFile.Text = ofdBrowse.FileName;
                }
            }

        }
        /**
         * confirms the location chosen from browse
         */
        private void btnOK_Click(object sender, EventArgs e)
        {
            //Change DialogResult and close
            strLocation = txtPathFile.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        /**
         * cancels the backup or restore operation and closes this form
         */
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //DialogResult already set, just close
            this.Close();
        }

        //Ensure the OK button cannot be clicked if the textbox is empty
        private void txtPathFile_TextChanged(object sender, EventArgs e)
        {
            if (txtPathFile.Text != null)
            {
                btnOK.Enabled = true;
            }
            else
            {
                btnOK.Enabled = false;
            }
        }

    }

}
