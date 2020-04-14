using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

        #region Methods
        private void LoadChartdata()
        {
            SqlConnection connect = new SqlConnection(dataSource);
            connect.Open();

            // Column Chart
            SqlCommand cmd = new SqlCommand("SELECT COUNT(status) AS status_data FROM task WHERE users=" + DataUser.Id_user + " GROUP BY status", connect);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            List<int> data = new List<int>(dt.Rows.Count);
            foreach ( DataRow row in dt.Rows )
            {
                data.Add((int) row["status_data"]);
            }

            SeriesColumn = new SeriesCollection
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

            // Pie Chart
            SqlCommand cmdPri = new SqlCommand("SELECT COUNT(priority) AS pri FROM task WHERE users = " + DataUser.Id_user + " GROUP BY priority", connect);
            SqlDataAdapter sdaPri = new SqlDataAdapter(cmdPri);
            DataTable dtPri = new DataTable();
            sdaPri.Fill(dtPri);

            List<int> datap = new List<int>(dtPri.Rows.Count);
            foreach ( DataRow dr in dtPri.Rows )
            {
                datap.Add((int) dr["pri"]);
            }

            int low = datap[0];
            int normal = datap[1];
            int high = datap[2];
            int urgent = datap[3];

            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            SeriesPie = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Low",
                    Values = new ChartValues<int>{ low },
                    DataLabels = true,
                    LabelPoint = PointLabel,
                    Fill = new SolidColorBrush(Color.FromRgb(76,175,80))
                },
                new PieSeries
                {
                    Title = "Normal",
                    Values = new ChartValues<int>{ normal },
                    DataLabels = true,
                    LabelPoint = PointLabel,
                    Fill = new SolidColorBrush(Color.FromRgb(255,235,59))
                },
                new PieSeries
                {
                    Title = "High",
                    Values = new ChartValues<int>{ high },
                    DataLabels = true,
                    LabelPoint = PointLabel,
                    Fill = new SolidColorBrush(Color.FromRgb(211,47,47))
                },
                new PieSeries
                {
                    Title = "Urgent",
                    Values = new ChartValues<int>{ urgent },
                    DataLabels = true,
                    LabelPoint = PointLabel,
                    Fill = new SolidColorBrush(Color.FromRgb(69,90,100))
                }
            };

            // Data Context
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
                    DataTotal.Text = string.Concat("Total " + total + " Data(s)");
                }

                connect.Close();
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (PieChart) chartpoint.ChartView;

            foreach ( PieSeries series in chart.Series )
                series.PushOut = 0;

            var selectedSeries = (PieSeries) chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
        #endregion

        public SeriesCollection SeriesColumn { get; set; }
        public SeriesCollection SeriesPie { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }
        public Func<ChartPoint, string> PointLabel { get; set; }
    }
}
