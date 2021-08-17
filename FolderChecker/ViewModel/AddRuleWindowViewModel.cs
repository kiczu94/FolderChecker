using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Win32;
using System.Collections.Specialized;

namespace FolderChecker.ViewModel
{
    public class AddRuleWindowViewModel : INotifyPropertyChanged, INotifyCollectionChanged
    {
        private string _newRuleName = string.Empty;
        private string _rulePath;
        private List<string> _newRuleEmailAdresses;
        public string MyRuleName
        {
            get
            {
                return _newRuleName;
            }
            set
            {
                _newRuleName = value;
                OnPropertyChanged();
            }
        }
        public string MyNewRulePath
        {
            get
            {
                return _rulePath;
            }
            set
            {
                _rulePath = value;
                OnPropertyChanged();
            }
        }
        public List<string> MyNewRuleEmailAdresses
        {
            get { return _newRuleEmailAdresses; }
            set { _newRuleEmailAdresses = value;
                OnPropertyChanged();
            }
        }
        public AddRuleWindowViewModel()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void OnCollectionChange(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }
    }
}
