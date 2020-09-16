using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCBackup
{
    class PublicDesktop : PublicUserFiles
    {
        public PublicDesktop(string pName, string pDir, bool pChecked) : base(pName, pDir, pChecked)
        {
            type = BackupType.publicdesktop;
        }
        public override void restore(string backupLoc)
        {
            //adding a folder ot the public desktop requires administrator privelages, so I just copy the 
            //contents of the public desktop to "olddesktop" on the users desktop
            this.realDirectory = Path.Combine(pathTranslate("%desktop%"), "OldDesktop\\PublicDesktop");
            base.restore(backupLoc);
        }
    }
}
