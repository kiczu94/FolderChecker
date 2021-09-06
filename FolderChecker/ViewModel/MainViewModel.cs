using FolderChecker.Model;
using FolderChecker.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FolderChecker.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Model.Rule> _rulesCollection;
        public ObservableCollection<Model.Rule> MyRulesCollection
        {
            get { return _rulesCollection; }
            set
            {
                _rulesCollection = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            if (File.Exists(@"C:\Users\tomasz.tkocz\Desktop\FolderChecker\FolderChecker\bin\jsony próbne\rule.json"))
            {
                MyRulesCollection = JSONoperations.loadRules();
            }
            else
            {
                MyRulesCollection = new ObservableCollection<Rule>();
            }
            FileWatcher fileWatcher = new FileWatcher(MyRulesCollection.ToList());
            fileWatcher.FileRenamed += onFileRenamed;
            RuleUpdated += fileWatcher.onRuleUpdated;
            RuleUpdated += JSONoperations.onRuleUpdated;
        }
        public delegate void RuleAddedEventHandler(object source, RuleEventArgs args);
        public delegate void RuleDeletedEventHandler(object sorce, RuleEventArgs args);
        public delegate void RuleUpdatedEventHandler(object source, RuleEventArgs args);
        public event PropertyChangedEventHandler PropertyChanged;
        public event RuleUpdatedEventHandler RuleUpdated;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void onRuleUpdated(List<Rule> rulesUpdated)
        {
            if (RuleUpdated != null)
                RuleUpdated(this, new RuleEventArgs() { rules = rulesUpdated });
        }
        public void DeleteRule(object rule)
        {
            if (rule != null)
            {
                if (rule.GetType() == typeof(Rule))
                {
                    Rule newRule = (Rule)rule;
                    MyRulesCollection.Remove(newRule);
                    onRuleUpdated(MyRulesCollection.ToList());
                }
            }
        }
        public void AddRule()
        {
            AddRuleWindow addRuleWindow = new AddRuleWindow(MyRulesCollection.ToList());
            addRuleWindow.ShowDialog();
            if (addRuleWindow.GetRule().myRuleName != null && addRuleWindow.GetRule().myPathToTrack != null && addRuleWindow.GetRule().myMailAdresses != null)
            {
                foreach (var item in addRuleWindow.GetRulesToDelete())
                {
                    MyRulesCollection.Remove(item);
                }
                MyRulesCollection.Add(addRuleWindow.GetRule());
                onRuleUpdated(MyRulesCollection.ToList());
            }
        }
        public void EditRule(object choosenRulesToEdit)
        {
            List<Rule> rules = ConvertObjectToList(choosenRulesToEdit);
            if (rules.Count != 0)
            {
                for (int i = 0; i < rules.Count; i++)
                {
                    EditRuleWindow editRuleWindow = new EditRuleWindow(rules[i]);
                    editRuleWindow.ShowDialog();
                    rules[i] = editRuleWindow.editRuleWindowViewModel.MyRuleToEdit;
                }
                UpdateCollection(rules);
            }
            onRuleUpdated(MyRulesCollection.ToList());
        }
        private List<Rule> ConvertObjectToList(object choosenRulesToEdit)
        {
            List<Object> collection = new List<Object>((IEnumerable<Object>)choosenRulesToEdit);
            List<Rule> rules = new List<Rule>();
            if (collection.Count != 0)
            {
                foreach (var item in collection)
                {
                    rules.Add((Rule)item);
                }
            }
            return rules;
        }
        private void UpdateCollection(List<Rule> rules)
        {
            foreach (var rule in rules)
            {
                for (int i = 0; i < MyRulesCollection.Count; i++)
                {
                    if (rule.myRuleID == MyRulesCollection[i].myRuleID)
                    {
                        MyRulesCollection[i] = rule;
                        MyRulesCollection[i].MyAdressMailstring = "";
                    }
                }
            }
        }
        private void onFileRenamed(object source, RenamedEventArgs args)
        {
            foreach (var rule in MyRulesCollection)
            {
                if (rule.myPathToTrack == args.OldFullPath && Path.HasExtension(rule.myPathToTrack))
                {
                    rule.myPathToTrack = args.FullPath;
                }
                else if (rule.myPathToTrack.Contains(args.OldFullPath))
                {
                    string fileName = rule.myPathToTrack.Remove(0, args.OldFullPath.Length);
                    string newPath = args.FullPath + fileName;
                    rule.myPathToTrack = newPath;
                }
            }
            onRuleUpdated(MyRulesCollection.ToList());
        }
    }
}
