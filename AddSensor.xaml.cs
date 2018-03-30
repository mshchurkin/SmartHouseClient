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
        String SERVER_PATH = "http://167.99.141.138:8080/api/";
        public string TOKEN = "";
        public string HOUSE_ID = "";
        public AddSensor(string HOUSE_ID, string TOKEN)
        {
            InitializeComponent();
            this.TOKEN = TOKEN;
            this.HOUSE_ID = HOUSE_ID;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int k = 0;
            if (Int32.TryParse(checkTxt.Text, out k) == false)
            {
                MessageBox.Show("Неверный ввод порога значения. Доступные значения от 0 до 1024 для аналоговых датчиков и занчения 0/1 для дискретных");
            }
            else
            {
                if ((k < 0)||(k>1024))
                {
                    MessageBox.Show("Неверный ввод порога значения. Доступные значения от 0 до 1024 для аналоговых датчиков и занчения 0/1 для дискретных");

                }
                else
                {
                    if ((discrete.IsChecked == true) && (k > 1))
                    {
                        MessageBox.Show("Неверный ввод порога значения. Доступные значения от 0 до 1024 для аналоговых датчиков и занчения 0/1 для дискретных");
                    }
                    else
                    {
                        String TYPE = "ANALOG";
                        if (discrete.IsChecked == true)
                            TYPE = "DISCRETE";
                        try
                        {
                            using (var httpClient = new HttpClient())
                            {
                                String request = SERVER_PATH + "/sensorAdd/" + HOUSE_ID + "/" + TOKEN + "/" + nameBox.Text + "/" + TYPE + "/" +checkTxt.Text;
                                var json = httpClient.GetStringAsync(request).Result;
                            }
                        }
                        catch (Exception em) { }
                    }
                }
            }
        }
    }
}
