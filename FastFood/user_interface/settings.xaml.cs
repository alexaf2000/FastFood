using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

namespace FastFood.user_interface
{
    /// <summary>
    /// Lógica de interacción para settings.xaml
    /// </summary>
    public partial class settings : Window
    {
        public settings()
        {
            InitializeComponent();

            copyright.Text = copyright.Text + DateTime.Now.Year.ToString();
            version.Text = version.Text + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (users.IsSelected)
            {
                MySqlConnection con = null;
                try
                {
                    String query = "SELECT username as 'nombre de usuario',name as 'nombre',email as 'e-mail' from users";
                    ConfigurationHandler Config = new ConfigurationHandler();
                    String host = Config.getSetting("host", "Connection");
                    String port = Config.getSetting("port", "Connection");
                    String database = Config.getSetting("database", "Connection");
                    String user = Config.getSetting("username", "Connection");
                    String pass = Config.getSetting("password", "Connection");
                    String ruta = "Data Source=" + host + ";port=" + port + ";Database=" + database + ";Uid=" + user + ";Password=" + pass;
                    con = new MySqlConnection(ruta);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, con);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    usersDatagrid.ItemsSource = table.DefaultView;
                    usersDatagrid.AutoGenerateColumns = true;
                    usersDatagrid.CanUserAddRows = false;
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





     
}
