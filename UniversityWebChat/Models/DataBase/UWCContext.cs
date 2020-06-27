using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UniversityWebChat.Models.RolesAssignment;
using UniversityWebChat.Models.Administration;

namespace UniversityWebChat.Models.DataBase
{
    public class UWCContext : DbContext
    {
        public UWCContext() : base("UWCDefaultConnection")
        {
            Crypteron.CipherDb.Session.Create(this);                                
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AdminRecord> AdminRecords { get; set; }
        public DbSet<PrivateRoom> PrivateRooms { get; set; }
    }
}

