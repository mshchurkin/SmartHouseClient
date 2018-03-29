using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Web;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace SmartHouseClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        String SERVER_PATH = "http://167.99.141.138:8080/api/";
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            //try
            //{
                using (var httpClient = new HttpClient())
                {
                    var json = httpClient.GetStringAsync(SERVER_PATH + "/auth/" + loginTextBox.Text + "/" + passBox.Password).Result;
                    user = JsonConvert.DeserializeObject<User>(json.ToString());
                }

                if (user.token.ToString() != String.Empty)
                {
                    ControlPanel controlPanel = new ControlPanel(user);
                    controlPanel.Show();
                    this.Close();
                }
            //}
            //catch (Exception ex) 
            //{
            //    errorLabel.Content = "Неправильный логин или пароль";
            //}
        }

        private void Login()
        {

        }
    }
}
