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
        public products_selection()
        {
            InitializeComponent();

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





            //items.Add(new Products() { Name = "John Doe"});
            //items.Add(new Products() { Name = "Jane Doe", Description = "sdfsdf", Image = byteArrayToImage(dr.GetSqlBytes(dr.GetOrdinal("img")).Buffer)});
            //items.Add(new Products() { Name = "Sammy Doe" });
            //ProductsListBox.ItemsSource = items;

        }

        private Image byteArrayToImage(object buffer)
        {
            throw new NotImplementedException();
        }

        public class Products
        {
            public string Name { get; set; }

            public string Description { get; set; }

            public Image Image { get; set; }
        }
    }
}
