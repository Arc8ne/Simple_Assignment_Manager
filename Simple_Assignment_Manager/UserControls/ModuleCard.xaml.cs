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
    /// Interaction logic for ModuleCard.xaml
    /// </summary>
    public partial class ModuleCard : UserControl
    {
        public ModuleCard()
        {
            InitializeComponent();
        }

        public delegate void on_module_card_button_clicked(object sender, RoutedEventArgs e, ModuleCard current_module_card, Button pressed_btn);

        public event on_module_card_button_clicked module_card_button_clicked;

        private void on_module_buttons_clicked(object sender, RoutedEventArgs e)
        {
            this.module_card_button_clicked.Invoke(sender, e, this, sender as Button);
        }
    }
}
