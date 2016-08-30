using System;
using System.Windows.Controls;
using TinyClient.Api;
namespace TinyClient
{
    public class ControlNotificationMessage : UserControl
    {
        /*
        private void Expander_Expanded(object sender, RoutedEventArgs e) {
            if (IsInitialized) {
                SendControl1.Visibility = Visibility.Visible;
                ScrollViewer1.Visibility = Visibility.Collapsed;
            }
        
        }
    
        private void Expander1_Collapsed(object sender, RoutedEventArgs e) {
            SendControl1.Visibility = Visibility.Collapsed;
            ScrollViewer1.Visibility = Visibility.Visible;
        }
    
        private void ScrollViewer1_ScrollChanged(object sender, ScrollChangedEventArgs e) {
            if (!bool.Parse(ScrollViewer1.Tag)) {
                if ((ScrollViewer1.ComputedVerticalScrollBarVisibility == Visibility.Visible)) {
                    PreviewTextBlock1.Visibility = Visibility.Collapsed;
                    Expander1.IsExpanded = false;
                }
                else {
                    Expander1.IsExpanded = true;
                }
            
                ScrollViewer1.Tag = true;
            }
        
        }
         */

        private void ControlNotificationMessage_OnInitialized(object sender, EventArgs e)
        {
            /* SendControl1.RootBorder.Background = Brushes.White;
             SendControl1.TextBox1.Foreground = Brushes.Black;
             SendControl1.AddAttachmentButton.Visibility = Visibility.Collapsed;
             SendControl1.SeparatorRectangle1.Visibility = Visibility.Collapsed;
             SendControl1.SendSettingButton.Visibility = Visibility.Collapsed;
             SendControl1.SmileListBox.Visibility = Visibility.Collapsed;
             SendControl1.KeyDown += new System.EventHandler(this.SendControl1_KeyDown);*/
            //SendControl1.OnSending +=; // TODO: Warning!!!! NULL EXPRESSION DETECTED...

        }
    }
}