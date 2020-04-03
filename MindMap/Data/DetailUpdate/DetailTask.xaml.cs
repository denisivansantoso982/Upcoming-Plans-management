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

namespace MindMap.Data.DetailUpdate
{
    /// <summary>
    /// Interaction logic for DetailTask.xaml
    /// </summary>
    public partial class DetailTask : UserControl
    {
        private string datasource = "Data Source=DENISIVANSANTOS;Initial Catalog=mind_map;Integrated Security=True";

        public DetailTask()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                SqlConnection connect = new SqlConnection(datasource);
                connect.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM users INNER JOIN task ON users.id_user = task.users INNER JOIN status ON task.status = status.id_status INNER JOIN priority ON task.priority = priority.id_priority WHERE id_user=" + DataUser.Id_user + " AND id_task=" + DataTask.Id_Task, connect);
                SqlDataReader reader = cmd.ExecuteReader();
                if ( reader.Read() )
                {
                    DateTime start = reader.GetFieldValue<DateTime>(reader.GetOrdinal("start_at"));
                    DateTime end = reader.GetFieldValue<DateTime>(reader.GetOrdinal("end_at"));
                    DateTime created = reader.GetFieldValue<DateTime>(reader.GetOrdinal("created_at"));
                    DateTime today = DateTime.Now;
                    int dueDate = Convert.ToInt32((end - today).TotalDays);

                    TextTitle.Text = reader.GetFieldValue<string>(reader.GetOrdinal("title"));
                    TextStatus.Text = reader.GetFieldValue<string>(reader.GetOrdinal("task_status"));
                    TextPriority.Text = reader.GetFieldValue<string>(reader.GetOrdinal("task_priority"));
                    TextStartDate.Text = start.ToString("dddd, MMMM dd yyyy");
                    TextEndDate.Text = end.ToString("dddd, MMMM dd yyyy");
                    TextCreated.Text = created.ToString("dddd, MMMM dd yyyy H:mm tt");

                    if ( reader["description"] == DBNull.Value )
                    {
                        TextDescription.Text = "(No Description!)";
                    }
                    else
                    {
                        TextDescription.Text = reader.GetFieldValue<string>(reader.GetOrdinal("description"));
                    }
                    if ( reader["last_update"] == DBNull.Value ) 
                    {
                        TextLastUpdate.Text = "(No Update!)";
                    }
                    else
                    {
                        DateTime updates = reader.GetFieldValue<DateTime>(reader.GetOrdinal("last_update"));
                        TextLastUpdate.Text = updates.ToString("dddd, MMMM dd yyyy H:mm tt");
                    }
                    if ( today < end )
                    {
                        string due = dueDate.ToString();
                        TextDeadline.Text = string.Concat("Due in ", due, " day(s)");
                    }
                    else if ( today == end )
                    {
                        TextDeadline.Text = "Due Today";
                    }
                    else
                    {
                        int calculate = dueDate * -1;
                        string due = calculate.ToString();
                        TextDeadline.Text = string.Concat("Late ", due, " day(s)");
                    }
                }

                reader.Close();
                connect.Close();
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
