using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCBackup
{
    class Desktop : UserFiles
    {
        public Desktop(string pName, string pDir, bool pChecked) : base(pName, pDir, pChecked)
        {
            type = BackupType.desktop;
        }
        public override void restore(string backupLoc)
        {
            this.realDirectory = Path.Combine(realDirectory, "OldDesktop\\OwnerDesktop");
            base.restore(backupLoc);
        }
    }
}
