using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MindMap.Models;
using System.Data;

namespace MindMap.Data
{
    /// <summary>
    /// Interaction logic for HomeDashboard.xaml
    /// </summary>
    public partial class HomeDashboard : UserControl
    {
        private string dataSource = "Data Source=DENISIVANSANTOS;Initial Catalog=mind_map;Integrated Security=True";

        public HomeDashboard()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadChartdata();
            TotalData();
        }

        private void LoadChartdata()
        {
            SqlConnection connect = new SqlConnection(dataSource);
            connect.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(status) AS status_data FROM task WHERE users=" + DataUser.Id_user + " GROUP BY status", connect);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            List<int> data = new List<int>(dt.Rows.Count);
            foreach ( DataRow row in dt.Rows )
            {
                data.Add((int) row["status_data"]);
            }

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Data : ",
                    Values = new ChartValues<int>(data),
                    MaxColumnWidth = 100,
                    Margin = new Thickness(0, 0, 0, 10)
                }
            };

            Labels = new[] { "To Do", "In Progress", "Done" };
            Formatter = value => value.ToString("N");

            DataContext = this;

            connect.Close();
        }

        private void TotalData()
        {
            try
            {
                SqlConnection connect = new SqlConnection(dataSource);
                connect.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) AS total FROM task WHERE users=" + DataUser.Id_user, connect);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if ( dt.Rows.Count > 0 )
                {
                    string total = dt.Rows[0]["total"].ToString();
                    DataCount.Text = string.Concat("Total " + total + " Data(s)");
                }

                connect.Close();
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }
    }
}
