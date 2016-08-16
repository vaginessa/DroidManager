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


using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DroidManager.Core.CLIAdb
{
    public class CommandLineProcessController
    {
        public Process UnderlyingProcess { get; }

        public CommandLineProcessController(Process proc)
        {
            UnderlyingProcess = proc;
            proc.Exited += UnderlyingProcessExited;
        }

        private void UnderlyingProcessExited(object sender, System.EventArgs e)
        {
            ProcessCompleted?.Invoke(sender, UnderlyingProcess.ExitCode);
        }

        public void Kill()
        {
            UnderlyingProcess.Kill();
        }

        /// <summary>
        /// This event is called when the process completes. The integer argument contains the exit code of the process.
        /// </summary>
        public event EventHandler<int> ProcessCompleted;

        public async Task<int> WaitForCompletion()
        {
            await Task.Run(() => UnderlyingProcess.WaitForExit());
            return UnderlyingProcess.ExitCode;
        }
    }
}