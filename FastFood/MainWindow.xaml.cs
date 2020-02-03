using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using FastFood.user_interface;

namespace FastFood
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConfigurationHandler Config = new ConfigurationHandler();
            Console.WriteLine("Dato: "+Config.getSetting("database","Connection"));
        }

        private void Login_ButtonClick(object sender, RoutedEventArgs e)
        {
            String username = "";
            String password = "";
            String query;
            String ruta;
            MySqlCommand cmd;
            MySqlConnection con = null;
            username = usernameInput.Text;
            password = passwordInput.Text;

            try
            {
                ConfigurationHandler Config = new ConfigurationHandler();
                String host = Config.getSetting("host", "Connection");
                String port = Config.getSetting("port", "Connection");
                String database = Config.getSetting("database", "Connection");
                String user = Config.getSetting("username", "Connection");
                String pass = Config.getSetting("password", "Connection");
                ruta = "Data Source="+ host + ";port=" + port + ";Database=" + database + ";Uid="+user+";Password="+pass;
                con = new MySqlConnection(ruta);
                con.Open();
                query = "SELECT username from users where username= ?username and password=?password;";
                cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?username", username);
                cmd.Parameters.AddWithValue("?password", password);
                System.Data.IDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dashboard form = new dashboard();
                    form.ShowDialog();
                }
                else
                {
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
    }
}
