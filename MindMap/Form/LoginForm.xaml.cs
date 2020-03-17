using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SqlClient;
using MindMap.Models;
using System.Data;

namespace MindMap.Form
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : UserControl
    {
        SqlConnection connect = new SqlConnection();
        string dataSource = "Data Source=DENISIVANSANTOS;Initial Catalog=mind_map;Integrated Security=True";
        Dashboard dashboard = new Dashboard();
        //private Preferences userStorage = new Preferences.UserAuth;


        public LoginForm()
        {
            InitializeComponent();
            TxtUsername.Focus();
        }

        #region Methods
        private bool ValidationLogin()
        {
            if ( TxtUsername.Text == string.Empty )
            {
                MessageBox.Show("Please Fill Username!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if ( TxtPassword.Password == string.Empty )
            {
                MessageBox.Show("Please Fill Password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
        #endregion

        #region Login Action
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if ( ValidationLogin() )
            {
                connect.ConnectionString = dataSource;
                connect.Open();
                string username = TxtUsername.Text;
                string password = TxtPassword.Password;
                SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE username = '" + username + "' AND password = '" + password + "'", connect);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if ( dt.Rows.Count > 0 )
                {
                    DataUser.Id_user = Convert.ToInt32(dt.Rows[0]["id_user"].ToString());
                    DataUser.Name = dt.Rows[0]["name"].ToString();
                    DataUser.Username = dt.Rows[0]["username"].ToString();
                    DataUser.Password = dt.Rows[0]["password"].ToString();
                    DataUser.Email = dt.Rows[0]["email"].ToString();

                    var myWindow = MainWindow.GetWindow(this);
                    myWindow.Close();
                    dashboard.Show();
                }
                else
                {
                    MessageBox.Show("Login Failed!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    TxtUsername.Focus();
                }
                TxtUsername.Text = "";
                TxtPassword.Password = "";

                connect.Close();
            }
        }
        #endregion

        #region Reset Login Form Action
        private void BtnResetLogin_Click(object sender, RoutedEventArgs e)
        {
            TxtUsername.Text = "";
            TxtPassword.Password = "";
            TxtUsername.Focus();
        }
        #endregion

        #region Validation Character
        private void TxtUsername_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.Enter )
            {
                TxtPassword.Focus();
                e.Handled = false;
            }
            base.OnPreviewKeyDown(e);
        }

        private void TxtPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.Enter )
            {
                BtnLogin_Click(sender, null);
                e.Handled = false;
            }
            base.OnPreviewKeyDown(e);
        }
        #endregion

    }
}
