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
using System.Windows.Controls.Primitives;
using MindMap.Dialog;
using MindMap.Data;
using MindMap.Models;

namespace MindMap
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        #region Drawer Action
        private void btnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            btnCloseMenu.Visibility = Visibility.Visible;
            btnOpenMenu.Visibility = Visibility.Hidden;
        }

        private void btnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            btnCloseMenu.Visibility = Visibility.Hidden;
            btnOpenMenu.Visibility = Visibility.Visible;
        }

        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            Content.Child=new HomeDashboard();
        }

        private void BtnSubTask_Click(object sender, RoutedEventArgs e)
        {
            Content.Child = new HomeTask();
        }

        private void BtnSearchTask_Click(object sender, RoutedEventArgs e)
        {
            Content.Child = new HomeSearch();
        }
        #endregion

        #region Button in Popup Action
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Hide();
        }

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            DialogTemplate template = new DialogTemplate
            {
                Content = new Help()
            };

            template.ShowDialog();
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            DialogTemplate template = new DialogTemplate
            {
                Content = new About()
            };

            template.ShowDialog();
           
        }
        #endregion

    }
}
