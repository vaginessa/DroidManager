using System;
using System.Diagnostics;

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
    }
}