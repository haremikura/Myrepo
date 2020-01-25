using MVCFramework.Infrastracture.Repositries;
using MVCFramework.Models;
using MVCFramework.Models.Entity;
using System;
using System.Web.Mvc;

namespace MVCFramework.Controllers
{
    public class MvcHtmlStringController : Controller
    {

        private readonly IDbContext _context = new TextEditorContext();
        //private readonly DbCruder _dbCruder;

        public MvcHtmlStringController(IDbContext mockDbContext)
        {
            _context = mockDbContext;
        }

        public MvcHtmlStringController()
        {
        }

        /// <summary>
        /// テキストファイルを作成として、DBにTextFilesListと、EditTextエンティティを登録する。
        /// </summary>
        /// <param name="fileName">ファイル名を</param>
        /// <returns></returns>
        public MvcHtmlString CrateFile(string fileName)
        {


            int newFileId = Convert.ToInt32(HttpSessionStateManager.GetValue(SessionBaseName.MaxFileId)) + 1;
            HttpSessionStateManager.SetVaue(SessionBaseName.MaxFileId, newFileId);

            TextFilesList textFilesList = new TextFilesList()
            {
                FileId = newFileId,
                FileName = fileName,
                Update = DateTime.Now,
                UserId = Convert.ToInt32(HttpSessionStateManager.GetValue(SessionBaseName.UserId)),
            };

            EditText editText = new EditText()
            {
                FileId = textFilesList.FileId,
                Text = "",
            };

            _context.TextFilesList.Add(textFilesList);
            _context.EditText.Add(editText);
            _context.SaveChanges();
            return MvcHtmlString.Create(new PartailView().GetFileSelectButton(textFilesList));


        }

        /// <summary>
        /// ファイル
        /// </summary>
        /// <param name="htmlElement"></param>
        /// <param name="markText"></param>
        /// <param name="colorCode"></param>
        /// <returns></returns>
        public MvcHtmlString CrateFileView(string htmlElement, string markText, string colorCode)
        {
            return MvcHtmlString.Create(
              new PartailView().GetMarkerColorIndex(htmlElement, markText, colorCode)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementText"></param>
        /// <param name="markedText"></param>
        /// <param name="caretPosition"></param>
        /// <param name="colorCode"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public MvcHtmlString GetMarkText(string elementText, string markedText, int caretPosition, string colorCode)
        {
            return MvcHtmlString.Create(
              new PartailView().GetMarkerText(elementText, markedText, caretPosition, colorCode)
                );
        }


    }
}