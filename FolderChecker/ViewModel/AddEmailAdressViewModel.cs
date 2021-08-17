using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FolderChecker.ViewModel
{
    public class AddEmailAdressViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _mailAdressesCollection;

        public ObservableCollection<string> MyMailAdressesCollection
        {
            get { return _mailAdressesCollection; }
            set { _mailAdressesCollection = value; }
        }

        ObservableCollection<string> _mailAdressCollection = new ObservableCollection<string>();
        private string _emailAdress;
        public string MyEmailString
        {
            get
            {
                return _emailAdress;
            }
            set
            {
                _emailAdress = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public AddEmailAdressViewModel()
        {
            MyMailAdressesCollection = new ObservableCollection<string>();
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddEmail()
        {
            MyMailAdressesCollection.Add(MyEmailString);
        }
    }
}
