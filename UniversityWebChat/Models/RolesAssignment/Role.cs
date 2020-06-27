
namespace UniversityWebChat.Models.RolesAssignment
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public struct UserRoles
    {
        public const int ADMIN_ROLE_ID = 1;
        public const int TEACHER_ROLE_ID = 2;
        public const int STUDENT_ROLE_ID = 3;
        public const string ADMIN_ROLE_NAME = "admin";
        public const string TEACHER_ROLE_NAME = "teacher";
        public const string STUDENT_ROLE_NAME = "student";
    }
}