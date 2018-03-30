using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Scenario sc1;
        ObservableCollection<ScenarioItem> readyItems = new ObservableCollection<ScenarioItem>();

        public ScenarioCreator(Scenario sc, String TOKEN)
        {
            this.TOKEN = TOKEN;
            sc1 = sc;
            InitializeComponent();
            LoadData();
            itemsGrid.ItemsSource = readyItems;
            TimerCheck();

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

        async void TimerCheck()
        {
            do
            {
                await GetData(5000);

            } while (true);
        }

        Task GetData(int time)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(time);
                LoadData();
            });
        }

        private void LoadData()
        {
            try
            {

                ScenarioItems scItems = new ScenarioItems();
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "scenario/" + sc1.id + "/" + TOKEN;
                    try
                    {
                        var json = httpClient.GetStringAsync(request).Result;
                        scItems = JsonConvert.DeserializeObject<ScenarioItems>(json.ToString());
                    }
                    catch (Exception e) { }
                }
                if (scItems.scenarioItems != null)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readyItems = new ObservableCollection<ScenarioItem>(scItems.scenarioItems);
                        itemsGrid.ItemsSource = null;
                        itemsGrid.ItemsSource = readyItems;

                    });
                }
                else
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readyItems.Clear();
                        itemsGrid.ItemsSource = null;
                    });
                }

            }
            catch (Exception e) { }
        }
    }
}
