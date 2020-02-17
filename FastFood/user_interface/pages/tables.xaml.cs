using FastFood.user_interface.components;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class tables : UserControl, INotifyPropertyChanged
    {
        public String SelectedTable
        {
            get { return (String)GetValue(SelectedTableProperty); }
            set { 
                SetValue(SelectedTableProperty, value);
                NotifyPropertyChanged("Title");
            }
        }

        public static DependencyProperty SelectedTableProperty =
           DependencyProperty.Register("SelectedTable", typeof(String), typeof(tables));

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        public tables()
        {
            InitializeComponent();
            //var numberButtons = Enumerable.Range(1, 30).Select(r => new KeyValuePair<string, bool>(r.ToString(), false)).ToList();
            //numberButtonItems.ItemsSource = numberButtons;
            updateTables();

           
        }

        private void updateTables()
        {
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
        private void table_button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var TableButton = (table_button) e.Source;
            var TableNumber = TableButton.GetValue(table_button.TableNumberProperty);
            SelectedTable = TableNumber.ToString();
        }

        private void serviceStart_Click(object sender, RoutedEventArgs e)
        {
            Boolean alreadyWorking = false;
            int serviceID = -1;

            String query;
            MySqlCommand cmd;
            MySqlConnection con = null;

            //Comprobar si existe
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
                query = "SELECT `actualServiceID` FROM tables WHERE id = ?selectedTableID;";
                cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?selectedTableID", SelectedTable.ToString());
                System.Data.IDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if(!dr.IsDBNull(0))
                    {
                        alreadyWorking = true;
                    }


                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hubo un error al comrpobar la existencia de un pedido en esta mesa: \n" + exc.Message.ToString() + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            Console.WriteLine(alreadyWorking.ToString());

            if (alreadyWorking == false) {
                Console.WriteLine("Hola");
            //Crear servicio
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
                query = "INSERT INTO services (start) values (CURRENT_TIMESTAMP); SELECT id FROM `services` WHERE `start`= CURRENT_TIMESTAMP;";
                cmd = new MySqlCommand(query, con);

                System.Data.IDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    serviceID = Int32.Parse(dr.GetString(0));
                }
                else
                {
                    MessageBox.Show("Error al crear el servicio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hubo un error al añadir el usuario: \n" + exc.Message.ToString() + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

            //Asignar a mesa actual
            if(serviceID != -1)
            {
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
                    query = "UPDATE `tables` SET `actualServiceID` = ?serviceID WHERE `tables`.`id` = ?selectedTableID;";
                    cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("?serviceID", serviceID);
                    cmd.Parameters.AddWithValue("?selectedTableID", SelectedTable.ToString());
                    System.Data.IDataReader dr;
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Error al asignar el servicio a esta mesa.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        //TODO OK
                        //Recargar la vista
                        updateTables();
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Hubo un error al añadir el usuario: \n" + exc.Message.ToString() + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void serviceFinish_Click(object sender, RoutedEventArgs e)
        {

            Boolean setTime = false;

            String query;
            MySqlCommand cmd;
            MySqlConnection con = null;


            //Crear servicio
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
                query = "UPDATE `services` SET `end` = CURRENT_TIMESTAMP WHERE `services`.`id` = (SELECT actualServiceID FROM tables where id = 1)";
                cmd = new MySqlCommand(query, con);

                System.Data.IDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // error
                    MessageBox.Show("Error al cerrar el servicio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    setTime = true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hubo un error al crear el pedido: \n" + exc.Message.ToString() + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

            //Asignar a mesa actual
            if (setTime)
            {
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
                    query = "UPDATE `tables` SET `actualServiceID` = null WHERE `tables`.`id` = ?selectedTableID;";
                    cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("?selectedTableID", SelectedTable.ToString());
                    System.Data.IDataReader dr;
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Error al limpiar la mesa.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        //TODO OK
                        //Recargar la vista
                        updateTables();
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Hubo un error al cerrar el pedido: \n" + exc.Message.ToString() + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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