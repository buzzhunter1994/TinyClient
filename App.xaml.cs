using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TinyClient
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public sealed partial class App : Application
    {
        private void StartupHandler(object sender, System.Windows.StartupEventArgs e)
        {
            //Elysium.Manager.Apply(this, Elysium.Theme.Dark,Elysium.AccentBrushes.Orange,Elysium.AccentBrushes.Mango);
        }
    }
}
