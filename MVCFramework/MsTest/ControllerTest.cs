using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MVCFramework.Controllers;
using MVCFramework.Infrastracture.Repositries;
using MVCFramework.Models.Entity;
using XUnitTestProject2.Domain;

namespace MsTest
{
    [TestClass]
    public class ControllerTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            List<IEntity> dataEntity = new List<IEntity>()
                    {
                        new ServiceUser {UserName = "テスト智之", Password = "1234" },
                        new ServiceUser {UserName = "tesuto", Password = "terahara" },
                    };

            IDbContext mockDbContext = new MockCreator(dataEntity).GetMockContext().Object;

            var testController = new LoginController(mockDbContext);

            var mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            var mockContoroller = new Mock<TextEditorController>();

            mockControllerContext.Setup(m => m.HttpContext.Session).Returns(mockSession.Object);
            testController.ControllerContext = mockControllerContext.Object;

            testController.ModelState.AddModelError("SessionName", "Required");
            TryLogin(new ServiceUser() { UserName = "テスト智之", Password = "1234" });
            TryLogin(new ServiceUser() { UserName = "テスト朋美", Password = "7890" });

            Assert.IsTrue(true);
            void TryLogin(ServiceUser serviceUser)
            {
                var Result = testController.Index(serviceUser);

                Debug.WriteLine($"\r=======\r {Result.ToString()}");
            }

        }
    }
}

