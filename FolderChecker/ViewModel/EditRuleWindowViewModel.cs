using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FolderChecker.Model;
using FolderChecker.View;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FolderChecker.ViewModel
{
    public class EditRuleWindowViewModel : INotifyPropertyChanged
    {

        private Rule _ruleToEdit;
        private string _ruleName;
        private string _rulePath;
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
        public string MyRulePath
        {
            get { return _rulePath; }
            set
            {
                _rulePath = value;
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
                MyMailAdresses = ConvertListToCollection(_ruleToEdit.myMailAdresses);
                MyRulePath = _ruleToEdit.myPathToTrack;
            }
        }
        public ObservableCollection<string> MyMailAdresses { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public EditRuleWindowViewModel()
        {

        }
        public void onClosed(object source, EventArgs args)
        {
            MyRuleToEdit.myRuleName = MyRuleName;
            MyRuleToEdit.myPathToTrack = MyRulePath;
        }
        public void EditName()
        {
            EditSimpleTextWindow editSimpleText = new EditSimpleTextWindow("Nowa nazwa reguły", MyRuleName);
            editSimpleText.ShowDialog();
            MyRuleName = editSimpleText.GetName();
        }
        public void EditMail(object choosenMailToEdit)
        {
            List<string> mailsToEdit = ConvertObjectToList(choosenMailToEdit);
            if (mailsToEdit.Count != 0)
            {
                for (int i = 0; i < mailsToEdit.Count; i++)
                {
                    EditSimpleTextWindow editSimpleText = new EditSimpleTextWindow("Nowa nazwa reguły", mailsToEdit[i]);
                    editSimpleText.ShowDialog();
                    for (int j = 0; j < MyRuleToEdit.myMailAdresses.Count; j++)
                    {
                        if (MyRuleToEdit.myMailAdresses[j] == editSimpleText.editSimpleTextViewModel.MyOldText)
                        {
                            MyRuleToEdit.myMailAdresses[j] = editSimpleText.editSimpleTextViewModel.MyNewText;
                            MyMailAdresses[j] = editSimpleText.editSimpleTextViewModel.MyNewText;
                        }
                    }
                }
                OnPropertyChanged("MyMailAdresses");
            }

        }
        public void EditChoosenFolder()
        {
            var dialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                Title = "Select folder..."
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                MyRulePath = dialog.FileName;
            }
        }
        public void EditChoosenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                MyRulePath = openFileDialog.FileName;
            }
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private List<string> ConvertObjectToList(object choosenMailToEdit)
        {
            List<Object> collection = new List<Object>((IEnumerable<Object>)choosenMailToEdit);
            List<string> mailAdresses = new List<string>();
            if (collection.Count != 0)
            {
                foreach (var item in collection)
                {
                    mailAdresses.Add(item.ToString());
                }
            }
            return mailAdresses;
        }
        private ObservableCollection<string> ConvertListToCollection(List<string> listToConvert)
        {
            ObservableCollection<string> vs = new ObservableCollection<string>();
            foreach (var item in listToConvert)
            {
                vs.Add(item);
            }
            return vs;
        }

    }
}
