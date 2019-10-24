using MVCFramework.Infrastracture.DBConnection;
using MVCFramework.Repositries;
using System.Web.Mvc;

namespace MVCFramework.Controllers

{
    //[AuthorizationFilter(_context)]

    public class TextEditorController : Controller
    {
        private readonly TextEditorContext _context = new TextEditorContext();
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