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

namespace WhlBrowser
{
    /// <summary>
    /// SettingsWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void btn_addSearchEngine_Click(object sender, RoutedEventArgs e)
        {
            DataTransfer dt = new DataTransfer();
            if (txt_name.Text.Length>0 && txt_prefix.Text.Length > 0)
            {
                if(dt.AddSearchEngine(txt_name.Text, txt_prefix.Text))
                {
                    MessageBox.Show("You added a search engine successfully.","Added Search Engine");
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTransfer dt = new DataTransfer();
            List<string> engines= dt.GetSearchEngines();
            foreach (var item in engines)
            {
                searchEngineCombo.Items.Add(item);
            }
        }
    }
}
