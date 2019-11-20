using MVCFramework.Infrastracture.DBConnection;using MVCFramework.Models.Entity;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace XUnitTestProject2.Domain
{
    public class Data
    {
        public int FileCount { get; set; }
    }

    internal class DatabaseTestClass
    {
        private readonly string textConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TextEditor;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Data DataLog { get; set; } = new Data();

        private DebugPrint Print = new DebugPrint();
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

        public void ShowSelectLog(string query, int number)
        {
            SetConnection(query);
            reader = command.ExecuteReader();
            GetData(reader);

            Debug.WriteLine("\r===================");
            while (reader.Read())
            {
                switch (number)
                {
                    case 2:
                        Print.ShowTwoColumn(reader);
                        break;

                    case 3:
                        Print.ShowThreeColumn(reader);
                        break;

                    case 5:
                        Print.ShowFiveColumn(reader);
                        break;
                }
            }
            reader.Close();
            connection.Close();
        }

        private void GetData(SqlDataReader reader)
        {
            DataLog.FileCount = reader.FieldCount;
        }

        internal bool ShowExeculteLog(DBProperty config, ServiceUser tryEntity)
        {
            throw new NotImplementedException();
        }
    }

    internal class DebugPrint
    {
        public void ShowTwoColumn(SqlDataReader reader)
        {
            Debug.WriteLine($"{reader[0]} {reader[1]}");
        }

        public void ShowFiveColumn(SqlDataReader reader)
        {
            Debug.WriteLine($"{reader[0]} {reader[1]} {reader[2]} {reader[3]} {reader[4]}");
        }

        internal void ShowThreeColumn(SqlDataReader reader)
        {
            Debug.WriteLine($"{reader[0]} {reader[1]} {reader[2]}");
        }
    }
}