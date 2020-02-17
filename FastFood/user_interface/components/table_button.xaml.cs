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

namespace FastFood.user_interface.components
{
    /// <summary>
    /// Lógica de interacción para table_button.xaml
    /// </summary>
    public partial class table_button : UserControl
    {
        public table_button()
        {
            InitializeComponent();
        }


        public String TableNumber
        {
            get { return (String)GetValue(TableNumberProperty); }
            set { SetValue(TableNumberProperty, value); }
        }

        public static DependencyProperty TableNumberProperty =
           DependencyProperty.Register("TableNumber", typeof(String), typeof(table_button));

        public bool Service
        {
            get { return (bool)GetValue(ServiceProperty); }
            set { SetValue(ServiceProperty, value); }
        }

        public static DependencyProperty ServiceProperty =
           DependencyProperty.Register("Service", typeof(bool), typeof(table_button));






    }
}
