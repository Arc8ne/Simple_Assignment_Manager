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
    /// Interaction logic for AddModuleDlgControl.xaml
    /// </summary>
    public partial class AddModuleDlgControl : UserControl
    {
        public string selected_module_name = null;

        public AddModuleDlgControl()
        {
            InitializeComponent();
        }
    }
}
