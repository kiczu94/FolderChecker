using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Win32;
using FolderChecker.View;
using System.Collections.ObjectModel;
using System.Windows;
using FolderChecker.Model;

namespace FolderChecker.ViewModel
{
    public class AddRuleWindowViewModel : INotifyPropertyChanged
    {
        private List<string> _emailAdresses;
        public List<string> adressesToDelete;
        public List<long> ruleIDtoEdit;
        private List<Rule> _rules;
        private Rule _workingRule;
        private string _rulePath;
        private ObservableCollection<string> _emailAdressesCollection;
        private string _ruleName;
        public List<Rule> MyRules
        {
            get { return _rules; }
            set { _rules = value; }
        }
        public List<string> MyEmailAdresses
        {
            get { return _emailAdresses; }
            set { _emailAdresses = value; }
        }
        public string MyRuleName
        {
            get { return _ruleName; }
            set { _ruleName = value; }
        }
        public string MyRulePath
        {
            get { return _rulePath; }
            set
            {
                _rulePath = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> MyEmailAdressesCollection
        {
            get { return _emailAdressesCollection; }
            set { _emailAdressesCollection = value; }
        }
        public Rule MyWorkingRule
        {
            get { return _workingRule; }
            set { _workingRule = value; }
        }
        public AddRuleWindowViewModel()
        {
            MyWorkingRule = new Rule();
            MyEmailAdresses = new List<string>();
            MyEmailAdressesCollection = new ObservableCollection<string>();
            adressesToDelete = new List<string>();
            ruleIDtoEdit = new List<long>();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void ChooseFolder()
        {
            MyRulePath = HelpClass.ChooseFolder();
        }
        public void ChooseFile()
        {
            _workingRule.myPathToTrack = HelpClass.ChooseFile();
            MyRulePath = _workingRule.myPathToTrack;
        }
        public void AddMail()
        {
            AddEmailAdressWindow emailAdressWindow = new AddEmailAdressWindow();
            emailAdressWindow.ShowDialog();
            foreach (var mail in emailAdressWindow.Export())
            {
                MyEmailAdresses.Add(mail);
            }
            MyEmailAdressesCollection = ConverseCollection(MyEmailAdresses);
            OnPropertyChanged("MyEmailAdressesCollection");
        }
        public void AddRule()
        {
            CheckIfAnythingBeneathIsTracked(_workingRule, MyRules);
            _workingRule.myMailAdresses = MyEmailAdresses;
            _workingRule.myRuleName = MyRuleName;
            _workingRule.myPathToTrack = MyRulePath;

        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private ObservableCollection<string> ConverseCollection(List<string> listToConverse)
        {
            ObservableCollection<string> collection = new ObservableCollection<string>();
            foreach (var item in listToConverse)
            {
                collection.Add(item);
            }
            return collection;
        }
        private bool CheckIfFolderAboveIsTracked(Rule rule, List<Rule> rules)
        {
            bool isCorrect = true;
            foreach (var item in rules)
            {
                if (rule.myPathToTrack.Contains(item.myPathToTrack))
                {
                    return isCorrect = false;
                }
                else
                {
                    return isCorrect = true;
                }
            }
            return isCorrect;
        }
        private bool CheckIfAnythingBeneathIsTracked(Rule newRule, List<Rule> rulesList)
        {
            bool isCorrect = true;
            foreach (var ruleInRuleList in rulesList)
            {
                if (ruleInRuleList.myPathToTrack.Contains(newRule.myPathToTrack))
                {

                    foreach (var adress in MyEmailAdressesCollection)
                    {
                        if (ruleInRuleList.myMailAdresses.Contains(adress))
                        {
                            ruleIDtoEdit.Add(ruleInRuleList.myRuleID);
                            adressesToDelete.Add(adress);
                        }
                    }
                    return isCorrect = false;
                }
            }
            return isCorrect;
        }
    }
}
