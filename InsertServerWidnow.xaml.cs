﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace SmartHouseClient
{
    /// <summary>
    /// Interaction logic for InsertServerWidnow.xaml
    /// </summary>
    public partial class InsertServerWidnow : Window
    {
        public InsertServerWidnow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String SERVER_PATH = "http://" + loginTextBox.Text + ":8080/api/";

            using (var httpClient = new HttpClient())
            {
                var json = httpClient.GetStringAsync(SERVER_PATH + "ping").Result;
                if (json.ToString() != null)
                {
                    LoginWindow loginScreen = new LoginWindow(SERVER_PATH);
                    loginScreen.Show();
                    this.Close();
                }
            }
        }
    }
}
