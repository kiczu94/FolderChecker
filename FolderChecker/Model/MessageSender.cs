using System;
using FolderChecker.View;
using System.IO;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using System.Windows;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderChecker.Model
{
    public class MessageSender
    {
        private bool alreadyWaiting = false;
        private List<MimeMessage> mimeMessages;
        private string _emailAdress;
        private string _password;
        public string MyEmailAdressSender
        {
            get { return _emailAdress; }
            set { _emailAdress = value; }
        }
        public string MyPassword
        {
            set { _password = value; }
        }
        public MessageSender()
        {
            mimeMessages = new List<MimeMessage>();
        }
        public void GetEmailAdress()
        {
            EditSimpleTextWindow editSimpleText = new EditSimpleTextWindow("Podaj adres e-mail", MyEmailAdressSender);
            editSimpleText.ShowDialog();
            MyEmailAdressSender = editSimpleText.GetNewText();
        }
        public void OnWatcherInvoked(object source, WatcherInvokedEventArgs args)
        {
            if (_password != null && _emailAdress != null)
            {
                mimeMessages.Add(GetMimeMessage(source, args, CreateText(args)));
                if (alreadyWaiting == false)
                {
                    SendMessages();
                }
            }
            else
            {
                MessageBox.Show("Nie wysłano maila- brak hasła i maila");
            }
        }
        private string CreateText(WatcherInvokedEventArgs args)
        {
            string textToreturn = string.Empty;
            switch (args.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    textToreturn = $"Utworzono nowy plik {args.Name} w folderze {GetFolderPath(args)}";
                    break;
                case WatcherChangeTypes.Renamed:
                    textToreturn = $"Zmieniono nazwę pliku z {args.OldName.Remove(0, args.OldName.LastIndexOf('\\') + 1)} na {args.Name.Remove(0, args.Name.LastIndexOf('\\') + 1)} w folderze {GetFolderPath(args)}";
                    break;
                case WatcherChangeTypes.Deleted:
                    textToreturn = $"Usunięto plik {args.Name.Remove(0, args.Name.LastIndexOf('\\') + 1)} w folderze {GetFolderPath(args)}";
                    break;
                case WatcherChangeTypes.Changed:
                    textToreturn = "1";
                    break;
            }
            return textToreturn;
        }
        private string GetFolderPath(WatcherInvokedEventArgs args)
        {
            string text = args.FullPath.Remove(args.FullPath.LastIndexOf('\\'), args.FullPath.Length - args.FullPath.LastIndexOf('\\'));
            return text;
        }
        private MimeMessage GetMimeMessage(object source, WatcherInvokedEventArgs args, string textToSend)
        {
            FileWatcher fileWatcher = GetFileWatcher(source);
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Folder Checker Bot", "testtkocz1@gmail.com"));
            foreach (var rule in fileWatcher.MyRules)
            {
                if (args.WatcherPath == rule.MyPathToTrack)
                {
                    foreach (var adres in rule.MyMailAdresses)
                    {
                        message.To.Add(MailboxAddress.Parse(adres));
                    }
                }
            }
            message.Subject = "Folder updated";
            message.Body = new TextPart("plain")
            {
                Text = textToSend
            };
            return message;
        }
        private FileWatcher GetFileWatcher(object source)
        {
            FileWatcher fileWatcher = new FileWatcher();
            if (source.GetType() == typeof(FileWatcher))
            {
                fileWatcher = (FileWatcher)source;
            }
            return fileWatcher;
        }
        public bool TryLoggin()
        {
            bool isEmailAndPasswordCorrect = false;
            SmtpClient smtp = new SmtpClient();
            try
            {
                smtp.Connect("smtp.gmail.com", 465, true);
                smtp.Authenticate(_emailAdress, _password);
                isEmailAndPasswordCorrect = true;
            }
            catch (Exception)
            {
                isEmailAndPasswordCorrect = false;
            }
            finally
            {
                smtp.Disconnect(true);
                smtp.Dispose();
            }
            return isEmailAndPasswordCorrect;
        }
        public async void SendMessages()
        {
            alreadyWaiting = true;
            await Task.Delay(20000);
            foreach (var message in mimeMessages)
            {
                SmtpClient smtp = new SmtpClient();
                try
                {
                    smtp.Connect("smtp.gmail.com", 465, true);
                    smtp.Authenticate(_emailAdress, _password);
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    //MetadataToken of an exception when adress email is uncorrect
                    if (ex.TargetSite.MetadataToken == 100665740 || ex.TargetSite.MetadataToken == 100665727)
                    {
                        MessageBox.Show($"Adres e-mail {message.To} nieprawidłowy");
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                finally
                {
                    smtp.Disconnect(true);
                    smtp.Dispose();
                }
            }
            mimeMessages.Clear();
        }
        private void CreateListOfEmail()
        {
            List<string> emailAdresses = new List<string>();
            foreach (var message in mimeMessages)
            {
                foreach (var emailAdress in message.To)
                {
                    if (!emailAdresses.Contains(emailAdress.ToString()))
                    {
                        emailAdresses.Add(emailAdress.ToString());
                    }
                }
            }
        }
    }
}
