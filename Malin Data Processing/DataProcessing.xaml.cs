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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Malin_Data_Processing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DataProcessing : Window
    {
        public DataProcessing()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        // 4.4	Create a button and associated click method that will call the LoadData and ShowAllSensorData methods. The input parameters are empty, and the return type is void.
        /*private void LoadData()
        {
            ReadData readData = new ReadData();
            int maxDataSize = 400;
            SensorAList.Clear();
            SensorBList.Clear();
            for(int x = 0; x < maxDataSize; x++)
            {
                SensorAList.AddLast(readData.SensorA(double.Parse(Mu.SelectedValue.ToString()), double.Parse(Sigma.SelectedValue.ToString())));
                SensorBList.AddLast(readData.SensorA(double.Parse(Mu.SelectedValue.ToString()), double.Parse(Sigma.SelectedValue.ToString())));
            }
        }*/

        // 4.3	Create a custom method called “ShowAllSensorData” which will display both LinkedLists in a ListView. Add column titles “Sensor A” and “Sensor B” to the ListView. The input parameters are 
        /*private void ShowAllSensorData()
        {
            RawData.Items.Clear();
            int max = NumberOfNodes(SensorAList);
            for(int i = 0; i < max; i++)
            {
                RawData.Items.Add(new { sensorA = SensorAList.ElementAt(i).ToString(), sensorB = SensorAList.ElementAt(i).ToString() })
            }
        }*/
    }
}
