using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client.Services
{
    internal class CouldNotMoveFileException : Exception {
        public CouldNotMoveFileException (string message, Exception innerException) : base (message, innerException) { }
    }
}
