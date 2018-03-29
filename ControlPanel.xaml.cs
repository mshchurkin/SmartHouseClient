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
    /// Логика взаимодействия для ControlPanel.xaml
    /// </summary>
    public partial class ControlPanel : Window
    {
        String SERVER_PATH = "http://167.99.141.138:8080/api/";
        public string TOKEN = "";
        public ControlPanel(User user)
        {
            InitializeComponent();
            TOKEN = user.token;
            if (user.integrator == false)
            {
                addScenario.Visibility = Visibility.Hidden;
                deleteScenario.Visibility = Visibility.Hidden;
                housesTab.Visibility = Visibility.Hidden;
                addSensorBtn.Visibility = Visibility.Hidden;
                addGadgetBtn.Visibility = Visibility.Hidden;
                addUserBtn.Visibility = Visibility.Hidden;
            }
            else
            {
                settingsTab.Visibility = Visibility.Hidden;
            }
            LoadData();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionSelect subWindow = new SubscriptionSelect();
            subWindow.ShowDialog();
        }


        private void floorCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void conditionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void conditionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void floorCheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }



        private void floorCheckBox_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void conditionCheckBox_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void addSensorBtn_Click(object sender, RoutedEventArgs e)
        {
            AddSensor addSensor = new AddSensor("");
            addSensor.Show();
        }

        private void addHouseBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditSensor editSensor = new EditSensor(false);
            editSensor.Show();
        }

        private void addUserBtn_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
        }

        private void addScenario_Click(object sender, RoutedEventArgs e)
        {
            ScenarioCreator scenarioCreator = new ScenarioCreator("");
        }

        private void delSensor_Click(object sender, RoutedEventArgs e)
        {
            Sensor s = sensorsGrid.SelectedItem as Sensor;
            using (var httpClient = new HttpClient())
            {
                String request = SERVER_PATH + "/sensorDelete/"+s.id+"/" + TOKEN;
                var json = httpClient.GetStringAsync(request).Result;
            }
        }

        async void TimerCheck()
        {
            do
            {
                await GetData(1000);
                
            } while (true);
        }

        Task GetData(int time)
        {
            return Task.Run(() => {
                Thread.Sleep(time);
                LoadData();
            });
        }

        private void LoadData()
        {
            Sensors sensors = new Sensors();
            List<Sensor> readySensors = new List<Sensor>();
            using (var httpClient = new HttpClient())
            {
                String request = SERVER_PATH + "/sensors/5abaac6d9433eed9c80fa69a/" + TOKEN;
                var json = httpClient.GetStringAsync(request).Result;
                sensors = JsonConvert.DeserializeObject<Sensors>(json.ToString());
            }
            readySensors = sensors.sensors;
            sensorsGrid.Items.Clear();
            sensorsGrid.ItemsSource = readySensors;

            Actors actors = new Actors();
            List<Sensor> readyActors = new List<Sensor>(); using (var httpClient = new HttpClient())
            {
                String request = SERVER_PATH + "/actors/5abaac6d9433eed9c80fa69a/" + TOKEN;
                var json = httpClient.GetStringAsync(request).Result;
                actors = JsonConvert.DeserializeObject<Actors>(json.ToString());
            }
            actorsGrid.Items.Clear();
            actorsGrid.ItemsSource = readyActors;
        }

        private void deleteGadgetBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
