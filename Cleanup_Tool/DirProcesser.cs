using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cleanup_Tool
{
    class DirProcesser
    {
        public static List<string> DirPool = new List<string>();

        public void Process(string[] RootPaths, Action<string> action)
        {
            AppProcessHandler ProcessStatus = new AppProcessHandler();
            ProcessStatus.ProcessChecker("FrostEd", action);
            ProcessStatus.ProcessChecker("Drone", action);
            ProcessStatus.ProcessChecker("maya", action);

            foreach (string value in RootPaths)
            {
                DoProcess(value, action);               
            }

        }

        private void DoProcess(string ProcessPath, Action<string> action)
        {
            try
            {
                string[] subDirs = Directory.GetDirectories(ProcessPath);

                foreach (var subDir in subDirs)
                {
                    if (ShouldIgnore(new FileInfo(ProcessPath)))
                    {
                        continue;
                    }
                    else if (IsFrostbiteFolder(ProcessPath))
                    {
                        action(string.Format("Find Folder {0}", ProcessPath));
                        DirPool.Add(ProcessPath);
                    }
                    else
                    {
                        DoProcess(subDir, action);
                    }            
                }
            }
            catch (UnauthorizedAccessException e)
            {
                action(string.Format("Error: {0}", e.Message));
            }            
        }

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
            // Check the folder is what you want
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
            {
                count++;
                string Dest = string.Format(@"C:\tempfolder" + count);
                DirMover(path, Dest, action);
            }          
        }
    }
}
