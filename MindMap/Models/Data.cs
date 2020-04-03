using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MindMap.Models
{
    public class DataUser
    {
        public static int Id_user { get; set; }
        public static string Name { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string Email { get; set; }
    }

    public class DataTask
    {
        public static int Id_Task { get; set; }
        public static string Title { get; set; }
        public static DateTime Created_At { get; set; }
        public static DateTime Start_At { get; set; }
        public static DateTime End_At { get; set; }
        public static int Status { get => DataStatus.Id_Status; }
        public static int Users { get => DataUser.Id_user; }
        public static string Description { get; set; }
        public static int Priority { get => DataPriority.Id_Priority; }
        public static DateTime Last_Update { get; set; }
    }

    public class DataStatus 
    { 
        public static int Id_Status { get; set; }
        public static string Task_Status { get; set; }
    }

    public class DataPriority
    {
        public static int Id_Priority { get;  set; }
        public static string Task_Priority { get;  set; }
    }

}
