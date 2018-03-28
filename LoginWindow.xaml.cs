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

namespace SmartHouseClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        
        public LoginWindow()
        {
            InitializeComponent();
            //Login();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //    Socket s = new Socket(AddressFamily.InterNetwork,
            //SocketType.Stream,
            //ProtocolType.Tcp);
            //    s.Connect();
            ControlPanel controlPanel = new ControlPanel();
            controlPanel.Show();
            this.Close();
            controlPanel.Activate();
        }



        private void Login()
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://167.99.137.168:22");
            request.Method = "POST";
            
            request.AllowAutoRedirect = false;
            request.Accept = "*/*";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            StringBuilder postData = new StringBuilder();
            postData.Append("grant_type=" + "password" + "&");
            postData.Append("username=" + "root" + "&");
            postData.Append("password=" + "pass1234");

            using (StreamWriter stOut = new StreamWriter(request.GetRequestStream(), Encoding.UTF8))
            {
                stOut.Write(postData);
                stOut.Close();
            }
        }
    }
}
