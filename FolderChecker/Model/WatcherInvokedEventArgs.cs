using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderChecker.Model
{
    public class WatcherInvokedEventArgs : RenamedEventArgs
    {
        public string WatcherPath { get; set; }
        public WatcherInvokedEventArgs(WatcherChangeTypes changeType, string directory, string name, string oldName) : base(changeType, directory, name, oldName)
        {

        }
        public WatcherInvokedEventArgs(WatcherChangeTypes changeType, string directory, string name, string oldName, string watcherPathInput) : base(changeType, directory, name, oldName)
        {
            WatcherPath = watcherPathInput;
        }
    }
}
