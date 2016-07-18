using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroidManager.Core.Utilities
{
    public class AdbChecker
    {
        public static bool VerifyAdbExecutable(string path)
        {
            try
            {
                AdbServer.Instance.StartServer(path, true);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
