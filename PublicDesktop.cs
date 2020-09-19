using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCBackup
{
    /**
     * the PublicDesktop class restores anything from the public desktop directory to the a directory "OldDesktop\PublicDesktop"
     * on the desktop of the user running this program
     */
    class PublicDesktop : PublicUserFiles
    {
        /**
         * constructs a PublicDesktop object and sets the type enum
         */
        public PublicDesktop(string pName, string pDir, bool pChecked) : base(pName, pDir, pChecked)
        {
            type = BackupType.publicdesktop;
        }
        /**
         * restores the files that were on the public desktop to the directory "OldDesktop\PublicDesktop" on the desktop of the 
         * user running this program
         */
        public override void restore(string backupLoc)
        {
            //adding a folder ot the public desktop requires administrator privelages, so I just copy the 
            //contents of the public desktop to "olddesktop" on the users desktop
            this.realDirectory = Path.Combine(pathTranslate("%desktop%"), "OldDesktop\\PublicDesktop");
            base.restore(backupLoc);
        }
    }
}
