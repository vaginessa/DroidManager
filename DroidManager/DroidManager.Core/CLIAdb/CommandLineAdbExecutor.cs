using System.Diagnostics;

namespace DroidManager.Core.CLIAdb
{
    internal class CommandLineAdbExecutor
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

        public void ExecuteCommand(string commandName, string[] arguments)
        {
            var startInfo = AdbStartInformation;
            startInfo.Arguments = $"{commandName} {string.Join(" ", arguments)}";
            Process.Start(startInfo);
        }
    }
}