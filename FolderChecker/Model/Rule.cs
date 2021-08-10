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
                return myMailAdresses;
            }
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
        public Rule(string name,string pathToTrack, List<string> mailAdresses)
        {
            _ruleName = name;
            _pathToTrack = pathToTrack;
            _MailAdresses = mailAdresses;
        }
        public Rule(string name,string pathToTrack)
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
    }
}
