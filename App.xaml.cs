using System.Windows;
using TinyClient.Api;
namespace TinyClient
{
    public partial class App 
    {
        private void StartupHandler(object sender, System.Windows.StartupEventArgs e)
        {
            Common.TinyMainWindow.InitializeComponent();
            Common.TinyMainWindow.Show();
        }
    }
}
