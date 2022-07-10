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
    /// Interaction logic for AddTaskDlgUserControl.xaml
    /// </summary>
    public partial class AddTaskDlgUserControl : UserControl
    {
        public string selected_task_name = null;
        public AddTaskDlgUserControl()
        {
            InitializeComponent();
        }
    }
}
