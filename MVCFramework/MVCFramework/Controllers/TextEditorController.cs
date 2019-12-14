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
            int currentUserId
                = int.Parse(HttpSessionStateManager.GetValue(SessionBaseName.UserId));

            var EditText
                = _context
                    .EditText
                    .SingleOrDefault(index => index.FileId.Equals(number));

            HttpSessionStateManager.SetVaue(SessionBaseName.FieldId, EditText.FileId);

            EditPageDto eidtPageDto = new EditPageDto()
            {
                EditText = EditText.Text,

                MarkerList = _context.Marker
                            .Where(index => index.UserId.Equals(currentUserId))
                            .OrderBy(index => index.DisplayOrder)
                            .ToArray(),
            };

            return View("~/Views/TextEditor/EditPage.cshtml", eidtPageDto);
        }

      
        public void GetView(string updateText)
        {
            var update
                = _context
                    .EditText
                    .SingleOrDefault(
                         index => index.FileId.Equals(
                            HttpSessionStateManager.GetValue(SessionBaseName.MaxFileId)
                        )
                    );
            update.Text = updateText;
            _context.SaveChanges();
        }

      

    }


}