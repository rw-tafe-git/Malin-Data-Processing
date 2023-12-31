﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Galileo6;


namespace Malin_Data_Processing
{
    // Student Riley, id 30002737, Date: 12/02/2023
    // Assessment Task 1

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DataProcessing : Window
    {
        #region Global Methods
        // 4.1	Create two data structures using the LinkedList<T> class from the C# Systems.Collections.Generic namespace. The data must be of type “double”; you are not permitted to use any additional classes, nodes, pointers or data structures (array, list, etc) in the implementation of this application. The two LinkedLists of type double are to be declared as global within the “public partial class”.   
        private LinkedList<double> SensorAList = new LinkedList<double>();
        private LinkedList<double> SensorBList = new LinkedList<double>();

        private bool sortedA;
        private bool sortedB;

        public DataProcessing()
        {
            InitializeComponent();
            FillComboBox(Sigma, 10, 20, 10);
            FillComboBox(Mu, 35, 75, 50);
        }

        // 4.2	Copy the Galileo.DLL file into the root directory of your solution folder and add the appropriate reference in the solution explorer. Create a method called “LoadData” which will populate both LinkedLists.Declare an instance of the Galileo library in the method and create the appropriate loop construct to populate the two LinkedList; the data from Sensor A will populate the first LinkedList, while the data from Sensor B will populate the second LinkedList. The LinkedList size will be hardcoded inside the method and must be equal to 400. The input parameters are empty, and the return type is void.
        private void LoadData()
        {
            ReadData readData = new ReadData();

            int maxDataSize = 400;

            SensorAList.Clear();
            SensorBList.Clear();

            for (int x = 0; x < maxDataSize; x++)
            {
                SensorAList.AddLast(readData.SensorA(double.Parse(Mu.SelectedValue.ToString()), double.Parse(Sigma.SelectedValue.ToString())));
                SensorBList.AddLast(readData.SensorB(double.Parse(Mu.SelectedValue.ToString()), double.Parse(Sigma.SelectedValue.ToString())));
            }
        }

        // 4.3	Create a custom method called “ShowAllSensorData” which will display both LinkedLists in a ListView. Add column titles “Sensor A” and “Sensor B” to the ListView. The input parameters are 
        private void ShowAllSensorData()
        {
            RawData.Items.Clear();
            int max = NumberOfNodes(SensorAList);
            for (int i = 0; i < max; i++)
                RawData.Items.Add(new { SensorA = SensorAList.ElementAt(i).ToString(), SensorB = SensorBList.ElementAt(i).ToString() });
        }

        // 4.4	Create a button and associated click method that will call the LoadData and ShowAllSensorData methods. The input parameters are empty, and the return type is void.
        private void ButtonLoadSensorData_Click(object sender, RoutedEventArgs e)
        {
            ListBoxA.Items.Clear();
            ListBoxB.Items.Clear();

            LoadData();
            ShowAllSensorData();

            sortedA = false;
            sortedB = false;
        }
        #endregion

        #region Utility Methods
        // 4.5	Create a method called “NumberOfNodes” that will return an integer which is the number of nodes(elements) in a LinkedList. The method signature will have an input parameter of type LinkedList, and the calling code argument is the linkedlist name.
        private int NumberOfNodes(LinkedList<double> linkedList)
        {
            return linkedList.Count;
        }

        // 4.6	Create a method called “DisplayListboxData” that will display the content of a LinkedList inside the appropriate ListBox. The method signature will have two input parameters; a LinkedList, and the ListBox name.  The calling code argument is the linkedlist name and the listbox name.
        private void DisplayListBoxData(LinkedList<double> linkedList, ListBox listBox)
        {
            listBox.Items.Clear();
            foreach (double data in linkedList)
                listBox.Items.Add(data);
        }

        // 4.7	Create a method called “SelectionSort” which has a single input parameter of type LinkedList, while the calling code argument is the linkedlist name. The method code must follow the pseudo code supplied below in the Appendix. The return type is Boolean.
        private bool SelectionSort(LinkedList<double> linkedList)
        {
            int min = 0;
            int max = NumberOfNodes(linkedList);

            for (int i = 0; i < max - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < max; j++)
                {
                    if (linkedList.ElementAt(j) < linkedList.ElementAt(min))
                        min = j;
                }

                // Supplied C# code
                LinkedListNode<double> currentMin = linkedList.Find(linkedList.ElementAt(min));
                LinkedListNode<double> currentI = linkedList.Find(linkedList.ElementAt(i));
                // End of supplied C# code
                var temp = currentMin.Value;
                currentMin.Value = currentI.Value;
                currentI.Value = temp;
            }

            return true;
        }

