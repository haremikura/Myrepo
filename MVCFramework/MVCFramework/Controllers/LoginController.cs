using MVCFramework.Infrastracture.DBConnection;
using MVCFramework.Infrastracture.Repositries;
using MVCFramework.Models.Entity;
using MVCFramework.Models.Session;
using System.Linq;
using System.Web.Mvc;

namespace MVCFramework.Controllers

{
    /// <summary>
    /// 「[]
    /// </summary>
    public class LoginController : Controller
    {
        private readonly IDbContext _context;
        private readonly DbCruder _dbCruder;

        public LoginController()
        {
            _context = new TextEditorContext();
        }

        public LoginController(IDbContext textEditorContext)
        {
            _context = textEditorContext;
        }

        public ActionResult LoginView()
        {
            return View();
        }

        public ActionResult Index(ServiceUser user)
        {
            var usesession = new UserSession(_context);
            bool isAuthorized = usesession.Login(user);

            if (isAuthorized)
            {
                var loginUser = usesession.GetLoginUser();
                Session["UserName"] = loginUser.UserName;
                Session["UserId"] = loginUser.UserId;
                Session["MaxFileId"] = _context.TextFilesList.Max(index => index.FileId);
            }

            return isAuthorized ? View("~/Views/TextEditor/Index.cshtml") : View("~/Views/Login/LoginView.cshtml");

            //return isAuthorized ? new TextEditorController().Index() : View("~/Views/Login/LoginView.cshtml");
        }
    }
}