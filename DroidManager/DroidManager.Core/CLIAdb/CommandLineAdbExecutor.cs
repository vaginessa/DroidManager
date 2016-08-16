#region Copyright and License Header
/*

	DroidManager

	Copyright (c) 2016 0xFireball, IridiumIon Software

	Author(s): 0xFireball
	

	This file is part of DroidManager.

	DroidManager is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	DroidManager is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with DroidManager.  If not, see <http://www.gnu.org/licenses/>.

*/
#endregion


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