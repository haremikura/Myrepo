using MVCFramework.Infrastracture.DBConnection;
using MVCFramework.Models.Entity;
using MVCFramework.Models.Session;
using MVCFramework.Infrastracture.Repositries;
using System.Web.Mvc;

namespace MVCFramework.Controllers

{
    /// <summary>
    /// 「[]
    /// </summary>
    public class LoginController : Controller
    {
        private readonly IDbContext _context = new TextEditorContext();
        private readonly DbCruder _dbCruder;

        public ActionResult LoginView()
        {
            return View();
        }

        public ActionResult Index(ServiceUser user)
        {

            bool isAuthorized = new UserSession((TextEditorContext)_context).Login(user);

            if (isAuthorized)
            {
                Session["UserName"] = user.UserName;
            }

            return isAuthorized ? new TextEditorController().Index() : View("~/Views/Login/LoginView.cshtml");
        }
    }
}