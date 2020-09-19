﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KCBackup
{
    public partial class frmRestoreSelect : Form
    {
        string root;
        FormBackup callingForm;
        public static string currentlySelected;
        public frmRestoreSelect(string rootFolder, FormBackup calling)
        {
            InitializeComponent();
            root = rootFolder;
            callingForm = calling;
            currentlySelected = "";
        }
        /**
         * when the form loads, populates the combobox with all the backups in the current parent backup directory,
         * starts the selection at the most recent backup
         */
        private void RestoreSelect_Load(object sender, EventArgs e)
        {
            string[] backups = Directory.GetDirectories(root);
            FileMethods.convertPathToFolder(backups);
            for (int i = backups.Length - 1; i >= 0; i--)
            {
                if (backups[i].StartsWith("\\backup") && Regex.Replace(backups[i], @"[^\d]", "") != "")
                {
                    cmbSelect.Items.Add(backups[i].Substring(1));
                }
            }
            if (cmbSelect.Items.Count > 0)
            {
                currentlySelected = cmbSelect.GetItemText(cmbSelect.Items[0]);
                cmbSelect.SelectedIndex = 0;
            }
            else
            {
                currentlySelected = null;
                this.Hide();
                callingForm.Show();
                this.Close();
            }
        }
        /**
         * when the index is changed, switch the currently selected path string and displays the date and time the newly selected backup
         * was created
         */
        private void cmbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentlySelected = cmbSelect.GetItemText(cmbSelect.Items[cmbSelect.SelectedIndex]);
            lblDate.Text = Directory.GetCreationTime(Path.Combine(root, currentlySelected)).ToString("MM-dd-yyyy hh:mmtt");
        }
        /**
         * confirms the backup selection displayed in the combobox
         */
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Hide();
            callingForm.Show();
            this.Close();
        }
    }
}
