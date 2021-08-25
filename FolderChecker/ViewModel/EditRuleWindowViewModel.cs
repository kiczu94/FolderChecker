using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FolderChecker.Model;

namespace FolderChecker.ViewModel
{
    public class EditRuleWindowViewModel : INotifyPropertyChanged
    {

        private Rule _ruleToEdit;
        private string _ruleName;
        public event PropertyChangedEventHandler PropertyChanged;
        public string MyRuleName
        {
            get
            { return _ruleName; }

            set
            {
                _ruleName = value;
                OnPropertyChanged();
            }
        }
        public Rule MyRuleToEdit
        {
            get { return _ruleToEdit; }
            set
            {
                _ruleToEdit = value;
                MyRuleName = _ruleToEdit.myRuleName;
            }
        }

        public EditRuleWindowViewModel()
        {

        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
