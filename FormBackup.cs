using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.CodeDom;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Threading;

namespace KCBackup
{
    public partial class FormBackup : Form
    {
        public static string backupRoot = "..\\backups";
        //the config file is a public static because some of the more specialized child classes (I.E firefox) need access to it.
        public static readonly string restoreFile = "\\config.con";
        //this file is used to indicate a root backup directory
        public static readonly string indicatorFile = "kcbackup.RSTR";
        public static string currentBackup = "\\backup0001";
        private int extraUserFoldersEndIndex = 0;
        private bool restoreOnlyMode = false;
        public FormBackup()
        {
            InitializeComponent();

            mthdScanPrograms();
        }

        #region User controls

        private void chkAllNone_Click(object sender, EventArgs e)
        {
            //If the All/None checkbox is changed check or uncheck all things under it accordingly
            chkDesktop.Checked = chkUserFiles.Checked && chkDesktop.Enabled;
            chkDocuments.Checked = chkUserFiles.Checked && chkDocuments.Enabled;
            chkDownloads.Checked = chkUserFiles.Checked && chkDownloads.Enabled;
            chkMusic.Checked = chkUserFiles.Checked && chkMusic.Enabled;
            chkPictures.Checked = chkUserFiles.Checked && chkPictures.Enabled;
            chkVideos.Checked = chkUserFiles.Checked && chkVideos.Enabled;

            //Check if chkAllNone is checked because we don't want to automatically check Favorites
            if (!chkUserFiles.Checked)
            {
                chkFavorites.Checked = false;
            }
        }

        private void chkAllNoneCommon_Click(object sender, EventArgs e)
        {
            //If the All/None checkbox is changed check or uncheck all things under it accordingly
            chkCommonDesktop.Checked = chkCommonUserFiles.Checked && chkCommonDesktop.Enabled;
            chkCommonDocuments.Checked = chkCommonUserFiles.Checked && chkCommonDocuments.Enabled;
            chkCommonDownloads.Checked = chkCommonUserFiles.Checked && chkCommonDownloads.Enabled;
            chkCommonMusic.Checked = chkCommonUserFiles.Checked && chkCommonMusic.Enabled;
            chkCommonPictures.Checked = chkCommonUserFiles.Checked && chkCommonPictures.Enabled;
            chkCommonVideos.Checked = chkCommonUserFiles.Checked && chkCommonVideos.Enabled;
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            //Call Backup/Restore form as Backup
            Form frmBackupRestore = new FormBackupRestore(false);
            DialogResult dlgResult = frmBackupRestore.ShowDialog();

            //If OK clicked to into Backup mode
            //XXXXX NOTE: When started the backup will make a new subdirectory in the chosen
            //directory.  The backup file will be put directly there, all other files will
            //be put in their own subdirectories under that.
            if (dlgResult == DialogResult.OK)
            {
                //Change the mode label
                lblMode.Text = "BACKUP";

                //Set path location
                txtLocation.Text = FormBackupRestore.strLocation;
                backupRoot = txtLocation.Text;
                //Disable the Backup and Restore buttons
                btnBackup.Enabled = false;
                btnLoadRestore.Enabled = false;
                //Enable the Mode Cancel button
                btnModeCancel.Enabled = true;

                //Enable Start button
                btnStartProcess.Enabled = true;

                //Enable the Browse button
                btnBrowse.Enabled = true;

                //Enable the refresh button
                btnRefresh.Enabled = true;
                checkExtraUserFolders();
                chkRestoreOnly.Enabled = true;
            }
        }

