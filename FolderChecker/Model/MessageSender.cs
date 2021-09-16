﻿using System;
using FolderChecker.View;
using System.IO;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using System.Windows;

namespace FolderChecker.Model
{
    public class MessageSender
    {
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
            if (_password != null && _emailAdress != null)
            {
                SendMessage(GetMimeMessage(source, args, CreateText(args)));
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
                    textToreturn = $"Utworzono nowy plik {args.Name} w folderze {args.FullPath.Remove(args.FullPath.LastIndexOf('\\'), args.FullPath.Length - args.FullPath.LastIndexOf('\\'))}";
                    break;
                case WatcherChangeTypes.Renamed:
                    break;
                case WatcherChangeTypes.Deleted:
                    break;
                case WatcherChangeTypes.Changed:
                    break;
            }
            return textToreturn;
        }
        private MimeMessage GetMimeMessage(object source, WatcherInvokedEventArgs args, string textToSend)
        {
            FileWatcher fileWatcher = GetFileWatcher(source);
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Folder Checker Bot", "testtkocz1@gmail.com"));
            foreach (var rule in fileWatcher.MyRules)
            {
                if (args.watcherPath == rule.myPathToTrack)
                {
                    foreach (var adres in rule.myMailAdresses)
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
        private void SendMessage(MimeMessage message)
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                smtp.Disconnect(true);
                smtp.Dispose();
            }
        }
    }
}
