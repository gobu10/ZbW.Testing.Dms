using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client.Services
{
    public class UserTestable {

        public virtual String GetCurrentUsersernme () {
            return Properties.Settings.Default.currentUser;
        }

        public virtual void SetCurrentUsersernme (string value) {
            Properties.Settings.Default.currentUser = value;
        }

        public virtual void SaveSettings () {
            Properties.Settings.Default.Save ();
        }
    }
}
