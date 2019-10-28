using MVCFramework.Infrastracture.DBConnection;
using MVCFramework.Infrastracture.Repositries;
using MVCFramework.Models.Entity;
using MVCFramework.Models.Session;
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
            bool isAuthorized = new UserSession(_context).Login(user);

            if (isAuthorized)
            {
                Session["UserName"] = user.UserName;
            }

            return isAuthorized ? new TextEditorController().Index() : View("~/Views/Login/LoginView.cshtml");
        }
    }
}