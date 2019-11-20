using MVCFramework.Infrastracture.Repositries;
using MVCFramework.Models.Entity;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace MVCFramework.Models.Session

{
    public class UserSession
    {
        private string hashkey = "";
        private TextEditorContext _context;
        public const string SESSION_COOKIE = "markofcain";
        private ServiceUser serviceUser;

        public string Hashkey
        {
            set { hashkey = value; }
            get
            {
                if (hashkey == "" || hashkey == null)
                {
                    throw new ArgumentNullException(
                        "Hashkey for session isn't given. Set hashkey in environment value as 'HASHKEY'."
                        );
                }
                return hashkey;
            }
        }

        public const string CollectionName = "Session";

        public UserSession()
        {
            _context = new TextEditorContext();
        }

        public UserSession(IDbContext context)
        {
            _context = (TextEditorContext)context;
        }

        /// <summary>
        /// ログインしているかの状態を返す
        /// </summary>
        /// <param name="token">セッショントークン</param>
        /// <returns>bool</returns>
        //public bool IsAuthorized(string token)
        //{
        //    if (token == null)
        //    {
        //        return false;
        //    }
        //    var sessionManager = _context.CurrentSession.Find(token);
        //    var isLogin = (sessionManager == null) ? false : true;
        //    return isLogin;
        //}

        /// <summary>
        /// ランダムトークンを取得
        /// </summary>
        /// <returns>ランダムトークン</returns>
        private string GetToken()
        {
            var rng = RandomNumberGenerator.Create();
            var buff = new byte[25];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// ユーザIDとパスワードからハッシュを作成する
        /// </summary>
        /// <param name="userName">ユーザID</param>
        /// <param name="password">パスワード</param>
        /// <returns>ハッシュ</returns>
        //private string GetSHA256(string userName, string password)
        //{
        //    //HMAC-SHA1を計算する文字列
        //    var sourceString = $"{userName}-{password}";
        //    //キーとする文字列
        //    var key = Hashkey;
        //    if (string.IsNullOrEmpty(key))
        //    {
        //        throw new ArgumentException("Couldn't get Hashkey.");
        //    }

        //    //文字列をバイト型配列に変換する
        //    byte[] data = Encoding.UTF8.GetBytes(sourceString);
        //    byte[] keyData = Encoding.UTF8.GetBytes(key);

        //    byte[] bs;
        //    //HMACSHA1オブジェクトの作成
        //    using (var hmac = new HMACSHA256(keyData))
        //    {
        //        //ハッシュ値を計算
        //        bs = hmac.ComputeHash(data);
        //    }

        //    //byte型配列を16進数に変換
        //    var result = BitConverter.ToString(bs).ToLower().Replace("-", "");

        //    return result;
        //}

        /// <summary>
        /// ログインを実行する
        /// </summary>
        /// <param name="id">ユーザID</param>
        /// <param name="pw">パスワード</param>
        /// <param name="cookies">クッキー</param>
        /// <returns>ログインの成否</returns>
        public bool Login(ServiceUser checkUser)//, IResponseCookies cookies
        {
            var array = _context.ServiceUser;

            var UserIsExist = array.SingleOrDefault(
                index => index.UserName == checkUser.UserName
                      && index.Password == checkUser.Password
                      );

            if (UserIsExist != null) // ユーザが登録されていない場合
            {
                serviceUser = UserIsExist;
                return CanLogin(checkUser);//, cookies
            }
            else // ユーザが登録されていた場合
            {
                Console.WriteLine("no user");
                return false;
            }
        }

        private bool CanLogin(ServiceUser checkUser)//, IResponseCookies cookies
        {
            // パスワードをハッシュ化
            // var sha256 = GetSHA256(checkUser.UserName, checkUser.Password);

            // パスワードの一致確認
            //if (sha256 != checkUser.Password)
            //{
            //    Console.WriteLine($"not match: ${sha256}");
            //    return false;
            //}
            //else
            //{
            var token = GetToken();

            //_context.CurrentSession.Add(
            //    new CurrentSession
            //    {
            //        Id = token,
            //        CreatedAt = DateTime.Now
            //    });
            //_context.SaveChanges();

            // cookieにsecure属性を付与
            //var cookieOption = new CookieOptions()
            //{
            //    Secure = true
            //};
            //cookies.Append(SESSION_COOKIE, token, cookieOption);

            return true;
            //  }
        }

        public ServiceUser GetLoginUser() => serviceUser;
    }
}