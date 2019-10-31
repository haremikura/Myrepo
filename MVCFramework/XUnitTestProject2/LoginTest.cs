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
using MVCFramework.Controllers;
using System.Web.Mvc;

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


        // [Fact]
        //TODO:system.missingmethodexceptionの問題
        //public void TestGenericType()
        //{
        //    List<IEntity> dataEntity = new List<IEntity>()
        //            {
        //                new ServiceUser {UserName = "テスト智之", Password = "1234" },
        //                new ServiceUser {UserName = "tesuto", Password = "terahara" },
        //            };

        //    var mockContext = new MockCreator(dataEntity).GetMockContext().Object;
        //    var testController = new LoginController(mockContext);
        //    testController.ModelState.AddModelError("SessionName", "Required");
        //    TryLogin(new ServiceUser() { UserName = "テスト智之", Password = "1234" });
        //    TryLogin(new ServiceUser() { UserName = "テスト朋美", Password = "7890" });

        //    Assert.True(true);
        //    void TryLogin(ServiceUser serviceUser)
        //    {
        //        ActionResult Result = testController.Index(serviceUser);

        //        Debug.WriteLine($"\r=======\r {Result.ToString()}");
        //    }



        //}

    }
}