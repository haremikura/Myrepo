using MVCFramework.Content.Content;using MVCFramework.Infrastracture.DBConnection;using MVCFramework.Models.Entity;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
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
        }        [Fact]
        public void TestMoq()
        {            List<IEntity> dataEntity = new List<IEntity>()
                    {
                        new ServiceUser {UserName = "tesuto", Password = "terahara" },
                        new ServiceUser {UserName = "tesuto", Password = "terahara" },
                    };            var mockContext = new MockCreator(dataEntity).GetMockContext();
            // DBContext‚ÉMock‚ðÝ’è

            Debug.WriteLine("\r======");
            foreach (var entityIndex in mockContext.Object.ServiceUser.ToList())
            {
                Debug.WriteLine($"{entityIndex.UserId} {entityIndex.UserName} {entityIndex.Password}");
            }

            Assert.True(mockContext.Object.ServiceUser.Count() == 2);        }

        [Fact]
        public void TestIsExist()
        {
            DatabaseTestClass databaseTestClass = new DatabaseTestClass();
            databaseTestClass
                .ShowSelectLog("SELECT * FROM ServiceUser WHERE Name = @Name AND @", 3);

            Assert.True(databaseTestClass.DataLog.FileCount > 0);
        }    }
}