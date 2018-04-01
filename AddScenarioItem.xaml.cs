using Newtonsoft.Json;
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
    /// Логика взаимодействия для AddScenarioItem.xaml
    /// </summary>
    public partial class AddScenarioItem : Window
    {
        String SERVER_PATH = "";
        String TOKEN = "";
        ObservableCollection<Sensor> readySensors = new ObservableCollection<Sensor>();
        String HOUSE_ID = "";
        String SENSOR_ID = "";
        String SCENARIO_ID = "";


        public AddScenarioItem(String SERVER_PATH,String TOKEN, String HOUSE_ID, String SCENARIO_ID)
        {
            InitializeComponent();
            Sensors sensors = new Sensors();
            this.TOKEN = TOKEN;
            this.SERVER_PATH = SERVER_PATH;
            this.SCENARIO_ID = SCENARIO_ID;
            this.HOUSE_ID = HOUSE_ID;
            using (var httpClient = new HttpClient())
            {
                String request = SERVER_PATH + "sensors/" + HOUSE_ID+"/"+TOKEN;
                var json = httpClient.GetStringAsync(request).Result;
                sensors = JsonConvert.DeserializeObject<Sensors>(json.ToString());
            }
            readySensors = new ObservableCollection<Sensor>(sensors.sensors);

            sensorBox.ItemsSource = readySensors;
            sensorBox.DisplayMemberPath = "fieldName";
            sensorBox.SelectedIndex = 0;

            if (readySensors.Count != 0)
            {
                 SENSOR_ID= readySensors.First().id;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Sensor s;
            if (SensorsStore.sensors.Where(x => x.id == SENSOR_ID).Count() != 0)
            {
                s = SensorsStore.sensors.Where(x => x.id == SENSOR_ID).First();
                int k = 0;
                if (Int32.TryParse(checkTxt.Text, out k) == false)
                {
                    MessageBox.Show("Неверный ввод значения.");
                }
                else
                {
                    if ((s.sensorType == "DISCRETE")&&(k>1))
                    {
                        MessageBox.Show("Неверный ввод значения.");
                    }
                    else
                    {
                        if ((s.sensorType == "DISCRETE") && (k < 0))
                        {
                            MessageBox.Show("Неверный ввод значения.");
                        }
                        else
                        {
                            try
                            {
                                using (var httpClient = new HttpClient())
                                {
                                    String request = SERVER_PATH + "scenarioAddCondition/" + SCENARIO_ID + "/" +SENSOR_ID+"/"+ k + "/" + TOKEN;
                                    var json = httpClient.GetStringAsync(request).Result;
                                    this.Close();
                                }
                            }

                            catch (Exception em) { }
                        }
                    }
                }
            }
        }
    }
}
