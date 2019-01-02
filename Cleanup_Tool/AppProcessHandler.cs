using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace Cleanup_Tool
{
    class AppProcessHandler
    {
        // Initiallization function
        public AppProcessHandler(Action<string> action)
        {
            StartProcess(action);
        }

        // Put a method chain in here
        private void StartProcess(Action<string> action)
        {
            string[] pgNames = {"FrostEd", "Drone", "maya" };

            foreach(string name in pgNames)
            {
                ProcessChecker(name, action);
            }
        }

        //  A process check method for suitble process name
        private void ProcessChecker(string appName, Action<string> action)
        {   
            if(IsProcessExists(appName))
            {
                EndProcess(appName, action);
            }
            else
            {
                action("Didn't find the requested process");
            }
        }

        // Kill those process by the name from string array
        private void EndProcess(string processName, Action<string> action)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName(processName))
                {
                    action(string.Format("Find process {0} ID {1}", proc.ProcessName, proc.Id));
                    proc.Kill();
                    action(string.Format("Process {0} has been closed", processName));
                }
            }
            catch (Exception e)
            {
                action(e.Message);
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
