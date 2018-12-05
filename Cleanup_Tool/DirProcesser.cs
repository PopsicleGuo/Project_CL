using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cleanup_Tool
{
    class DirProcesser
    {   //Put the query result into this static list
        public static List<string> DirPool = new List<string>();

        public void Process(string[] RootPaths, Action<string> action)
        {   //Try to clean the current processes 
            AppProcessHandler ProcessStatus = new AppProcessHandler();
            ProcessStatus.ProcessChecker("FrostEd", action);
            ProcessStatus.ProcessChecker("Drone", action);
            ProcessStatus.ProcessChecker("maya", action);
            //Check the player's input folder array
            foreach (string value in RootPaths)
            {
                DoProcess(value, action);               
            }

        }

        private void DoProcess(string ProcessPath, Action<string> action)
        {
            try
            {   //Get subfolder list from path
                string[] subDirs = Directory.GetDirectories(ProcessPath);

                foreach (var subDir in subDirs)
                {   //Try to skip some folders with some condiations
                    if (ShouldIgnore(new FileInfo(ProcessPath)))
                    {
                        continue;
                    }
                    else if (IsFrostbiteFolder(ProcessPath))
                    {   //If the contdiation is true then put suitble folder name with path into static list
                        action(string.Format("Find Folder {0}", ProcessPath));
                        DirPool.Add(ProcessPath);
                    }
                    else
                    {   //Try to search the folder recusively
                        DoProcess(subDir, action);
                    }            
                }
            }
            catch (UnauthorizedAccessException e)
            {
                action(string.Format("Error: {0}", e.Message));
            }            
        }
        //Try to check current folder info, if folder attribute contains System flag, then return true
        private bool ShouldIgnore(FileInfo fi)
        {
            if (fi.Name.Contains("Administrator") || fi.Name.Contains("Documents and Settings"))
            {
                return true;
            }

            if (fi.Attributes.HasFlag(FileAttributes.System))
            {
                return true;
            }
            return false;
        }

        private bool IsFrostbiteFolder(string path)
        {
            // Check the folder name is belong to condiation
            FileInfo DirName = new FileInfo(path);
            if (DirName.Name.Equals("FrostEd"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Use this private method for frostbite folder renaming
        private void DirMover(string SourceDir, string DestinationDir, Action<string> action)
        {
            if (Directory.Exists(DestinationDir))
            {
                action("Destination folder is already there");
                return;
            }
            else
            {
                try
                {
                    Directory.Move(SourceDir, DestinationDir);
                    action(string.Format("The folder {0} has been moved in {1}", SourceDir, DestinationDir));
                }
                catch (Exception e)
                {
                    action(string.Format("Exception info: {0}", e.Message));
                }
            }
        }

        public void DirMoveProcesser(Action<string> action)
        {
            int count = 0;
            foreach (string path in DirPool)
            {   //Put a counter for several folder names as destination path  
                count++;
                string Dest = string.Format(@"C:\tempfolder" + count);
                DirMover(path, Dest, action);
            }          
        }
    }
}
