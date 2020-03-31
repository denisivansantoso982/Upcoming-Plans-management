using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
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
using MindMap.Models;

namespace MindMap.Data
{
    /// <summary>
    /// Interaction logic for HomeSearch.xaml
    /// </summary>
    public partial class HomeSearch : UserControl
    {
        private string valueSearch = "";
        private string dataSource = "Data Source=DENISIVANSANTOS;Initial Catalog=mind_map;Integrated Security=True";

        public HomeSearch()
        {
            InitializeComponent();
            LoadData(valueSearch);
        }

        #region Methods
        private void LoadData(string valueSearch)
        {
            string value = valueSearch;
            try
            {
                using ( SqlConnection connect = new SqlConnection(dataSource) )
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM users INNER JOIN task ON users.id_user = task.users INNER JOIN status ON task.status = status.id_status INNER JOIN priority ON task.priority = priority.id_priority WHERE id_user=" + DataUser.Id_user + " AND title LIKE '%" + value + "%'", connect);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    ds.Clear();
                    sda.Fill(dt);

                    dataGridRecords.ItemsSource = dt.DefaultView;

                    string dataCount = dataGridRecords.Items.Count.ToString();
                    DataCount.Text = string.Concat("Total ", dataCount, " Rows");
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.Key == Key.Back && e.Key == Key.Delete )
            {
                valueSearch = TxtSearch.Text;
                LoadData(valueSearch);
            }
            else if ( e.Key == Key.Enter )
            {
                valueSearch = TxtSearch.Text;
                LoadData(valueSearch);
            }
            else
            {
                valueSearch = TxtSearch.Text;
                LoadData(valueSearch);
            }
        }

        #region Button in Popup
        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowId = (DataRowView) dataGridRecords.SelectedItems[0];
            var Id = rowId["id_task"];

            DataTask.Id_Task = Convert.ToInt32(Id);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowId = (DataRowView) dataGridRecords.SelectedItems[0];
            var Id = rowId["id_task"];

            DataTask.Id_Task = Convert.ToInt32(Id);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowId = (DataRowView) dataGridRecords.SelectedItems[0];
            var Id = rowId["id_task"];

            SqlConnection connect = new SqlConnection(dataSource);
            SqlCommand cmd = new SqlCommand("DELETE FROM task WHERE id_task = @id_task", connect);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id_task", Id);

            connect.Open();
            cmd.ExecuteNonQuery();
            connect.Close();

            valueSearch = TxtSearch.Text;
            LoadData(valueSearch);
        }
        #endregion
    }
}
