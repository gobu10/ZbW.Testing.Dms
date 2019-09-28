using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client.Services
{
    public class CouldNotCopyFileException:Exception
    {
        public CouldNotCopyFileException(string message, Exception innerException) : base (message, innerException) { }
    }
}
