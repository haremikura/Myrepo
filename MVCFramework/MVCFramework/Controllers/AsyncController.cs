using MVCFramework.Infrastracture.Repositries;
using MVCFramework.Models.Entity;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace MVCFramework.Controllers
{
    public class AsyncController : Controller
    {

        private TextEditorContext _context = new TextEditorContext();
        [HttpGet]
        public ViewResult List()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Details(int Id)

        {
            return View(Id);
        }

        [HttpGet]
        public ViewResult Edit(int Id)

        {
            return View(Id);
        }

        [HttpGet]
        public PartialViewResult AsyncList()

        {
            var users = _context.ServiceUser.ToList();

            return PartialView(users);
        }

        [HttpGet]
        public PartialViewResult AsyncDetails(int Id)

        {
            var user = new ServiceUser() { UserId = 1, UserName = "asdf", Password = "aa" };

            return PartialView(user);
        }

        [HttpGet]
        public PartialViewResult AsyncEdit(int Id)

        {
            var user = _context.ServiceUser.Find(Id);

            return PartialView(user);
        }

        [HttpPost]
        public PartialViewResult AsyncEdit(ServiceUser model)

        {
            if (ModelState.IsValid)
            {
                _context.ServiceUser.Add(model);

                ViewBag["Inji"] = "OK";
            }
            else
            {
                var res = ModelState.Values;

                ViewBag["Inji"] = "False";
            }

            return PartialView(model);
        }
    }
}