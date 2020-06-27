using Crypteron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityWebChat.Models.DataBase;
using UniversityWebChat.Models.RolesAssignment;
using UniversityWebChat.Security;

namespace UniversityWebChat.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }       
    }
}

