using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
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
using MySql.Data.MySqlClient;

namespace FastFood {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>s
    public partial class MainWindow : Window {
        public MainWindow () {
            InitializeComponent ();
            ver.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void Login_ButtonClick (object sender, RoutedEventArgs e) {
            Login ();
        }

        private void Login_EnterKey (object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                Login ();
            }
        }

        private void Login () {
            String username = "";
            String password = "";
            String query;
            String ruta;
            MySqlCommand cmd;
            MySqlConnection con = null;
            username = usernameInput.Text;
            password = passwordInput.Password;

            try {
                ConfigurationHandler Config = new ConfigurationHandler ();
                String host = Config.getSetting ("host", "Connection");
                String port = Config.getSetting ("port", "Connection");
                String database = Config.getSetting ("database", "Connection");
                String user = Config.getSetting ("username", "Connection");
                String pass = Config.getSetting ("password", "Connection");
                ruta = "Data Source=" + host + ";port=" + port + ";Database=" + database + ";Uid=" + user + ";Password=" + pass;
                con = new MySqlConnection (ruta);
                con.Open ();
                query = "SELECT password from users where username= ?username;";
                cmd = new MySqlCommand (query, con);
                cmd.Parameters.AddWithValue ("?username", username);
                System.Data.IDataReader dr;
                dr = cmd.ExecuteReader ();
                if (dr.Read ()) {
                    String user_hash = dr.GetString (0);
                    bool valid = BCrypt.Net.BCrypt.Verify (password, user_hash);
                    if (valid) { // Si se ha autenticado correctamente:

                        //Generar token
                        String Token = BCrypt.Net.BCrypt.HashPassword (username + new Random ().Next () + DateTime.Now.ToString ("dd-MM-yyyy HH:mm:ss") + new Random ().Next ());
                        //Lo enviamos a la base de datos
                        try {
                            MySqlConnection co = new MySqlConnection (ruta);
                            co.Open ();
                            query = "UPDATE users SET token = ?token where username= ?username;";
                            MySqlCommand command = new MySqlCommand (query, co);
                            command.Parameters.AddWithValue ("?token", Token);
                            command.Parameters.AddWithValue ("?username", username);
                            System.Data.IDataReader test = command.ExecuteReader ();
                            if (test.Read ()) {
                                MessageBox.Show ("Error al guardar token.");
                            }
                        } finally {
                            if (con != null) {
                                con.Close ();
                            }
                        }
                        //Guardar token de manera local
                        // Muestra el token ConfigurationManager.AppSettings["token"];
                        var configFile = ConfigurationManager.OpenExeConfiguration (ConfigurationUserLevel.None);
                        var settings = configFile.AppSettings.Settings;
                        settings.Remove ("token");
                        settings.Remove ("username");
                        settings.Add ("token", Token);
                        settings.Add ("username", username);
                        configFile.Save (ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection (configFile.AppSettings.SectionInformation.Name);

                        //Finaliza el guardado del token

                        dashboard form = new dashboard ();
                        this.Hide ();
                        form.ShowDialog ();
                    } else {
                        MessageBox.Show ("Contraseña incorrecta.", "Fallo de autenticación", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                } else {
                    MessageBox.Show ("Usuario incorrecto.", "Fallo de autenticación", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } catch (Exception exc) {
                MessageBox.Show ("Ups! Ha habido un error de conexión con la base de datos:\n" + exc.Message.ToString (), "Error de conexión", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            } finally {
                if (con != null) {
                    con.Close ();
                }
            }

        }

        private void Connection_Settings (object sender, RoutedEventArgs e) {
            connection_settings form = new connection_settings ();
            form.ShowDialog ();
        }

        private void Window_Loaded (object sender, RoutedEventArgs e) {
            if (ConfigurationManager.AppSettings["token"] != null) // Si tenemos guardado un token de inicio de sesion
            {
                ConfigurationHandler Config = new ConfigurationHandler ();
                String host = Config.getSetting ("host", "Connection");
                String port = Config.getSetting ("port", "Connection");
                String database = Config.getSetting ("database", "Connection");
                String user = Config.getSetting ("username", "Connection");
                String pass = Config.getSetting ("password", "Connection");
                String ruta = "Data Source=" + host + ";port=" + port + ";Database=" + database + ";Uid=" + user + ";Password=" + pass;
                String query = "SELECT token from users where username= ?username;";
                MySqlCommand cmd;
                MySqlConnection con = null;

                try {
                    con = new MySqlConnection (ruta);
                    con.Open ();
                    cmd = new MySqlCommand (query, con);
                    cmd.Parameters.AddWithValue ("?username", ConfigurationManager.AppSettings["username"]);
                    System.Data.IDataReader dr;
                    dr = cmd.ExecuteReader ();
                    if (dr.Read ()) {
                        String token = dr.GetString (0);
                        if (token == ConfigurationManager.AppSettings["token"]) {
                            dashboard form = new dashboard ();
                            this.Hide ();
                            form.ShowDialog ();
                        } else {
                            var configFile = ConfigurationManager.OpenExeConfiguration (ConfigurationUserLevel.None);
                            var settings = configFile.AppSettings.Settings;
                            settings.Remove ("token");
                            configFile.Save (ConfigurationSaveMode.Modified);
                            ConfigurationManager.RefreshSection (configFile.AppSettings.SectionInformation.Name);
                        }
                    }
                } catch (Exception exc) {
                    MessageBox.Show ("Ups! Ha habido un error de conexión con la base de datos:\n" + exc.Message.ToString (), "Error de conexión", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                } finally {
                    if (con != null) {
                        con.Close ();
                    }
                }

            }
        }

    }
}