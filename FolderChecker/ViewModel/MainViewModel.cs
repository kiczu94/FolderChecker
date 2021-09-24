using FolderChecker.Model;
using FolderChecker.View;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace FolderChecker.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<Rule> rulesToDelete = new List<Rule>();
        private MessageSender _messageSender = new MessageSender();
        private string jsonPath;
        private ObservableCollection<Rule> _rulesCollection;
        public ObservableCollection<Rule> MyRulesCollection
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
            LoadSettings();
            LoadRules();
            RuleUpdated += JSONoperations.onRuleUpdated;
            FileWatcher fileWatcher = new FileWatcher(CheckRules());
            fileWatcher.FileRenamed += onFileRenamed;
            RuleUpdated += fileWatcher.onRuleUpdated;
            fileWatcher.WatcherInvoked += _messageSender.onWatcherInvoked;
        }
        public delegate void RuleUpdatedEventHandler(object source, RuleEventArgs args);
        public event PropertyChangedEventHandler PropertyChanged;
        public event RuleUpdatedEventHandler RuleUpdated;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        private void EditRules(AddRuleWindowViewModel viewModel)
        {
            for (int i = 0; i < viewModel.ruleIDtoEdit.Count; i++)
            {
                foreach (var rule in MyRulesCollection)
                {
                    if (rule.myRuleID == viewModel.ruleIDtoEdit[i])
                    {
                        rule.myMailAdresses.Remove(viewModel.adressesToDelete[i]);
                        rule.MyAdressMailstring = "";
                    }
                }
            }
        }
        private void LoadSettings()
        {
            if (Properties.Settings.Default.PathToJson == string.Empty)
            {
                MessageBox.Show("Wybierz ścieżkę, gdzie mają zostać zapisane dane programu");
                Properties.Settings.Default.PathToJson = HelpClass.ChooseFolder();
                Properties.Settings.Default.Save();
                jsonPath = Properties.Settings.Default.PathToJson;
            }
            else if (!Directory.Exists(Properties.Settings.Default.PathToJson))
            {
                Properties.Settings.Default.PathToJson = HelpClass.ChooseFolder();
                Properties.Settings.Default.Save();
                jsonPath = Properties.Settings.Default.PathToJson;
            }
            else
            {
                jsonPath = Properties.Settings.Default.PathToJson;
            }
        }
        private void LoadRules()
        {
            if (File.Exists(jsonPath + "\\rule.json"))
            {
                MyRulesCollection = JSONoperations.loadRules(jsonPath + "\\rule.json");
            }
            else
            {
                MyRulesCollection = new ObservableCollection<Rule>();
            }
        }
        private List<Rule> CheckRules()
        {
            bool ruleUpdated=false;
            List<Rule> listToReturn = new List<Rule>();
            foreach (var rule in MyRulesCollection)
            {
                if (File.Exists(rule.myPathToTrack))
                {
                    listToReturn.Add(rule);
                }
                else if (Directory.Exists(rule.myPathToTrack))
                {
                    listToReturn.Add(rule);
                }
                else if (rule.myPathToTrack==string.Empty)
                {
                    listToReturn.Add(rule);
                }
                else
                {
                    rulesToDelete.Add(rule);
                    MessageBox.Show($"Ścieżka {rule.myPathToTrack} nie istnieje. Usunięto regułę.");
                    ruleUpdated = true;
                }
            }
            foreach (var rule in rulesToDelete)
            {
                MyRulesCollection.Remove(rule);
            }
            if (ruleUpdated==true)
            {
                onRuleUpdated(MyRulesCollection.ToList());
            }
            rulesToDelete.Clear();
            return listToReturn;
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
        protected virtual void onRuleUpdated(List<Rule> rulesUpdated)
        {
            if (RuleUpdated != null)
                RuleUpdated(this, new RuleEventArgs() { rules = rulesUpdated, jsonPath=jsonPath });
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
            Rule ruleToAdd = addRuleWindow.GetRule();
            if (ruleToAdd.myRuleName != null && ruleToAdd.myPathToTrack != null && ruleToAdd.myMailAdresses != null)
            {
                //Function edit rules to avoid sending to the same person multiple times same message about change in folder
                EditRules(addRuleWindow.AddRuleWindowViewModel);
                MyRulesCollection.Add(ruleToAdd);
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
        public void Login()
        {
            _messageSender.GetEmailAdress();
            _messageSender.GetPassword();
        }
        public void Settings()
        {
            
        }
    }
}
