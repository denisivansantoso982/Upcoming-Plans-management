using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MindMap.Models;

namespace MindMap.Data
{
    /// <summary>
    /// Interaction logic for InProgress.xaml
    /// </summary>
    public partial class InProgress : UserControl
    {
        SqlConnection connect = new SqlConnection();

        string dataSource = "Data Source=DENISIVANSANTOS;Initial Catalog=mind_map;Integrated Security=True";

        public InProgress()
        {
            InitializeComponent();
            LoadData();
        }

        #region Methods
        private void LoadData()
        {
            try
            {
                connect.ConnectionString = dataSource;
                connect.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM users INNER JOIN task ON users.id_user = task.users INNER JOIN status ON task.status = status.id_status INNER JOIN priority ON task.priority = priority.id_priority WHERE id_user=" + DataUser.Id_user + " AND id_status=2", connect);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds.Clear();
                sda.Fill(dt);

                dataGridInProgress.ItemsSource = dt.DefaultView;

                string dataCount = dataGridInProgress.Items.Count.ToString();
                DataCount.Text = string.Concat("Total ", dataCount, " Rows");
            }
            catch ( Exception ex )
            {
                MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connect.Close();
            }
        }
        #endregion

        #region Button in Popup every Row
        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowId = (DataRowView) dataGridInProgress.SelectedItems[0];
            var Id = rowId["id_task"];

            DataTask.Id_Task = Convert.ToInt32(Id);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowId = (DataRowView) dataGridInProgress.SelectedItems[0];
            var Id = rowId["id_task"];

            DataTask.Id_Task = Convert.ToInt32(Id);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowId = (DataRowView) dataGridInProgress.SelectedItems[0];

            SqlConnection connect = new SqlConnection(dataSource);
            SqlCommand cmd = new SqlCommand("DELETE FROM task WHERE id_task = @id_task", connect);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id_task", rowId["id_task"]);

            connect.Open();
            cmd.ExecuteNonQuery();
            connect.Close();

            LoadData();
        }
        #endregion
    }
}
