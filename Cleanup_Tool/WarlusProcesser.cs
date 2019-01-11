using System;

namespace Cleanup_Tool
{
    class WarlusProcesser
    {
        // Initalize the function 
        public WarlusProcesser(Action<string> action)
        {
            // Remove Warlus related local variables
            new LocalVariable();
            // Kill the process
            new AppProcessHandler();
            // Find related folders and take care of them
            new DirProcesser(action);
        }         
    }
}
