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
using System.Data;
using System.Data.SqlClient;
using MindMap.Models;

namespace MindMap.Data
{
    /// <summary>
    /// Interaction logic for HomeCreateTask.xaml
    /// </summary>
    public partial class HomeCreateTask : UserControl
    {
        private string dataSource = "Data Source=DENISIVANSANTOS;Initial Catalog=mind_map;Integrated Security=True";

        public HomeCreateTask()
        {
            InitializeComponent();
            GetDataStatus();
            GetDataPriority();
        }

        #region Methods
        private void GetDataStatus()
        {
            try
            {
                using ( SqlConnection connection = new SqlConnection(dataSource) )
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM status", connection);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds, 0.ToString());
                    ComboStatus.ItemsSource = ds.Tables[0].DefaultView;
                    ComboStatus.SelectedValuePath = "id_status";
                    ComboStatus.DisplayMemberPath = "task_status";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void GetDataPriority()
        {
            try
            {
                using ( SqlConnection connection = new SqlConnection(dataSource) )
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM priority", connection);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds, 0.ToString());
                    ComboPriority.ItemsSource = ds.Tables[0].DefaultView;
                    ComboPriority.SelectedValuePath = "id_priority";
                    ComboPriority.DisplayMemberPath = "task_priority";
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool Validation()
        {
            if ( TxtTitle.Text == string.Empty)
            {
                ErrorTitle.Text = "Please Fill Title!";
                ErrorTitle.Visibility = Visibility.Visible;
                TxtTitle.Focus();
                return false;
            }
            else if ( string.IsNullOrEmpty(ComboStatus.Text) )
            {
                ErrorStatus.Text = "Please Select the Status!";
                ErrorStatus.Visibility = Visibility.Visible;
                ComboStatus.Focus();
                return false;
            }
            else if ( string.IsNullOrEmpty(ComboPriority.Text) )
            {
                ErrorPriority.Text = "Please Select the Priority!";
                ErrorPriority.Visibility = Visibility.Visible;
                ComboPriority.Focus();
                return false;
            }
            else if ( DateStart.SelectedDate == null )
            {
                ErrorStart.Text = "Select Start Date!";
                ErrorStart.Visibility = Visibility.Visible;
                DateStart.Focus();
                return false;
            }
            else if ( DateEnd.SelectedDate == null )
            {
                ErrorEnd.Text = "Select End Date!";
                ErrorEnd.Visibility = Visibility.Visible;
                DateEnd.Focus();
                return false;
            }

            return true;
        }

        private void ResetForm()
        {
            TxtTitle.Text = "";
            string.IsNullOrEmpty(ComboStatus.Text);
            ComboStatus.SelectedValuePath = null;
            string.IsNullOrEmpty(ComboPriority.Text);
            ComboPriority.SelectedValuePath = null;
            DateStart.SelectedDate = null;
            DateEnd.SelectedDate = null;
            TxtDescription.Text = "";

        }
        #endregion

        #region Button and Other Action
        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            int cmbs = ComboStatus.SelectedIndex;
            int resultStatus = cmbs + 1;
            int cmbp = ComboPriority.SelectedIndex;
            int resultPriority = cmbp + 1;

            if ( Validation() )
            {
                try
                {
                    SqlConnection connect = new SqlConnection(dataSource);
                    connect.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO task VALUES (@title, @status, @start_at, @end_at, @description, @created_at, @users, @priority, null)", connect);
                    cmd.CommandType = CommandType.Text;
                    if ( string.IsNullOrEmpty(TxtTitle.Text) )
                    {
                        cmd.Parameters.AddWithValue("@title", DBNull.Value);
                    }
                    else
                        cmd.Parameters.AddWithValue("@title", TxtTitle.Text);
                    if ( string.IsNullOrEmpty(ComboStatus.Text) )
                    {
                        cmd.Parameters.AddWithValue("@status", DBNull.Value);
                    }
                    else
                        cmd.Parameters.AddWithValue("@status", SqlDbType.Int).Value = resultStatus;
                    if ( DateStart.SelectedDate == null )
                    {
                        cmd.Parameters.AddWithValue("@start_at", DBNull.Value);
                    }
                    else
                        cmd.Parameters.AddWithValue("@start_at", DateStart.SelectedDate.Value.ToString());
                    if ( DateEnd.SelectedDate == null )
                    {
                        cmd.Parameters.AddWithValue("@end_at", DBNull.Value);
                    }
                    else
                        cmd.Parameters.AddWithValue("@end_at", DateEnd.SelectedDate.Value.ToString());
                     if ( string.IsNullOrEmpty(TxtDescription.Text) )
                    {
                        cmd.Parameters.AddWithValue("@description", DBNull.Value);
                    }
                    else
                        cmd.Parameters.AddWithValue("@description", TxtDescription.Text);

                    cmd.Parameters.AddWithValue("@created_at", DateTime.Now);
                    cmd.Parameters.AddWithValue("@users", DataUser.Id_user);
                    if ( string.IsNullOrEmpty(ComboPriority.Text) )
                    {
                        cmd.Parameters.AddWithValue("@priority", DBNull.Value);
                    }
                    else
                        cmd.Parameters.AddWithValue("@priority", SqlDbType.Int).Value = resultPriority;

                    cmd.ExecuteNonQuery();
                    connect.Close();

                    MessageBox.Show("Create task success!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void TxtDescription_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ( e.Key == Key.Enter )
            {
                BtnAddTask_Click(sender, e);
            }
        }
        #endregion

        #region Validation Character
        private void TxtTitle_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ErrorTitle.Visibility = Visibility.Collapsed;
        }

        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ErrorStatus.Visibility = Visibility.Collapsed;
        }

        private void ComboPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ErrorPriority.Visibility = Visibility.Collapsed;
        }

        private void DateStart_GotFocus(object sender, RoutedEventArgs e)
        {
            ErrorStart.Visibility = Visibility.Collapsed;
        }

        private void DateEnd_GotFocus(object sender, RoutedEventArgs e)
        {
            ErrorEnd.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}
