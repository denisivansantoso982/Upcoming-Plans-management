using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MindMap.Models
{

    // Mapping Class
    [AttributeUsage(AttributeTargets.Property, Inherited =true)]
    [Serializable]
    public class MappingAttribute : Attribute
    {
        public string ColumnName = null;
    }

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
        public int Id_Task { get; set; }
        public string Title { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Start_At { get; set; }
        public DateTime End_At { get; set; }
        public int Status { get => DataStatus.Id_Status; }
        public int Users { get => DataUser.Id_user; }
        public string Description { get; set; }
        public DateTime Last_Update { get; set; }
        public int Progress { get; set; }
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
