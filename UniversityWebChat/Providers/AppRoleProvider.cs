﻿using System;
using System.Linq;
using System.Web.Security;
using UniversityWebChat.Models;
using UniversityWebChat.Models.DataBase;
using UniversityWebChat.Models.RolesAssignment;

namespace UniversityWebChat.Providers
{
    public class AppRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        } 

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = null;                                                
            using (UWCContext db = new UWCContext())
            {
                User user = db.Users.FirstOrDefault(u => u.Email == username);
                if(user != null)
                {
                    Role userRole = db.Roles.Find(user.RoleId);
                    if(userRole != null)
                    {
                        roles = new string[] 
                        {
                            userRole.Name                                          
                        }; 
                    }
                }
            }
            return roles;  
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (UWCContext db = new UWCContext())
            {
                User user = db.Users.FirstOrDefault(u => u.Email == username);
                if(user != null)
                {
                    Role userRole = db.Roles.Find(user.RoleId);
                    return (userRole != null && userRole.Name == roleName)
                        ? true
                        : false;
                }
            }
            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}


