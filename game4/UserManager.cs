using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game4
{
    internal class UserManager
    {
        // รายการผู้ใช้ (List<User>)
        public static List<user> UserList { get; set; } = new List<user>();

        // Method สำหรับเพิ่มผู้ใช้
        public static void AddUser(string username, string password)
        {
            UserList.Add(new user(username, password));
        }

        // Method สำหรับตรวจสอบว่าผู้ใช้มีอยู่หรือไม่
        public static bool IsUserExists(string username)
        {
            return UserList.Exists(u => u.Username == username);
        }

        // Method สำหรับตรวจสอบล็อกอิน
        public static user ValidateUser(string username, string password)
        {
            return UserList.Find(u => u.Username == username && u.Password == password);
        }
    }
}

