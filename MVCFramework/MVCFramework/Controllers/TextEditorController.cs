using MVCFramework.Infrastracture.DBConnection;
using MVCFramework.Infrastracture.Repositries;
using System.Linq;
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
            var list = _context.TextFilesList.ToList();

            return View("~/Views/TextEditor/Index.cshtml", list);
        }

        public ActionResult EditPage(int number)
        {
            string text = _context.EditText.Find(number).Text;
            return View("~/Views/TextEditor/EditPage.cshtml", text);
        }
    }
}