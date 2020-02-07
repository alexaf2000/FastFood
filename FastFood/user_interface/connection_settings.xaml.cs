using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FastFood.user_interface
{
    /// <summary>
    /// Lógica de interacción para connection_settings.xaml
    /// </summary>
    public partial class connection_settings : Window
    {
        public connection_settings()
        {
            InitializeComponent();
            ConfigurationHandler Config = new ConfigurationHandler();
            host.Text = Config.getSetting("host","Connection");
            port.Text = Config.getSetting("port", "Connection");
            user.Text = Config.getSetting("username", "Connection");
            password.Text = Config.getSetting("password", "Connection");
            database.Text = Config.getSetting("database", "Connection");

        }



        private void Save_Connection_Click(object sender, RoutedEventArgs e)
        {
            ConfigurationHandler Config = new ConfigurationHandler();
            Config.setSetting("host", "Connection", host.Text);
            Config.setSetting("port", "Connection", port.Text);
            Config.setSetting("username", "Connection", user.Text);
            Config.setSetting("password", "Connection", password.Text);
            Config.setSetting("database", "Connection", database.Text);
            MessageBox.Show("La configuración ha sido guardada correctamente.","Configuración guardada.",MessageBoxButton.OK,MessageBoxImage.Information);
            this.Close();
        }
    }
}
