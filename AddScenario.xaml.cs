﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddScenario.xaml
    /// </summary>
    public partial class AddScenario : Window
    {
        String SERVER_PATH = "";
        String TOKEN = "";
        ObservableCollection<House> readyHouses = new ObservableCollection<House>();
        String HOUSE_ID = "";

        public AddScenario(String SERVER_PATH, String TOKEN)
        {
            this.SERVER_PATH = SERVER_PATH;
            this.TOKEN = TOKEN;
            InitializeComponent();
            Houses houses = new Houses();
            using (var httpClient = new HttpClient())
            {
                String request = SERVER_PATH + "houses/" + TOKEN;
                var json = httpClient.GetStringAsync(request).Result;
                houses = JsonConvert.DeserializeObject<Houses>(json.ToString());
            }
            readyHouses = new ObservableCollection<House>(houses.houses);

            houseComboBox.ItemsSource = readyHouses;
            houseComboBox.DisplayMemberPath = "name";
            houseComboBox.SelectedIndex = 0;

            if (readyHouses.Count != 0)
            {
                HOUSE_ID = readyHouses.First().id;
            }
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                House h = houseComboBox.SelectedItem as House;
                HOUSE_ID = h.id;
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "scenarioAdd/" + HOUSE_ID + "/" + nameTextBox.Text + "/" + TOKEN;
                    var json = httpClient.GetStringAsync(request).Result;
                    this.Close();
                }
            }
            catch (Exception ed) { }
        }
    }
}
