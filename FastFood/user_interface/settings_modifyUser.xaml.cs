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
    /// Lógica de interacción para settings_modifyUser.xaml
    /// </summary>
    public partial class settings_modifyUser : Window
    {
        private String userOriginal;

        public settings_modifyUser(String username)
        {
            InitializeComponent();

            this.userOriginal = username;
            usernameOriginal.Text = this.userOriginal;

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
                query = "SELECT username,name,email FROM users WHERE username = ?userOriginal;";
                cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?userOriginal", userOriginal);
                cmd.Parameters.AddWithValue("?username", userUsername.Text);
                System.Data.IDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    userUsername.Text = dr.GetString(0);
                    userName.Text = dr.GetString(1);
                    userMail.Text = dr.GetString(2);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hubo un error al cargar el usuario: \n" + exc.Message.ToString() + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private void userModify_Click(object sender, RoutedEventArgs e)
        {
            if (userName.Text != "" && userUsername.Text != "" && userMail.Text != "")
            {
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
                    if (userPassword.Password == "") {
                        query = "UPDATE users SET name = ?name , username = ?username , email = ?email where username = ?userOriginal;";
                    }
                    else
                    {
                        query = "UPDATE users SET name = ?name , username = ?username , password = ?password ,email = ?email where username = ?userOriginal;";
                    }
                    cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("?name", userName.Text);
                    cmd.Parameters.AddWithValue("?userOriginal", userOriginal);
                    cmd.Parameters.AddWithValue("?username", userUsername.Text);
                    cmd.Parameters.AddWithValue("?password", BCrypt.Net.BCrypt.HashPassword(userPassword.Password));
                    cmd.Parameters.AddWithValue("?email", userMail.Text);
                    System.Data.IDataReader dr;
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Error al modificar el usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Cambios guardados satisfactoriamente.", "Añadido correctamente", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Hubo un error modificar el usuario: \n" + exc.Message.ToString() + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Has de completar todos los campos", "Datos incompletos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
