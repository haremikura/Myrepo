using MVCFramework.Infrastracture.Repositries;
using MVCFramework.Models;
using MVCFramework.Models.DataTransferObject;
using MVCFramework.Models.Entity;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFramework.Controllers

{
    //[AuthorizationFilter(_context)]

    public class TextEditorController : Controller
    {
        private readonly IDbContext _context = new TextEditorContext();
        //private readonly DbCruder _dbCruder;

        public TextEditorController(IDbContext mockDbContext)
        {
            _context = mockDbContext;
        }

        public TextEditorController()
        {
        }


        [NoCache]
        public ActionResult Index()
        {
            var list = _context
                        .TextFilesList
                        .OrderByDescending(index => index.Update)
                        .ToList();

            return View("~/Views/TextEditor/Index.cshtml", list);
        }

        public ActionResult EditPage(int number)
        {
            int currentUserId = int.Parse(Session["UserId"].ToString());
            EditPageDto eidtPageDto = new EditPageDto()
            {
                EditText = _context.EditText
                            .SingleOrDefault(index => index.FileId.Equals(number))
                            .Text,
                MarkerList = _context.Marker
                            .Where(index => index.UserId.Equals(currentUserId))
                            .OrderBy(index => index.DisplayOrder)
                            .ToArray(),
            };

            return View("~/Views/TextEditor/EditPage.cshtml", eidtPageDto);
        }

        public MvcHtmlString CrateFile(string fileName)
        {


            int newFileId = Convert.ToInt32(Session["MaxFileId"]) + 1;
            Session["MaxFileId"] = newFileId;

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
            _context.SaveChanges();
            return MvcHtmlString.Create(new PartailView().GetButton(textFilesList));


        }

        public MvcHtmlString CrateFileView(string htmlElement, string markText, string colorCode)
        {
            return MvcHtmlString.Create(
              new PartailView().GetColor(htmlElement, markText, colorCode)
                );
        }

        public MvcHtmlString MarkText(string elementText, string markedText, int caretPosition, string colorCode)
        {
            return MvcHtmlString.Create(
              new PartailView().GetMarkerText(elementText, markedText, caretPosition, colorCode)
                );
        }
    }


}