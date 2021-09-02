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
        private FileSystemWatcher watcher = new FileSystemWatcher();
        private FileSystemWatcher containerWatcher = new FileSystemWatcher();
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
            watcher.Renamed += OnRenamed;
            containerWatcher.Renamed += OnRenamed;
            MyRules = rules;
            UpdateWatchers(rules);
        }
        private void UpdateWatchers(List<Rule> rules)
        {
            MyFileSystemWatchers.Clear();
            foreach (var rule in rules)
            {
                if (Path.HasExtension(rule.myPathToTrack))
                {
                    watcher.Path = Path.GetDirectoryName(rule.myPathToTrack);
                    watcher.Filter = Path.GetFileName(rule.myPathToTrack);
                    watcher.EnableRaisingEvents = true;
                    MyFileSystemWatchers.Add(watcher);
                }
                else
                {

                    watcher.Path = rule.myPathToTrack;
                    int whenLastSlash = watcher.Path.LastIndexOf('\\');
                    string container = watcher.Path.Remove(whenLastSlash, watcher.Path.Length - whenLastSlash);
                    containerWatcher.Path = container;
                    watcher.EnableRaisingEvents = true;
                    containerWatcher.EnableRaisingEvents = true;
                    containerWatcher.IncludeSubdirectories = true;
                    containerWatcher.Filter = watcher.Path.Remove(0, whenLastSlash + 1);
                    MyFileSystemWatchers.Add(watcher);
                    MyFileSystemWatchers.Add(containerWatcher);
                }

            }
        }
        private void onCreated(object sender, FileSystemEventArgs createEventArgs)
        {
            MessageBox.Show($"Created: {createEventArgs.Name}");
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
