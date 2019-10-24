using MVCFramework.Models.Entity;
using System.Data.SqlClient;

namespace MVCFramework.Infrastracture.DBConnection

{
    internal class DbCruder
    {
        public class Data
        {
            public int FileCount { get; set; }
        }

        private readonly string textConnection = @"(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\Database1.mdf;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

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