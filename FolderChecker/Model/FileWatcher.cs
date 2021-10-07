using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace FolderChecker.Model
{
    public class FileWatcher
    {
        private List<FileSystemWatcher> _fileSystemWatchers;
        private List<Rule> _rules;
        public List<FileSystemWatcher> MyFileSystemWatchers
        {
            get { return _fileSystemWatchers; }
            set { _fileSystemWatchers = value; }
        }
        public List<Rule> MyRules
        {
            get { return _rules; }
            set { _rules = value; }
        }
        public delegate void WatcherInvokedEventHandler(object source, WatcherInvokedEventArgs args);
        public delegate void FileRenamedEventHandler(object source, RenamedEventArgs args);
        public event WatcherInvokedEventHandler WatcherInvoked;
        public event FileRenamedEventHandler FileRenamed;
        public FileWatcher(List<Rule> rules)
        {
            MyFileSystemWatchers = new List<FileSystemWatcher>();
            MyRules = rules;
            UpdateWatchers(rules);
            SetWatchers();    
        }
        public FileWatcher()
        {

        }
        private void UpdateWatchers(List<Rule> rules)
        {
            MyFileSystemWatchers.Clear();
            foreach (var rule in rules)
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                FileSystemWatcher containerWatcher = new FileSystemWatcher();
                if (Path.HasExtension(rule.MyPathToTrack))
                {
                    watcher.Path = Path.GetDirectoryName(rule.MyPathToTrack);
                    watcher.Filter = Path.GetFileName(rule.MyPathToTrack);
                    MyFileSystemWatchers.Add(watcher);
                }
                else
                {
                    watcher.Path = rule.MyPathToTrack;
                    int whenLastSlash = watcher.Path.LastIndexOf('\\');
                    string container = watcher.Path.Remove(whenLastSlash, watcher.Path.Length - whenLastSlash);
                    containerWatcher.Path = container;
                    containerWatcher.Filter = watcher.Path.Remove(0, whenLastSlash + 1);
                    if (!MyFileSystemWatchers.Contains(containerWatcher))
                    {
                        MyFileSystemWatchers.Add(containerWatcher);
                    }
                    if (!MyFileSystemWatchers.Contains(watcher))
                    {
                        MyFileSystemWatchers.Add(watcher);
                    }
                }
            }
        }
        public void OnCreated(object sender, FileSystemEventArgs e)
        {
            this.OnWatcherInvoked(new WatcherInvokedEventArgs(e.ChangeType, e.FullPath.Replace(e.Name, string.Empty), e.Name, string.Empty, GetFileSystemWatcher(sender).Path));
        }
        public void OnRenamed(object sender, RenamedEventArgs e)
        {  
            this.OnFileRenamed(e);
            this.OnWatcherInvoked(new WatcherInvokedEventArgs(e.ChangeType, e.FullPath.Replace(e.Name, string.Empty), e.Name, e.OldName, GetFileSystemWatcher(sender).Path));
        }
        public void OnDeleted(object sender, FileSystemEventArgs e)
        {
            this.OnWatcherInvoked(new WatcherInvokedEventArgs(e.ChangeType, e.FullPath.Replace(e.Name, string.Empty), e.Name, string.Empty, GetFileSystemWatcher(sender).Path));
        }
        public void OnChanged(object sender, FileSystemEventArgs e)
        {
            this.OnWatcherInvoked(new WatcherInvokedEventArgs(e.ChangeType, e.FullPath.Replace(e.Name, string.Empty), e.Name, string.Empty, e.FullPath));
        }
        private void ResetWatchers()
        {
            foreach (var watcher in MyFileSystemWatchers)
            {
                watcher.Renamed -= OnRenamed;
                watcher.Created -= OnCreated;
                watcher.Deleted -= OnDeleted;
                watcher.Changed -= OnChanged;
            }
        }
        private void SetWatchers()
        {
            foreach (var watcher in MyFileSystemWatchers)
            {
                watcher.EnableRaisingEvents = true;
                watcher.IncludeSubdirectories = true;
                //watcher.Renamed += OnRenamed;
                //watcher.Created += OnCreated;
                //watcher.Deleted += OnDeleted;
                watcher.Changed += OnChanged;
            }
        }
        private FileSystemWatcher GetFileSystemWatcher(object sender)
        {
            var sender2 = new FileSystemWatcher();
            if (sender.GetType() == typeof(FileSystemWatcher))
            {
                sender2 = (FileSystemWatcher)sender;
            }
            return sender2;
        }
        protected virtual void OnFileRenamed(RenamedEventArgs args)
        {
            FileRenamed?.Invoke(this, args);
        }
        protected virtual void OnWatcherInvoked(WatcherInvokedEventArgs args)
        {
            WatcherInvoked?.Invoke(this, args);
        }
        public void OnRuleUpdated(object source, RuleEventArgs ruleEventArgs)
        {
            ResetWatchers();
            UpdateWatchers(ruleEventArgs.rules);
            SetWatchers();
        }
    }
}
