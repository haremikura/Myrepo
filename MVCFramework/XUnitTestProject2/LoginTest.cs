using Moq;

using MVCFramework.Content.Content;
using MVCFramework.Infrastracture.DBConnection;

using MVCFramework.Models.Entity;
using MVCFramework.Models.Session;
using MVCFramework.Repositries;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Xunit;
using XUnitTestProject2.Domain;

namespace XUnitTestProject2
{
    public class LoginTest
    {
        [Fact]
        public void TestLoginLogic()
        {
            var tryEntity = new ServiceUser() { UserName = "テスト智之", Password = "1234" };

            new DatabaseTestClass().ShowSelectLog("SELECT * FROM ServiceUser", 2);
            DBContexTestTemplate template = new DBContexTestTemplate(TextDBName.ServiceUser);

            bool answer = new UserSession(template.DbContext).Login(tryEntity);



            Assert.True(answer);
        }

        [Fact]
        public void TestIsExist()
        {
            DatabaseTestClass databaseTestClass = new DatabaseTestClass();
            databaseTestClass
                .ShowSelectLog("SELECT * FROM ServiceUser WHERE Name = @Name AND @", 3);

            Assert.True(databaseTestClass.DataLog.FileCount > 0);
        }

        [Fact]
        public void TestMoq()
        {
            var dataEntity = new List<ServiceUser>
                    {
                        new ServiceUser {UserName = "tesuto", Password = "terahara" },
                        new ServiceUser {UserName = "tesuto", Password = "terahara" },
                    }.AsQueryable();

            // DbSetのMock
            var mockMyEntity = new Mock<DbSet<ServiceUser>>();
            // DbSetとテスト用データを紐付け
            mockMyEntity.As<IQueryable<ServiceUser>>().Setup(m => m.Provider).Returns(dataEntity.Provider);
            mockMyEntity.As<IQueryable<ServiceUser>>().Setup(m => m.Expression).Returns(dataEntity.Expression);
            mockMyEntity.As<IQueryable<ServiceUser>>().Setup(m => m.ElementType).Returns(dataEntity.ElementType);
            mockMyEntity.As<IQueryable<ServiceUser>>().Setup(m => m.GetEnumerator()).Returns(dataEntity.GetEnumerator());

            // DBContextにMockを設定
            var mockContext = new Mock<TextEditorContext>();
            mockContext.Setup(m => m.ServiceUser).Returns(mockMyEntity.Object);
            mockMyEntity.Setup(f => f.Create()).Returns(new ServiceUser());




        }

        [Fact]
        public void TestGenericType()
        {
            //var dataEntity = new List<ServiceUser>
            //        {
            //            new ServiceUser {UserName = "tesuto", Password = "terahara" },
            //            new ServiceUser {UserName = "tesuto", Password = "terahara" },
            //        }.AsQueryable();

            //Type parameterType = dataEntity.GetType().GetGenericArguments()[0];

            //Debug.WriteLine(parameterType);

            //Type genericBaseType = typeof(IQueryable<>);
            //Type genericType = genericBaseType.MakeGenericType(parameterType);
            //IQueryable IQueryable = (IQueryable)Activator.CreateInstance(genericType);
            //Debug.WriteLine(IQueryable.GetType());


            //Type genericBaseType2 = typeof(DbSet<>);
            //Type genericType2 = genericBaseType2.MakeGenericType(parameterType);
            //Debug.WriteLine(genericType2.ToString());
            //DbSet dbset = (DbSet)Activator.CreateInstance(genericType2);
            //Debug.WriteLine(dbset.GetType());
            //Assert.True(true);

            //var mockMyEntity = new Mock<DbSet<ServiceUser>>();
        }
    }
}