        private void btnLoadRestore_Click(object sender, EventArgs e)
        {

            //Call Backup/Restore form as Restore
            FormBackupRestore frmBackupRestore = new FormBackupRestore(true);
            DialogResult dlgResult = frmBackupRestore.ShowDialog();

            //Set mode to Restore
            //XXXXX NOTE: Need code here to get the text from the textbox on the Backup/Restore form.
            //Also need to do checks on the restore file to ensure that the backed up files match the
            //restore file.
            if (dlgResult == DialogResult.OK)
            {
                //Change the mode label
                lblMode.Text = "RESTORE";

                //Set location path
                txtLocation.Text = Path.GetDirectoryName(FormBackupRestore.strLocation);

                //Disable the Backup and Restore buttons
                btnBackup.Enabled = false;
                btnLoadRestore.Enabled = false;
                //Enbale the Cancel button
                btnModeCancel.Enabled = true;

                //Enable Start button
                btnStartProcess.Enabled = true;

                //Disable the Browse button
                btnBrowse.Enabled = false;

                //Enable the refresh button
                btnRefresh.Enabled = true;
                //txtLocation.Text = frmBackupRestore.strReturnValue;

                backupRoot = txtLocation.Text;

                frmRestoreSelect sel = new frmRestoreSelect(backupRoot, this);
                sel.ShowDialog();
                if (frmRestoreSelect.currentlySelected == null)
                {
                    MessageBox.Show("There are no backups from which to choose");
                    setButtonState(true);
                    return;
                }
                else
                {
                    currentBackup = "\\" + frmRestoreSelect.currentlySelected;
                }

                setCheckBoxState(false);
                readConfig();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdBrowse = new FolderBrowserDialog();

            if (fbdBrowse.ShowDialog() == DialogResult.OK)
            {
                //Bool to see if the item is a duplicate
                bool bolDuplicate = false;

                //Iterate through current list items to see if the path is already there
                foreach (string strItem in lstCustom.Items)
                {
                    //Check if it's a duplicate
                    if (strItem == fbdBrowse.SelectedPath)
                    {
                        //Set the duplicante bool to true
                        bolDuplicate = true;
                    }
                }

                //Add the path to the list if it's not a duplicate
                if (!bolDuplicate)
                {
                    lstCustom.Items.Add(fbdBrowse.SelectedPath);
                    btnRemoveAll.Enabled = true;
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            lstCustom.Items.RemoveAt(lstCustom.SelectedIndex);
            //if the selected index is less than the end index for the extra user files,
            //one will be delted so the end index must be less
            if (lstCustom.SelectedIndex < extraUserFoldersEndIndex)
            {
                extraUserFoldersEndIndex--;
            }
            if (lstCustom.Items.Count < 1)
            {
                btnRemoveAll.Enabled = false;
            }
        }
        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            btnRemove.Enabled = false;
            btnRemoveAll.Enabled = false;
            lstCustom.Items.Clear();
            extraUserFoldersEndIndex = 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRemove.Enabled = false;
            btnRemoveAll.Enabled = false;
            if (lblMode.Text == "BACKUP")
            {
                checkExtraUserFolders();
            }
            else if (lblMode.Text == "RESTORE")
            {
                readConfig();
            }
        }

        private void lstCustom_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check if something is selected
            if (lstCustom.SelectedIndex > -1)
            {
                //Enable the Remove button
                btnRemove.Enabled = true;
            }
            else
            {
                //Disable the Remove button
                btnRemove.Enabled = false;
            }
        }

        private void btnModeCancel_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void reset()
        {
            //Change the Mode label to NONE
            lblMode.Text = "NONE";

            //Clear the textbox
            txtLocation.Text = "";

            //Clear the list of all items
            lstCustom.Items.Clear();
            //disables the remove button, otherwise clicking on it will throw an error after a clear
            btnRemove.Enabled = false;
            btnRemoveAll.Enabled = false;
            //resets the extraUserFoldersEndIndex to 0 because the list was cleared
            extraUserFoldersEndIndex = 0;
            //Disable the Cancel button
            btnModeCancel.Enabled = false;
            //Enable Backup and Restore buttons
            btnBackup.Enabled = true;
            btnLoadRestore.Enabled = true;

            //Disable Start button
            btnStartProcess.Enabled = false;

            //Disable the Browse button
            btnBrowse.Enabled = false;

            //Disable the refresh button
            btnRefresh.Enabled = false;

            //Disable the Restore Only button
            chkRestoreOnly.Enabled = false;
            chkRestoreOnly.Checked = false;

            //Enable rescan button
            btnRescan.Enabled = true;

            //Reenable checkboxes
            setCheckBoxState(true);
            //Rescan programs
            mthdScanPrograms();
        }

        private void btnRescan_Click(object sender, EventArgs e)
        {
            mthdScanPrograms();
        }

        #endregion User controls

        //Method to set up initial and "rescan" settings DONE
        private void mthdScanPrograms()
        {

            //Check the individual programs and see if their directories exist.  Enable, check, disable and uncheck as necessary
            #region Programs

            //Check if Firefox exists
            if (Directory.Exists(Path.Combine(KnownFolders.GetPath(KnownFolder.RoamingAppData), @"Mozilla\Firefox")))
            {
                //Check the checkbox if it was disabled before
                if (!chkFirefox.Enabled)
                {
                    chkFirefox.Checked = true;
                }
                //Firefox exists, enable the checkbox
                chkFirefox.Enabled = true;
            }
            else
            {
                //Firefox does not exist, uncheck and disable Firefox checkbox
                chkFirefox.Enabled = false;
                chkFirefox.Checked = false;
            }

            //Check if Chrome esists
            if (Directory.Exists(Path.Combine(KnownFolders.GetPath(KnownFolder.LocalAppData), @"Google\Chrome")))
            {
                //Check the checkbox if it was disabled before
                if (!chkChrome.Enabled)
                {
                    chkChrome.Checked = true;
                }
                //Chrome exists, enable the checkbox
                chkChrome.Enabled = true;
            }
            else
            {
                //Chrome does not exist, uncheck and disable Firefox checkbox
                chkChrome.Enabled = false;
                chkChrome.Checked = false;
            }

            //Check if Thunderbird esists
            if (Directory.Exists(Path.Combine(KnownFolders.GetPath(KnownFolder.LocalAppData), @"Thunderbird")))
            {
                //Check the checkbox if it was disabled before
                if (!chkThunderbird.Enabled)
                {
                    chkThunderbird.Checked = true;
                }
                //Thunderbird exists, enable the checkbox
                chkThunderbird.Enabled = true;
            }
            else
            {
                //Thunderbird does not exist, uncheck and disable Firefox checkbox
                chkThunderbird.Enabled = false;
                chkThunderbird.Checked = false;
            }

            //Removed - Not providing support for outdated, extinct software
            ////Check if Windows Live Mail esists
            //if (Directory.Exists(Path.Combine(KnownFolders.GetPath(KnownFolder.LocalAppData), @"Microsoft\Windows Live Mail")))
            //{
            //    //Check the checkbox if it was disabled before
            //    if (!chkWindowsLiveMail.Enabled)
            //    {
            //        chkWindowsLiveMail.Checked = true;
            //    }
            //    //Windows Live Mail exists, enable the checkbox
            //    chkWindowsLiveMail.Enabled = true;
            //}
            //else
            //{
            //    //Windows Live Mail does not exist, uncheck and disable Firefox checkbox
            //    chkWindowsLiveMail.Enabled = false;
            //    chkWindowsLiveMail.Checked = false;
            //}

            #endregion Programs

            //This will iterate through the folders in the list box and remove anything which no longer exists
            #region Folders list

            //List to hold erroneous folder names
            List<string> lstCheck = new List<string>();

            //Check for folders which no longer exist
            foreach (string strItem in lstCustom.Items)
            {
                //Check if the directory exists
                if (!Directory.Exists(strItem))
                {
                    lstCheck.Add(strItem);
                }
            }

            //Iterate through the folders which no longer exist and remove them from the listbox
            foreach (string strItem2 in lstCheck)
            {
                //Remove each item from the listbox
                lstCustom.Items.Remove(strItem2);
            }

            #endregion Foldres list

        }

        private void btnStartProcess_Click(object sender, EventArgs e)
        {
            //Set current backup/restore location
            //     
            lblBackupNotice.Text = "Backing up/Restoring! Please wait!";
            lblBackupNotice.Refresh();
            if (lblMode.Text == "BACKUP")
            {
                backup();
            }
            else if (lblMode.Text == "RESTORE")
            {
                restore();
            }
            lblBackupNotice.Text = "";
            reset();
        }
        /**
         * Runs a backup of the checked programs on the form as well as all the folders in the extra folders list.
         * backs the files up to a directory backupXXXX in the chosen backup location where the Xs are a number
         * representing the number of the backup
         */
        private void backup()
        {
            //disable the controls so that nothing can be changed or repeated during runtime
            setButtonState(false);
            if (!File.Exists(Path.Combine(backupRoot, indicatorFile)))
            {
                //creates the indicator file so that when you go to restore you know this is a valid root location
                //to choose from
                string temp = Path.Combine(backupRoot, indicatorFile);
                File.Create(Path.Combine(backupRoot, indicatorFile)).Close();
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fVI = FileVersionInfo.GetVersionInfo(assembly.Location);
                FileStream fs = new FileStream(Path.Combine(backupRoot, indicatorFile), FileMode.Append);
                byte[] write = Encoding.Default.GetBytes("Version=" + fVI.ProductVersion + "\n");
                fs.Write(write, 0, write.Length);
                fs.Flush();
                fs.Close();
            }
            else
            {
                string[] backups = Directory.GetDirectories(backupRoot);
                //converts the entire path to just the highest up folder name
                FileMethods.convertPathToFolder(backups);
                for (int i = backups.Length - 1; i >= 0; i--)
                {
                    if (backups[i].StartsWith("\\backup") && Regex.Replace(backups[i], @"[^\d]", "") != "")
                    {
                        int lastNumber = Int32.Parse(Regex.Replace(backups[i], @"[^\d]", ""));
                        string currNumber = (lastNumber + 1).ToString();
                        for (int j = currNumber.Length; j < 4; j++)
                        {
                            currNumber = '0' + currNumber;
                        }
                        currentBackup = "\\backup" + currNumber;
                        break;
                    }
                }
            }
            //a boolean indicating if the current restore file has the readonly variable set
            bool foundReadOnly = false;
            string[] arrLine = File.ReadAllLines(Path.Combine(backupRoot, indicatorFile));
            for (int i = 0; i < arrLine.Length; i++)
            {
                string[] splitLine = arrLine[i].Split('=');
                if (splitLine[0] == "RESTOREONLY")
                {
                    foundReadOnly = true;
                    if (splitLine[1] == "TRUE")
                    {
                        if (chkRestoreOnly.Checked)
                        {
                            DialogResult dialogResult = MessageBox.Show("This backup directory is already set to restore only. If you continue, the program will automatically restore the backup you are trying to create now when opened in restore only mode, and not the already created backup. Would you like to continue?", "Continue backup?", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                changeIndicatorROBackup(arrLine, i);
                            }
                            else
                            {
                                reset();
                                return;
                            }
                        }
                        else
                        {
                            DialogResult dialogResult = MessageBox.Show("This backup location is set as restore only. Would you like to change this backup location to no longer be restore only? If you select no, a new backup will be made in this location, but the restore only mode will still restore the backup it is set to.", "Keep backup restore only?", MessageBoxButtons.YesNoCancel);
                            if (dialogResult == DialogResult.Yes)
                            {
                                splitLine[1] = "FALSE";
                                arrLine[i] = string.Join("=", splitLine);
                            }
                            else if (dialogResult == DialogResult.Cancel)
                            {
                                reset();
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (chkRestoreOnly.Checked)
                        {
                            splitLine[1] = "TRUE";
                            arrLine[i] = string.Join("=", splitLine);
                            changeIndicatorROBackup(arrLine, i);
                        }
                    }
                    //arrLine[i] = string.Join("=", splitLine);
                    File.WriteAllLines(Path.Combine(backupRoot, indicatorFile), arrLine);
                    break;
                }
            }
            if (!foundReadOnly)
            {
                FileStream fs = new FileStream(Path.Combine(backupRoot, indicatorFile), FileMode.Append);
                byte[] configWrite;
                if (chkRestoreOnly.Checked)
                {
                    configWrite = Encoding.Default.GetBytes("RESTOREONLY=TRUE\nRESTOREONLYFOLDER=" + currentBackup + "\n");
                }
                else
                {
                    configWrite = Encoding.Default.GetBytes("RESTOREONLY=FALSE");
                }
                fs.Write(configWrite, 0, configWrite.Length);
                fs.Flush();
                fs.Close();
            }

            List<BackupLocation> backupList = constructList();
            long backupSize = 0;
            foreach(BackupLocation bl in backupList)
            {
                backupSize += bl.getSize();
            }
            //creates a DriveInfo for the backup location's drive
            DriveInfo di = new DriveInfo(Path.GetPathRoot(backupRoot));
            if (backupSize <= di.AvailableFreeSpace)
            {
                foreach (BackupLocation bl in backupList)
                {
                    bl.specialActions();
                    //Must convert "backupRoot" from a relative path, otherwise adding "\\?\" later does not work
                    bl.backup(Path.GetFullPath(backupRoot) + currentBackup);
                    bl.writeToConfig(Path.GetFullPath(backupRoot) + currentBackup + restoreFile);
                }
                if (chkRestoreOnly.Checked)
                {
                    //if the exe already exists, delete it so you can replace it with the current exe
                    if (File.Exists(Path.Combine(backupRoot, "KCBackup.exe")))
                    {
                        File.Delete(Path.Combine(backupRoot, "KCBackup.exe"));
                    }
                    File.Copy(".\\KCBackup.exe", Path.Combine(backupRoot, "KCBackup.exe"));
                }
                MessageBox.Show("BACKUP COMPLETE");
            }
            else
            {
                MessageBox.Show("There is not enough free space on the selected drive to create this backup");
            }
            //reset program to start state
            reset();
        }
        /**
         * changes the Restore Only backup location in the indicator file
         */
        private void changeIndicatorROBackup(string[] lines, int restoreOnlyIndex)
        {
            bool foundRestoreOnlyFolder = false;
            for (int j = 0; j < lines.Length; j++)
            {
                string[] splitLineJLoop = lines[j].Split('=');
                if (splitLineJLoop[0] == "RESTOREONLYFOLDER")
                {
                    foundRestoreOnlyFolder = true;
                    splitLineJLoop[1] = currentBackup;
                    lines[j] = string.Join("=", splitLineJLoop);
                    break;
                }
            }
            if (!foundRestoreOnlyFolder)
            {
                lines[restoreOnlyIndex] += ("\nRESTOREONLYFOLDER=" + currentBackup);
            }
        }
        /**
         * adds all the extra directories in the user folder to the displayed list on the form
         */
        private void checkExtraUserFolders()//List<BackupLocation> backupList)
        {
            List<string> commonFolders = new List<string>();
            //Change the next 3 to anything that starts with "."
            commonFolders.Add("3D Objects");//
            commonFolders.Add("AppData");
            commonFolders.Add("Apple");//
            commonFolders.Add("Application Data");//
            commonFolders.Add("Contacts");//
            commonFolders.Add("Cookies");//
            commonFolders.Add("Desktop");
            commonFolders.Add("Documents");
            commonFolders.Add("Downloads");
            commonFolders.Add("Favorites");
            commonFolders.Add("IntelGraphicsProfiles");//
            commonFolders.Add("Links");//
            commonFolders.Add("Local Settings");//
            commonFolders.Add("MicrosoftEdgeBackups");//
            commonFolders.Add("Music");
            commonFolders.Add("My Documents");
            commonFolders.Add("NetHood");//
            commonFolders.Add("OneDrive");
            commonFolders.Add("Pictures");
            commonFolders.Add("PrintHood");//
            commonFolders.Add("Recent");//
            commonFolders.Add("Saved Games");//
            commonFolders.Add("Searches");//
            commonFolders.Add("SendTo");//
            commonFolders.Add("source");//
            commonFolders.Add("Start Menu");
            commonFolders.Add("Templates");//
            commonFolders.Add("Videos");
            foreach (string dir in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToString()))
            {
                string[] split = dir.Split('\\');
                if (!(commonFolders.Contains(split[split.Length - 1])) && !(split[split.Length - 1][0] == '.'))
                {
                    bool shouldAdd = true;
                    foreach (string s in lstCustom.Items)
                    {
                        if (dir == s)
                        {
                            shouldAdd = false;
                        }
                    }
                    if (shouldAdd)
                    {
                        lstCustom.Items.Add(dir);
                    }
                    btnRemoveAll.Enabled = true;
                }
            }
            extraUserFoldersEndIndex = lstCustom.Items.Count;
        }
        /**
         * Restores the checked and listed options on the form to the location specified in the configuration file
         */
        private void restore()
        {
            setButtonState(false);

            List<BackupLocation> restoreList = constructList();
            Console.WriteLine(restoreList.Count);
            if (restoreList == null)
            {
                MessageBox.Show("There was an issue with the restore, The configuration file may be missing or there may not yet be a backup. Skipping restore.");
                setButtonState(true);
                return;
            }
            foreach (BackupLocation restore in restoreList)
            {
                restore.restore(Path.GetFullPath(backupRoot) + currentBackup);
            }
            MessageBox.Show("Restore Complete");
            setButtonState(true);
            if (restoreOnlyMode)
            {
                this.Close();
            }
        }
        /**
         * constructs the list of BackupLocation objects to be either backed up or restored
         */
        private List<BackupLocation> constructList()
        {
            List<BackupLocation> backupList = new List<BackupLocation>();
            backupList.Add(new Desktop("Desktop", "%desktop%", chkDesktop.Checked));
            backupList.Add(new UserFiles("Documents", "%documents%", chkDocuments.Checked));
            backupList.Add(new UserFiles("Downloads", "%downloads%", chkDownloads.Checked));
            backupList.Add(new UserFiles("Favorites", "%favorites%", chkFavorites.Checked));
            backupList.Add(new UserFiles("Music", "%music%", chkMusic.Checked));
            backupList.Add(new UserFiles("Pictures", "%pictures%", chkPictures.Checked));
            backupList.Add(new UserFiles("Videos", "%videos%", chkVideos.Checked));
            backupList.Add(new PublicDesktop("PublicDesktop", "%publicdesktop%", chkCommonDesktop.Checked));
            backupList.Add(new PublicUserFiles("PublicDocuments", "%publicdocuments%", chkCommonDocuments.Checked));
            backupList.Add(new PublicUserFiles("PublicDownloads", "%publicdownloads%", chkCommonDownloads.Checked));
            backupList.Add(new PublicUserFiles("PublicMusic", "%publicmusic%", chkCommonMusic.Checked));
            backupList.Add(new PublicUserFiles("PublicPictures", "%publicpictures%", chkCommonPictures.Checked));
            backupList.Add(new PublicUserFiles("PublicVideos", "%publicvideos%", (chkCommonVideos.Checked)));
            backupList.Add(new FirefoxLocation("Firefox", "%AppDataRoaming%\\Mozilla\\Firefox\\Profiles", chkFirefox.Checked, "Firefox"));
            backupList.Add(new ChromeLocation("Chrome", "%appdatalocal%\\Google\\Chrome", chkChrome.Checked, "Chrome"));
            int i = 0;
            //construct a list of the folders to get their name without path. Since
            //the list box uses objects, not strings we have to loop through each entry
            //and convert it to a string before storing it in the string array. After
            //that I just use my covertPathToFolder method to just get the folder name
            string[] extraFolderNames = new string[lstCustom.Items.Count];
            for (i = 0; i < lstCustom.Items.Count; i++)
            {
                extraFolderNames[i] = lstCustom.Items[i].ToString();
            }
            FileMethods.convertPathToFolder(extraFolderNames);
            //loop through the items in lstCustom adding ExtraUserFolders up until we
            //reach the extraUserFoldersEndIndex which indicates the end of the ExtraUserFolders
            //and the beginning of the StrayFolders. Then continue through the rest of 
            //the list adding them as stray folders.
            for (i = 0; i < extraUserFoldersEndIndex; i++)
            {
                backupList.Add(new ExtraUserFolders(extraFolderNames[i], lstCustom.Items[i].ToString(), true));
            }
            //I have to use i = i because the first part of a for loop must have an assignment
            for (i = i; i < lstCustom.Items.Count; i++)
            {
                backupList.Add(new StrayFolder(extraFolderNames[i], lstCustom.Items[i].ToString(), true));
            }
            return backupList;
        }
        /**
         * Reads the configuration file and limits the users restore options to only directories that are in the current backup
         */
        private void readConfig()
        {
            string line = "";
            string name = "";
            string type = "";
            string processName = "";
            string directory = "";
            using (StreamReader file = new StreamReader(backupRoot + currentBackup + restoreFile))
            {
                line = file.ReadLine();
                if (line != null)
                {
                    name = line.Substring(1);
                }
                line = file.ReadLine();
                while (line != null)
                {
                    while (!line.StartsWith("+"))
                    {
                        string[] configSplit = line.Split('=');
                        switch (configSplit[0])
                        {
                            case "-PROCESSNAME":
                                //just in case something else in the variables had an '='
                                //we join everything past the first part of the split with '='
                                processName = string.Join("=", configSplit, 1, configSplit.Length - 1);
                                break;
                            case "-DIRECTORY":
                                directory = string.Join("=", configSplit, 1, configSplit.Length - 1);
                                break;
                            case "-TYPE":
                                type = string.Join("=", configSplit, 1, configSplit.Length - 1);
                                break;
                        }
                        line = file.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                    }
                    switch (type)
                    {
                        case "userfiles":
                            switch (name)
                            {
                                case "Documents":
                                    chkDocuments.Enabled = true;
                                    chkDocuments.Checked = true;
                                    break;
                                case "Downloads":
                                    chkDownloads.Enabled = true;
                                    chkDownloads.Checked = true;
                                    break;
                                case "Favorites":
                                    chkFavorites.Enabled = true;
                                    chkFavorites.Checked = true;
                                    break;
                                case "Music":
                                    chkMusic.Enabled = true;
                                    chkMusic.Checked = true;
                                    break;
                                case "Pictures":
                                    chkPictures.Enabled = true;
                                    chkPictures.Checked = true;
                                    break;
                                case "Videos":
                                    chkVideos.Enabled = true;
                                    chkPictures.Checked = true;
                                    break;
                            }
                            break;
                        case "programfiles":
                            //a switch statement that gets the name of the program
                            //and if enables the correct checkbox
                            break;
                        case "firefox":
                            chkFirefox.Enabled = true;
                            chkFirefox.Checked = true;
                            break;
                        case "chrome":
                            chkChrome.Enabled = true;
                            chkChrome.Checked = true;
                            break;
                        case "extrauserfiles":
                            lstCustom.Items.Insert(extraUserFoldersEndIndex, directory);
                            btnRemoveAll.Enabled = true;
                            extraUserFoldersEndIndex++;
                            break;
                        case "desktop":
                            chkDesktop.Enabled = true;
                            chkDesktop.Checked = true;
                            break;
                        case "publicdesktop":
                            chkCommonDesktop.Enabled = true;
                            chkCommonDesktop.Checked = true;
                            break;
                        case "strayfolder":
                            lstCustom.Items.Insert(extraUserFoldersEndIndex, directory);
                            btnRemoveAll.Enabled = true;
                            break;
                        case "publicuserfiles":
                            switch (name)
                            {
                                case "PublicDocuments":
                                    chkCommonDocuments.Enabled = true;
                                    chkCommonDocuments.Checked = true;
                                    break;
                                case "PublicDownloads":
                                    chkCommonDownloads.Enabled = true;
                                    chkCommonDownloads.Checked = true;
                                    break;
                                case "PublicMusic":
                                    chkCommonMusic.Enabled = true;
                                    chkCommonMusic.Checked = true;
                                    break;
                                case "PublicPictures":
                                    chkCommonPictures.Enabled = true;
                                    chkCommonPictures.Checked = true;
                                    break;
                                case "PublicVideos":
                                    chkCommonVideos.Enabled = true;
                                    chkCommonPictures.Checked = true;
                                    break;
                            }
                            break;
                    }
                    if (line != null)
                    {
                        name = line.Substring(1);
                    }
                    line = file.ReadLine();
                }
            }
        }
        /**
         * sets all buttons to the bool state
         */
        private void setButtonState(bool state)
        {
            btnLoadRestore.Enabled = state;
            btnBackup.Enabled = state;
            btnModeCancel.Enabled = state;
            btnRemove.Enabled = state;
            btnRescan.Enabled = state;
            btnStartProcess.Enabled = state;
            btnBrowse.Enabled = state;
            btnRemoveAll.Enabled = state;
            btnRefresh.Enabled = state;
        }
        /**
         * sets all checkboxes to the bool state
         */
        private void setCheckBoxState(bool state)
        {

            chkVideos.Enabled = state;
            chkThunderbird.Enabled = state;
            chkPictures.Enabled = state;
            chkMusic.Enabled = state;
            chkFirefox.Enabled = state;
            chkFavorites.Enabled = state;
            chkDownloads.Enabled = state;
            chkDocuments.Enabled = state;
            chkDesktop.Enabled = state;
            chkCommonVideos.Enabled = state;
            chkCommonPictures.Enabled = state;
            chkCommonMusic.Enabled = state;
            chkCommonDownloads.Enabled = state;
            chkCommonDocuments.Enabled = state;
            chkCommonDesktop.Enabled = state;
            chkChrome.Enabled = state;
            chkUserFiles.Checked = state;
            chkVideos.Checked = state;
            chkThunderbird.Checked = state;
            chkPictures.Checked = state;
            chkMusic.Checked = state;
            chkFirefox.Checked = state;
            chkFavorites.Checked = false;
            chkDownloads.Checked = state;
            chkDocuments.Checked = state;
            chkDesktop.Checked = state;
            chkCommonUserFiles.Checked = state;
            chkCommonVideos.Checked = state;
            chkCommonPictures.Checked = state;
            chkCommonMusic.Checked = state;
            chkCommonDownloads.Checked = state;
            chkCommonDocuments.Checked = state;
            chkCommonDesktop.Checked = state;
            chkChrome.Checked = state;
        }

        /** 
         * on a load check if this run of the program is in 'restore only' mode and sets variables accordingly
         */
        private void FormBackup_Load(object sender, EventArgs e)
        {
            bool restoreOnly = false;
            string backupFolder = null;
            if (File.Exists(Path.Combine(".", indicatorFile)))
            {
                string[] arrLine = File.ReadAllLines(Path.Combine(".", indicatorFile));
                for (int i = 0; i < arrLine.Length; i++)
                {
                    string[] splitLine = arrLine[i].Split('=');
                    if (splitLine[0] == "RESTOREONLY")
                    {
                        if (splitLine[1] == "TRUE")
                        {
                            restoreOnly = true;
                        }
                    }
                    else if (splitLine[0] == "RESTOREONLYFOLDER")
                    {
                        backupFolder = splitLine[1];
                    }
                }
                if (restoreOnly && !(backupFolder == null))
                {
                    //Change the mode label
                    lblMode.Text = "RESTORE";

                    //set the restoreOnlyMode variable to true
                    restoreOnlyMode = true;

                    //Hide buttons not requried to start the restore
                    btnBackup.Visible = false;
                    btnLoadRestore.Visible = false;
                    btnModeCancel.Visible = false;
                    btnBrowse.Visible = false;

                    //Enable Start button
                    btnStartProcess.Enabled = true;

                    //Enable the refresh button
                    btnRefresh.Enabled = true;

                    backupRoot = ".";
                    currentBackup = backupFolder;
                    setCheckBoxState(false);
                    readConfig();
                }
            }
        }
    }
}
