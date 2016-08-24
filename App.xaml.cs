using System.Windows;
using System.Drawing;
using System.Windows.Media;
using TinyClient.Api;
using Un4seen.Bass;
namespace TinyClient
{
    public sealed partial class App : Application
    {
        private void StartupHandler(object sender, System.Windows.StartupEventArgs e)
        {
            Elysium.Manager.Apply(this, TinyClient.Properties.Settings.Default.theme, TinyClient.Properties.Settings.Default.accentColor, TinyClient.Properties.Settings.Default.contrastColor);
            Common.TinyMainWindow.InitializeComponent();
            Common.TinyMainWindow.Show();
        }
        public void ApplyTheme()
        {
            Elysium.Manager.Apply(this, Elysium.Theme.Dark);
        }
    }
}
