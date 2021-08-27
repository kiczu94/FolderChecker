using FolderChecker.Model;
using FolderChecker.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
            if (File.Exists(@"C:\Users\tomasz.tkocz\Desktop\ghghg\rule.json"))
            {
                MyRulesCollection = JSONoperations.loadRules();
            }
            else
            {
                MyRulesCollection = new ObservableCollection<Rule>();
            }
            RuleAdded += JSONoperations.onRuleAdded;
            RuleDeleted += JSONoperations.onRuleDeleted;
        }
        public delegate void RuleAddedEventHandler(object source, RuleEventArgs args);
        public delegate void RuleDeletedEventHandler(object sorce, RuleEventArgs args);
        public event PropertyChangedEventHandler PropertyChanged;
        public event RuleAddedEventHandler RuleAdded;
        public event RuleDeletedEventHandler RuleDeleted;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void onRuleAdded(List<Rule> rulesUpdated)
        {
            if (RuleAdded != null)
                RuleAdded(this, new RuleEventArgs() { rules = rulesUpdated });
        }
        protected virtual void onRuleDeleted(List<Rule> rulesUpdated)
        {
            if (RuleDeleted != null)
                RuleDeleted(this, new RuleEventArgs() { rules = rulesUpdated });
        }
        public void DeleteRule(object rule)
        {
            if (rule!=null)
            {
                if (rule.GetType() == typeof(Rule))
                {
                    Rule newRule = (Rule)rule;
                    MyRulesCollection.Remove(newRule);
                    onRuleDeleted(MyRulesCollection.ToList());
                }
            }

        }
        public void AddRule()
        {
            AddRuleWindow addRuleWindow = new AddRuleWindow();
            addRuleWindow.ShowDialog();
            if (addRuleWindow.GetRule().myRuleName != null && addRuleWindow.GetRule().myPathToTrack != null && addRuleWindow.GetRule().myMailAdresses != null)
            {
                MyRulesCollection.Add(addRuleWindow.GetRule());
                onRuleAdded(MyRulesCollection.ToList());
            }

        }
        public void EditRule(object choosenRulesToEdit)
        {
            List<Rule> rules = ConvertObjectToList(choosenRulesToEdit);
            if (rules.Count!=0)
            {
                for (int i = 0; i < rules.Count; i++)
                {
                    EditRuleWindow editRuleWindow = new EditRuleWindow(rules[i]);
                    editRuleWindow.ShowDialog();
                    rules[i] = editRuleWindow.editRuleWindowViewModel.MyRuleToEdit;
                }
                UpdateCollection(rules);
            }

            onRuleAdded(MyRulesCollection.ToList());
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
                        MyRulesCollection[i].MyAdressMailstring ="";

                    }
                }

            }
        }
    }
}
