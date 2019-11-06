using CoreMVC.Models.Entity;
using System.Data.SqlClient;



namespace XUnitTestProject2.Domain.DB
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
