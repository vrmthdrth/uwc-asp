using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.AspNet.SignalR;
using UniversityWebChat.Models;
using UniversityWebChat.Models.DataBase;
using UniversityWebChat.Security;

namespace UniversityWebChat.Hubs
{
    
    public class ChatHub : Hub
    {
        public void SendPrivateMessage(string userId, string message)
        {
            using(UWCContext db = new UWCContext())
            {
                User senderUser = db
                                 .Users
                                 .Where(u => u.Name == HttpContext.Current.User.Identity.Name)
                                 .FirstOrDefault();

                User recieverUser = db
                                    .Users
                                    .Where(u => u.Id.ToString() == userId)
                                    .FirstOrDefault();

                PrivateRoom privateRoom = db
                                          .PrivateRooms
                                          .Where(pr => pr.UserIds.Contains(senderUser.Id) && pr.UserIds.Contains(Guid.Parse(userId)))
                                          .FirstOrDefault();

                if (privateRoom == null)
                {
                    privateRoom = ChatHub.CreatePrivateRoom(senderUser.Id, recieverUser.Id);
                    db.PrivateRooms.Add(privateRoom);
                }

                string encodedMessage = AesEncoder.EncryptToBytes(message, Encoding.Default.GetBytes(privateRoom.Key), Encoding.Default.GetBytes(privateRoom.IV)).ToString();
                privateRoom.MessagesListing.Add(new MessageInfo()
                {
                    Id = Guid.NewGuid(),
                    FromUserId = senderUser.Id,
                    FromUserName = senderUser.Name,
                    ToUserId = recieverUser.Id,
                    ToUserName = recieverUser.Name,
                    Message = encodedMessage, 
                    Time = DateTime.UtcNow
                });
                db.Entry(privateRoom).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                Groups.Add(senderUser.Id.ToString(), privateRoom.Id.ToString());
                Groups.Add(recieverUser.Id.ToString(), privateRoom.Id.ToString());
                Clients.Group(privateRoom.Id.ToString(), senderUser.Id.ToString()).send(message); 
            }   
        }

        private static PrivateRoom CreatePrivateRoom(Guid senderId, Guid recieverId)
        {
            Random rnd = new Random();
            byte[] ivBytes = new byte[16];
            byte[] keyBytes = new byte[32];
            rnd.NextBytes(ivBytes);
            rnd.NextBytes(keyBytes);

            return new PrivateRoom
            {
                Id = Guid.NewGuid(),
                IV = Encoding.Default.GetString(ivBytes),
                Key = Encoding.Default.GetString(keyBytes),
                MessagesListing = new List<MessageInfo>(),
                UserIds = new List<Guid>()
            };
        }
    }
}

