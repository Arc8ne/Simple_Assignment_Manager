using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simple_Assignment_Manager.UserControls
{
    /// <summary>
    /// Interaction logic for TasksViewerListControl.xaml
    /// </summary>
    public partial class TasksViewerListControl : UserControl
    {
        public delegate void on_search_box_text_changed(object sender, RoutedEventArgs e, TextBox search_box_obj);

        public event on_search_box_text_changed search_box_text_changed;

        public TasksViewerListControl()
        {
            InitializeComponent();
        }

        private void search_box_GotFocus(object sender, RoutedEventArgs e)
        {
            if (search_box.Text == "Search")
            {
                search_box.Text = string.Empty;
            }
        }

        private void search_box_LostFocus(object sender, RoutedEventArgs e)
        {
            search_box.Text = "Search";
        }

        private void search_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            search_box_text_changed?.Invoke(sender, e, search_box);
        }
    }
}
