using MVCFramework.Infrastracture.DBConnection;
using MVCFramework.Infrastracture.Repositries;
using System.Web.Mvc;

namespace MVCFramework.Controllers

{
    //[AuthorizationFilter(_context)]

    public class TextEditorController : Controller
    {
        private readonly IDbContext _context = new TextEditorContext();
        private readonly DbCruder _dbCruder;

        public TextEditorController()
        {
        }

        public ActionResult Index()
        {
            return View("~/Views/TextEditor/Index.cshtml");
        }
    }
}