using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace WPFGrowlNotification
{
    public class Notification : INotifyPropertyChanged
    {
        public int type;
        public int Index;
        public string date;
        private string message;
        public string Message
        {
            get { return message; }

            set
            {
                if (message == value) return;
                message = value;
                OnPropertyChanged("Message");
            }
        }

        private int id;
        public int Id
        {
            get { return id; }

            set
            {
                if (id == value) return;
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get { return imageUrl; }

            set
            {
                if (imageUrl == value) return;
                imageUrl = value;
                OnPropertyChanged("ImageUrl");
            }
        }

        private string title;
        public string Title
        {
            get { return title; }

            set
            {
                if (title == value) return;
                title = value;
                OnPropertyChanged("Title");
            }
        }

        UIElement m_content;
        public UIElement Content
        {
            get
            {
                return m_content;
            }
            set
            {
                m_content = value;
                OnPropertyChanged("Content");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Notifications : ObservableCollection<Notification> { }
}