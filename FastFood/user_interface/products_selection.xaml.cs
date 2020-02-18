using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para products_selection.xaml
    /// </summary>
    public partial class products_selection : Window
    {

        public String serviceID;

        public products_selection(String serviceID)
        {
            InitializeComponent();

            this.serviceID = serviceID;

            List<Products> items = new List<Products>();

            MySqlConnection con = null;
            try
            {
                String query = "SELECT *  FROM `products`";
                ConfigurationHandler Config = new ConfigurationHandler();
                String host = Config.getSetting("host", "Connection");
                String port = Config.getSetting("port", "Connection");
                String database = Config.getSetting("database", "Connection");
                String user = Config.getSetting("username", "Connection");
                String pass = Config.getSetting("password", "Connection");
                String ruta = "Data Source=" + host + ";port=" + port + ";Database=" + database + ";Uid=" + user + ";Password=" + pass;
                con = new MySqlConnection(ruta);
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                ProductsListBox.ItemsSource = ds.Tables[0].DefaultView;
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



        public class Products
        {
            public string Name { get; set; }

            public string Description { get; set; }

            public Image Image { get; set; }
        }

        private void ProductsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductsListBox.SelectedItem != null)
            {

                DataRowView selected = (DataRowView) ProductsListBox.SelectedItem;

                String query;
                MySqlCommand cmd;
                MySqlConnection con = null;

                //Añadir este id a la comanda
                try {
                ConfigurationHandler Config = new ConfigurationHandler();
                String host = Config.getSetting("host", "Connection");
                String port = Config.getSetting("port", "Connection");
                String database = Config.getSetting("database", "Connection");
                String user = Config.getSetting("username", "Connection");
                String pass = Config.getSetting("password", "Connection");
                String ruta = "Data Source=" + host + ";port=" + port + ";Database=" + database + ";Uid=" + user + ";Password=" + pass;
                con = new MySqlConnection(ruta);
                con.Open();
                query = "INSERT INTO consumitions (serviceID, productID) values (?serviceID, ?productID);";
                cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?productID", selected["id"]);
                cmd.Parameters.AddWithValue("?serviceID", this.serviceID);
                    System.Data.IDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                        MessageBox.Show("Error al añadir.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
                catch (Exception exc)
            {
                MessageBox.Show("Hubo un error al añadir el producto: \n" + exc.Message.ToString() + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

            this.Close();
            }
        }
    }
}
