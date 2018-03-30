using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ScenarioCreator.xaml
    /// </summary>
    public partial class ScenarioCreator : Window
    {
        String SERVER_PATH = "http://167.99.141.138:8080/api/";
        public string TOKEN = "";
        public ScenarioCreator(string houseId)
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void addElem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void delElem_Click(object sender, RoutedEventArgs e)
        {

        }

        async void TimerCheck(bool housesNeeded)
        {
            do
            {
                await GetData(1000, housesNeeded);

            } while (true);
        }

        Task GetData(int time, bool housesNeeded)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(time);
                LoadData(housesNeeded);
            });
        }

        private void LoadData(bool housesNeeded)
        {
            //try
            //{
            //    if (housesNeeded)
            //    {
            //        Houses houses = new Houses();
            //        using (var httpClient = new HttpClient())
            //        {
            //            String request = SERVER_PATH + "houses/" + TOKEN;
            //            var json = httpClient.GetStringAsync(request).Result;
            //            houses = JsonConvert.DeserializeObject<Houses>(json.ToString());
            //        }
            //        readyHouses = new ObservableCollection<House>(houses.houses);
            //    }

            //}
        }
    }
}
