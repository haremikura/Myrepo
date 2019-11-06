using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace XUnitTestProject2.Domain.DB
{
    public class ReaderRepository
    {
        public bool IsExist(SqlDataReader sqlDataReader)
        {
            return sqlDataReader.HasRows;
        }

    }

}
