using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FolderChecker.Model;

namespace FolderChecker.ViewModel
{
    public class EditRuleWindowViewModel
    {
        private Rule _ruleToEdit;

        public Rule MyRuleToEdit
        {
            get { return _ruleToEdit; }
            set { _ruleToEdit = value; }
        }

        public EditRuleWindowViewModel(Rule rule)
        {
            MyRuleToEdit = rule;
        }
    }
}
