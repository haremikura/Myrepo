using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MsTest.Domain;
using MVCFramework.Controllers;
using MVCFramework.Infrastracture.Repositries;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XUnitTestProject2.Domain;

namespace MsTest
{
    [TestClass]
    class MvcHtmlStringControllerTest
    {

        private MvcHtmlStringController mvcHtmlStringController;
        private IDbContext mockDbContext;

        [TestMethod]
        public void CrateFileTest()
        {
            mockDbContext = CreateMock();
            mvcHtmlStringController = new MvcHtmlStringController(mockDbContext);

            var mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            var mockContoroller = new Mock<TextEditorController>();

            SetMockSession();

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

                var result = mvcHtmlStringController.CrateFile(fileName);

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

        }

    }
}
