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
    /// Lógica de interacción para dashboard.xaml
    /// </summary>
    public partial class dashboard : Window
    {
        public dashboard()
        {
            InitializeComponent();
        }


        private void Dashboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
            MessageBoxResult reply = MessageBox.Show("Se va a cerrar FastFood.\n¿Estás seguro de que deseas salir?", "Cerrando FastFood", MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if (reply == MessageBoxResult.Yes)
            {
                App.Current.MainWindow.Close();
            }
            else{
                e.Cancel = true;
            }
        }


    }
}
