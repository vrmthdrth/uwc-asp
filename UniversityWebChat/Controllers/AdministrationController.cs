using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityWebChat.Models.Administration;
using UniversityWebChat.Models.DataBase;
using UniversityWebChat.Models.RolesAssignment;
using UniversityWebChat.Security;

namespace UniversityWebChat.Controllers
{
    public class AdministrationController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult SetAdminRecord()
        {
            ViewBag.AllowedRoles = new SelectList(new string[] { "Преподаватель", "Студент" });
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult SetAdminRecord(SetAdminRecordModel model)
        {
            if (ModelState.IsValid)
            {
                using (UWCContext db = new UWCContext())
                {
                    string roleName = model.RoleName == "Преподаватель"
                                    ? UserRoles.TEACHER_ROLE_NAME
                                    : UserRoles.STUDENT_ROLE_NAME;

                    AdminRecord record = db.AdminRecords.FirstOrDefault(r => r.RoleName == roleName);
                    Guid salt = Guid.NewGuid();
                    if (record != null)
                    {
                        record.Salt = salt;
                        record.Password = Rfc2898Encoder.Encode(model.AccessPassword, salt.ToString());
                        db.Entry(record).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        AdminRecord newRecord = new AdminRecord()
                        {
                            Salt = salt,
                            RoleName = roleName,
                            Password = Rfc2898Encoder.Encode(model.AccessPassword, salt.ToString())
                        };


                        db.AdminRecords.Add(newRecord);
                        db.SaveChanges();
                    }
                    return RedirectToAction("AdminRecordAddedMessage", "Administration");
                }
            }
            ViewBag.AllowedRoles = new SelectList(new string[] { "Преподаватель", "Студент" });
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AdminRecordAddedMessage()
        {
            return View();
        }
    }
}