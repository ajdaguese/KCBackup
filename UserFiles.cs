using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KCBackup
{
    /**
     * A class for backing up and restoring common user files
     */
    class UserFiles : BackupLocation
    {
        /**
         * constructs a UserFiles object and sets the type enum
         */
        public UserFiles(string pName, string pDir, bool pChecked) : base(pName, pDir, pChecked)
        {
            type = BackupType.userfiles;
        }
        /**
         * backs the user's file up to a directory "UserFiles\folderName" in the given backup directory
         */
        public override void backup(string dest)
        {
            string[] separated = realDirectory.Split('\\');
            base.backup(dest + "\\UserFiles" + "\\" + name);
        }
        /**
         * restores the user's file from the "UserFiles\folderName" directory in the given backup  directory to the cooresponding
         * folder in the user's user directory
         */
        public override void restore(string backupLoc)
        {
            string[] separated = realDirectory.Split('\\');
            base.restore(backupLoc + "\\UserFiles" + "\\" + name);
        }
    }
}
