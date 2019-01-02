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
        public void KeyChecker(string keyName, Action<string> action)
        {   // Initialize the application process handler for process cleanup
            new AppProcessHandler(action);
            
            // Call the function to remove those keys
            Win32bitProcesser(keyName, action);
            Win64bitProcesser(keyName, action);
        }

        private void Win32bitProcesser(string keyName, Action<string> action)
        {   // the Registry.LocalMachine parameter will only get keys of WOW6432Node, if you open it
            RegistryKey view32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            RegistryKey baseKey = view32.OpenSubKey(@"SOFTWARE", true); //Retrieve the key under localmachine\software and add write access
            var paths = baseKey.GetSubKeyNames();
            if (Array.Exists(paths, element => element.Contains(keyName))) // Add condiation for keyName of user input
            {
                foreach (string path in paths)
                {
                    if (path.ToString() == keyName)
                    {   // If the key name is right then delete key
                        action(string.Format("Find key {0} in 32bit list...", path));
                        try
                        {
                            baseKey.DeleteSubKeyTree(path);
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

        // Almost same constrution of code block
        private void Win64bitProcesser(string keyName, Action<string> action)
        {
            RegistryKey view64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey baseKey = view64.OpenSubKey(@"SOFTWARE", true);
            string[] paths = baseKey.GetSubKeyNames();
            if (Array.Exists(paths, element => element.Contains(keyName)))
            {
                foreach (string path in paths)
                {
                    if (path.Contains(keyName))
                    {
                        action(string.Format("Find key {0} in 64bit list...", path));
                        try
                        {
                            baseKey.DeleteSubKeyTree(path);
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
        public void CreateSubkey(string keyName, Action<string> action)
        {
            try
            {
                RegistryKey win32bitKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                win32bitKey.CreateSubKey(keyName);
                RegistryKey win64bitKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                win64bitKey.CreateSubKey(keyName);

                action("The keys have been created!");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
