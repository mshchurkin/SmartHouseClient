using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для ControlPanel.xaml
    /// </summary>
    public partial class ControlPanel : Window
    {
        public string TOKEN = "";
        public ControlPanel(bool isIntegrator, string token)
        {
            InitializeComponent();
            TOKEN = token;
            if (isIntegrator == false)
            {
                addScenario.Visibility = Visibility.Hidden;
                deleteScenario.Visibility = Visibility.Hidden;
                housesTab.Visibility = Visibility.Hidden;
                addSensorBtn.Visibility = Visibility.Hidden;
                addGadgetBtn.Visibility = Visibility.Hidden;
                addUserBtn.Visibility = Visibility.Hidden;
            }
            else
            {
                settingsTab.Visibility = Visibility.Hidden;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionSelect subWindow = new SubscriptionSelect();
            subWindow.ShowDialog();
        }


        private void floorCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void conditionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void conditionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void floorCheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }



        private void floorCheckBox_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void conditionCheckBox_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void addSensorBtn_Click(object sender, RoutedEventArgs e)
        {
            AddSensor addSensor = new AddSensor("");
            addSensor.Show();
        }

        private void addHouseBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditSensor editSensor = new EditSensor(false);
            editSensor.Show();
        }

        private void addUserBtn_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
        }

        private void addScenario_Click(object sender, RoutedEventArgs e)
        {
            ScenarioCreator scenarioCreator = new ScenarioCreator("");
        }
    }
}
