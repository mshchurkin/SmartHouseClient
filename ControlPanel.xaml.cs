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
    /// Логика взаимодействия для ControlPanel.xaml
    /// </summary>
    public partial class ControlPanel : Window
    {
        String SERVER_PATH = "http://167.99.141.138:8080/api/";
        public string TOKEN = "";
        public string HOUSE_ID_SENSORS = "";
        public string HOUSE_ID_ACTORS = "";
        public string HOUSE_ID_SCENARIOS = "";
        public string HOUSE_ID_JOURNALS = "";

        ObservableCollection<House> readyHouses = new ObservableCollection<House>();
        ObservableCollection<Sensor> readySensors = new ObservableCollection<Sensor>();
        ObservableCollection<Actor> readyActors = new ObservableCollection<Actor>();
        ObservableCollection<Event> readyJournals = new ObservableCollection<Event>();
        ObservableCollection<Scenario> readyScenarios = new ObservableCollection<Scenario>();  

        public ControlPanel(User user)
        {
            InitializeComponent();
            TOKEN = user.token;
            if (user.isIntegrator == false)
            {
                HOUSE_ID_SENSORS = user.houseId;
                HOUSE_ID_ACTORS = user.houseId;
                HOUSE_ID_SCENARIOS = user.houseId;
                HOUSE_ID_JOURNALS = user.houseId;

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
                Houses houses = new Houses();
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "houses/" + TOKEN;
                    var json = httpClient.GetStringAsync(request).Result;
                    houses = JsonConvert.DeserializeObject<Houses>(json.ToString());
                }
                readyHouses = new ObservableCollection<House>(houses.houses);


                housesCombo.ItemsSource = readyHouses;
                housesCombo.DisplayMemberPath = "name";
                housesCombo.SelectedIndex = 1;
                housesACT.ItemsSource = readyHouses;
                housesACT.DisplayMemberPath = "name";
                housesACT.SelectedIndex = 1;
                housesCombo2.ItemsSource = readyHouses;
                housesCombo2.DisplayMemberPath = "name";
                housesCombo2.SelectedIndex = 1;
                houseComboScen.ItemsSource = readyHouses;
                houseComboScen.DisplayMemberPath = "name";
                houseComboScen.SelectedIndex = 1;
                housesComboBox.ItemsSource = readyHouses;
                housesComboBox.DisplayMemberPath = "name";
                housesComboBox.SelectedIndex = 1;


                if (readyHouses.Count != 0)
                {
                    HOUSE_ID_SENSORS = readyHouses[1].id;
                    HOUSE_ID_ACTORS = readyHouses[1].id;
                    HOUSE_ID_JOURNALS = readyHouses[1].id;
                }
                housesDataGrid.ItemsSource = readyHouses;

            }

            LoadData(user.isIntegrator);

            sensorsGrid.ItemsSource = readySensors;
            actorsGrid.ItemsSource = readyActors;
            journalGrid.ItemsSource = readyJournals;
            scenarioDataGrid.ItemsSource = readyScenarios;

            TimerCheck(user.isIntegrator);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionSelect subWindow = new SubscriptionSelect();
            subWindow.ShowDialog();
        }



        private void addSensorBtn_Click(object sender, RoutedEventArgs e)
        {
            AddSensor addSensor = new AddSensor(HOUSE_ID_SENSORS, TOKEN);
            addSensor.Show();
        }

        private void addHouseBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "/houseAdd/" + houseAddressTextBox.Text + "/" + TOKEN;
                    var json = httpClient.GetStringAsync(request).Result;
                }
            }
            catch (Exception em) { }
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditSensor editSensor = new EditSensor(false);
            editSensor.Show();
        }

        private void addUserBtn_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow(SERVER_PATH, TOKEN);
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
                String request = SERVER_PATH + "/sensorDelete/" + s.id + "/" + TOKEN;
                var json = httpClient.GetStringAsync(request).Result;
            }
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
            try
            {
                if (housesNeeded)
                {
                    Houses houses = new Houses();
                    using (var httpClient = new HttpClient())
                    {
                        String request = SERVER_PATH + "houses/" + TOKEN;
                        var json = httpClient.GetStringAsync(request).Result;
                        houses = JsonConvert.DeserializeObject<Houses>(json.ToString());
                    }
                    readyHouses = new ObservableCollection<House>(houses.houses);
                }

                Sensors sensors = new Sensors();

                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "sensors/" + HOUSE_ID_SENSORS + "/" + TOKEN;
                    try
                    {
                        var json = httpClient.GetStringAsync(request).Result;
                        sensors = JsonConvert.DeserializeObject<Sensors>(json.ToString());
                    }
                    catch (Exception e)
                    {
                    }
                }


                Actors actors = new Actors();

                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "actors/" + HOUSE_ID_ACTORS + "/" + TOKEN;
                    try
                    {
                        var json = httpClient.GetStringAsync(request).Result;
                        actors = JsonConvert.DeserializeObject<Actors>(json.ToString());
                    }
                    catch (Exception e)
                    {
                    }
                }

                Events events = new Events();

                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "historyList/" + HOUSE_ID_JOURNALS + "/" + TOKEN;
                    try
                    {
                        var json = httpClient.GetStringAsync(request).Result;
                        events = JsonConvert.DeserializeObject<Events>(json.ToString());
                    }
                    catch (Exception e)
                    {
                    }
                }
                if (sensors.sensors != null)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readySensors = new ObservableCollection<Sensor>(sensors.sensors);
                        sensorsGrid.ItemsSource = null;
                        sensorsGrid.ItemsSource = readySensors;

                    });
                }
                else
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readySensors.Clear();
                        sensorsGrid.ItemsSource = null;
                    });
                }
                if (actors.actors != null)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readyActors = new ObservableCollection<Actor>(actors.actors);
                        actorsGrid.ItemsSource = null;
                        actorsGrid.ItemsSource = readyActors;
                    });
                }
                else
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readyActors.Clear();
                        actorsGrid.ItemsSource = null;
                    });
                }
                if (events.events != null)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readyJournals = new ObservableCollection<Event>(events.events);
                        journalGrid.ItemsSource = null;
                        journalGrid.ItemsSource = readyJournals;

                    });
                }
                else
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readyJournals.Clear();
                    });
                }
            }
            catch (Exception e) { }
        }

        private void deleteGadgetBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Actor a = actorsGrid.SelectedItem as Actor;
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "/actorDelete/" + a.id + "/" + TOKEN;
                    var json = httpClient.GetStringAsync(request).Result;
                }
            }
            catch (Exception ed)
            {
            }
        }
        private void delUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User u = usersDataGrid.SelectedItem as User;
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "/userDelete/" + u.login + "/" + TOKEN;
                    var json = httpClient.GetStringAsync(request).Result;
                }
            }
            catch (Exception ed)
            {
            }
        }

        private void housesACT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (housesACT.SelectedItem != null)
            {
                House hACT = housesACT.SelectedItem as House;
                HOUSE_ID_ACTORS = hACT.id;
            }
        }

        private void housesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (housesCombo.SelectedItem != null)
            {
                House hSEN = housesCombo.SelectedItem as House;
                HOUSE_ID_SENSORS = hSEN.id;
            }
        }

        private void housesCombo2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (housesCombo2.SelectedItem != null)
            {
                House hEVENT = housesCombo2.SelectedItem as House;
                HOUSE_ID_SENSORS = hEVENT.id;
            }
        }

        private void delHouseBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                House h = housesDataGrid.SelectedItem as House;
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "/houseDelete/" + h.id + "/" + TOKEN;
                    var json = httpClient.GetStringAsync(request).Result;
                }
            }
            catch (Exception ed) { }
        }
    }
}
