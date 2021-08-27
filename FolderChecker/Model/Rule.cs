using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FolderChecker.Model
{
    public class Rule : INotifyPropertyChanged
    {
        private static long lastID;
        private long _ruleID;

        public long myRuleID
        {
            get { return _ruleID; }
            set { _ruleID = value; }
        }
        private string _ruleName;
        private List<string> _MailAdresses = new List<string>();
        private string _pathToTrack;
        public List<string> myMailAdresses
        {
            get
            {
                return _MailAdresses;
            }
            set { _MailAdresses = value; }
        }
        public string myPathToTrack
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
        public string myRuleName
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
        private string _AdressMailString;
        public event PropertyChangedEventHandler PropertyChanged;
        public string MyAdressMailstring
        {
            set
            {
                _AdressMailString = null;
                foreach (var adres in myMailAdresses)
                {
                    _AdressMailString += adres + " ";
                }
                OnPropertyChanged();
            }
            get
            {
                if (_AdressMailString == null)
                {
                    foreach (var adres in myMailAdresses)
                    {
                        _AdressMailString += adres + " ";
                    }
                }
                return _AdressMailString;

            }
        }
        public Rule(string name, string pathToTrack, List<string> mailAdresses)
        {
            _ruleName = name;
            _pathToTrack = pathToTrack;
            _MailAdresses = mailAdresses;
            myRuleID = GetID();
        }
        public Rule(string name, string pathToTrack)
        {
            _ruleName = name;
            _pathToTrack = pathToTrack;
            myRuleID = GetID();
        }
        public Rule()
        {
            myRuleID = GetID();
        }
        public void AddEmailAdress(string emailAdress)
        {
            _MailAdresses.Add(emailAdress);
        }
        private static long GetID()
        {
            return lastID += 1;
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
