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
    /// Interaction logic for AddResult.xaml
    /// </summary>
    public partial class AddResult : Window
    {
        String SERVER_PATH = "";
        String TOKEN = "";
        ObservableCollection<Actor> readyActors = new ObservableCollection<Actor>();
        String HOUSE_ID = "";
        String GADGET_ID = "";
        String SCENARIO_ID = "";
        public AddResult(String SERVER_PATH,String TOKEN, String HOUSE_ID, String SCENARIO_ID)
        {
            InitializeComponent();
            Actors actors = new Actors();
            this.TOKEN = TOKEN;
            this.SERVER_PATH = SERVER_PATH;
            this.SCENARIO_ID = SCENARIO_ID;
            this.HOUSE_ID = HOUSE_ID;
            using (var httpClient = new HttpClient())
            {
                String request = SERVER_PATH + "actors/" + HOUSE_ID + "/" + TOKEN;
                var json = httpClient.GetStringAsync(request).Result;
                actors = JsonConvert.DeserializeObject<Actors>(json.ToString());
            }
            readyActors= new ObservableCollection<Actor>(actors.actors);

            gadgetBox.ItemsSource = readyActors;
            gadgetBox.DisplayMemberPath = "fieldName";
            gadgetBox.SelectedIndex = 0;

            if (readyActors.Count != 0)
            {
                GADGET_ID = readyActors.First().id;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Actor a;
            if (SensorsStore.actors.Where(x => x.id == GADGET_ID).Count() != 0)
            {
                a = SensorsStore.actors.Where(x => x.id == GADGET_ID).First();
                int k = 0;
                if (Int32.TryParse(setTxt.Text, out k) == false)
                {
                    MessageBox.Show("Неверный ввод значения.");
                }
                else
                {
                    if ((a.actorType == "DISCRETE") && (k > 1))
                    {
                        MessageBox.Show("Неверный ввод значения.");
                    }
                    else
                    {
                        if ((a.actorType == "DISCRETE") && (k < 0))
                        {
                            MessageBox.Show("Неверный ввод значения.");
                        }
                        else
                        {
                            try
                            {
                                using (var httpClient = new HttpClient())
                                {
                                    String request = SERVER_PATH + "scenarioAddAction/" + SCENARIO_ID + "/"+GADGET_ID+"/" + k + "/" + TOKEN;
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
