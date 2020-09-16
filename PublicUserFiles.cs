using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCBackup
{
    class PublicUserFiles : BackupLocation
    {
        public PublicUserFiles(string pName, string pDir, bool pChecked) : base(pName, pDir, pChecked)
        {
            type = BackupType.publicuserfiles;
        }

        public override void backup(string dest)
        {
            string[] separated = realDirectory.Split('\\');
            base.backup(dest + "\\PublicUserFiles" + "\\" + name);
        }
        public override void restore(string backupLoc)
        {
            string[] separated = realDirectory.Split('\\');
            base.restore(backupLoc + "\\PublicUserFiles" + "\\" + name);
        }
    }
}
