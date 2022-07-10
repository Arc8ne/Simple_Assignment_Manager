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
using System.Windows.Forms;

namespace Simple_Assignment_Manager.UserControls
{
    /// <summary>
    /// Interaction logic for ExportDataAsCSVDlgControl.xaml
    /// </summary>
    public partial class ExportDataAsCSVDlgControl : System.Windows.Controls.UserControl
    {
        public ExportDataAsCSVDlgControl()
        {
            InitializeComponent();
        }

        private void choose_path_from_file_dlg_btn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog new_folder_browser_dlg = new FolderBrowserDialog();

            new_folder_browser_dlg.ShowNewFolderButton = true;

            if (new_folder_browser_dlg.ShowDialog() == DialogResult.OK)
            {
                csv_folder_path_box.Text = new_folder_browser_dlg.SelectedPath;
            }
        }
    }
}
