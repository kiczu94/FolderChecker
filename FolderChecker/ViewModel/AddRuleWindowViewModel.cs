using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Win32;
using FolderChecker.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace FolderChecker.ViewModel
{
    public class AddRuleWindowViewModel : INotifyPropertyChanged
    {
        private Model.Rule _workingRule;
        private string _rulePath;
        private ObservableCollection<string> _emailAdressesCollection;
        private List<string> _emailAdresses;
        private string _ruleName;
        private List<Model.Rule> _rules;
        public List<Model.Rule> MyRules
        {
            get { return _rules; }
            set { _rules = value; }
        }
        public string MyRuleName
        {
            get { return _ruleName; }
            set { _ruleName = value; }
        }
        public List<string> MyEmailAdresses
        {
            get { return _emailAdresses; }
            set { _emailAdresses = value; }
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
        public Model.Rule MyWorkingRule
        {
            get { return _workingRule; }
            set { _workingRule = value; }
        }
        public AddRuleWindowViewModel()
        {
            MyWorkingRule = new Model.Rule();
            MyEmailAdresses = new List<string>();
            MyEmailAdressesCollection = new ObservableCollection<string>();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void ChooseFolder()
        {
            var dialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                Title = "Select folder..."
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                _workingRule.myPathToTrack = dialog.FileName;
                MyRulePath = dialog.FileName;
            }
        }
        public void ChooseFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                _workingRule.myPathToTrack = openFileDialog.FileName;
                MyRulePath = openFileDialog.FileName;
            }
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
            if (CheckIfRuleCorrect(_workingRule,MyRules))
            {
                _workingRule.myMailAdresses = MyEmailAdresses;
                _workingRule.myRuleName = MyRuleName;
                _workingRule.myPathToTrack = MyRulePath;
            }
            else
            {
                MessageBox.Show("Śledzisz już folder nadrzędny, nie potrzeba śledzić plików w nim się znajdujących");
            }
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
        private bool CheckIfRuleCorrect(Model.Rule rule, List<Model.Rule> rules)
        {
            bool isCorrect=true;
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
    }
}
