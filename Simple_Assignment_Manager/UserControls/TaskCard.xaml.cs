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
    /// Interaction logic for TaskCard.xaml
    /// </summary>
    
    public partial class TaskCard : UserControl
    {
        public delegate void on_card_button_clicked(object sender, RoutedEventArgs e, TaskCard current_task_card, Button pressed_btn);

        public event on_card_button_clicked card_button_clicked;

        public TaskCard()
        {
            InitializeComponent();
        }

        private void on_buttons_clicked(object sender, RoutedEventArgs e)
        {
            this.card_button_clicked.Invoke(sender, e, this, sender as Button);
        }
    }
}
