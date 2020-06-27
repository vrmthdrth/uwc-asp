using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UniversityWebChat.Models.Administration;
using UniversityWebChat.Models.RolesAssignment;
using UniversityWebChat.Security;
using Crypteron.CipherDb;

namespace UniversityWebChat.Models.DataBase
{
    public class UWCDBInitializer : CreateDatabaseIfNotExists<UWCContext>
    {
        protected override void Seed(UWCContext db)
        {
            db.Roles.Add(new Role { Id = UserRoles.ADMIN_ROLE_ID, Name = UserRoles.ADMIN_ROLE_NAME });
            db.Roles.Add(new Role { Id = UserRoles.TEACHER_ROLE_ID, Name = UserRoles.TEACHER_ROLE_NAME });
            db.Roles.Add(new Role { Id = UserRoles.STUDENT_ROLE_ID, Name = UserRoles.STUDENT_ROLE_NAME });

            Guid uid = Guid.NewGuid();

            db.Users.Add(new User
            {
                Id = uid,
                Surname = "Иванов",
                Name = "Иван",
                Patronymic = "Иванович",
                Age = 22,
                Email = "ivanov.ivan9999@gmail.com",
                Password = Rfc2898Encoder.Encode("PK2a82mxu", uid.ToString()),
                RoleId = UserRoles.ADMIN_ROLE_ID
            });

            base.Seed(db);
        }
    }
}


