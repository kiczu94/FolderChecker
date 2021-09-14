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
        private void UpdateWatchers(List<Rule> rules)
        {
            MyFileSystemWatchers.Clear();
            foreach (var rule in rules)
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                FileSystemWatcher containerWatcher = new FileSystemWatcher();
                if (Path.HasExtension(rule.myPathToTrack))
                {
                    watcher.Path = Path.GetDirectoryName(rule.myPathToTrack);
                    watcher.Filter = Path.GetFileName(rule.myPathToTrack);
                    MyFileSystemWatchers.Add(watcher);
                }
                else
                {
                    watcher.Path = rule.myPathToTrack;
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
        public void onCreated(object sender, FileSystemEventArgs createEventArgs)
        {
            var sender2 = new FileSystemWatcher();
            if (sender.GetType()==typeof(FileSystemWatcher))
            {
                sender2 = (FileSystemWatcher)sender;
            }
            this.onWatcherInvoked(new WatcherInvokedEventArgs(createEventArgs.ChangeType, createEventArgs.FullPath.Replace(createEventArgs.Name, string.Empty), createEventArgs.Name, string.Empty) { watcherPath=sender2.Path});
        }
        public void OnRenamed(object sender, RenamedEventArgs e)
        {  
            MessageBox.Show($"Renamed: {e.OldName} to {e.Name}");
            this.onFileRenamed(e);
            this.onWatcherInvoked(new WatcherInvokedEventArgs(e.ChangeType, e.FullPath.Replace(e.Name, string.Empty), e.Name, e.OldName));
        }
        protected virtual void onFileRenamed(RenamedEventArgs args)
        {
            if (FileRenamed != null)
                FileRenamed(this, args);
        }
        protected virtual void onWatcherInvoked(WatcherInvokedEventArgs args)
        {
            if (WatcherInvoked != null)
                WatcherInvoked(this, args);
        }
        public void onRuleUpdated(object source, RuleEventArgs ruleEventArgs)
        {
            ResetWatchers();
            UpdateWatchers(ruleEventArgs.rules);
            SetWatchers();
        }
        private void ResetWatchers()
        {
            foreach (var watcher in MyFileSystemWatchers)
            {
                watcher.Renamed -= OnRenamed;
                watcher.Created -= onCreated;
            }
        }
        private void SetWatchers()
        {
            foreach (var watcher in MyFileSystemWatchers)
            {
                watcher.EnableRaisingEvents = true;
                watcher.IncludeSubdirectories = true;
                watcher.Renamed += OnRenamed;
                watcher.Created += onCreated;
            }
        }
    }
}
