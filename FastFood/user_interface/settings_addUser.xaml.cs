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
using System.Windows.Shapes;

namespace FastFood.user_interface
{
    /// <summary>
    /// Lógica de interacción para settings_addUser.xaml
    /// </summary>
    public partial class settings_addUser : Window
    {
        public settings_addUser()
        {
            InitializeComponent();
        }

        private void userCreate_Click(object sender, RoutedEventArgs e)
        {
            if (userName.Text != "" && userUsername.Text != "" && userMail.Text != "" && userPassword.Password != "") {
                String query;
                MySqlCommand cmd;
                MySqlConnection con = null;
                try
                {
                    ConfigurationHandler Config = new ConfigurationHandler();
                    String host = Config.getSetting("host", "Connection");
                    String port = Config.getSetting("port", "Connection");
                    String database = Config.getSetting("database", "Connection");
                    String user = Config.getSetting("username", "Connection");
                    String pass = Config.getSetting("password", "Connection");
                    String ruta = "Data Source=" + host + ";port=" + port + ";Database=" + database + ";Uid=" + user + ";Password=" + pass;
                    con = new MySqlConnection(ruta);
                    con.Open();
                    query = "INSERT INTO users (name,username, password,email) values (?name,?username,?password,?email);";
                    cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("?name", userName.Text);
                    cmd.Parameters.AddWithValue("?username", userUsername.Text);
                    cmd.Parameters.AddWithValue("?password", BCrypt.Net.BCrypt.HashPassword(userPassword.Password));
                    cmd.Parameters.AddWithValue("?email", userMail.Text);
                    System.Data.IDataReader dr;
                    dr = cmd.ExecuteReader();
                    Window parentWindow = Window.GetWindow(this);
                    if (dr.Read())
                    {
                        MessageBox.Show("Error al añadir usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Usuario añadido correctamente.");
                        this.Close();
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
            else
            {
                MessageBox.Show("Has de completar todos los campos","Datos incompletos",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
