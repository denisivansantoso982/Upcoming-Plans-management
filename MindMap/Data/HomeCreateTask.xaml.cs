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
using System.Text.RegularExpressions;

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
            else if ( DateEnd.Text == null )
            {
                ErrorEnd.Text = "Select End Date!";
                ErrorEnd.Visibility = Visibility.Visible;
                DateEnd.Focus();
                return false;
            }

            return true;
        }
        #endregion

        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        #region Button Action
        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            if ( Validation() )
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(dataSource) )
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion
    }
}
