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
        private List<MimeMessage> mimeMessagesViaAdress = new List<MimeMessage>();
        private bool alreadyWaiting = false;
        private List<MimeMessage> mimeMessages;
        private List<string> emailAdresses;
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
            emailAdresses = new List<string>();
        }
        public void GetEmailAdress()
        {
            EditSimpleTextWindow editSimpleText = new EditSimpleTextWindow("Podaj adres e-mail", MyEmailAdressSender);
            editSimpleText.ShowDialog();
            MyEmailAdressSender = editSimpleText.GetNewText();
        }
        public void OnWatcherInvoked(object source, WatcherInvokedEventArgs args)
        {
            GetMimeMessage(source, args, CreateText(args));
            
            if (_password != null && _emailAdress != null)
            {
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
        private void GetMimeMessage(object source, WatcherInvokedEventArgs args, string textToSend)
        {
            FileWatcher fileWatcher = GetFileWatcher(source);
            foreach (var rule in fileWatcher.MyRules)
            {
                if (args.WatcherPath == rule.MyPathToTrack)
                {
                    foreach (var adres in rule.MyMailAdresses)
                    {
                        if (!emailAdresses.Contains(adres))
                        {
                            MimeMessage mimeMessage = new MimeMessage();
                            emailAdresses.Add(adres);
                            mimeMessage.To.Add(MailboxAddress.Parse(adres));
                            mimeMessage.From.Add(new MailboxAddress("Folder Checker Bot", "testtkocz1@gmail.com"));
                            mimeMessage.Body = new TextPart("plain")
                            {
                                Text = textToSend
                            };
                            mimeMessagesViaAdress.Add(mimeMessage);
                        }
                        else
                        {
                            foreach (var message in mimeMessagesViaAdress)
                            {
                                if (message.To.ToString()==adres)
                                {
                                    string helper = message.TextBody;
                                    helper += Environment.NewLine;
                                    helper += textToSend;
                                    message.Body = new TextPart("plain")
                                    {
                                        Text = helper
                                    };
                                }
                            }
                        }

                    }
                }
            }
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
            CreateListOfEmail();
            await Task.Delay(20000);
            
            foreach (var message in mimeMessagesViaAdress)
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
            alreadyWaiting = false;
            emailAdresses.Clear();
            mimeMessagesViaAdress.Clear();
        }
        private void CreateListOfEmail()
        {
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
