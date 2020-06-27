using Crypteron;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using UniversityWebChat.Models.DataBase;
using UniversityWebChat.Security;

namespace UniversityWebChat.Models
{
    public class PrivateRoom
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string IV { get; set; } 
        public List<Guid> UserIds { get; set; } 
        public ICollection<MessageInfo> MessagesListing { get; set; } 
    }

    public class MessageInfo
    {
        public Guid Id { get; set; } 
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
    }
}