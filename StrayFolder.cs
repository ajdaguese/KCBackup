using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCBackup
{
    /**
     * a class for backing up and restoring hand chosen files that do not have checkboxes
     */
    class StrayFolder : BackupLocation
    {
        /**
         * constructs a StrayFolder object and sets the type enum
         */
        public StrayFolder(string pName, string pDir, bool pChecked) : base(pName, pDir, pChecked)
        {
            type = BackupType.strayfolder;
        }
        /**
         * backs up the she selected stray folder, including its full path, to the passed backup destination
         */
        public override void backup(string dest)
        {
            string pathNoDrive = this.realDirectory[0] + this.realDirectory.Substring(2); 
            base.backup(Path.Combine(dest, pathNoDrive));
        }
        /**
         * restores the stary folder from its backup destination to its original path
         */
        public override void restore(string backupLoc)
        {
            string pathNoDrive = this.realDirectory[0] + this.realDirectory.Substring(2);
            base.restore(Path.Combine(backupLoc, pathNoDrive));
        }
    }
}
