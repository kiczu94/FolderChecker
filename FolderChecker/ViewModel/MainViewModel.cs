using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FolderChecker.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<Model.Rule> _rules;
        private List<string> _rulesNames;
        public List<Model.Rule> MyRules
        {
            get { return _rules; }
            set
            {
                _rules = value;
                OnPropertyChanged();
            }
        }
        public List<string> MyRulesNames
        {
            get { return _rulesNames; }
            set { _rulesNames = value; }
        }
        public MainViewModel()
        {
            MyRules = GetRule();
            _rulesNames = GetRuleNames();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private List<Model.Rule> GetRule()
        {
            List<Model.Rule> randomList = new List<Model.Rule>();
            List<string> vs = GetAdressList();
            randomList.Add(new Model.Rule("zasada 1", "ścieżka1", vs));
            randomList.Add(new Model.Rule("zasada 2", "ścieżka2"));
            randomList.Add(new Model.Rule("zasada 3", "ścieżka3"));
            randomList.Add(new Model.Rule("zasada 4", "ścieżka4"));
            randomList.Add(new Model.Rule("zasada 5", "ścieżka5"));
            randomList.Add(new Model.Rule("zasada 6", "ścieżka6"));
            return randomList;
        }
        private List<string> GetAdressList()
        {
            List<string> vs = new List<string>();
            vs.Add("adres1");
            vs.Add("adres2");
            vs.Add("adres3");
            vs.Add("adres4");
            vs.Add("adres5");
            vs.Add("adres6");
            vs.Add("adres7");
            vs.Add("adres8");
            return vs;
        }
        private List<string> GetRuleNames()
        {
            List<string> ruleNames = new List<string>();
            foreach (var rule in _rules)
            {
                ruleNames.Add(rule.myRuleName);
            }
            return ruleNames;
        }

    }
}
