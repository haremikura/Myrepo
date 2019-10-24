using MVCFramework.Models.Entity;
using System.Data.SqlClient;

namespace MVCFramework.Infrastracture.DBConnection

{
    public class InsertPraseHolderRepository
    {
        public void InsertServerUserHolder(SqlCommand sqlCommand, IEntity entity)
        {
            ServiceUser serviceUser = (ServiceUser)entity;
            sqlCommand.Parameters.Add(new SqlParameter("@UserName", serviceUser.UserName));
            sqlCommand.Parameters.Add(new SqlParameter("@Password", serviceUser.Password));
        }
    }
}