using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KCBackup
{
    /**
     * A class for program files that need backed up. When backing up or restoring these files, the user will be asked to close the program
     */
    class ProgramFiles : BackupLocation
    {
        //the name of the process to search for and end
        private string processName;
        /**
         * constructs a ProgramFiles object, sets the type enum and adds the processname to the configuration file write variable
         */
        public ProgramFiles (string pName, string pDir, Boolean pChecked, string processName) : base(pName, pDir, pChecked)
        {
            type = BackupType.programfiles;
            this.processName = processName;
            extraConfigWrites += "-PROCESSNAME=" + processName + "\n";
        }
        /**
         * backs up the program files after ensuring that the program is closed
         */
        public override void backup(string dest)
        {
            if (canUse && bolChecked)
            {
                Process[] processes = Process.GetProcessesByName(processName);
                while (processes.Length > 0)
                {
                    new frmKillProcess(this.name).ShowDialog();
                    if (frmKillProcess.response == btnResponse.ok)
                    {
                        //do nothing, if the program is actually closed the loop should break, if not display message again
                    }
                    else if (frmKillProcess.response == btnResponse.fStop)
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
                    else if (frmKillProcess.response == btnResponse.cancel)
                    {
                        //we set canUse to false, otherwise it will try to write to the configuration file
                        canUse = false;
                        return;
                    }
                    processes = Process.GetProcessesByName(processName);
                }
            }

            base.backup(dest);
        }
        /**
         * restores the program files after ensuring the program is closed
         */
        public override void restore(string backupLoc)
        {
            string[] directoryNames = Directory.GetDirectories(backupLoc);
            FileMethods.convertPathToFolder(directoryNames);
            //change the profile names before backing up so the regular use of firefox is not impeded until backup is done
            foreach (string dir in directoryNames)
            {
                Directory.Move(backupLoc + dir, backupLoc + dir + "KCRESTORE");
            }
            base.restore(backupLoc);
            //restore the folder names in the backup
            foreach (string dir in directoryNames)
            {
                Directory.Move(backupLoc  + dir + "KCRESTORE", backupLoc + dir);
            }
            Process[] processes = Process.GetProcessesByName(processName);
            while (processes.Length > 0)
            {
                DialogResult dialogResult = MessageBox.Show(name + " must be closed to complete the restore, would you like the program to do this for you? Close " + name + " before selecting no", "close " + name, MessageBoxButtons.YesNo);
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
                processes = Process.GetProcessesByName(processName);
            }
            FileMethods.CheckDirectoryNames(backupLoc, realDirectory);
            //Finally, rename the newly restored folders to their original name
            foreach (string dir in directoryNames)
            {
                Directory.Move(realDirectory + dir + "KCRESTORE", realDirectory + dir);
            }
        }
    }
}
