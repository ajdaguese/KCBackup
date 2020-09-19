using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCBackup
{
    /**
     * a class for backing up extra files inside the user's user directory
     */
    class ExtraUserFolders : BackupLocation
    {
        private string saveLocation = "\\UserFiles\\Extras";
        /**
         * constructs an ExtraUserFolders object, sets the type and adds a folder to the saveLocation directory named after
         * this extra directory
         */
        public ExtraUserFolders(string pName, string pDir, bool pChecked) : base(pName, pDir, pChecked)
        {
            type = BackupType.extrauserfiles;
            string[] split = pDir.Split('\\');
            saveLocation += ("\\" + split[split.Length - 1]);
        }
        /**
         * runs the parent backup method with the backup location plus \UserFiles\Extras\DirectoryToBackUp
         */
        public override void backup(string dest)
        {
            base.backup(dest + saveLocation);
        }
        /**
         * restores the extra user files to a folder in documents called "KCRestore"
         */
        public override void restore(string backupLoc)
        {
            string[] split = realDirectory.Split('\\');
            //we move extra user files to the documents folder instead of returning them to the user files
            this.realDirectory = Path.Combine(pathTranslate("%Documents%\\KCRestore"), split[split.Length - 1]);
            base.restore(backupLoc + saveLocation);
        }
    }
}
