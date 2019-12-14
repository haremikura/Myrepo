using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MsTest.Domain;
using MVCFramework.Controllers;
using MVCFramework.Infrastracture.Repositries;
using MVCFramework.Models;
using MVCFramework.Models.DataTransferObject;
using MVCFramework.Models.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
        public void LoginTest()
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
            testViewResult[0]
                = DebugAndGetViewResult(
                    new ServiceUser() { UserName = "テスト智之", Password = "1234" }
                    )
                .ViewName;

            testViewResult[1]
                = DebugAndGetViewResult(
                    new ServiceUser() { UserName = "テスト朋美", Password = "7890" }
                    )
                .ViewName;

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
        public void IndexTest()
        {
            mockDbContext = CreateMock();
            textEditorControlelr = new TextEditorController(mockDbContext);

            var mockControllerContext = new Mock<ControllerContext>();

            SetMockController();
            TestAndDebug();

            IDbContext CreateMock()
            {
                var list = new List<IEntity>()
                    {
                        new TextFilesList { FileId = 1, FileName = "testFileList", UserId = 1 ,Update =  DateTime.Parse("2018/05/01 12:34:56")},
                         new TextFilesList { FileId = 2, FileName = "testFileList", UserId = 1 ,Update =  DateTime.Parse("2018/05/01 12:37:56")},
                        new TextFilesList { FileId = 4, FileName = "testFileList", UserId = 2, Update =  DateTime.Parse("2019/06/01 12:34:56")}
                    };

                var mock = new MockCreator(list);

                return mock.GetMockContext().Object;
            }

            void TestAndDebug()
            {
                string Before1
                    = ViewEntity.WriteEntityData(
                        mockDbContext.TextFilesList.ToList()
                        );

                Debug.WriteLine($"Before List :\r {Before1}");

                ViewResult result = textEditorControlelr.Index() as ViewResult;

                string After1
                   = ViewEntity.WriteEntityData(
                       (IEnumerable<IEntity>)result.Model
                       );
                Debug.WriteLine($"After View :\r {After1}");
            }

            void SetMockController()
            {

                textEditorControlelr.ControllerContext = mockControllerContext.Object;
                textEditorControlelr.ModelState.AddModelError("SessionName", "Required");
            }
        }



        [TestMethod]
        public void EditPageTest()
        {
            mockDbContext = CreateMock();
            textEditorControlelr = new TextEditorController(mockDbContext);

            var mockControllerContext = new Mock<ControllerContext>();

            SetMockSession();
            SetMockController();
            TestAndDebug();

            IDbContext CreateMock()
            {
                var fileList = new List<IEntity>()
                    {
                        new EditText { FileId = 1, Text = "testFileList",},
                        new EditText { FileId = 2, Text = "testFileList2",},
                    };

                var markerList = new List<IEntity>()
                    {
                        new Marker() { MarkerId = 1, Name = "color1", UserId = 1 ,Color="#998877",DisplayOrder=1},
                        new Marker() { MarkerId = 1, Name = "color2", UserId = 1 ,Color="#998877",DisplayOrder=2},
                        new Marker() { MarkerId = 2, Name = "color3", UserId = 2 ,Color="#665544",DisplayOrder=2},

                    };

                var mock = new MockCreator(fileList);
                mock.SetMock(markerList);
                return mock.GetMockContext().Object;
            }

            void TestAndDebug()
            {
                string Before1
                  = ViewEntity.WriteEntityData(
                      mockDbContext.Marker.ToArray()
                      );
                Debug.WriteLine($"Before List :\r {Before1}");

                int UserId = 1;

                ViewResult result = textEditorControlelr.EditPage(UserId) as ViewResult;

                EditPageDto editPageDto = (EditPageDto)result.Model;

                string After1
                   = ViewEntity.WriteEntityData(
                       editPageDto.MarkerList
                       );

                Debug.WriteLine($"After View :\r {After1}");
            }

            void SetMockSession()
            {
                mockControllerContext.Setup(x => x.HttpContext.Session["UserId"]).Returns("1");
            }

            void SetMockController()
            {

                textEditorControlelr.ControllerContext = mockControllerContext.Object;
                textEditorControlelr.ModelState.AddModelError("SessionName", "Required");
            }
        }


        [TestMethod]
        public void GetViewTest()
        {
            mockDbContext = CreateMock();
            textEditorControlelr = new TextEditorController(mockDbContext);


            var mockControllerContext = new Mock<ControllerContext>();
            SetMockSession();
            SetMockController();
            TestAndDebug();
            IDbContext CreateMock()
            {
                var list = new List<IEntity>()
                {
                    new EditText{FileId=1,Text = "aaabbcc"},
                    new EditText{FileId=2,Text = "ddeeff"}

                };

                var mock = new MockCreator(list);

                return mock.GetMockContext().Object;
            }

            void SetMockSession()
            {
                HttpSessionStateManager.SetVaue(SessionBaseName.MaxFileId, 1);

            }

            void SetMockController()
            {
                textEditorControlelr.ControllerContext = mockControllerContext.Object;
                textEditorControlelr.ModelState.AddModelError("SessionName", "Required");
            }

            void TestAndDebug()
            {
                string Before1
                 = ViewEntity.WriteEntityData(
                     mockDbContext.EditText.ToArray()
                     );
                Debug.WriteLine($"Before List :\r {Before1}");

                textEditorControlelr.GetView("11223344");

                Type type = textEditorControlelr.GetType();

                FieldInfo newContext = type.GetField("_context", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance);

                TextEditorContext context
                    = newContext.GetValue(textEditorControlelr) as TextEditorContext;

                string After1
                   = ViewEntity.WriteEntityData(
                       context.EditText.ToArray()
                       );

                Debug.WriteLine($"After View :\r {After1}");
            }
        }


    }
}