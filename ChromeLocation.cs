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
    class ChromeLocation : ProgramFiles
    {
        public ChromeLocation(string pName, string pDir, Boolean pChecked, string processName) : base(pName, pDir, pChecked, processName)
        {
            type = BackupType.chrome;
        }
        public override void specialActions()
        {
            if (canUse && bolChecked)
            {
                string chromeVersion = getChromeVersion();
                if (chromeVersion != "VERSION NOT FOUND")
                {
                    extraConfigWrites = "CHROMEVERSION=" + chromeVersion + "\n";
                }
                else
                {
                    MessageBox.Show("COULD NOT GET A VERSION ON CHROME, CHROME PROFILES CAN NOT BE BACKED UP");
                    canUse = false;
                }
            }
        }

        public override void backup(string dest)
        {
            if (canUse && bolChecked)
            {
                base.backup(dest + "\\Chrome");
            }
        }
        public override void restore(string backupLoc)
        {
            if (canUse && bolChecked)
            {
                base.restore(Path.Combine(backupLoc, "Chrome"));
            }
        }
        private string getChromeVersion()
        {
            string x86Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string standardPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string chromePath = "\\Google\\Chrome\\Application\\Chrome.exe";
            string chrome64 = "VERSION NOT FOUND";
            string chrome86 = "VERSION NOT FOUND";
            string chromeVersion;
            if(File.Exists(x86Path + chromePath))
            {
                chrome86 = System.Diagnostics.FileVersionInfo.GetVersionInfo(x86Path + chromePath).ToString();
            }
            if (File.Exists(standardPath + chromePath))
            {
                chrome64 = System.Diagnostics.FileVersionInfo.GetVersionInfo(standardPath + chromePath).FileVersion.ToString();
            }
            if(versionCompare(chrome86, chrome64))
            {
                chromeVersion = chrome86;
            }
            else
            {
                chromeVersion = chrome64;
            }
            return chromeVersion;
        }
        /**
         * compares two chrome versions, returns true if the first version is larger
         */
        private bool versionCompare(string version1, string version2)
        {
            int major1 = 0;
            int minor1 = 0;
            int build1 = 0;
            int major2 = 0;
            int minor2 = 0;
            int build2 = 0;
            bool is1Bigger = false;
            string[] temp1 = version1.Split('.');
            string[] temp2 = version2.Split('.');
            //if either version is not formatted as x.x.x, return the other version as bigger
            if (temp1.Length < 3)
            {
                is1Bigger = false;
            }
            else if (temp2.Length < 3)
            {
                is1Bigger = true;
            }
            else
            {
                major1 = Int32.Parse(temp1[0]);
                minor1 = Int32.Parse(temp1[1]);
                build1 = Int32.Parse(temp1[2]);
                major2 = Int32.Parse(temp2[0]);
                minor2 = Int32.Parse(temp2[1]);
                build2 = Int32.Parse(temp2[2]);

                if (major1 != major2)
                {
                    if (major1 > major2)
                    {
                        is1Bigger = true;
                    }
                    else
                    {
                        is1Bigger = false;
                    }
                }
                else if (minor1 != minor2)
                {
                    if (minor1 > minor2)
                    {
                        is1Bigger = true;
                    }
                    else
                    {
                        is1Bigger = false;
                    }
                }
                else if (build1 != build2)
                {
                    if (build1 > build2)
                    {
                        is1Bigger = true;
                    }
                    else
                    {
                        is1Bigger = false;
                    }
                }
                else
                {
                    is1Bigger = true;
                }
            }
            return is1Bigger;
        }
    }
}
