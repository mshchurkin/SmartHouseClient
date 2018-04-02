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
        String SERVER_PATH = "";
        string TOKEN = "";
        string SCENARIO_ID = "";
        string HOUSE_ID = "";

        ObservableCollection<Condition> readyConditions = new ObservableCollection<Condition>();
        ObservableCollection<Action> readyActions = new ObservableCollection<Action>();


        public ScenarioCreator(String TOKEN, String SERVER_PATH, String SCENARIO_ID, String HOUSE_ID)
        {
            this.TOKEN = TOKEN;
            this.SERVER_PATH = SERVER_PATH;
            this.SCENARIO_ID = SCENARIO_ID;
            this.HOUSE_ID = HOUSE_ID;

            InitializeComponent();
            LoadData();
            TimerCheck();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddScenarioItem addScenarioItem = new AddScenarioItem(SERVER_PATH,TOKEN, HOUSE_ID, SCENARIO_ID);
            addScenarioItem.Show();
        }

        private void addElem_Click(object sender, RoutedEventArgs e)
        {
            AddScenarioItem addScenarioItem = new AddScenarioItem(SERVER_PATH,TOKEN, HOUSE_ID, SCENARIO_ID);
            addScenarioItem.Show();
        }

        private void delElem_Click(object sender, RoutedEventArgs e)
        {
            Condition c = ifGrid.SelectedItem as Condition;
            if (c != null)
            {
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "scenarioRemoveCondition/" + SCENARIO_ID + "/" + c.id + "/" + TOKEN;
                    try
                    {
                        var json = httpClient.GetStringAsync(request).Result;
                    }
                    catch (Exception ev) { }
                }
            }
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

                Conditions scItems = new Conditions();
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "scenarioConditions/" + SCENARIO_ID + "/" + TOKEN+"";
                    try
                    {
                        var json = httpClient.GetStringAsync(request).Result;
                        scItems = JsonConvert.DeserializeObject<Conditions>(json.ToString());
                    }
                    catch (Exception e) { }
                }
                if (scItems.conditions != null)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readyConditions = new ObservableCollection<Condition>(scItems.conditions);
                        ifGrid.ItemsSource = null;
                        ifGrid.ItemsSource = readyConditions;

                    });
                }
                else
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readyConditions.Clear();
                        ifGrid.ItemsSource = null;
                    });
                }

            }
            catch (Exception e) { }

            try
            {

                Actions actions = new Actions();
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "scenarioActions/" + SCENARIO_ID + "/" + TOKEN+"";
                    try
                    {
                        var json = httpClient.GetStringAsync(request).Result;
                        actions = JsonConvert.DeserializeObject<Actions>(json.ToString());
                    }
                    catch (Exception e) { }
                }
                if (actions.actions != null)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readyActions = new ObservableCollection<Action>(actions.actions);
                        thenGrid.ItemsSource = null;
                        thenGrid.ItemsSource =  readyActions;

                    });
                }
                else
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        readyActions.Clear();
                        thenGrid.ItemsSource = null;
                    });
                }

            }
            catch (Exception e) { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddResult addResult = new AddResult(SERVER_PATH,TOKEN, HOUSE_ID, SCENARIO_ID);
            addResult.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Action a = thenGrid.SelectedItem as Action;
            if (a != null)
            {
                using (var httpClient = new HttpClient())
                {
                    String request = SERVER_PATH + "scenarioRemoveAction/" + SCENARIO_ID + "/" + a.id + "/" + TOKEN+"";
                    try
                    {
                        var json = httpClient.GetStringAsync(request).Result;
                    }
                    catch (Exception ev) { }
                }
            }
        }
    }
}
