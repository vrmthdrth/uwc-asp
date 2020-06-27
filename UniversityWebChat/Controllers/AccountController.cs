using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using UniversityWebChat.Models;
using UniversityWebChat.Models.DataBase;
using UniversityWebChat.Models.Authentication;
using UniversityWebChat.Models.RolesAssignment;
using UniversityWebChat.Models.Administration;
using UniversityWebChat.Security;

namespace UniversityWebChat.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("IsAlreadyAuthenticated", "Account");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (UWCContext db = new UWCContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email);
                }

                if (user != null && Rfc2898Encoder.Validate(model.Password, user.Password, user.Id.ToString()))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("IsAlreadyAuthenticated", "Account");
            }
            ViewBag.AllowedRoles = new SelectList(new string[] { "Преподаватель", "Студент" } );
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using(UWCContext db = new UWCContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email);
                }

                if(user == null)
                {
                    using(UWCContext db = new UWCContext())
                    {
                        string roleName = model.RoleName == "Преподаватель"
                                   ? UserRoles.TEACHER_ROLE_NAME
                                   : UserRoles.STUDENT_ROLE_NAME;

                        AdminRecord adminRecord = db.AdminRecords.FirstOrDefault(r => r.RoleName == roleName);
                        if(adminRecord != null)
                        {

                            Guid uid = Guid.NewGuid();
                            string userSalt = uid.ToString();
                            string encodedPassword = Rfc2898Encoder.Encode(model.Password, userSalt);

                            if (Rfc2898Encoder.Validate(model.RoleAccessPassword, adminRecord.Password, adminRecord.Salt.ToString()))
                            {
                                User newUser = new User()
                                {
                                    Id = uid,
                                    Surname = model.Surname,
                                    Name = model.Name,
                                    Patronymic = model.Patronymic,
                                    Age = model.Age,
                                    Email = model.Email,
                                    Password = encodedPassword,
                                    RoleId = model.RoleName == "Преподаватель"
                                             ? UserRoles.TEACHER_ROLE_ID
                                             : UserRoles.STUDENT_ROLE_ID
                                };

                                db.Users.Add(newUser);
                                db.SaveChanges();
                            }
                            else
                            {
                                ModelState.AddModelError("", "Неверный пользовательский или преподавательский пароль");
                            }

                            user = db.Users.Where(u => u.Email == model.Email && u.Password == encodedPassword).FirstOrDefault();
                        }
                        else
                        {
                            ModelState.AddModelError("", "Пароль для регистрации с ролью \"" + roleName + "\" еще не задан администратором, попробуйте позже.");
                        }
                    }
                    if(user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользоваетль с таким адресом Email уже существует");
                }
            }

            ViewBag.AllowedRoles = new SelectList(new string[] { "Преподаватель", "Студент" });
            return View(model);
        }

        [HttpGet]
        public ActionResult AuthNeeded()
        {
            return View();
        }

        [HttpGet]
        public ActionResult IsAlreadyAuthenticated()
        {
            return View();
        }

    }
}