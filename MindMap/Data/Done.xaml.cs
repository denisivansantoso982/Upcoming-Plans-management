﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MindMap.Models;
using MindMap.Data.DetailUpdate;

namespace MindMap.Data
{
    /// <summary>
    /// Interaction logic for Done.xaml
    /// </summary>
    public partial class Done : UserControl
    {
        SqlConnection connect = new SqlConnection();

        string dataSource = "Data Source=DENISIVANSANTOS;Initial Catalog=mind_map;Integrated Security=True";

        public Done()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #region Methods
        private void LoadData()
        {
            try
            {
                connect.ConnectionString = dataSource;
                connect.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM users INNER JOIN task ON users.id_user = task.users INNER JOIN status ON task.status = status.id_status INNER JOIN priority ON task.priority = priority.id_priority WHERE id_user=" + DataUser.Id_user + " AND id_status=3", connect);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds.Clear();
                sda.Fill(dt);

                dataGridDone.ItemsSource = dt.DefaultView;

                string dataCount = dataGridDone.Items.Count.ToString();
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
            DataRowView rowId = (DataRowView) dataGridDone.SelectedItems[0];
            var Id = rowId["id_task"];

            DataTask.Id_Task = Convert.ToInt32(Id);

            Window window = new Window
            {
                Title = "Detail Task",
                Content = new DetailTask(),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowState = WindowState.Normal,
                WindowStyle = WindowStyle.ThreeDBorderWindow,
                Icon = new BitmapImage(new Uri(@"E:\MyProject\Desktop_Project\C#\Upcoming Plans management\MindMap\Images\MyIcon.png", UriKind.Relative)),
                Width = 900,
                Height = 600
            };

            window.ShowDialog();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowId = (DataRowView) dataGridDone.SelectedItems[0];
            var Id = rowId["id_task"];

            DataTask.Id_Task = Convert.ToInt32(Id);

            Window window = new Window
            {
                Title = "Edit Task",
                Content = new UpdateTask(),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowState = WindowState.Normal,
                WindowStyle = WindowStyle.ThreeDBorderWindow,
                Icon = new BitmapImage(new Uri(@"E:\MyProject\Desktop_Project\C#\Upcoming Plans management\MindMap\Images\MyIcon.png", UriKind.Relative)),
                Width = 900,
                Height = 600
            };

            window.ShowDialog();

            this.LoadData();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowId = (DataRowView) dataGridDone.SelectedItems[0];

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
