using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FolderChecker.ViewModel
{
    public class AddEmailAdressViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _mailAdressesCollection;
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
        public ObservableCollection<string> MyMailAdressesCollection
        {
            get { return _mailAdressesCollection; }
            set
            {
                if (value.GetType() == typeof(List<string>))
                {
                    foreach (var item in value)
                    {
                        MyMailAdressesCollection.Add(item);
                    }
                }
                else
                {
                    _mailAdressesCollection = value;
                }
            }
        }
        public AddEmailAdressViewModel()
        {
            MyMailAdressesCollection = new ObservableCollection<string>();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void AddEmail()
        {
            if (MyEmailString!=String.Empty)
            {
                MyMailAdressesCollection.Add(MyEmailString);
                MyEmailString = String.Empty;
            }
        }
        public List<string> exportEmailAdresses()
        {
            return MyMailAdressesCollection.ToList();
        }
    }
}
