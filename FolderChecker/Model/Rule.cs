using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderChecker.Model
{
    public class Rule
    {
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
            }
        }
        private string _AdressMailString;
        public string MyAdressMailstring
        {
            get
            {
                CreateAdressString();
                return _AdressMailString;
            }
        }
        public Rule(string name, string pathToTrack, List<string> mailAdresses)
        {
            _ruleName = name;
            _pathToTrack = pathToTrack;
            _MailAdresses = mailAdresses;
        }
        public Rule(string name, string pathToTrack)
        {
            _ruleName = name;
            _pathToTrack = pathToTrack;
        }
        public Rule()
        {

        }
        public void AddEmailAdress(string emailAdress)
        {
            _MailAdresses.Add(emailAdress);
        }
        private void CreateAdressString()
        {
            foreach (var adress in _MailAdresses)
            {
                _AdressMailString += adress;
                _AdressMailString += " ";
            }
        }
    }
}
