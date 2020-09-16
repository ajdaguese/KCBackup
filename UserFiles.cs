using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KCBackup
{
    class UserFiles : BackupLocation
    {
        public UserFiles(string pName, string pDir, bool pChecked) : base(pName, pDir, pChecked)
        {
            type = BackupType.userfiles;
        }

        public override void backup(string dest)
        {
            string[] separated = realDirectory.Split('\\');
            base.backup(dest + "\\UserFiles" + "\\" + name);
        }
        public override void restore(string backupLoc)
        {
            string[] separated = realDirectory.Split('\\');
            base.restore(backupLoc + "\\UserFiles" + "\\" + name);
        }
    }
}
