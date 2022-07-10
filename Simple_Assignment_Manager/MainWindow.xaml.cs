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
using System.Diagnostics;
using Simple_Assignment_Manager.UserControls;

namespace Simple_Assignment_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public ApplicationModel current_app_model;

        public MainWindow()
        {
            InitializeComponent();

            initializing_label.Visibility = Visibility.Visible;

            current_app_model = new ApplicationModel();

            //To keep track of loading times
            //Stopwatch load_timer = new Stopwatch();

            //load_timer.Start();

            current_app_model.load_all_data();

            //load_timer.Stop();

            //MessageBox.Show($"Total time taken to load all application data: {load_timer.Elapsed.ToString("fff")} milliseconds");

            main_nav_bar.view_tasks_btn.Click += on_view_tasks_btn_clicked;

            main_nav_bar.add_tasks_btn.Click += on_add_tasks_btn_clicked;

            main_nav_bar.view_modules_btn.Click += on_view_modules_btn_clicked;

            main_nav_bar.add_modules_btn.Click += on_add_module_btn_clicked;

            main_nav_bar.export_task_data_btn.Click += on_export_task_data_btn_clicked;

            main_nav_bar.export_module_data_btn.Click += on_export_module_data_btn_clicked;

            main_nav_bar.bulk_file_creator_btn.Click += on_nav_bar_bulk_file_creator_btn_clicked;

            add_task_dlg_ui.add_task_btn.Click += on_add_task_btn_clicked;

            add_module_dlg_ui.add_module_btn.Click += on_create_module_btn_clicked;

            tasks_viewer_list_ui.search_box_text_changed += on_search_box_text_change;

            modules_viewer_list_ui.search_box.TextChanged += on_modules_viewer_search_box_text_changed;

            export_data_csv_dlg_ui.export_task_data_btn.Click += on_begin_export_module_data_btn_clicked;

            bulk_file_creator_dlg_ui.create_files_btn.Click += on_start_bulk_create_file_btn_clicked;

            update_module_combo_box();

            initializing_label.Visibility = Visibility.Collapsed;
        }

        public void update_task_list()
        {
            tasks_viewer_list_ui.task_cards_list_panel.Children.Clear();

            if (current_app_model.start_task_obj == null)
            {
                tasks_viewer_list_ui.no_tasks_label.Visibility = Visibility.Visible;

                //MessageBox.Show("No tasks label visible.");

                return;
            }
            else
            {
                tasks_viewer_list_ui.no_tasks_label.Visibility = Visibility.Collapsed;

                //MessageBox.Show("No tasks label not visible.");
            }

            Task temp_task_obj = current_app_model.start_task_obj;

            while (temp_task_obj != null)
            {
                TaskCard new_task_card = new TaskCard();

                new_task_card.task_name_label.Text = temp_task_obj.task_name;

                new_task_card.task_type_label.Text = temp_task_obj.task_type;

                new_task_card.module_name_label.Text = temp_task_obj.module_name;

                new_task_card.due_date_label.Text = temp_task_obj.deadline_date_str;

                new_task_card.task_status_label.Text = temp_task_obj.task_status;

                new_task_card.Margin = new Thickness(0, 0, 0, 10);

                new_task_card.card_button_clicked += on_task_card_button_pressed;

                tasks_viewer_list_ui.task_cards_list_panel.Children.Add(new_task_card);

                temp_task_obj = temp_task_obj.next_task_obj;
            }
        }

        public void update_module_list()
        {
            modules_viewer_list_ui.module_cards_list_panel.Children.Clear();

            if (current_app_model.start_module_obj == null)
            {
                modules_viewer_list_ui.no_modules_label.Visibility = Visibility.Visible;

                //MessageBox.Show("No tasks label visible.");

                return;
            }
            else
            {
                modules_viewer_list_ui.no_modules_label.Visibility = Visibility.Collapsed;

                //MessageBox.Show("No tasks label not visible.");
            }

            Module temp_module_obj = current_app_model.start_module_obj;

            while (temp_module_obj != null)
            {
                ModuleCard new_module_card = new ModuleCard();

                new_module_card.module_name_label.Text = temp_module_obj.module_name;

                new_module_card.module_credits_label.Text = "Module Credits: " + Convert.ToString(temp_module_obj.module_credits);

                new_module_card.module_grade_label.Text = "Module Grade: " + temp_module_obj.module_grade;

                new_module_card.Margin = new Thickness(0, 0, 0, 10);

                new_module_card.module_card_button_clicked += on_module_card_btn_clicked;

                modules_viewer_list_ui.module_cards_list_panel.Children.Add(new_module_card);

                temp_module_obj = temp_module_obj.next_module_obj;
            }
        }

        public void collapse_other_non_core_widgets(UIElement chosen_ui_element)
        {
            foreach(UIElement ui_element in window_content_grid.Children)
            {
                if (ui_element != top_sys_bar && ui_element != main_nav_bar && ui_element != chosen_ui_element)
                {
                    ui_element.Visibility = Visibility.Collapsed;

                    //MessageBox.Show(Convert.ToString(ui_element));
                }
            }
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void minimize_btn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void maximize_btn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Normal)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void on_main_window_border_clicked(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void on_view_tasks_btn_clicked(object sender, RoutedEventArgs e)
        {
            collapse_other_non_core_widgets(tasks_viewer_list_ui);

            update_task_list();

            //MessageBox.Show("Tasks list updated.");

            if (tasks_viewer_list_ui.Visibility == Visibility.Collapsed)
            {
                tasks_viewer_list_ui.Visibility = Visibility.Visible;

                //MessageBox.Show("Tasks viewer list visible.");
            }
            else
            {
                tasks_viewer_list_ui.Visibility = Visibility.Collapsed;
            }
        }

        private void on_add_tasks_btn_clicked(object sender, RoutedEventArgs e)
        {
            add_task_dlg_ui.add_task_btn.Content = "Add Task";

            collapse_other_non_core_widgets(add_task_dlg_ui);

            if (add_task_dlg_ui.Visibility == Visibility.Collapsed)
            {
                add_task_dlg_ui.Visibility = Visibility.Visible;
            }
            else
            {
                add_task_dlg_ui.Visibility = Visibility.Collapsed;
            }
        }

        private void on_view_modules_btn_clicked(object sender, RoutedEventArgs e)
        {
            collapse_other_non_core_widgets(modules_viewer_list_ui);

            update_module_list();

            if (modules_viewer_list_ui.Visibility == Visibility.Collapsed)
            {
                modules_viewer_list_ui.Visibility = Visibility.Visible;
            }
            else
            {
                modules_viewer_list_ui.Visibility = Visibility.Collapsed;
            }
        }

        private void on_add_module_btn_clicked(object sender, RoutedEventArgs e)
        {
            add_module_dlg_ui.add_module_btn.Content = "Add Module";

            collapse_other_non_core_widgets(add_module_dlg_ui);

            update_module_list();

            if (add_module_dlg_ui.Visibility == Visibility.Collapsed)
            {
                add_module_dlg_ui.Visibility = Visibility.Visible;
            }
            else
            {
                add_module_dlg_ui.Visibility = Visibility.Collapsed;
            }
        }

        private void on_add_task_btn_clicked(object sender, RoutedEventArgs e)
        {
            if ((string)(sender as Button).Content == "Add Task")
            {
                Task task_to_create = new Task(add_task_dlg_ui.task_name_box.Text, add_task_dlg_ui.task_type_combo_box.Text, add_task_dlg_ui.module_name_box.Text, add_task_dlg_ui.deadline_box.Text, "Not Completed");

                current_app_model.add_task(task_to_create);
            }
            else if ((string)(sender as Button).Content == "Apply Changes")
            {
                if (add_task_dlg_ui.selected_task_name != null)
                {
                    current_app_model.edit_task_by_name(add_task_dlg_ui.selected_task_name, add_task_dlg_ui.task_name_box.Text, add_task_dlg_ui.task_type_combo_box.Text, add_task_dlg_ui.module_name_box.Text, add_task_dlg_ui.deadline_box.Text);

                    add_task_dlg_ui.selected_task_name = null;
                }
            }

            collapse_other_non_core_widgets(tasks_viewer_list_ui);

            update_task_list();

            tasks_viewer_list_ui.Visibility = Visibility.Visible;
        }

        private void on_task_card_button_pressed(object sender, RoutedEventArgs e, TaskCard task_card_sender, Button clicked_btn)
        {
            //MessageBox.Show("A button on the task card was clicked.");

            if ((string)clicked_btn.Content == "Complete")
            {
                //MessageBox.Show("Complete button pressed.");

                Task current_task = current_app_model.find_task_by_name(task_card_sender.task_name_label.Text);

                current_task.task_status = "Completed";

                task_card_sender.task_status_label.Text = current_task.task_status;
            }
            else if ((string)clicked_btn.Content == "Edit")
            {
                //MessageBox.Show("Edit button pressed.");

                Task current_task = current_app_model.find_task_by_name(task_card_sender.task_name_label.Text);

                add_task_dlg_ui.selected_task_name = current_task.task_name;

                add_task_dlg_ui.task_name_box.Text = current_task.task_name;

                add_task_dlg_ui.task_type_combo_box.Text = current_task.task_type;

                add_task_dlg_ui.module_name_box.Text = current_task.module_name;

                add_task_dlg_ui.deadline_box.Text = current_task.deadline_date_str;

                add_task_dlg_ui.add_task_btn.Content = "Apply Changes";

                collapse_other_non_core_widgets(add_task_dlg_ui);

                add_task_dlg_ui.Visibility = Visibility.Visible;
            }
            else if ((string)clicked_btn.Content == "Remove")
            {
                //MessageBox.Show("Remove button pressed");

                current_app_model.remove_task_by_name(task_card_sender.task_name_label.Text);

                tasks_viewer_list_ui.Visibility = Visibility.Collapsed;

                update_task_list();

                tasks_viewer_list_ui.Visibility = Visibility.Visible;
            }
        }

        private void on_search_box_text_change(object sender, RoutedEventArgs e, TextBox search_box)
        {
            if (search_box.Text == "Search" || search_box.Text == string.Empty)
            {
                foreach(UIElement child_task_card in tasks_viewer_list_ui.task_cards_list_panel.Children)
                {
                    child_task_card.Visibility = Visibility.Visible;
                }

                return;
            }

            foreach (UIElement child_task_card in tasks_viewer_list_ui.task_cards_list_panel.Children)
            {
                if ((child_task_card as TaskCard).task_name_label.Text.ToLower().Contains(search_box.Text.ToLower()))
                {
                    child_task_card.Visibility = Visibility.Visible;
                }
                else
                {
                    child_task_card.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void on_create_module_btn_clicked(object sender, RoutedEventArgs e)
        {
            if ((string)(sender as Button).Content == "Add Module")
            {
                Module module_to_create = new Module(add_module_dlg_ui.module_name_box.Text, add_module_dlg_ui.module_grade_combo_box.Text, Convert.ToInt32(add_module_dlg_ui.module_credits_box.Text));

                current_app_model.add_module(module_to_create);
            }
            else if ((string)(sender as Button).Content == "Apply Changes")
            {
                if (add_module_dlg_ui.selected_module_name != null)
                {
                    current_app_model.edit_module_by_name(add_module_dlg_ui.selected_module_name, add_module_dlg_ui.module_name_box.Text, add_module_dlg_ui.module_grade_combo_box.Text, Convert.ToInt32(add_module_dlg_ui.module_credits_box.Text));

                    Task temp_task_obj = current_app_model.start_task_obj;

                    if (add_module_dlg_ui.selected_module_name != add_module_dlg_ui.module_name_box.Text)
                    {
                        while (temp_task_obj != null)
                        {
                            if (temp_task_obj.module_name == add_module_dlg_ui.selected_module_name)
                            {
                                temp_task_obj.module_name = add_module_dlg_ui.module_name_box.Text;
                            }

                            temp_task_obj = temp_task_obj.next_task_obj;
                        }

                        //MessageBox.Show("Task data updated.");
                    }

                    add_module_dlg_ui.selected_module_name = null;
                }
            }

            update_module_list();

            update_module_combo_box();

            collapse_other_non_core_widgets(modules_viewer_list_ui);

            modules_viewer_list_ui.Visibility = Visibility.Visible;
        }

        public void update_module_combo_box()
        {
            add_task_dlg_ui.module_name_box.Items.Clear();

            add_task_dlg_ui.module_name_box.Items.Add("None");

            Module temp_module_obj = current_app_model.start_module_obj;

            while (temp_module_obj != null)
            {
                add_task_dlg_ui.module_name_box.Items.Add(temp_module_obj.module_name);

                temp_module_obj = temp_module_obj.next_module_obj;
            }
        }

        private void on_modules_viewer_search_box_text_changed(object sender, RoutedEventArgs e)
        {
            TextBox search_box = modules_viewer_list_ui.search_box;

            if (search_box.Text == "Search" || search_box.Text == string.Empty)
            {
                foreach (UIElement child_module_card in modules_viewer_list_ui.module_cards_list_panel.Children)
                {
                    child_module_card.Visibility = Visibility.Visible;
                }

                return;
            }

            foreach (UIElement child_module_card in modules_viewer_list_ui.module_cards_list_panel.Children)
            {
                if ((child_module_card as ModuleCard).module_name_label.Text.ToLower().Contains(search_box.Text.ToLower()))
                {
                    child_module_card.Visibility = Visibility.Visible;
                }
                else
                {
                    child_module_card.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void on_main_window_close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            shut_down_label.Visibility = Visibility.Visible;

            current_app_model.save_all_data();

            shut_down_label.Visibility = Visibility.Collapsed;
        }

        private void on_module_card_btn_clicked(object sender, RoutedEventArgs e, ModuleCard current_module_card, Button clicked_btn)
        {
            if ((string)clicked_btn.Content == "Remove")
            {
                current_app_model.remove_module_by_name(current_module_card.module_name_label.Text);
            }
            else if ((string)clicked_btn.Content == "Edit")
            {
                Module current_module = current_app_model.find_module_by_name(current_module_card.module_name_label.Text);

                add_module_dlg_ui.selected_module_name = current_module.module_name;

                add_module_dlg_ui.module_name_box.Text = current_module.module_name;

                //MessageBox.Show($"Current module grade: {current_module.module_grade}");

                /*
                Note:
                Upon further testing, it is observed that the line of code below only works if the text that the property is set to has more than 1
                character long. As some grades are only 1 character long (e.g. grade 'A', grade 'B'), this method has not been used.
                */
                //add_module_dlg_ui.module_grade_combo_box.Text = current_module.module_grade;

                foreach(ComboBoxItem module_grade_item in add_module_dlg_ui.module_grade_combo_box.Items)
                {
                    if ((string)module_grade_item.Content == current_module.module_grade)
                    {
                        add_module_dlg_ui.module_grade_combo_box.SelectedItem = module_grade_item;
                    }
                }

                add_module_dlg_ui.module_credits_box.Text = Convert.ToString(current_module.module_credits);

                add_module_dlg_ui.add_module_btn.Content = "Apply Changes";

                collapse_other_non_core_widgets(add_module_dlg_ui);

                add_module_dlg_ui.Visibility = Visibility.Visible;
            }

            update_module_list();

            update_module_combo_box();
        }

        private void on_export_task_data_btn_clicked(object sender, RoutedEventArgs e)
        {
            export_data_csv_dlg_ui.export_dlg_header_label.Text = "Export Task Data as CSV";

            collapse_other_non_core_widgets(export_data_csv_dlg_ui);

            export_data_csv_dlg_ui.export_result_label.Foreground = new SolidColorBrush(System.Windows.Media.Colors.LightGreen);

            export_data_csv_dlg_ui.export_result_label.Visibility = Visibility.Collapsed;

            export_data_csv_dlg_ui.Visibility = Visibility.Visible;
        }

        private void on_export_module_data_btn_clicked(object sender, RoutedEventArgs e)
        {
            export_data_csv_dlg_ui.export_dlg_header_label.Text = "Export Module Data as CSV";

            collapse_other_non_core_widgets(export_data_csv_dlg_ui);

            export_data_csv_dlg_ui.export_result_label.Foreground = new SolidColorBrush(System.Windows.Media.Colors.LightGreen);

            export_data_csv_dlg_ui.export_result_label.Visibility = Visibility.Collapsed;

            export_data_csv_dlg_ui.Visibility = Visibility.Visible;
        }

        private void on_begin_export_module_data_btn_clicked(object sender, RoutedEventArgs e)
        {
            if (export_data_csv_dlg_ui.csv_file_name_box.Text == "")
            {
                export_data_csv_dlg_ui.export_result_label.Foreground = new SolidColorBrush(System.Windows.Media.Colors.Red);

                export_data_csv_dlg_ui.export_result_label.Text = "File name field cannot be blank.";

                export_data_csv_dlg_ui.export_result_label.Visibility = Visibility.Visible;

                return;
            }

            if (export_data_csv_dlg_ui.csv_folder_path_box.Text == "")
            {
                //Use default application directory path instead
                if (export_data_csv_dlg_ui.export_dlg_header_label.Text.Contains("Task"))
                {
                    current_app_model.export_tasks_data_to_csv("./" + export_data_csv_dlg_ui.csv_file_name_box.Text);

                    export_data_csv_dlg_ui.export_result_label.Foreground = new SolidColorBrush(System.Windows.Media.Colors.LightGreen);

                    export_data_csv_dlg_ui.export_result_label.Text = "Task data exported successfully.";

                    export_data_csv_dlg_ui.export_result_label.Visibility = Visibility.Visible;
                }
                else
                {
                    current_app_model.export_modules_data_to_csv("./" + export_data_csv_dlg_ui.csv_file_name_box.Text);

                    export_data_csv_dlg_ui.export_result_label.Foreground = new SolidColorBrush(System.Windows.Media.Colors.LightGreen);

                    export_data_csv_dlg_ui.export_result_label.Text = "Module data exported successfully.";

                    export_data_csv_dlg_ui.export_result_label.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (export_data_csv_dlg_ui.export_dlg_header_label.Text.Contains("Task"))
                {
                    current_app_model.export_tasks_data_to_csv(export_data_csv_dlg_ui.csv_folder_path_box.Text + "/" + export_data_csv_dlg_ui.csv_file_name_box.Text);

                    export_data_csv_dlg_ui.export_result_label.Foreground = new SolidColorBrush(System.Windows.Media.Colors.LightGreen);

                    export_data_csv_dlg_ui.export_result_label.Text = "Task data exported successfully.";

                    export_data_csv_dlg_ui.export_result_label.Visibility = Visibility.Visible;
                }
                else
                {
                    current_app_model.export_modules_data_to_csv(export_data_csv_dlg_ui.csv_folder_path_box.Text + "/" + export_data_csv_dlg_ui.csv_file_name_box.Text);

                    export_data_csv_dlg_ui.export_result_label.Foreground = new SolidColorBrush(System.Windows.Media.Colors.LightGreen);

                    export_data_csv_dlg_ui.export_result_label.Text = "Module data exported successfully.";

                    export_data_csv_dlg_ui.export_result_label.Visibility = Visibility.Visible;
                }
            }
        }

        private void on_nav_bar_bulk_file_creator_btn_clicked(object sender, RoutedEventArgs e)
        {
            collapse_other_non_core_widgets(bulk_file_creator_dlg_ui);

            bulk_file_creator_dlg_ui.file_creation_result_label.Foreground = new SolidColorBrush(System.Windows.Media.Colors.LightGreen);

            bulk_file_creator_dlg_ui.file_creation_result_label.Visibility = Visibility.Collapsed;

            bulk_file_creator_dlg_ui.Visibility = Visibility.Visible;
        }

        private void on_start_bulk_create_file_btn_clicked(object sender, RoutedEventArgs e)
        {
            if (bulk_file_creator_dlg_ui.chosen_file_extension_box.Text == "")
            {
                bulk_file_creator_dlg_ui.file_creation_result_label.Foreground = new SolidColorBrush(System.Windows.Media.Colors.Red);

                bulk_file_creator_dlg_ui.file_creation_result_label.Text = "File extension field cannot be blank. Please enter the file extension of the files that you would like to create (e.g. .txt .csv .py).";

                bulk_file_creator_dlg_ui.file_creation_result_label.Visibility = Visibility.Visible;

                return;
            }

            if (bulk_file_creator_dlg_ui.creation_count_box.Text == "")
            {
                bulk_file_creator_dlg_ui.file_creation_result_label.Foreground = new SolidColorBrush(System.Windows.Media.Colors.Red);

                bulk_file_creator_dlg_ui.file_creation_result_label.Text = "Please enter the number of files you would like to create and try again.";

                bulk_file_creator_dlg_ui.file_creation_result_label.Visibility = Visibility.Visible;

                return;
            }

            if (bulk_file_creator_dlg_ui.chosen_folder_path_box.Text != "")
            {
                current_app_model.bulk_create_files(bulk_file_creator_dlg_ui.chosen_folder_path_box.Text, bulk_file_creator_dlg_ui.chosen_file_name_prefix_box.Text, bulk_file_creator_dlg_ui.chosen_file_extension_box.Text, Convert.ToInt32(bulk_file_creator_dlg_ui.creation_count_box.Text), bulk_file_creator_dlg_ui.initial_file_content_box.Text);
            }
            else
            {
                current_app_model.bulk_create_files(".", bulk_file_creator_dlg_ui.chosen_file_name_prefix_box.Text, bulk_file_creator_dlg_ui.chosen_file_extension_box.Text, Convert.ToInt32(bulk_file_creator_dlg_ui.creation_count_box.Text), bulk_file_creator_dlg_ui.initial_file_content_box.Text);
            }

            bulk_file_creator_dlg_ui.file_creation_result_label.Foreground = new SolidColorBrush(System.Windows.Media.Colors.LightGreen);

            bulk_file_creator_dlg_ui.file_creation_result_label.Text = $"{bulk_file_creator_dlg_ui.creation_count_box.Text} {bulk_file_creator_dlg_ui.chosen_file_extension_box.Text} files created successfully.";

            bulk_file_creator_dlg_ui.file_creation_result_label.Visibility = Visibility.Visible;
        }
    }
}
