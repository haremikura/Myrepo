using Moq;
using MVCFramework.Content.Content;
using MVCFramework.Controllers;
using MVCFramework.Models.Entity;
using MVCFramework.Models.Session;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
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
        //TODO:system.missingmethodexceptionの問題
        public void TestGenericType()
        {

        }

    }
}