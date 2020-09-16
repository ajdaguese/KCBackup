using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace KCBackup
{
    class FirefoxLocation : ProgramFiles
    {
        string profileiniPath;
        //the name of the profiles file and what we will rename the currently existing profiles file to on a new install of firefox
        string profiles = "\\profiles.ini";
        public FirefoxLocation(string pName, string pDir, Boolean pChecked, string processName) : base(pName, pDir, pChecked, processName)
        {
            type = BackupType.firefox;
            //Path to the profiles.ini file which will also need backed up and restored
            profileiniPath = base.pathTranslate("%AppDataRoaming%\\Mozilla\\Firefox");
        }
        public override void backup(string dest)
        {
            if (canUse && bolChecked)
            {
                /*Process[] processes = Process.GetProcessesByName("Firefox");
                while (processes.Length > 0)
                {
                    new frmKillBrowser("firefox").ShowDialog();
                    if (frmKillBrowser.response == btnResponse.ok)
                    {
                        //do nothing, if the browser is actually closed the loop should break, if not display message again
                    }
                    else if (frmKillBrowser.response == btnResponse.fStop)
                    {
                        foreach (Process p in processes)
                        {
                            try
                            {
                                p.Kill();
                            }
                            catch (Exception e)
                            {
                                //Likely only caught trying to kill a process that was automatically killed when another died
                            }
                        }
                        //sleeps for half a second giving the proccesses time to close
                        Thread.Sleep(500);
                    }
                    else if (frmKillBrowser.response == btnResponse.cancel)
                    {
                        //we set canUse to false, otherwise it will try to write to the configuration file
                        canUse = false;
                        return;
                    }
                    processes = Process.GetProcessesByName("Firefox");
                }*/
                base.backup(dest + "\\Firefox\\Profiles");
                if (Directory.Exists(dest + "\\Firefox"))
                {
                    File.Copy(profileiniPath + profiles, (dest + "\\Firefox" + profiles));
                }
            }
        }
        public override void restore(string backupLoc)
        {
            if (canUse && bolChecked)
            {
                
                string thisFirefoxVersion = getFirefoxVersion();
                string backedupFirefoxVersion = "";
                if (thisFirefoxVersion == "VERSION NOT FOUND")
                {
                    MessageBox.Show("COULD NOT GET A VERSION ON FIREFOX, FIREFOX PROFILES CAN NOT RESTORE PROFILES");
                    canUse = false;
                }
                using (StreamReader file = new StreamReader(backupLoc + FormBackup.restoreFile))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.StartsWith("-FIREFOXVERSION="))
                        {
                            string[] version = line.Split('=');
                            backedupFirefoxVersion = version[1];
                            break;
                        }
                    }
                }
                //if the current version of firefox is newer or the same copy the profiles
                if (versionCompare(thisFirefoxVersion, backedupFirefoxVersion) || thisFirefoxVersion == backedupFirefoxVersion)
                {
                    try
                    {
                        string profileHash = "";
                        string currentoldProfiles = FileMethods.fileRename(profileiniPath + profiles);
                        File.Move(backupLoc + "\\Firefox" + profiles, profileiniPath + profiles);
                        using (StreamReader file = new StreamReader(currentoldProfiles))
                        {
                            string line = "";
                            while ((line = file.ReadLine()) != null)
                            {
                                if (line.StartsWith("[Install"))
                                {
                                    profileHash = line;
                                    break;
                                }
                            }
                            file.Close();
                        }
                        //an array of every line in the recently moved backedup profiles file
                        string[] arrLine = File.ReadAllLines(profileiniPath + profiles);
                        for (int i = 0; i < arrLine.Length; i++)
                        {
                            if (arrLine[i].StartsWith("[Install"))
                            {
                                arrLine[i] = profileHash;
                                break;
                            }
                        }
                        File.WriteAllLines(profileiniPath + profiles, arrLine);
                        base.restore(Path.Combine(backupLoc, "Firefox\\Profiles"));
                        /*string[] directoryNames = Directory.GetDirectories(backupLoc + "\\Firefox\\Profiles");
                        FileMethods.convertPathToFolder(directoryNames);
                        //change the profile names before backing up so the regular use of firefox is not impeded until backup is done
                        foreach (string dir in directoryNames)
                        {
                            Directory.Move(backupLoc + "\\Firefox\\Profiles" + dir, backupLoc + "\\Firefox\\Profiles" + dir + "KCRESTORE");
                        }
                        base.restore(backupLoc + "\\Firefox");
                        //restore the folder names in the backup
                        foreach (string dir in directoryNames)
                        {
                            Directory.Move(backupLoc + "\\Firefox\\Profiles" + dir + "KCRESTORE", backupLoc + "\\Firefox\\Profiles" + dir);
                        }
                        Process[] processes = Process.GetProcessesByName("Firefox");
                        while (processes.Length > 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("Firefox must be closed to complete the restore, would you like the program to do this for you? Close firefox before selecting no", "close firefox", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                foreach (Process p in processes)
                                {
                                    p.Kill();
                                    Thread.Sleep(500);
                                }
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                //as above, do nothing and if the user actually closed their browser the loop will break
                            }
                            processes = Process.GetProcessesByName("Firefox");
                        }
                        FileMethods.CheckDirectoryNames(backupLoc + "\\Firefox\\Profiles", realDirectory);
                        //Finally, rename the newly restored folders to their original name
                        foreach (string dir in directoryNames)
                        {
                            Directory.Move(realDirectory + dir + "KCRESTORE", realDirectory + dir);
                        }*/
                        //to avoid copying the profiles.ini a second time in the main copy method, it gets moved. After everything else copies
                        //we copy the profile.ini back into the backup
                        File.Copy(profileiniPath + profiles, backupLoc + "\\Firefox" + profiles);
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show("There was an error with the firefox backup, error message {0}", e.Message);
                        if(!File.Exists(backupLoc + "\\Firefox" + profiles))
                        {
                            //this check is just in case so another error is not thrown, but if a profiles file is missing from the backup,
                            //it should be in the firefox folder because it gets moved from backup to the firefox folder. If this is not the
                            //case, it is likely that the user has deleted the file from the backup folder
                            if(File.Exists(profileiniPath + profiles))
                            {
                                //if something goes wrong, it's important to restore the profiles file to the backup
                                File.Copy(profileiniPath + profiles, backupLoc + "\\Firefox" + profiles);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cannot restore firefox files because your current version of firefox is less up to date than the copied profile. Please update firefox and try again");
                }
            }
        }
        public override void specialActions()
        {
            if (canUse && bolChecked)
            {
                string firefoxVersion = getFirefoxVersion();
                if (firefoxVersion != "VERSION NOT FOUND")
                {
                    extraConfigWrites = "-FIREFOXVERSION=" + firefoxVersion + "\n";
                }
                else
                {
                    MessageBox.Show("COULD NOT GET A VERSION ON FIREFOX, FIREFOX PROFILES CAN NOT BE BACKED UP");
                    canUse = false;
                }
            }
        }

        private string getFirefoxVersion()
        {
            string x86Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string standardPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string mozillaPath = "\\Mozilla Firefox\\application.ini";
            Console.WriteLine(standardPath + mozillaPath);
            //the default value for the version is "VERSION NOT FOUND", if this returns that means the firefox version could not be ascertained 
            string firefoxVersion = "VERSION NOT FOUND";
            string line;
            if (!(File.Exists(x86Path + mozillaPath) || File.Exists(standardPath + mozillaPath)))
            {
                MessageBox.Show("no firefox install detected, skipping firefox backup");
            }
            else
            {
                string version86 = "";
                string version64 = "";

                #region version parsing
                //   XXXXX Need to figure out specific location of INI file and which path to open, not always X86
                if (File.Exists(x86Path + mozillaPath))
                {
                    using (StreamReader file = new StreamReader(x86Path + mozillaPath))
                    {
                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.StartsWith("Version="))
                            {
                                version86 = line.Split('=')[1];
                                break;
                            }
                        }
                        file.Close();
                    }
                }
                if (File.Exists(standardPath + mozillaPath))
                {
                    using (StreamReader file = new StreamReader(standardPath + mozillaPath))
                    {
                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.StartsWith("Version="))
                            {
                                version64 = line.Split('=')[1];
                                break;
                            }
                        }
                        file.Close();
                    }
                }
                #endregion version parsing

                firefoxVersion = "";
                if (versionCompare(version64, version86))
                {
                    firefoxVersion += version64;
                }
                else
                {
                    firefoxVersion += version86;
                }
            }
            return firefoxVersion;
        }
        /**
         * compares two firefox versions, returns true if the first version is larger
         */
        private bool versionCompare(string version1, string version2)
        {
            bool is1Bigger = false;
            //if either version is empty
            if (version1.Length == 0)
            {
                is1Bigger = false;
            }
            else if (version2.Length == 0)
            {
                is1Bigger = true;
            }
            else
            {
                string[] temp1 = version1.Split('.');
                string[] temp2 = version2.Split('.');
                //an integer containing the size of the smaller version array.
                int smallerArray = temp1.Length;
                if(temp1.Length > temp2.Length)
                {
                    smallerArray = temp2.Length;
                }
                for(int i = 0; i < smallerArray; i++)
                {
                    if(Int32.Parse(temp1[i]) > Int32.Parse(temp2[i]))
                    {
                        is1Bigger = true;
                        break;
                    }
                    else if(Int32.Parse(temp1[i]) < Int32.Parse(temp2[i]))
                    {
                        is1Bigger = false;
                        break;
                    }
                }
            }
            return is1Bigger;
        }
    }
}
