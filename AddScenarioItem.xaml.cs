using System;
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
    /// Логика взаимодействия для AddScenarioItem.xaml
    /// </summary>
    public partial class AddScenarioItem : Window
    {
        String SERVER_PATH = "http://167.99.141.138:8080/api/";
        public string TOKEN = "";
        public AddScenarioItem(String TOKEN, String HOUSE_ID)
        {
            InitializeComponent();
            Sensors houses = new Sensors();
            //using (var httpClient = new HttpClient())
            //{
            //    String request = SERVER_PATH + "sensors/" + TOKEN;
            //    var json = httpClient.GetStringAsync(request).Result;
            //    houses = JsonConvert.DeserializeObject<Houses>(json.ToString());
            //}
            //readyHouses = new ObservableCollection<House>(houses.houses);

            //houseComboBox.ItemsSource = readyHouses;
            //houseComboBox.DisplayMemberPath = "name";
            //houseComboBox.SelectedIndex = 0;

            //if (readyHouses.Count != 0)
            //{
            //    HOUSE_ID = readyHouses.First().id;
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
