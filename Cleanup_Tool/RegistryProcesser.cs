using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;


namespace Cleanup_Tool
{
    class RegistryProcesser
    {
        // Public this method for reference
        public void KeyChecker(string KeyName, Action<string> action)
        {   //Create process killing instance and shutdown related process
            AppProcessHandler ProcessStatus = new AppProcessHandler();
            ProcessStatus.ProcessChecker("FrostEd", action);
            ProcessStatus.ProcessChecker("Drone", action);
            ProcessStatus.ProcessChecker("maya", action);
            //Call Registry key process
            Win32bitProcesser(KeyName, action);
            Win64bitProcesser(KeyName, action);
        }

        private void Win32bitProcesser(string KeyName, Action<string> action)
        {   //Didn't use Registry.LocalMachine to retrieve the key in localMachine, 
            RegistryKey View32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            RegistryKey BaskKey = View32.OpenSubKey(@"SOFTWARE", true); //Retrieve the key under localmachine\software and add write access
            var paths = BaskKey.GetSubKeyNames();
            if (Array.Exists(paths, element => element.Contains(KeyName)))//Add condiation for keyName of user input
            {
                foreach (string path in paths)
                {
                    if (path.ToString() == KeyName)
                    {   //If the key name is right then delete key
                        action(string.Format("Find key {0} in 32bit list...", path));
                        try
                        {
                            BaskKey.DeleteSubKeyTree(path);
                            action("Key has been deleted!");
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
            else
            {
                action("Couldn't find the key...");
            }
        }

        private void Win64bitProcesser(string KeyName, Action<string> action)
        {
            RegistryKey View64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey BaskKey = View64.OpenSubKey(@"SOFTWARE", true);
            string[] paths = BaskKey.GetSubKeyNames();
            if (Array.Exists(paths, element => element.Contains(KeyName)))
            {
                foreach (string path in paths)
                {
                    if (path.Contains(KeyName))
                    {
                        action(string.Format("Find key {0} in 64bit list...", path));
                        try
                        {
                            BaskKey.DeleteSubKeyTree(path);
                            action("Key has been deleted!");
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
            else
            {
                action("Couldn't find the key...");
            }
        }

        // Test method!!!
        public void CreateSubkey(string KeyName, Action<string> action)
        {
            try
            {
                RegistryKey Win32bitKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                Win32bitKey.CreateSubKey(KeyName);
                RegistryKey Win64bitKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                Win64bitKey.CreateSubKey(KeyName);

                action("The keys have been created!");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
