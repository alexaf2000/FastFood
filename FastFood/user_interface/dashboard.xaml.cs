using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FastFood.user_interface
{
    /// <summary>
    /// Lógica de interacción para dashboard.xaml
    /// </summary>
    public partial class dashboard : Window , INotifyPropertyChanged
    {

        private bool Expanded = false;
        private bool ForceClose = false;

        public String userName
        {
            get
            {                
                return (String)GetValue(userNameProperty);
            }
            set
            {
                SetValue(userNameProperty, value);
                NotifyPropertyChanged("userName");
            }
        }

        public static DependencyProperty userNameProperty =
           DependencyProperty.Register("userNameProperty", typeof(String), typeof(dashboard));

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        public dashboard()
        {
            InitializeComponent();

            // Set onload this table
            contentContainer.Content = new user_interface.pages.tables();


            ConfigurationHandler Config = new ConfigurationHandler();
            String host = Config.getSetting("host", "Connection");
            String port = Config.getSetting("port", "Connection");
            String database = Config.getSetting("database", "Connection");
            String user = Config.getSetting("username", "Connection");
            String pass = Config.getSetting("password", "Connection");
            String ruta = "Data Source=" + host + ";port=" + port + ";Database=" + database + ";Uid=" + user + ";Password=" + pass;
            String query = "SELECT name from users where username= ?username;";
            MySqlCommand cmd;
            MySqlConnection con = null;

            try
            {
                con = new MySqlConnection(ruta);
                con.Open();
                cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?username", ConfigurationManager.AppSettings["username"]);
                System.Data.IDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    this.userName = dr.GetString(0);
                }
                else
                {
                    this.userName = "usuario";
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ups! Ha habido un error de conexión con la base de datos:\n" + exc.Message.ToString(), "Error de conexión", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        private void Dashboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ForceClose) {
            MessageBoxResult reply = MessageBox.Show("Se va a cerrar FastFood.\n¿Estás seguro de que deseas salir?", "Cerrando FastFood", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (reply == MessageBoxResult.Yes)
            {
                App.Current.MainWindow.Close();
            }
            else
            {
                e.Cancel = true;
            }
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            GridLength OpenSize = new GridLength(180, GridUnitType.Pixel);
            sidebar.Width = OpenSize;
            int from, to;
            if (Expanded) {
                from = 180;
                to = 60;
                Expanded = false;
            }
            else {
                from = 60;
                to = 180;
                Expanded = true;
            }
            Storyboard storyboard = new Storyboard();

            Duration duration = new Duration(TimeSpan.FromMilliseconds(150));
            CubicEase ease = new CubicEase { EasingMode = EasingMode.EaseOut };
            DoubleAnimation animation = new DoubleAnimation();
            animation.EasingFunction = ease;
            animation.Duration = duration;
            storyboard.Children.Add(animation);
            animation.From = from;
            animation.To = to;
            Storyboard.SetTarget(animation, sidebar);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(ColumnDefinition.MaxWidth)"));

            storyboard.Begin();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            settings.Remove("token");
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

            ForceClose = true;
            App.Current.MainWindow.Show();
            this.Close();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            settings form = new settings();
            form.ShowDialog();
        }

        private void btnTables_Click(object sender, RoutedEventArgs e)
        {
            contentContainer.Content = new user_interface.pages.tables();
        }
    }
}
