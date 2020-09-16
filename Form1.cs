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

namespace KCBackup
{
    public partial class frmBackup : Form
    {
        public static readonly string backupRoot = "..\\backups";
        //the config file is a public static because some of the more specialized child classes (I.E firefox) need access to it.
        public static readonly string restoreFile = "\\config.ini";

        public static string currentBackup = "\\backup0001";
        public frmBackup()
        {
            InitializeComponent();

            //Put version in title
            this.Text += Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void chkStandard_CheckedChanged(object sender, EventArgs e)
        {
            chkDesktop.Enabled = chkStandard.Checked;
            chkDocuments.Enabled = chkStandard.Checked;
            chkDownloads.Enabled = chkStandard.Checked;
            chkFavorites.Enabled = chkStandard.Checked;
            chkMusic.Enabled = chkStandard.Checked;
            chkPictures.Enabled = chkStandard.Checked;
            chkVideos.Enabled = chkStandard.Checked;
        }


        private void Backup(object sender, EventArgs e)
        {
            //disable the controls so that nothing can be changed or repeated during runtime
            setButtonState(false);
            if(!Directory.Exists(backupRoot))
            {
                Directory.CreateDirectory(backupRoot);
                //current backup will most likely be this file anyway
                currentBackup = "\\backup0001";
            }
            else
            {
                string[] backups = Directory.GetDirectories(backupRoot);
                //converts the entire path to just the highest up folder name
                FileMethods.convertPathToFolder(backups);
                for(int i = backups.Length -1; i >= 0; i--)
                {
                    if(backups[i].StartsWith("\\backup") && Regex.Replace(backups[i], @"[^\d]", "") != "")
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
            List<BackupLocation> backupList = constructList();
            //runs the checkExtraUserFolders function, if it returns true the user chose to cancel,
            //so we return the method after reenabling the buttons.
            if (checkExtraUserFolders(backupList))
            {
                setButtonState(true);
                return;
            }
            foreach (BackupLocation bl in backupList)
            {
                /* string strSuffix = "";

                 if (bl.originalDirectory.StartsWith("%userprofile%"))
                 {
                     strSuffix = "User Files";
                 }

                 if (bl.originalDirectory.StartsWith("%userprofile%"))
                 {
                     strSuffix = "\\User Files";
                 }*/
                bl.specialActions();
                //Must convert "backupRoot" from a relative path, otherwise adding "\\?\" later does not work
                bl.backup(Path.GetFullPath(backupRoot) + currentBackup);
                bl.writeToConfig(Path.GetFullPath(backupRoot) + currentBackup + restoreFile);
            }
            //make configuration file readonly XXXXX TEMPORAIRLY DISABLED UNTIL NEW BACKUPS GO TO A NEW FOLDER
            FileInfo info = new FileInfo(backupRoot + restoreFile);
            /*  if (info.Exists)
              {
                  info.IsReadOnly = true;
              }*/
            //reenable controls
            MessageBox.Show("BACKUP COMPLETE");
            setButtonState(true);
        }

        /**
         * This method checks for extra folders in the user directory that are not standard. If they exist, it asks if they should be backed up
         * the functin takes the backupList as an argument, this method returns a boolean indicating whether or not the entire operation was
         * cancelled. It does not have to return the backupList because objects in C# are shallow copied, so modifying the list in this method
         * modifies it elsewhere as well.
         */
        private bool checkExtraUserFolders(List<BackupLocation> backupList)
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
            bool toCancel = false;
            foreach (string dir in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToString()))
            {
                string[] split = dir.Split('\\');
                if (!(commonFolders.Contains(split[split.Length - 1])) && !(split[split.Length - 1][0] == '.'))
                {
                    DialogResult dialogResult = MessageBox.Show("Would you like to back up the folder " + split[split.Length - 1] + " from the user Directory?", "Save Folder?", MessageBoxButtons.YesNoCancel);
                    if (dialogResult == DialogResult.Yes)
                    {
                        backupList.Add(new BackupLocation(split[split.Length - 1], "%userprofile%\\" + split[split.Length - 1], true));
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do nothing
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        toCancel = true;
                        break;
                    }
                }
            }
            return toCancel;
        }

        private void Restore(object sender, EventArgs e)
        {
            setButtonState(false);
            this.Hide();
           /* frmRestoreSelect sel = new frmRestoreSelect(backupRoot, this);
            sel.ShowDialog();
            if(frmRestoreSelect.currentlySelected == null)
            {
                MessageBox.Show("There are no backups from which to choose");
                setButtonState(true);
                return;
            }*/

            currentBackup = "\\" + frmRestoreSelect.currentlySelected;
            List<BackupLocation> restoreList = refineList(constructList());
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
        }

        private List<BackupLocation> constructList()
        {
            List<BackupLocation> backupList = new List<BackupLocation>();
            backupList.Add(new UserFiles("Desktop", "%desktop%", (chkDesktop.Checked && chkStandard.Checked)));
            backupList.Add(new UserFiles("Documents", "%documents%", (chkDocuments.Checked && chkStandard.Checked)));
            backupList.Add(new UserFiles("Downloads", "%downloads%", (chkDownloads.Checked && chkStandard.Checked)));
            backupList.Add(new UserFiles("Favorites", "%favorites%", (chkFavorites.Checked && chkStandard.Checked)));
            backupList.Add(new UserFiles("Music", "%music%", (chkMusic.Checked && chkStandard.Checked)));
            backupList.Add(new UserFiles("Pictures", "%pictures%", (chkPictures.Checked && chkStandard.Checked)));
            backupList.Add(new UserFiles("Videos", "%videos%", (chkVideos.Checked && chkStandard.Checked)));
            backupList.Add(new FirefoxLocation("Firefox", "%AppDataRoaming%\\Mozilla\\Firefox\\Profiles", chkFirefox.Checked));
            backupList.Add(new ChromeLocation("Chrome", "%appdatalocal%\\Google\\Chrome", chkChrome.Checked));
            
            return backupList;
        }
        /**
         * refines the list for the restore by searching the configuration file and adding
         * any BackupLocation whose name is in front of a '+' in the configuration file to
         * the refined list. Also adds custom saved files found in the configuration file
         * to the list
         */
        private List<BackupLocation> refineList(List<BackupLocation> original)
        {
            List<BackupLocation> refinedList = new List<BackupLocation>();
            string line;
            string name = "";
            //a bool indicating if the file being examined in the config file is found
            //in the list of standard backups. It is initially set to true because I 
            //also use this bool to detect if something was never found
            bool found = true;
            using (StreamReader file = new StreamReader(backupRoot + currentBackup + restoreFile))
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (line.StartsWith("+"))
                    {
                        //if no directory or list association was found since the last '+' display warning
                        if (!found)
                        {
                            MessageBox.Show("WARNING: the configuration has a file called " + name + " that had no associated directory. This file cannot be restored.");
                        }
                        name = line.Substring(1);
                        //TEST DEBUG CODE
                        Console.WriteLine(name);
                        found = false;
                        foreach (BackupLocation bl in original)
                        {
                            if (line.EndsWith(bl.name))
                            {
                                found = true;
                                refinedList.Add(bl);
                                break;
                            }
                        }
                    }
                    //if file is not found in standard backuplist
                    if (!found)
                    {
                        if (line.StartsWith("-DIRECTORY="))
                        {
                            string[] split = line.Split('=');
                            refinedList.Add(new BackupLocation(name, split[1], true));
                            //set found to true so that we skip this code until there is another 
                            found = true;
                        }
                    }
                }
                file.Close();
            }
            return refinedList;
        }

        private void setButtonState(bool state)
        {
            btnRestore.Enabled = state;
            btnBackup.Enabled = state;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process[] processesffox = Process.GetProcessesByName("Firefox");
            Process[] processeschrome = Process.GetProcessesByName("Chrome");
            foreach(Process p in processesffox)
            {
                p.Kill();
                p.Kill();
            }
            foreach(Process p in processeschrome)
            {
                p.Kill();
                p.Kill();
            }
        }

        private void frmBackup_Load(object sender, EventArgs e)
        {

        }
    }
}
