# エラー解決のログメモ

## 2019-10-22 20:36:43 : C#

1. 問: System.MissingMethodException: このプロジェクトで、引数なしコンストラクターは定義されていません。
1. 回答（解決,因果:強）
    1. http://bbs.wankuma.com/search.cgi?no=0&word=26278&andor=and&logs=all&PAGE=20
    1. controllerのコードで、controllerクラスのコンストラクタが、引数ありだけ、という状態から、引数なしコンストラクタを追加して、解決に至る。
        ```C#
            public class TextEditorController : Controller
            {
    
                public TextEditorController()//追加した引数なしコンストラクタ
                {
            
                }
                public TextEditorController(TextEditorDbContext context)
                {
                    _context = context;
            }

                ...
            }
        ```

## 2019-10-22 20:55:10 : Razor

1. 問：コンパイラ エラー CS1003
    1. ソースエラー
            public class _Page_Views_Login_LoginView_cshtml : System.Web.Mvc.WebViewPage<CoreMVC.Models.Entity.ServiceUser;> {

    1. https://docs.microsoft.com/ja-jp/dotnet/csharp/misc/cs1003
1. 回答(解決,因果:遠回り)
    1. ログ「2019-10-22 23:43:41 」のものを見よ

## 2019-10-22 23:43:41 
1. 問：razorページが、お手本通りに表記しても、シンタックスエラーを起こす。
1. 回答(解決,因果:遠回り)   
    1. 一度プロジェクトのビルドをクリーンにする。その後、もう一度ビルドを行う。すると、治る     

## 2019-10-27 16:08:09
1. 問:Moqの試験運用で、Contextが原因で、'System.TypeInitializationException'が発生してしまう
    1. 例外がスローされました: 'System.TypeInitializationException' (EntityFramework.dll の中)。型 'System.TypeInitializationException' の例外が EntityFramework.dll で発生しましたが、ユーザー コード内ではハンドルされませんでした。The type initializer for 'System.Data.Entity.Internal.AppConfig' threw an exception.
    1. Contextで、以下の箇所でエラー 
        ```C#
            public TextEditorContext() : base("TextEditorContext") // ここで、System.TypeInitializationExceptionが発生
            {
                Configuration.LazyLoadingEnabled = false;
            }
        ```
1. 回答(解決,因果)
   
    1. 
1. 考察
    1. このエラーは、初期化におけるエラーである。 
    1. エラーのスタックトレース
        ```                    
            例外がスローされました: 'System.TypeInitializationException' (EntityFramework.dll の中)
            型 'System.TypeInitializationException' の例外が EntityFramework.dll で発生しましたが、ユーザー コード内ではハンドルされませんでした
        The type initializer for 'System.Data.Entity.Internal.AppConfig' threw an exception.

            スレッド 0x4ee8 はコード 0 (0x0) で終了しました。
            スレッド 0x4c60 はコード 0 (0x0) で終了しました。
            例外がスローされました: 'System.Reflection.TargetInvocationException' (System.Private.CoreLib.dll の中)
            at System.RuntimeMethodHandle.InvokeMethod(Object target, Object[] arguments, Signature sig, Boolean constructor, Boolean wrapExceptions)
            at System.Reflection.RuntimeConstructorInfo.Invoke(BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
            at System.RuntimeType.CreateInstanceImpl(BindingFlags bindingAttr, Binder binder, Object[] args, CultureInfo culture, Object[] activationAttributes)
            at Castle.DynamicProxy.ProxyGenerator.CreateClassProxyInstance(Type proxyType, List`1 proxyArguments, Type classToProxy, Object[] constructorArguments)
            at Moq.CastleProxyFactory.CreateProxy(Type mockType, IInterceptor interceptor, Type[] interfaces, Object[] arguments)
            at Moq.Mock`1.InitializeInstance()
            at Moq.Mock`1.OnGetObject()
            at Moq.Mock`1.get_Object()
            at XUnitTestProject2.LoginTest.TestMoq() in C:\Users\TR\OneDrive\Program\C#File\MVCFrameworkFile\GitRepository\MVCFramework\XUnitTestProject2\LoginTest.cs:line 70
        ```
```
    
    1. このエラーは、「インターフェースIDBcontextを利用して、抽象化して、見せかけにしていない」のが原因のこと。
        1. 参考
            1. http://answers.flyppdevportal.com/MVC/Post/Thread/603748b9-1a4b-446c-8be6-1349bb854f4d?category=vsunittest
            1. https://entityframework.net/ja/knowledge-base/37630564/
    1.  やり方
        1. DBcontextのクラスに、独自に作成するインターフェースIDbContextを実装する。
        1. IDbContextには、任意のDbSetのプロパティを設置する。
        1. MoqにDBContextには、以下のように、IDbContextを型パラメータにする。
    1. 予想
        
        1. DbContext直接にMoqを含めると、別機能が発動して、やりにくい。よって、IDBContextのインターフェースを使って、別機能を発動させないように、
```

