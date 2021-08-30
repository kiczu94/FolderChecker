using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public delegate void FileRenamedEventHandler(object source, RenamedEventArgs args);
        public event FileRenamedEventHandler FileRenamed;
        public FileWatcher(List<Rule> rules)
        {
            MyFileSystemWatchers = new List<FileSystemWatcher>();
            MyRules = rules;
            UpdateWatchers(rules);
        }
        private void UpdateWatchers(List<Rule> rules)
        {
            foreach (var rule in rules)
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = Path.GetDirectoryName(rule.myPathToTrack);
                watcher.Filter = Path.GetFileName(rule.myPathToTrack);
                watcher.EnableRaisingEvents = true;
                watcher.IncludeSubdirectories = true;
                watcher.Renamed += OnRenamed;
                _fileSystemWatchers.Add(watcher);
            }
        }
        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            MessageBox.Show($"Renamed: {e.OldName} to {e.Name}");
            this.onFileRenamed(e);
        }
        protected virtual void onFileRenamed(RenamedEventArgs args)
        {
            if (FileRenamed != null)
                FileRenamed(this, args);
        }
        public void onRuleUpdated(object source, RuleEventArgs ruleEventArgs)
        {
            UpdateWatchers(ruleEventArgs.rules);
        }
    }
}
