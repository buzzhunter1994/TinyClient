using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Windows.Documents;

namespace TinyClient.Api
{

    public class MyTabItem : NotifyPropertyChanged
    {
        private string _title;
        public string ForwardMessages;
        public FlowDocument FlowDocument;
        public TextPointer CaretPosition;

        private bool _isChat1;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Description { get; set; }

        public string Photo { get; set; }

        public string RawTitle { get; set; }

        public string Unread { get; set; }

        public bool Status { get; set; }

        public string ToolTip { get; set; }

        public bool IsSelected { get; set; }

        public bool IsChat
        {
            get { return false;/* Description.GetParametr("chat_id").Length > 0; */}
        }

        public Uri Content { get; set; }
        public ModernFrame Frame { get; set; }
        public ModernWindow FloatWindows { get; set; }
    }
}