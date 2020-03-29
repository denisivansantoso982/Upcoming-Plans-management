using MindMap.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MindMap.Data
{
    /// <summary>
    /// Interaction logic for ToDo.xaml
    /// </summary>
    public partial class ToDo : UserControl
    {
        SqlConnection connect = new SqlConnection();

        string dataSource = "Data Source=DENISIVANSANTOS;Initial Catalog=mind_map;Integrated Security=True";

        public ToDo()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                connect.ConnectionString = dataSource;
                connect.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM users INNER JOIN task ON users.id_user = task.users INNER JOIN status ON task.status = status.id_status INNER JOIN priority ON task.priority = priority.id_priority WHERE id_user="+ DataUser.Id_user +" AND id_status=1", connect);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("task");
                sda.Fill(dt);

                dataGridTodo.ItemsSource = dt.DefaultView;
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

        #region Button in Popup every Row
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowId = (DataRowView) dataGridTodo.SelectedItems[0];

            SqlConnection connect = new SqlConnection(dataSource);
            SqlCommand cmd = new SqlCommand("DELETE FROM task WHERE id_task = @id_task", connect);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id_task", rowId["id_task"]);

            connect.Open();
            cmd.ExecuteNonQuery();
            connect.Close();

            UserControl_Loaded(sender, e);
        }

        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
