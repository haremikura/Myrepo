using MVCFramework.Infrastracture.DBConnection;
using MVCFramework.Infrastracture.Repositries;
using MVCFramework.Models;
using MVCFramework.Models.Entity;
using MVCFramework.Models.Session;
using System.Linq;
using System.Web.Mvc;

namespace MVCFramework.Controllers

{
    /// <summary>
    /// ログイン処理を担うコントローラー
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

        /// <summary>
        /// ログインを認証して、テキストディタページに戻る。
        /// </summary>
        /// <param name="user">入力されたユーザーエンティティ</param>
        /// <returns>データーベースに存在する名前とパスワードなら、</returns>
        public ActionResult Index(ServiceUser user)
        {

            var usesession = new UserSession();
            bool isAuthorized = usesession.Login(user);

            if (isAuthorized)
            {
                var loginUser = usesession.GetLoginUser();
                HttpSessionStateManager.SetVaue(SessionBaseName.UserName, loginUser.UserName);
                HttpSessionStateManager.SetVaue(SessionBaseName.UserId, loginUser.UserId);
                HttpSessionStateManager.SetVaue(SessionBaseName.MaxFileId, _context.TextFilesList.Max(index => index.FileId));
            }

            //テスト用コード
            //return isAuthorized ? View("~/Views/TextEditor/Index.cshtml") : View("~/Views/Login/LoginView.cshtml");

            return isAuthorized ? new TextEditorController().Index() : View("~/Views/Login/LoginView.cshtml");
        }
    }
}