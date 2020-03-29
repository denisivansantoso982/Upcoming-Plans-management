using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace MindMap.Form
{
    /// <summary>
    /// Interaction logic for RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : UserControl
    {
        SqlConnection connect = new SqlConnection();
        string dataSource = "Data Source=DENISIVANSANTOS;Initial Catalog=mind_map;Integrated Security=True";
        MainWindow mainObj = new MainWindow();

        public RegisterForm()
        {
            InitializeComponent();
        }

        #region Register Action
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if ( ValidationRegister() )
            {
                connect.ConnectionString = dataSource;
                SqlCommand cmd = new SqlCommand("INSERT INTO users VALUES(@name, @username, @password, @email)", connect);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", TxtName.Text);
                cmd.Parameters.AddWithValue("@username", TxtRegisterUsername.Text);
                cmd.Parameters.AddWithValue("@password", TxtRegisterPassword.Text);
                cmd.Parameters.AddWithValue("@email", TxtEmail.Text);

                connect.Open();
                cmd.ExecuteNonQuery();
                connect.Close();

                MessageBox.Show("Register Success! You can do Login now!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                TxtName.Text = "";
                TxtEmail.Text = "";
                TxtRegisterUsername.Text = "";
                TxtRegisterPassword.Text = "";

                mainObj.UserContent.Child = new LoginForm();
            }
        }
        #endregion

        #region Reset Register Action
        private void BtnResetRegister_Click(object sender, RoutedEventArgs e)
        {
            TxtName.Text = "";
            TxtEmail.Text = "";
            TxtRegisterUsername.Text = "";
            TxtRegisterPassword.Text = "";
        }
        #endregion

        #region TextBox Character Validation
        private void TxtUsername_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ( e.Key == Key.Space )
            {
                e.Handled = true;
            }
            base.OnPreviewKeyDown(e);
        }

        private void TxtEmail_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ( e.Key == Key.Space )
            {
                e.Handled = true;
            }
            base.OnPreviewKeyDown(e);
        }

        private void TxtRegisterPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ( e.Key == Key.Space )
            {
                e.Handled = true;
            }
            base.OnPreviewKeyDown(e);
        }
        #endregion

        #region Methods
        private bool ValidationRegister()
        {
            if ( TxtName.Text == string.Empty )
            {
                MessageBox.Show("Please Fill Name!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtName.Focus();
                return false;
            }
            else if ( TxtEmail.Text == string.Empty )
            {
                MessageBox.Show("Please Fill Email!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtEmail.Focus();
                return false;
            }
            else if ( !Regex.IsMatch(TxtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$") )
            {
                MessageBox.Show("The Email doesn't match!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtEmail.Focus();
                return false;
            }
            else if ( TxtRegisterUsername.Text == string.Empty )
            {
                MessageBox.Show("Please Fill Username!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtRegisterUsername.Focus();
                return false;
            }
            else if ( TxtRegisterPassword.Text == string.Empty )
            {
                MessageBox.Show("Please Fill Password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtRegisterPassword.Focus();
                return false;
            }

            return true;
        }
        #endregion

    }
}
