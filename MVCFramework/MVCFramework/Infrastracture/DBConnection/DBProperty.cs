using MVCFramework.Models.Entity;
using System;
using System.Data.SqlClient;

namespace MVCFramework.Infrastracture.DBConnection

{
    public class DBProperty
    {
        public string Query { get; set; }

        public Action<SqlCommand, IEntity> SetPlaseHolder { get; set; }
        public Action<SqlDataReader> SqlDataReader { get; set; }
        public Func<SqlDataReader, bool> Boolanswer { get; set; }
    }
}