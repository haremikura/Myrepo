using MVCFramework.Infrastracture.DBConnection;
using MVCFramework.Infrastracture.Repositries;
using MVCFramework.Models.Entity;
using System;
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
        public PartialViewResult CrateFile(string fileName)
        {
            int newFileId = Convert.ToInt32(Session["FileId"]) + 1;
            Session["FileId"] = newFileId;
            TextFilesList textFilesList = new TextFilesList()
            {
                FileId = newFileId,
                FileName = fileName,
                Update = DateTime.Now,
                UserId = Convert.ToInt32(Session["UserId"]),
            };

            EditText editText = new EditText()
            {
                FileId = textFilesList.FileId,
                Text = "",
            };

            _context.TextFilesList.Add(textFilesList);
            _context.EditText.Add(editText);

            return null;
        }
    }
}