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
    /// Interaction logic for EditGadget.xaml
    /// </summary>
    public partial class EditGadget : Window
    {
        string TOKEN = "";
        string SERVER_PATH = "";
        bool isDiscrete = false;
        Actor ak;
        public EditGadget(Actor a, String TOKEN, String SERVER_PATH)
        {
            ak = a;
            //isActiveCheckBox.IsChecked = s.active;
            InitializeComponent();
            checkTxt.Text = a.value.ToString();
            this.SERVER_PATH = SERVER_PATH;
            this.TOKEN = TOKEN;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int k = 0;
            if (Int32.TryParse(checkTxt.Text, out k) == false)
            {
                MessageBox.Show("Неверный ввод порога значения.");
            }
            else
            {

                if ((isDiscrete == true) && (k > 1))
                {
                    MessageBox.Show("Неверный ввод порога значения.");
                }
                else
                {

                    try
                    {
                        using (var httpClient = new HttpClient())
                        {
                            String request = SERVER_PATH + "actorNewValue/" + ak.id + "/" + TOKEN + "/" + checkTxt.Text;
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

