using MindMap.Models;
using System;
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
        DataTask dataTask = new DataTask();

        string dataSource = "Data Source=DENISIVANSANTOS;Initial Catalog=mind_map;Integrated Security=True";
        //string queryDelete = @"DELETE FROM Task WHERE id_task=@id_task";

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

                SqlCommand cmd = new SqlCommand("SELECT * FROM users INNER JOIN task ON users.id_user = task.users INNER JOIN status ON task.status = status.id_status WHERE id_user="+ DataUser.Id_user +" AND id_status=1", connect);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds.Clear();
                sda.Fill(dt);

                //for(int i = 0; i < dt.Rows.Count;i++ )
                //{ 
                //    DataTask.Id_Task = Convert.ToInt32(dt.Rows[i]["id_task"].ToString());
                //    DataTask.Title = dt.Rows[i]["title"].ToString();
                //    DataStatus.Id_Status = Convert.ToInt32(dt.Rows[i]["status"].ToString());
                //    DataTask.Created_At = DateTime.Parse(dt.Rows[i]["created_at"].ToString());
                //    DataTask.Start_At = DateTime.Parse(dt.Rows[i]["start_at"].ToString());
                //    DataTask.End_At = DateTime.Parse(dt.Rows[i]["end_at"].ToString());
                //    DataTask.Progress = Convert.ToInt32(dt.Rows[i]["progress"].ToString());
                //    DataUser.Id_user = Convert.ToInt32(dt.Rows[i]["users"].ToString());
                //    DataTask.Last_Update = DateTime.Parse(dt.Rows[i]["last_update"].ToString());
                //}
                
                listviewTodo.ItemsSource = dt.DefaultView;

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

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
