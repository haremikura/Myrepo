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

        /// <summary>
        /// 作成スタート画面を表示数する
        /// </summary>
        /// <returns>作成スタート画面を返す</returns>
        [NoCache]
        public ActionResult Index()
        {
            var list = _context
                        .TextFilesList
                        .OrderByDescending(index => index.Update)
                        .ToList();

            return View("~/Views/TextEditor/Index.cshtml", list);
        }

        /// <summary>
        /// テキスト入力画面を作成する
        /// </summary>
        /// <param name="fileId">入力されたファイルId</param>
        /// <returns>テキスト入力画面</returns>
        public ActionResult EditPage(string fileId)
        {
            int currentUserId
                = int.Parse(HttpSessionStateManager.GetValue(SessionBaseName.UserId));

            int fieldId = int.Parse(fileId);
            var EditText
                = _context
                    .EditText
                    .SingleOrDefault(index => index.FileId.Equals(fieldId));

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