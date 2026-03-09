using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game4
{
    internal class user
    {
        public string Username { get; set; } // ชื่อผู้ใช้
        public string Password { get; set; } // รหัสผ่าน

        // Constructor สำหรับสร้าง User
        public user(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}

