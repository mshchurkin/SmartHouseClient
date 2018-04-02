using Newtonsoft.Json;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddSensor : Window
    {
        public string SERVER_PATH = "";
        public string TOKEN = "";
        public string HOUSE_ID = "";
        public AddSensor(string HOUSE_ID, string TOKEN, string SERVER_PATH)
        {
            InitializeComponent();
            this.SERVER_PATH = SERVER_PATH;
            this.TOKEN = TOKEN;
            this.HOUSE_ID = HOUSE_ID;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int k = 0;
            if (Int32.TryParse(checkTxt.Text, out k) == false)
            {
                MessageBox.Show("Неверный ввод порога значения. ");
            }
            else
            {

                if ((discrete.IsChecked == true) && (k > 1))
                {
                    MessageBox.Show("Неверный ввод порога значения. ");
                }
                else
                {
                    if (Int32.TryParse(upTxt.Text, out k) == false)
                    {
                        MessageBox.Show("Неверный ввод верхнего порога значения. ");
                    }
                    else
                    {
                        if (Int32.TryParse(downTxt.Text, out k) == false)
                        {
                            MessageBox.Show("Неверный ввод нижнего порога значения. ");
                        }
                        else
                        {
                            if (Int32.Parse(downTxt.Text) >= Int32.Parse(upTxt.Text))
                            {
                                MessageBox.Show("Верхний порог меньше нижнего ");
                            }
                            else
                            {
                                Sensor s = new Sensor();
                                String TYPE = "ANALOG";
                                if (discrete.IsChecked == true)
                                    TYPE = "DISCRETE";
                                String ACTIVE = "FALSE";
                                try
                                {
                                    using (var httpClient = new HttpClient())
                                    {
                                        String request = SERVER_PATH + "sensorAdd/" + HOUSE_ID + "/" + TOKEN + "/" + nameBox.Text + "/" + TYPE + "/" + checkTxt.Text;
                                        var json = httpClient.GetStringAsync(request).Result;
                                        s = JsonConvert.DeserializeObject<Sensor>(json.ToString());
                                        s.MinValue = Int32.Parse(downTxt.Text);
                                        s.MaxValue = Int32.Parse(upTxt.Text);
                                        s.Start(SERVER_PATH, TOKEN);
                                        SensorsStore.sensors.Add(s);
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

        private void discrete_Checked(object sender, RoutedEventArgs e)
        {
            AnalogControls.Visibility = Visibility.Hidden;
        }

        private void analog_Checked(object sender, RoutedEventArgs e)
        {
            AnalogControls.Visibility = Visibility.Visible;
        }
    }
}
