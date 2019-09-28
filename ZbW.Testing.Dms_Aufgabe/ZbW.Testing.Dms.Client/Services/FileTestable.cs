using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client.Services
{
    public class FileTestable
    {
        public virtual void Copy(string sourceFileName, string destFileName, Boolean overwrite)
        {
            File.Copy(sourceFileName, destFileName, true);
        }
    }
}
