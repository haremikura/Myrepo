using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MsTest.Domain;
using MVCFramework.Controllers;
using MVCFramework.Infrastracture.Repositries;
using MVCFramework.Models.Entity;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XUnitTestProject2.Domain;

namespace MsTest
{
    [TestClass]
    public class ControllerTest
    {
        private LoginController testController;
        private TextEditorController textEditorControlelr;
        private IDbContext mockDbContext;

        [TestMethod]
        public void TestMethod1()
        {
            List<IEntity> dataEntity = new List<IEntity>()
                    {
                        new ServiceUser {UserName = "テスト智之", Password = "1234" },
                        new ServiceUser {UserName = "tesuto", Password = "terahara" },
                    };

            mockDbContext = CreateMock();

            testController = new LoginController(mockDbContext);

            var mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            var mockContoroller = new Mock<TextEditorController>();

            mockControllerContext.Setup(m => m.HttpContext.Session).Returns(mockSession.Object);
            testController.ControllerContext = mockControllerContext.Object;

            testController.ModelState.AddModelError("SessionName", "Required");

            string[] testViewResult = new string[2];
            testViewResult[0] = DebugAndGetViewResult(new ServiceUser() { UserName = "テスト智之", Password = "1234" }).ViewName;
            testViewResult[1] = DebugAndGetViewResult(new ServiceUser() { UserName = "テスト朋美", Password = "7890" }).ViewName;

            Assert.IsTrue(testViewResult[0] == "~/Views/TextEditor/Index.cshtml");
            Assert.IsTrue(testViewResult[1] == "~/Views/Login/LoginView.cshtml");

            IDbContext CreateMock()
            {
                var mock = new MockCreator(dataEntity);
                mock.SetMockCurrentSession();
                mock.SetMockTextFilesList();
                return mock.GetMockContext().Object;
            }

            ViewResult DebugAndGetViewResult(ServiceUser serviceUser)
            {
                ViewResult result = (ViewResult)testController.Index(serviceUser);

                Debug.WriteLine($"\r=======\r {result.ViewName}");
                return result;
            }
        }

        [TestMethod]
        public void CrateFileTest()
        {
            mockDbContext = CreateMock();
            textEditorControlelr = new TextEditorController(mockDbContext);

            var mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            var mockContoroller = new Mock<TextEditorController>();

            SetMockSession();
            SetMockController();
            DebugAndGetViewResult("TestName");


            IDbContext CreateMock()
            {
                var mock = new MockCreator();
                mock.SetMockTextFilesList();
                mock.SetMockEidtText();
                return mock.GetMockContext().Object;
            }

            MvcHtmlString DebugAndGetViewResult(string fileName)
            {

                string Before1
                    = ViewEntity.WriteEntityData(mockDbContext.TextFilesList.ToList());

                string Before2
                    = ViewEntity.WriteEntityData(mockDbContext.EditText.ToList());

                Debug.WriteLine($"Check Data　Before :\r {Before1} \r {Before2}");

                var result = textEditorControlelr.CrateFile(fileName);

                string After1
                    = ViewEntity.WriteEntityData(mockDbContext.TextFilesList.ToList());

                string After2
                    = ViewEntity.WriteEntityData(mockDbContext.EditText.ToList());

                Debug.WriteLine($"Check Data After : \r {After1} \r {After2}");

                Debug.WriteLine($"\r=======\r {result.ToHtmlString()}");

                return result;
            }


            void SetMockSession()
            {
                mockControllerContext.Setup(x => x.HttpContext.Session["FileId"]).Returns("1");
                mockControllerContext.Setup(x => x.HttpContext.Session["MaxFileId"]).Returns("");
                mockControllerContext.Setup(x => x.HttpContext.Session["UserId"]).Returns("2");
            }

            void SetMockController()
            {
                //mockControllerContext.Setup(m => m.HttpContext.Session).Returns(mockSession.Object);
                textEditorControlelr.ControllerContext = mockControllerContext.Object;
                textEditorControlelr.ModelState.AddModelError("SessionName", "Required");
            }
        }
    }
}