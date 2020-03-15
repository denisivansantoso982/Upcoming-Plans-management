using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Text.RegularExpressions;
using MindMap.Form;

namespace MindMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UserContent.Child = new LoginForm();
        }

        #region BtnClose Action
        private async void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(200);
            Application.Current.Shutdown();
        }
        #endregion

        #region Switch UserControl Action
        private void BtnToRegister_Click(object sender, RoutedEventArgs e)
        {
            BtnToLogin.Visibility = Visibility.Visible;
            BtnToRegister.Visibility = Visibility.Collapsed;
            UserContent.Child = new RegisterForm();
        }

        private void BtnToLogin_Click(object sender, RoutedEventArgs e)
        {
            BtnToLogin.Visibility = Visibility.Collapsed;
            BtnToRegister.Visibility = Visibility.Visible;
            UserContent.Child = new LoginForm();
        }
        #endregion
    }
}
