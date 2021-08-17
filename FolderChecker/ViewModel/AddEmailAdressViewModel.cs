﻿using System;
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
            set
            {
                if (value.GetType()==typeof(List<string>))
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
