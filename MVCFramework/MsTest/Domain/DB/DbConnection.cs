using CoreMVC.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace XUnitTestProject2.Domain.DB
{

    internal class DbConnection
    {
        public class Data
        {
            public int FileCount { get; set; }
        }

        private readonly string textConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public Data DataLog { get; set; } = new Data();
        private SqlConnection connection;
        private SqlDataReader reader;
        private SqlCommand command;




        private void SetConnection(string query)
        {
            connection = new SqlConnection(textConnection);
            command = new SqlCommand(query, connection);
            connection.Open();
        }

        public bool ShowExeculteLog(DBProperty dBProperty, IEntity entity)
        {
            SetConnection(dBProperty.Query);
            dBProperty.SetPlaseHolder(command, entity);
            var existObject = command.ExecuteReader();
            bool answer = dBProperty.Boolanswer(existObject);
            connection.Close();
            return answer;
        }


        private void GetData(SqlDataReader reader)
        {
            DataLog.FileCount = reader.FieldCount;
        }
    }
}
