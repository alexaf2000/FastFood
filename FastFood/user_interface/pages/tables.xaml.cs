﻿using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FastFood.user_interface.pages
{
    /// <summary>
    /// Lógica de interacción para home.xaml
    /// </summary>
    public partial class tables : UserControl
    {
        public tables()
        {
            InitializeComponent();
            //var numberButtons = Enumerable.Range(1, 30).Select(r => new KeyValuePair<string, bool>(r.ToString(), false)).ToList();
            //numberButtonItems.ItemsSource = numberButtons;


            MySqlConnection con = null;
            try
            {
                String query = "SELECT * FROM `tables`";
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
                adapter.Fill(ds, "LoadDataBinding");
                TablesList.DataContext = ds;
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

        private void table_button_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
