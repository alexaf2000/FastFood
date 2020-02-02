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

using IniParser;
using IniParser.Model;

namespace FastFood
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("config.ini");
            string databaseUsername = data["Connection"]["database"];
            Console.WriteLine(databaseUsername);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