        // 4.8	Create a method called “InsertionSort” which has a single parameter of type LinkedList, while the calling code argument is the linkedlist name. The method code must follow the pseudo code supplied below in the Appendix. The return type is Boolean.
        private bool InsertionSort(LinkedList<double> linkedList)
        {
            int max = NumberOfNodes(linkedList);
            for (int i = 0; i < max - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (linkedList.ElementAt(j - 1) > linkedList.ElementAt(j))
                    {
                        // Supplied C# code
                        LinkedListNode<double> current = linkedList.Find(linkedList.ElementAt(j));
                        // End of supplied C# code
                        // Add Swap code here by swapping
                        // previous value with current value.
                        var temp = current.Previous.Value;
                        current.Previous.Value = current.Value;
                        current.Value = temp;
                    }
                }
            }

            return true;
        }

        // 4.9	Create a method called “BinarySearchIterative” which has the following four parameters: LinkedList, SearchValue, Minimum and Maximum. This method will return an integer of the linkedlist element from a successful search or the nearest neighbour value. The calling code argument is the linkedlist name, search value, minimum list size and the number of nodes in the list. The method code must follow the pseudo code supplied below in the Appendix.
        private int BinarySearchIterative(LinkedList<double> linkedList, int key, int min, int max)
        {
            while (min <= max - 1)
            {
                int mid = (min + max) / 2;

                if (key == linkedList.ElementAt(mid))
                    return ++mid;
                else if (key < linkedList.ElementAt(mid))
                    max = mid - 1;
                else
                    min = mid + 1;
            }
            return min;
        }

        // 4.10	Create a method called “BinarySearchRecursive” which has the following four parameters: LinkedList, SearchValue, Minimum and Maximum. This method will return an integer of the linkedlist element from a successful search or the nearest neighbour value. The calling code argument is the linkedlist name, search value, minimum list size and the number of nodes in the list. The method code must follow the pseudo code supplied below in the Appendix.
        private int BinarySearchRecursive(LinkedList<double> linkedList, int key, int min, int max)
        {
            if (min <= max - 1)
            {
                int mid = (min + max) / 2;

                if (key == linkedList.ElementAt(mid))
                    return mid;
                else if (key < linkedList.ElementAt(mid))
                    return BinarySearchRecursive(linkedList, key, min, mid - 1);
                else
                    return BinarySearchRecursive(linkedList, key, mid + 1, max);
            }
            return min;
        }
        #endregion

        #region UI Button Methods
        private bool SearchNumberOK(TextBox textBox, LinkedList<double> linkedList)
        {
            if (!string.IsNullOrEmpty(textBox.Text) && linkedList.Count > 0)
            {
                int searchValue = int.Parse(textBox.Text);
                if (searchValue > MinValue(linkedList) && searchValue < MaxValue(linkedList))
                    return true;
                else
                {
                    textBox.Clear();
                    return false;
                }
            }
            else
                return false;
        }

        private int MinValue(LinkedList<double> linkedList)
        {
            return (int)Math.Floor(linkedList.ElementAt(0));
        }
        private int MaxValue(LinkedList<double> linkedList)
        {
            return (int)Math.Ceiling(linkedList.ElementAt(399));
        }

        private void HighlightListBox(int found, ListBox listBox)
        {
            if (found >= 0 && found <= 2)
            {
                for (int x = 0; x <= 3; x++)
                    listBox.SelectedItems.Add(listBox.Items.GetItemAt(x));
            }
            else if (found >= 397 && found <= 399)
            {
                for (int x = 397; x <= 399; x++)
                    listBox.SelectedItems.Add(listBox.Items.GetItemAt(x));
            }
            else
            {
                for (int x = found - 2; x <= found + 2; x++)
                    listBox.SelectedItems.Add(listBox.Items.GetItemAt(x));
            }
            //listBox.SelectedItems.Add(listBox.Items.GetItemAt(found));
        }

        // 4.11	Create four button click methods that will search the LinkedList for an integer value entered into a textbox on the form. The four methods are:
        // 1.	Method for Sensor A and Binary Search Iterative
        // 2.	Method for Sensor A and Binary Search Recursive
        // 3.	Method for Sensor B and Binary Search Iterative
        // 4.	Method for Sensor B and Binary Search Recursive
        private void ButtonASearchIterative_Click(object sender, RoutedEventArgs e)
        {
            if (SearchNumberOK(TextBoxASearch, SensorAList) && sortedA)
            {
                var stopwatch = Stopwatch.StartNew();
                int found = BinarySearchIterative(SensorAList, int.Parse(TextBoxASearch.Text), 0, NumberOfNodes(SensorAList));
                stopwatch.Stop();
                TimeIterativeA.Content = stopwatch.ElapsedTicks.ToString() + " Ticks";
                DisplayListBoxData(SensorAList, ListBoxA);
                TextBoxASearch.Clear();
                HighlightListBox(found, ListBoxA);
            }
        }
        private void ButtonASearchRecursive_Click(object sender, RoutedEventArgs e)
        {
            if (SearchNumberOK(TextBoxASearch, SensorAList) && sortedA)
            {
                var stopwatch = Stopwatch.StartNew();
                int found = BinarySearchRecursive(SensorAList, int.Parse(TextBoxASearch.Text), 0, NumberOfNodes(SensorAList));
                stopwatch.Stop();
                TimeRecursiveA.Content = stopwatch.ElapsedTicks.ToString() + " Ticks";
                DisplayListBoxData(SensorAList, ListBoxA);
                TextBoxASearch.Clear();
                HighlightListBox(found, ListBoxA);
            }
        }

        private void ButtonBSearchIterative_Click(object sender, RoutedEventArgs e)
        {
            if (SearchNumberOK(TextBoxBSearch, SensorBList) && sortedB)
            {
                var stopwatch = Stopwatch.StartNew();
                int found = BinarySearchIterative(SensorBList, int.Parse(TextBoxBSearch.Text), 0, NumberOfNodes(SensorBList));
                stopwatch.Stop();
                TimeIterativeB.Content = stopwatch.ElapsedTicks.ToString() + " Ticks";
                DisplayListBoxData(SensorBList, ListBoxB);
                TextBoxBSearch.Clear();
                HighlightListBox(found, ListBoxB);
            }
        }

        private void ButtonBSearchRecursive_Click(object sender, RoutedEventArgs e)
        {
            if (SearchNumberOK(TextBoxBSearch, SensorBList) && sortedB)
            {
                var stopwatch = Stopwatch.StartNew();
                int found = BinarySearchRecursive(SensorBList, int.Parse(TextBoxBSearch.Text), 0, NumberOfNodes(SensorBList));
                stopwatch.Stop();
                TimeRecursiveB.Content = stopwatch.ElapsedTicks.ToString() + " Ticks";
                DisplayListBoxData(SensorBList, ListBoxB);
                TextBoxBSearch.Clear();
                HighlightListBox(found, ListBoxB);
            }
        }

        // 4.12	Create four button click methods that will sort the LinkedList using the Selection and Insertion methods. The four methods are:
        // 1.	Method for Sensor A and Selection Sort
        // 2.	Method for Sensor A and Insertion Sort
        // 3.	Method for Sensor B and Selection Sort
        // 4.	Method for Sensor B and Insertion Sort
        private void ButtonASortSelection_Click(object sender, RoutedEventArgs e)
        {
            var stopwatch = Stopwatch.StartNew();
            sortedA = SelectionSort(SensorAList);
            stopwatch.Stop();
            TimeSelectionA.Content = stopwatch.ElapsedMilliseconds.ToString() + " millisecs";
            DisplayListBoxData(SensorAList, ListBoxA);
        }
        private void ButtonASortInsertion_Click(object sender, RoutedEventArgs e)
        {
            var stopwatch = Stopwatch.StartNew();
            sortedA = InsertionSort(SensorAList);
            stopwatch.Stop();
            TimeInsertionA.Content = stopwatch.ElapsedMilliseconds.ToString() + " millisecs";
            DisplayListBoxData(SensorAList, ListBoxA);
        }

        private void ButtonBSortSelection_Click(object sender, RoutedEventArgs e)
        {
            var stopwatch = Stopwatch.StartNew();
            sortedB = SelectionSort(SensorBList);
            stopwatch.Stop();
            TimeSelectionB.Content = stopwatch.ElapsedMilliseconds.ToString() + " millisecs";
            DisplayListBoxData(SensorBList, ListBoxB);
        }
        private void ButtonBSortInsertion_Click(object sender, RoutedEventArgs e)
        {
            var stopwatch = Stopwatch.StartNew();
            sortedB = InsertionSort(SensorBList);
            stopwatch.Stop();
            TimeInsertionB.Content = stopwatch.ElapsedMilliseconds.ToString() + " millisecs";
            DisplayListBoxData(SensorBList, ListBoxB);
        }

        // 4.13	Add two numeric input controls for Sigma and Mu. The value for Sigma must be limited with a minimum of 10 and a maximum of 20. Set the default value to 10. The value for Mu must be limited with a minimum of 35 and a maximum of 75. Set the default value to 50.
        private void FillComboBox(ComboBox Cmb, int min, int max, int deflt)
        {
            for (int i = min; i <= max; i++)
                Cmb.Items.Add(i);

            Cmb.SelectedItem = deflt;
        }

        // 4.14	Add two textboxes for the search value; one for each sensor, ensure only numeric integer values can be entered.
        private void TextBoxSearch_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
        #endregion
    }
}
