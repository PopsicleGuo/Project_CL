using System;
using System.Collections.Generic;
using System.Collections;

namespace Cleanup_Tool
{
    class LocalVariable
    {
        InfoOutput info = new InfoOutput();
        readonly string[] artoolKeys = { "ATCORE", "ATROOT", "ATWORKSPACE" };

        public LocalVariable()
        {
            var valueList = GetLocalVariables();

            foreach (var key in valueList)
            {
                if (CheckKeyName(key.Key))
                {
                    info.OutPut(string.Format("Find Local Variable KeyName: {0}", key.Key));
                    Environment.SetEnvironmentVariable(key.Key, null, EnvironmentVariableTarget.User); //Set value to null by the key name
                    info.OutPut(string.Format("Removed this local variable key name: {0}", key.Key));
                }
            }
        }

        // Create a dictionary to store Warlus related local variable
        private Dictionary<string, string> GetLocalVariables()
        {
            var countQuery = new Dictionary<string, string>();
            foreach (DictionaryEntry value in Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User)) //Get current user's variables
            {
                countQuery.Add((string)value.Key, (string)value.Value);
            }
            return countQuery;
        }

        // A method to check related variable name in dictionary or not
        private bool CheckKeyName(string name)
        {
            return Array.Exists(artoolKeys, element => element == name);
        }
    }
}
