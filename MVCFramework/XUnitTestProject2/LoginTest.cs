using Moq;

using MVCFramework.Content.Content;
using MVCFramework.Infrastracture.DBConnection;

using MVCFramework.Models.Entity;
using MVCFramework.Models.Session;
using MVCFramework.Infrastracture.Repositries;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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
            //try
            //{

            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine(e.StackTrace);
            //    Assert.False(true);
            //}

            List<IEntity> dataEntity = new List<IEntity>()
                    {
                        new ServiceUser {UserName = "tesuto", Password = "terahara" },
                        new ServiceUser {UserName = "tesuto", Password = "terahara" },
                    };


            var mockContext = new MockCreator(dataEntity).GetMockContext();
            // DBContextにMockを設定

            Debug.WriteLine("\r======");
            foreach (var entityIndex in mockContext.Object.ServiceUser.ToList())
            {
                Debug.WriteLine($"{entityIndex.UserId} {entityIndex.UserName} {entityIndex.Password}");
            }

            Assert.True(mockContext.Object.ServiceUser.Count() == 2);

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