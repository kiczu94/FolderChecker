using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FolderChecker.Model
{
    public class Rule : INotifyPropertyChanged
    {
        public bool isContainer = false;
        private static long lastID;
        private long _ruleID;
        private string _ruleName;
        private string _pathToTrack;
        private string _AdressMailString;
        private List<string> _MailAdresses = new List<string>();
        public long MyRuleID
        {
            get { return _ruleID; }
            set { _ruleID = value; }
        }
        public List<string> MyMailAdresses
        {
            get
            {
                return _MailAdresses;
            }
            set { _MailAdresses = value; }
        }
        public string MyPathToTrack
        {
            get
            {
                return _pathToTrack;
            }
            set
            {
                _pathToTrack = value;
                OnPropertyChanged();
            }
        }
        public string MyRuleName
        {
            get
            {
                return _ruleName;
            }
            set
            {
                _ruleName = value;
                OnPropertyChanged();
            }
        }
        public string MyAdressMailstring
        {
            set
            {
                _AdressMailString = null;
                foreach (var adres in MyMailAdresses)
                {
                    _AdressMailString += adres + " ";
                }
                OnPropertyChanged();
            }
            get
            {
                if (_AdressMailString == null)
                {
                    foreach (var adres in MyMailAdresses)
                    {
                        _AdressMailString += adres + " ";
                    }
                }
                return _AdressMailString;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public Rule(string name, string pathToTrack, List<string> mailAdresses)
        {
            _ruleName = name;
            _pathToTrack = pathToTrack;
            _MailAdresses = mailAdresses;
            MyRuleID = GetID();
        }
        public Rule(string name, string pathToTrack)
        {
            _ruleName = name;
            _pathToTrack = pathToTrack;
            MyRuleID = GetID();
        }
        public Rule()
        {
            MyRuleID = GetID();
        }
        public void AddEmailAdress(string emailAdress)
        {
            _MailAdresses.Add(emailAdress);
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private static long GetID()
        {
            return lastID += 1;
        }
    }
}
