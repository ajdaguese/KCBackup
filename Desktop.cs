using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCBackup
{
    /**
     * Desktop is a specific type of UserFile because restored desktop files need to be put in a folder on the new desktop
     */
    class Desktop : UserFiles
    {
        /**
         * constructs a Desktop object and assignes the type enum to desktop
         */
        public Desktop(string pName, string pDir, bool pChecked) : base(pName, pDir, pChecked)
        {
            type = BackupType.desktop;
        }
        /**
         * on a restore, restores files to ...\Desktop\OldDesktop\\OwnerDesktop
         */
        public override void restore(string backupLoc)
        {
            this.realDirectory = Path.Combine(realDirectory, "OldDesktop\\OwnerDesktop");
            base.restore(backupLoc);
        }
    }
}
