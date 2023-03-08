﻿using CefSharp;
using CefSharp.Wpf;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WhlBrowser
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        int tabCount=0;

        TabItem currentTabItem = null;
        ChromiumWebBrowser currentBrowserShowing = null;
        public MainWindow()
        {
            InitializeComponent();
            DataTransfer dataTransfer = new DataTransfer();
            dataTransfer.CreateXmlFile();

        }
        private void newTabShortCut(object sender, object e)
        {
            newTab();
        }

        private void closeTabShortCut(object sender, object e)
        {
            if (tabCount > 0 && currentTabItem != null)
            {
                tabControl.Items.Remove(currentTabItem);
            }
        }

        private void newTabMenuItem_Click(object sender, RoutedEventArgs e)
        {
            newTab();
        }
        private void newTab()
        {
            TabItem tabItem = new TabItem();
            ChromiumWebBrowser browser = new ChromiumWebBrowser();
            browser.Name = "browser_" + tabCount;
            tabControl.Items.Add(tabItem);
            tabItem.Name = "tab_" + tabCount;
            tabCount++;

            tabItem.Content = browser;
            browser.Address = "https://www.google.com";
            tabItem.Header = "New Blank Page (" + tabCount + ")";
            tabControl.SelectedItem = tabItem;
            currentTabItem = tabItem;
            currentBrowserShowing = browser;
            browser.Loaded += LoadedPage;
        }
        private void LoadedPage(object sender, RoutedEventArgs e)
        {
            var sndr = sender as ChromiumWebBrowser;
            if (currentTabItem!=null)
            {
                string removeHttp = sndr.Address.Replace("http://www.","");
                string host = removeHttp.Replace("https://www.","");
                int pos=host.IndexOf(".com");
                host=host.Substring(0,pos);
                currentTabItem.Header = host;   
            }
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if (currentBrowserShowing.CanGoBack)
            {
                currentBrowserShowing.Back();
            }
            
        }

        private void btn_Forward_Click(object sender, RoutedEventArgs e)
        {
            if (currentBrowserShowing.CanGoForward)
            {
                currentBrowserShowing.Forward();
            }
            
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            currentBrowserShowing.Reload();
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl.SelectedItem!=null)
            {
                currentTabItem = tabControl.SelectedItem as TabItem;
            }
            if (currentTabItem !=null)
            {
                currentBrowserShowing = currentTabItem.Content as ChromiumWebBrowser;
            }
        }

        private void AddressBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                Search();
                AddressBar.Text = currentBrowserShowing.Address;
            }
        }
        private void Search()
        {
            if (isValidDomain(AddressBar.Text))
            {
                currentBrowserShowing.Address = AddressBar.Text;
            }
            else
            {
                currentBrowserShowing.Address = "https://www.google.com/search?q=" + AddressBar.Text;
            }
        }

        public static bool isValidDomain(string str)
        {
            string strRegex = @"^((?!-)[A-Za-z0-9-]{1,63}(?<!-)\.)+[A-Za-z]{2,6}$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(str))
                return (true);
            else
                return (false);
        }

        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.ShowDialog();
        }
        private void closeTabMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (tabCount>0&&currentTabItem!=null)
            {
                tabControl.Items.Remove(currentTabItem);
            }
        }

        private void defaultBrowser_AddressChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            /*
             * Hata çıkarmaması için yorum satırına alındı.
            var sndr = sender as ChromiumWebBrowser;
            AddressBar.Text = sndr.Address;
            string removeHttp = sndr.Address.Replace("http://www.", "");
            string host = removeHttp.Replace("https://www.", "");
            int pos = host.IndexOf(".com");
            host = host.Substring(0, pos);
            tItem.Header = host;

            AddressBar.Text = sndr.Address;
            */
        }

        private void window_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key==Key.LeftCtrl )
            //{
            //    newTab();
            //}
        }
    }

    class Shortcut : ICommand
    {
        public event EventHandler<object> Executed;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (Executed != null)
                Executed(this, parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
