using FolderChecker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            set { _rulesCollection = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            MyRulesCollection = JSONoperations.loadRules();
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
        public void AddRule(Rule rule)
        {
            MyRulesCollection.Add(rule);
            onRuleAdded(MyRulesCollection.ToList());
        }
        protected virtual void onRuleAdded(List<Rule> rulesUpdated)
        {
            if (RuleAdded != null)
                RuleAdded(this, new RuleEventArgs() { rules=rulesUpdated });            
        }
        protected virtual void onRuleDeleted(List<Rule> rulesUpdated)
        {
            if (RuleDeleted != null)
                RuleDeleted(this, new RuleEventArgs() { rules = rulesUpdated });
        }
        public void DeleteRule(object rule)
        {
            if (rule.GetType()==typeof(Rule))
            {
                Rule newRule = (Rule)rule;
                MyRulesCollection.Remove(newRule);
                onRuleDeleted(MyRulesCollection.ToList());
            }
        }
    }
}
