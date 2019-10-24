namespace MVCFramework.Infrastracture.DBConnection

{
    //internal class AuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    //{
    //    private TextEditorDbContext _context;
    //    public AuthorizationFilterAttribute(TextEditorDbContext context)
    //    {
    //        _context = context;
    //    }
    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        var token
    //            = context.HttpContext.Request.Cookies[UserSession.SESSION_COOKIE];

    //        if (!new UserSession(_context).IsAuthorized(token))
    //        {
    //            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
    //        }
    //    }
    //}
}