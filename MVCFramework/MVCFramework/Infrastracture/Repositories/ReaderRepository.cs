using System.Data.SqlClient;

namespace MVCFramework.Infrastracture.Repositries

{
    public class ReaderRepository
    {
        public bool IsExist(SqlDataReader sqlDataReader)
        {
            return sqlDataReader.HasRows;
        }
    }
}