﻿using Newtonsoft.Json;
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
        String SERVER_PATH = "";
        public string TOKEN = "";
        public string HOUSE_ID_SENSORS = "";
        public string HOUSE_ID_ACTORS = "";
        public string HOUSE_ID_SCENARIOS = "";
        public string HOUSE_ID_JOURNALS = "";
        public string HOUSE_ID_USERS = "";

        ObservableCollection<House> readyHouses = new ObservableCollection<House>();
        ObservableCollection<Sensor> readySensors = new ObservableCollection<Sensor>();
        ObservableCollection<Actor> readyActors = new ObservableCollection<Actor>();
        ObservableCollection<Event> readyJournals = new ObservableCollection<Event>();
        ObservableCollection<Scenario> readyScenarios = new ObservableCollection<Scenario>();
        ObservableCollection<User> readyUsers = new ObservableCollection<User>();


        public ControlPanel(User user,String SERVER_PATH)
        {
            InitializeComponent();
            TOKEN = user.token;
            this.SERVER_PATH = SERVER_PATH;
            if (user.userType != "INTEGRATOR")
            {
                HOUSE_ID_SENSORS = user.houseId;
                HOUSE_ID_ACTORS = user.houseId;
                HOUSE_ID_SCENARIOS = user.houseId;
                HOUSE_ID_JOURNALS = user.houseId;
                HOUSE_ID_SENSORS = user.houseId;

                addScenario.Visibility = Visibility.Hidden;
                deleteScenario.Visibility = Visibility.Hidden;
                housesTab.Visibility = Visibility.Hidden;
                usersTab.Visibility = Visibility.Hidden;
                addSensorBtn.Visibility = Visibility.Hidden;
                addGadgetBtn.Visibility = Visibility.Hidden;
                addUserBtn.Visibility = Visibility.Hidden;
                delUser.Visibility = Visibility.Hidden;
                housesCombo.Visibility = Visibility.Hidden;
                housesACT.Visibility = Visibility.Hidden;
                housesCombo2.Visibility = Visibility.Hidden;
                housesComboScen.Visibility = Visibility.Hidden;
                housesComboBox.Visibility = Visibility.Hidden;
                l1.Visibility = Visibility.Hidden;
                l2.Visibility = Visibility.Hidden;
                l3.Visibility = Visibility.Hidden;
                l4.Visibility = Visibility.Hidden;
                l5.Visibility = Visibility.Hidden;
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

                if (readyHouses.Count != 0)
                {
                    housesCombo.ItemsSource = readyHouses;
                    housesCombo.DisplayMemberPath = "name";
                    housesCombo.SelectedIndex = 0;
                    housesACT.ItemsSource = readyHouses;
                    housesACT.DisplayMemberPath = "name";
                    housesACT.SelectedIndex = 0;
                    housesCombo2.ItemsSource = readyHouses;
                    housesCombo2.DisplayMemberPath = "name";
                    housesCombo2.SelectedIndex = 0;
                    housesComboScen.ItemsSource = readyHouses;
                    housesComboScen.DisplayMemberPath = "name";
                    housesComboScen.SelectedIndex = 0;
                    housesComboBox.ItemsSource = readyHouses;
                    housesComboBox.DisplayMemberPath = "name";
                    housesComboBox.SelectedIndex = 0;



                    HOUSE_ID_SENSORS = readyHouses[0].id;
                    HOUSE_ID_ACTORS = readyHouses[0].id;
                    HOUSE_ID_JOURNALS = readyHouses[0].id;
                    HOUSE_ID_USERS = readyHouses[0].id;
                    HOUSE_ID_SCENARIOS = readyHouses[0].id;
                }
                housesDataGrid.ItemsSource = readyHouses;

            }

            LoadData(user.userType);

            sensorsGrid.ItemsSource = readySensors;
            actorsGrid.ItemsSource = readyActors;
            journalGrid.ItemsSource = readyJournals;
            scenarioDataGrid.ItemsSource = readyScenarios;
            usersDataGrid.ItemsSource = readyUsers;

            TimerCheck(user.userType);
            TimerCheck2();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionSelect subWindow = new SubscriptionSelect();
            subWindow.ShowDialog();
        }



        private void addSensorBtn_Click(object sender, RoutedEventArgs e)
        {
            AddSensor addSensor = new AddSensor(HOUSE_ID_SENSORS, TOKEN, SERVER_PATH);
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
                    houseAddressTextBox.Text = String.Empty;
                }
            }
            catch (Exception em) { }
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Sensor s = sensorsGrid.SelectedItem as Sensor;
            var final = SensorsStore.sensors.Where(x => x.id == s.id);
            if (final.Count() != 0)
            {
                Sensor finalS = final.First();
                EditSensor editSensor = new EditSensor(finalS, TOKEN, SERVER_PATH);
                editSensor.Show();
            }
        }

        private void addUserBtn_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow(SERVER_PATH, TOKEN);
            signUpWindow.Show();
        }

        private void addScenario_Click(object sender, RoutedEventArgs e)
        {
            AddScenario addScenario = new AddScenario(SERVER_PATH, TOKEN);
            addScenario.Show();
        }

        private void delSensor_Click(object sender, RoutedEventArgs e)
        {
            Sensor s = sensorsGrid.SelectedItem as Sensor;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "/sensorDelete/" + s.id + "/" + TOKEN;
                    var json = httpClient.GetStringAsync(request).Result;
                }
            }
            catch (Exception ex) { }
        }
        async void TimerCheck2()
        {
            do
            {
                await GetData2(30000);

            } while (true);
        }

        Task GetData2(int time)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(time);
                CheckDanger();
            });
        }

        async void TimerCheck(String userType)
        {
            do
            {
                await GetData(5000, userType);

            } while (true);
        }

        Task GetData(int time, String userType)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(time);
                LoadData(userType);
                CheckDanger();
            });
        }

        private void CheckDanger()
        {
            using (var httpClient = new HttpClient())
            {
                Extremes extremes = new Extremes();
                String request = SERVER_PATH + "extremeList/" + HOUSE_ID_SENSORS + "/" + TOKEN;
                try
                {
                    var json = httpClient.GetStringAsync(request).Result;
                    extremes = JsonConvert.DeserializeObject<Extremes>(json.ToString());
                }
                catch (Exception edv) { }
                List<Extreme> exCheck = extremes.extremes;
                try
                {
                    if (exCheck != null)
                    {
                        foreach (Extreme e in exCheck)
                        {
                            Thread.Sleep(5000);
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                lError.Content = "Нешататная ситуация! ID датчика: " + e.sensorId + ", значение: " + e.value;
                            });
                        }
                    }
                }
                catch (Exception exc) { }
            }
        }
        private void LoadData(String userType)
        {
            try
            {
                if (userType == "INTEGRATOR")
                {
                    Houses houses = new Houses();
                    using (var httpClient = new HttpClient())
                    {
                        String request = SERVER_PATH + "houses/" + TOKEN;
                        try
                        {
                            var json = httpClient.GetStringAsync(request).Result;
                            houses = JsonConvert.DeserializeObject<Houses>(json.ToString());
                        }
                        catch (Exception edv) { }
                    }
                    if (houses.houses != null)
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            readyHouses = new ObservableCollection<House>(houses.houses);
                            housesDataGrid.ItemsSource = null;
                            housesDataGrid.ItemsSource = readyHouses;

                        });
                    }
                    else
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            readyHouses.Clear();
                            housesDataGrid.ItemsSource = null;
                        });
                    }
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

                Scenarios scenarios = new Scenarios();

                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "scenarios/" + HOUSE_ID_SCENARIOS + "/" + TOKEN;
                    try
                    {
                        var json = httpClient.GetStringAsync(request).Result;
                        scenarios = JsonConvert.DeserializeObject<Scenarios>(json.ToString());
                    }
                    catch (Exception e)
                    {
                    }
                }

                Users users = new Users();

                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "users/" + HOUSE_ID_USERS + "/" + TOKEN;
                    try
                    {
                        var json = httpClient.GetStringAsync(request).Result;
                        users = JsonConvert.DeserializeObject<Users>(json.ToString());
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
                        journalGrid.ItemsSource = null;

                    });
                }

                if (scenarios.scenarios != null)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readyScenarios = new ObservableCollection<Scenario>(scenarios.scenarios);
                        scenarioDataGrid.ItemsSource = null;
                        scenarioDataGrid.ItemsSource = readyScenarios;

                    });
                }
                else
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readyScenarios.Clear();
                        scenarioDataGrid.ItemsSource = null;

                    });
                }

                if (users.users != null)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readyUsers = new ObservableCollection<User>(users.users);
                        usersDataGrid.ItemsSource = null;
                        usersDataGrid.ItemsSource = readyUsers;

                    });
                }
                else
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readyUsers.Clear();
                        usersDataGrid.ItemsSource = null;

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

        }

        private void housesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void housesCombo2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

                Houses houses = new Houses();
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "houses/" + TOKEN;
                    var json = httpClient.GetStringAsync(request).Result;
                    houses = JsonConvert.DeserializeObject<Houses>(json.ToString());
                }
                readyHouses = new ObservableCollection<House>(houses.houses);

                if (readyHouses.Count != 0)
                {
                    housesCombo.ItemsSource = null;
                    housesCombo.ItemsSource = readyHouses;
                    housesCombo.DisplayMemberPath = "name";
                    housesCombo.SelectedIndex = 0;

                    housesACT.ItemsSource = null;
                    housesACT.ItemsSource = readyHouses;
                    housesACT.DisplayMemberPath = "name";
                    housesACT.SelectedIndex = 0;

                    housesCombo2.ItemsSource = null;
                    housesCombo2.ItemsSource = readyHouses;
                    housesCombo2.DisplayMemberPath = "name";
                    housesCombo2.SelectedIndex = 0;

                    housesComboScen.ItemsSource = null;
                    housesComboScen.ItemsSource = readyHouses;
                    housesComboScen.DisplayMemberPath = "name";
                    housesComboScen.SelectedIndex = 0;

                    housesComboBox.ItemsSource = null;
                    housesComboBox.ItemsSource = readyHouses;
                    housesComboBox.DisplayMemberPath = "name";
                    housesComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ed) { }
        }

        private void deleteScenario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Scenario s = scenarioDataGrid.SelectedItem as Scenario;
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "/scenarioDelete/" + s.id + "/" + TOKEN;
                    var json = httpClient.GetStringAsync(request).Result;
                }
            }
            catch (Exception ed)
            {
            }
        }
        private void addGadgetBtn_Click(object sender, RoutedEventArgs e)
        {
            AddGadget addGadget = new AddGadget(HOUSE_ID_SENSORS, TOKEN, SERVER_PATH);
            addGadget.Show();
        }

        private void housesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void scenarioDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Scenario s = scenarioDataGrid.SelectedItem as Scenario;
            if (s != null)
            {
                ScenarioCreator sc = new ScenarioCreator(TOKEN,SERVER_PATH,s.id,s.houseId);
                sc.Show();
            }
        }

        private void housesCombo_DropDownClosed(object sender, EventArgs e)
        {
            if (housesCombo.SelectedItem != null)
            {
                House hSEN = housesCombo.SelectedItem as House;
                HOUSE_ID_SENSORS = hSEN.id;
            }
        }

        private void housesComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (housesComboBox.SelectedItem != null)
            {
                House hUSER = housesComboBox.SelectedItem as House;
                HOUSE_ID_USERS = hUSER.id;
            }
        }

        private void housesACT_DropDownClosed(object sender, EventArgs e)
        {
            if (housesACT.SelectedItem != null)
            {
                House hACT = housesACT.SelectedItem as House;
                HOUSE_ID_ACTORS = hACT.id;
            }
        }

        private void housesComboScen_DropDownClosed(object sender, EventArgs e)
        {
            if (housesComboScen.SelectedItem != null)
            {
                House hSCEN = housesComboScen.SelectedItem as House;
                HOUSE_ID_SCENARIOS = hSCEN.id;
            }
        }

        private void housesCombo2_DropDownClosed(object sender, EventArgs e)
        {
            if (housesCombo2.SelectedItem != null)
            {
                House hEVENT = housesCombo2.SelectedItem as House;
                HOUSE_ID_JOURNALS = hEVENT.id;
            }
        }

        private void actorsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Actor a = actorsGrid.SelectedItem as Actor;
            var final = SensorsStore.actors.Where(x => x.id == a.id);
            if (final.Count() != 0)
            {
                Actor finalA = final.First();
                EditGadget edit = new EditGadget(finalA, TOKEN, SERVER_PATH);
                edit.Show();
            }
        }
    }
}
