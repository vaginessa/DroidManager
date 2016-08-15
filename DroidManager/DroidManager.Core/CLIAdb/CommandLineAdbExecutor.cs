using System.Diagnostics;
using System.Linq;

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
                startInfo.UseShellExecute = false;
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
            var quoteEncapsulatedArguments = arguments.Select(argument => "\"" + argument + "\"").ToArray();
            startInfo.Arguments = $"{commandName} {string.Join(" ", quoteEncapsulatedArguments)}";
            var proc = Process.Start(startInfo);
            return new CommandLineProcessController(proc);
        }
    }
}