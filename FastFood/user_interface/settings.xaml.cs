﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

namespace FastFood.user_interface
{
    /// <summary>
    /// Lógica de interacción para settings.xaml
    /// </summary>
    public partial class settings : Window
    {
        public settings()
        {
            InitializeComponent();

            copyright.Text = copyright.Text + DateTime.Now.Year.ToString();
            version.Text = version.Text + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void users_GotFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Aqui");
        }
    }
}
