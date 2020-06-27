using System.Linq;
using System.Web.Mvc;
using UniversityWebChat.Models;
using UniversityWebChat.Models.DataBase;
using UniversityWebChat.Providers;

namespace UniversityWebChat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult ShowProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                using(UWCContext db = new UWCContext())
                {
                    User user = db
                                .Users
                                .Where(u => u.Email == User.Identity.Name)
                                .FirstOrDefault();
                    ViewBag.User = user;
                    ViewBag.UserRoleName = new AppRoleProvider().GetRolesForUser(User.Identity.Name)[0];
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Обратная связь";
            return View();
        }
    }
}