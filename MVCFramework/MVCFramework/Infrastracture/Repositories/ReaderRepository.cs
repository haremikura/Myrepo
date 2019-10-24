using System.Data.SqlClient;

namespace MVCFramework.Infrastracture.DBConnection

{
    public class ReaderRepository
    {
        public bool IsExist(SqlDataReader sqlDataReader)
        {
            return sqlDataReader.HasRows;
        }
    }
}