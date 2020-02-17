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
            //List<String> Test = new List<String>();
            var numberButtons = Enumerable.Range(1, 30)
    .Select(r => new KeyValuePair<string, string>(r.ToString(), r.ToString()))
    .ToList();
            numberButtonItems.ItemsSource = numberButtons;
        }

        private void table_button_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
