using System;

namespace UniversityWebChat.Models.Administration
{
    public class AdminRecord
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public Guid Salt { get; set; }
        public string Password { get; set; }
    }
}