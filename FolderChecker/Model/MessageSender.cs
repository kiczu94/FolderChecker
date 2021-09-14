using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FolderChecker.View;
using FolderChecker.ViewModel;

namespace FolderChecker.Model
{
    public class MessageSender
    {
        private static string _emailAdress;
        private static string _password;

        public string MyEmailAdressSender
        {
            get { return _emailAdress; }
            set { _emailAdress = value; }
        }

        public string MyPassword
        {
            get { return _password; }
            set { _password = value; }
        }
        public MessageSender()
        {

        }
        public void GetEmailAdress()
        {
            EditSimpleTextWindow editSimpleText = new EditSimpleTextWindow("Podaj adres e-mail", MyEmailAdressSender);
            editSimpleText.ShowDialog();
            MyEmailAdressSender = editSimpleText.GetNewText();
        }
        public void GetPassword()
        {
            EditSimpleTextWindow editSimpleText = new EditSimpleTextWindow("Podaj hasło", String.Empty);
            editSimpleText.ShowDialog();
            MyPassword = editSimpleText.GetNewText();
        }
        public void onWatcherInvoked(object source, WatcherInvokedEventArgs args)
        {
            MessageBox.Show($"{args.watcherPath}");
        }

    }
}