## 2019-10-27 21:42:27

1. 問:ジェネリック間の型のキャスト変換がうまくいかない。
    1. 問題のコード
        ```C#
            public MockCreator(IList<IEntity> mockEntityList)
            {
                ...           
                    _mockMyEntityList = MockDbSet((IList<ServiceUser>)mockEntityList);
            }

            interface IEntity{}

            class ServerUser : IEntity
            {
                public int abc {get; set;}
                ...
            }

            private Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> list) where T : class, new()
            {
                ...
            }
    
        ```
    1. 表示されるエラー
       
        1. 
1. 答え
    1. パラメータに入る型のクラスのコンストラクタを、インターフェースを引数にとり、それを通じで、コンストラクタに、返還前のオブジェクトを
    ```C#
        public MockCreator(IList<IEntity> mockEntityList)
        {
        ...           
                    
        _mockMyEntityList = MockDbSet(mockEntityList.Select(x => new ServiceUser(x)).ToList());
            
    }

        class ServerUser : IEntity
        {
            public ServiceUser(IEntity x){
                ServiceUser current = (IEntity)x;
                Abc = current.Abc;
                ...
            }
            public int Abc {get; set;}
            ...
    }

    ```
    1. 参考
        1. https://teratail.com/questions/157052

## 2019-10-28 20:49:19
1. 問:Controllerクラスのテスト中、System.MissingMethodExceptionが発生した
    1. コードの例
        ```C#
            var mockContext = new MockCreator(dataEntity).GetMockContext().Object;
            var testController = new LoginController(mockContext);
            TryLogin(new ServiceUser() { UserName = "テスト智之", Password = "1234" });

            Assert.True(true);
            void TryLogin(ServiceUser serviceUser)
            {
                ActionResult Result = testController.Index(serviceUser);
                //System.MissingMethodException : 'Method not found: 'System.Web.HttpSessionStateBase System.Web.Mvc.Controller.get_Session()'.'

                ...
            }
        ```
	    
	1. 回答
	
	    1. ` var mockSession = new Mock<HttpSessionStateBase>(); mockControllerContext.Setup(m => m.HttpContext.Session).Returns(mockSession.Object);`を追加する。
	
	    1. コード
	
	        ```C#
	            var mockContext = new MockCreator(dataEntity).GetMockContext().Object;
	            var testController = new LoginController(mockContext);
	            TryLogin(new ServiceUser() { UserName = "テスト智之", Password = "1234"  });
	        
	            var mockSession = new Mock<HttpSessionStateBase>(); 
	            mockControllerContext
	                    .Setup(m => m.HttpContext.Session).Returns(mockSession.Object);`
	        
	            Assert.True(true);
	            void TryLogin(ServiceUser serviceUser)
	            {
	                ActionResult Result = testController.Index(serviceUser);
	                
	                ...
	            }
	        ```
	
	        
	
	    1. 上記のモックを、System.Web.HttpSessionStateBase.Sessionに対応する。
	
	    1. また、相性のせいで、上記テストをMsTestにした。
	
	    1. 参考
	
	        1.  https://dontpaniclabs.com/blog/post/2011/03/22/testing-session-in-mvc-in-four-lines-of-code/ 
	
	    
	
## 2019-11-05 22:16

1. 問:xunitにて、以下が発生した
   1. 型または名前空間の名前 'HttpSessionStateBase' が名前空間 'System.Web' に存在しません (アセンブリ参照があることを確認してください)。

##　2019-11-06 20:00

1. 課題

   1. controllerにあるUserSessionのコンストラクタで、型の違いによるエラー
   2. 
   3. 「System.InvalidCastException: 型 'Castle.Proxies.IDbContextProxy' のオブジェクトを型 'MVCFramework.Infrastracture.Repositries.TextEditorContext' にキャストできません。」

2. 解決

   1. 要約：

   
## 2019-11-06 23:43:31
1. 問:あるクラスのMockで、以下のえらーが発生した
    1. System.NotSupportedException: Unsupported expression: m => m.ServiceUser
    Non-overridable members (here: TextEditorContext.get_ServiceUser) may not be used in setup / verification expressions.
1. 解決
    1. そのクラスのメソッドが、virtualをつけていなかったから
    1. https://qiita.com/TsuyoshiUshio@github/items/ae3c2c155e904b348638
