using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FastFood.user_interface
{
    /// <summary>
    /// Lógica de interacción para dashboard.xaml
    /// </summary>
    public partial class dashboard : Window
    {

        private bool Expanded = false;
        private bool ForceClose = false;


        public dashboard()
        {
            InitializeComponent();
        }


        private void Dashboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ForceClose) {
            MessageBoxResult reply = MessageBox.Show("Se va a cerrar FastFood.\n¿Estás seguro de que deseas salir?", "Cerrando FastFood", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (reply == MessageBoxResult.Yes)
            {
                App.Current.MainWindow.Close();
            }
            else
            {
                e.Cancel = true;
            }
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            GridLength OpenSize = new GridLength(180, GridUnitType.Pixel);
            sidebar.Width = OpenSize;
            int from, to;
            if (Expanded) {
                from = 180;
                to = 60;
                Expanded = false;
            }
            else {
                from = 60;
                to = 180;
                Expanded = true;
            }
            Storyboard storyboard = new Storyboard();

            Duration duration = new Duration(TimeSpan.FromMilliseconds(200));
            CubicEase ease = new CubicEase { EasingMode = EasingMode.EaseOut };
            DoubleAnimation animation = new DoubleAnimation();
            animation.EasingFunction = ease;
            animation.Duration = duration;
            storyboard.Children.Add(animation);
            animation.From = from;
            animation.To = to;
            Storyboard.SetTarget(animation, sidebar);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(ColumnDefinition.MaxWidth)"));

            storyboard.Begin();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            settings.Remove("token");
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

            ForceClose = true;
            App.Current.MainWindow.Show();
            this.Close();
        }
    }
}
