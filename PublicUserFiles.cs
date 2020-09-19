using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCBackup
{
    /**
     * PublicUserFiles is a class for backing up and restoring the directories kept in the public users directory
     */
    class PublicUserFiles : BackupLocation
    {
        /**
         * constructs a PublicUserFiles object and sets the type enum
         */
        public PublicUserFiles(string pName, string pDir, bool pChecked) : base(pName, pDir, pChecked)
        {
            type = BackupType.publicuserfiles;
        }
        /**
         * backs up the public user directory to a directory under the same name that exists in the backup directory
         */
        public override void backup(string dest)
        {
            string[] separated = realDirectory.Split('\\');
            base.backup(dest + "\\PublicUserFiles" + "\\" + name);
        }
        /**
         * restores the public user directory from the backup location to the public users folder
         */
        public override void restore(string backupLoc)
        {
            string[] separated = realDirectory.Split('\\');
            base.restore(backupLoc + "\\PublicUserFiles" + "\\" + name);
        }
    }
}
