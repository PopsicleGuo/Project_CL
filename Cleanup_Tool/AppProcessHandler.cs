using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace Cleanup_Tool
{
    class AppProcessHandler
    {
        InfoOutput info = new InfoOutput();

        // Initiallization function
        public AppProcessHandler()
        {
            StartProcess();
        }

        // Put a method chain in here
        private void StartProcess()
        {
            string[] pgNames = {"FrostEd", "Drone", "maya", "arttools" };

            foreach(string name in pgNames)
            {
                ProcessChecker(name);
            }
        }

        //  A process check method for suitble process name
        private void ProcessChecker(string appName)
        {   
            if(IsProcessExists(appName))
            {
                EndProcess(appName);
            }
            else
            {
                info.OutPut("Didn't find the requested process");
            }
        }

        // Kill those process by the name from string array
        private void EndProcess(string processName)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName(processName))
                {
                    info.OutPut(string.Format("Find process {0} ID {1}", proc.ProcessName, proc.Id));
                    proc.Kill();
                    info.OutPut(string.Format("Process {0} has been closed", processName));
                }
            }
            catch (Exception e)
            {
                info.OutPut(e.Message);
            }
        }

        // Check current process list to get process list
        private bool IsProcessExists(string taskName)
        {
            Process[] processPool = Process.GetProcessesByName(taskName);

            if (processPool.Length > 0)
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
