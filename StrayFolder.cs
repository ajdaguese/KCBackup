using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCBackup
{
    class StrayFolder : BackupLocation
    {
        public StrayFolder(string pName, string pDir, bool pChecked) : base(pName, pDir, pChecked)
        {
            type = BackupType.strayfolder;
        }
        public override void backup(string dest)
        {
            string pathNoDrive = this.realDirectory[0] + this.realDirectory.Substring(2); 
            base.backup(Path.Combine(dest, pathNoDrive));
        }
        public override void restore(string backupLoc)
        {
            string pathNoDrive = this.realDirectory[0] + this.realDirectory.Substring(2);
            base.restore(Path.Combine(backupLoc, pathNoDrive));
        }
    }
}
