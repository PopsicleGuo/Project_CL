using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace Cleanup_Tool
{
    class AppProcessHandler
    {
        public void ProcessChecker(string AppName, Action<string> action)
        {
            if(IsProcessExists(AppName))
            {
                EndProcess(AppName, action);
            }
            else
            {
                action("Didn't find the requested process");
            }
        }

        private void EndProcess(string ProcessName, Action<string> action)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName(ProcessName))
                {
                    action(string.Format("Find process {0} ID {1}", proc.ProcessName, proc.Id));
                    proc.Kill();
                    action(string.Format("Process {0} has been closed", ProcessName));
                }
            }
            catch (Exception e)
            {
                action(e.Message);
            }
        }

        private bool IsProcessExists(string TaskName)
        {
            Process[] ProcessPool = Process.GetProcessesByName(TaskName);

            if (ProcessPool.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }  
    }
}
