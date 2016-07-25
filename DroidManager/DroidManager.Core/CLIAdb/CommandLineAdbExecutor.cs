using System.Diagnostics;

namespace DroidManager.Core.CLIAdb
{
    public class CommandLineAdbExecutor
    {
        public string AdbLocation { get; }

        public ProcessStartInfo AdbStartInformation
        {
            get
            {
                var startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = AdbLocation;
                return startInfo;
            }
        }

        public CommandLineAdbExecutor(string adbExecutablePath)
        {
            AdbLocation = adbExecutablePath;
        }

        public CommandLineProcessController ExecuteCommand(string commandName, string[] arguments)
        {
            var startInfo = AdbStartInformation;
            startInfo.Arguments = $"{commandName} {string.Join(" ", arguments)}";
            var proc = Process.Start(startInfo);
            return new CommandLineProcessController(proc);
        }
    }
}