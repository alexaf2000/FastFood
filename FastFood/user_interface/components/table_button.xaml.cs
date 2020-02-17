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
            DataContext = this;
        }

        

        public static readonly DependencyProperty TableNumberProperty = DependencyProperty.Register("TableNumber",typeof(String),typeof(table_button));
        public String TableNumber
        {
            
            get
            {
  
                return tableID.Text;
            }
            set
            {
     
                tableID.Text = value;
            }
            
    


        }


        public static readonly DependencyProperty serviceProperty = DependencyProperty.Register("service", typeof(String), typeof(table_button));


        public String service
        {
            get
            {
                return serviceON.IsVisible.ToString();
            }
            set
            {
                if (value != "false" || value != null)
                {
                    serviceON.Visibility = Visibility.Visible;
                }
                else
                {
                    serviceON.Visibility = Visibility.Hidden;
                }
            }
        }

    }
}
