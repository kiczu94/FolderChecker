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
            set { _rulesCollection = value; }
        }
        public MainViewModel(List<Model.Rule> rules)
        {
            MyRulesCollection = new ObservableCollection<Model.Rule>();
            foreach (var rule in rules)
            {
                MyRulesCollection.Add(rule);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
