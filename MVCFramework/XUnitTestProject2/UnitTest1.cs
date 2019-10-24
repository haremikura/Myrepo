using MVCFramework.Models;
using MVCFramework.Content.Content;using MVCFramework.Infrastracture.DBConnection;
using MVCFramework.Repositries; 
 using MVCFramework.Models.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using Xunit;
using XUnitTestProject2.Domain;

namespace XUnitTestProject2
{
    public class UnitTest1
    {
        private readonly string textQuery = @"SELECT * FROM dbo.TextFilesList";
        private string textConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TextEditor;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [Fact]
        public void TestCheckDBData()
        {
            using (var connection = new SqlConnection(textConnection))
            {
                var command = new SqlCommand(textQuery, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.WriteLine($"{reader[0]} {reader[1]} {reader[2]} {reader[3]} {reader[4]}");
                    }
                }
            }

            Assert.True(true);
        }

        [Fact]
        public void TestDbContextTry()
        {
            DBContexTestTemplate dBContexTestTemplate
                = new DBContexTestTemplate(TextDBName.TextFile);

            Debug.WriteLine(dBContexTestTemplate.ShowList());
            Assert.True(true);
        }

        [Fact]
        public void TestDbContextTry2()
        {
            DBContexTestTemplate dBContexTestTemplate
                = new DBContexTestTemplate(TextDBName.ServiceUser);

            Debug.WriteLine(dBContexTestTemplate.ShowList());
            Assert.True(true);
        }

        [Fact]
        public void TestPraseHolder()
        {
            DatabaseTestClass databaseTestClass = new DatabaseTestClass();

            var tryEntity = new ServiceUser() { UserName = "ƒeƒXƒg’q”V", Password = "1234" };
            var tryEntity2 = new ServiceUser() { UserName = "asdf", Password = "ddd" };

            var config = new CrudContext().GetProperty(CrudEnum.GetLogin);
            bool answer = databaseTestClass.ShowExeculteLog(config, tryEntity);
            bool answer2 = databaseTestClass.ShowExeculteLog(config, tryEntity2);

            Assert.True(answer && !answer2);
        }
    }
}