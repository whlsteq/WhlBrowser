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

namespace WhlBrowser
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        String url;
        int count = 0;
        public MainWindow()
        {
            InitializeComponent();
            browser.Address = "https://www.google.com";
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if ((!(txt_box.Text is null)) &&e.Key==Key.Enter)
            {
                url = txt_box.Text;
                browser.Address = url;
            }
        }

        private void show(object sender, KeyEventArgs e)
        {
            if (count==0 && e.Key==Key.LeftCtrl && e.Key==Key.LeftShift && e.Key==Key.N)
            {
                this.IsEnabled = false;
                count++;
            }
            else if (count==1 && e.Key == Key.LeftCtrl && e.Key == Key.LeftShift && e.Key == Key.N)
            {
                this.IsEnabled=true;
                count=0;

            }
        }

    }
}
