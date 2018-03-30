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
    /// Interaction logic for EditSensor.xaml
    /// </summary>
    public partial class EditSensor : Window
    {
        string TOKEN = "";
        string SERVER_PATH = "";
        bool isDiscrete = false;
        Sensor sk;
        public EditSensor(Sensor s , String TOKEN)
        {
            sk = s;
            isActiveCheckBox.IsChecked = s.active;
            checkTxt.Text = s.extreme;
            InitializeComponent();
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
                if ((k < 0) || (k > 1024))
                {
                    MessageBox.Show("Неверный ввод порога значения. Доступные значения от 0 до 1024 для аналоговых датчиков и занчения 0/1 для дискретных");

                }
                else
                {
                    if ((isDiscrete== true) && (k > 1))
                    {
                        MessageBox.Show("Неверный ввод порога значения. Доступные значения от 0 до 1024 для аналоговых датчиков и занчения 0/1 для дискретных");
                    }
                    else
                    {
                        String ACTIVE = "FALSE";
                        if (isActiveCheckBox.IsChecked == true)
                            ACTIVE = "true";
                        try
                        {
                            using (var httpClient = new HttpClient())
                            {
                                String request = SERVER_PATH + "sensorEdit/" + sk.houseId + "/" + TOKEN + "/" + ACTIVE + "/" + checkTxt.Text;
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
