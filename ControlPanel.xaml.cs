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
        public ControlPanel()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionSelect subWindow = new SubscriptionSelect();
            subWindow.ShowDialog();
        }

        private void floorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            floorCheckBox.Content = "Температура: " + (int)(floorSlider.Value) + " град.С";
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

        private void conditionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            conditionCheckBox.Content = "Температура: " + (int)(conditionSlider.Value) + " град.С";

        }

        private void floorCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (floorCheckBox.IsChecked == true)
            {
                floorSlider.IsEnabled = true;
            }
            else
            {
                floorSlider.IsEnabled = false;
            }
        }

        private void conditionCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (conditionCheckBox.IsChecked == true)
            {
                conditionSlider.IsEnabled = true;

            }
            else
            {
                conditionSlider.IsEnabled = false;

            }
        }
    }
}
