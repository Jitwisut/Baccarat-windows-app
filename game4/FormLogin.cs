using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq; // สำหรับใช้ Exists หรือ LINQ

namespace game4
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            var user = UserManager.ValidateUser(username, password);

            if (user != null)
            {
                MessageBox.Show("เข้าสู่ระบบสำเร็จ!");

                // สร้าง FormGame
                FormBaccarat gameForm = new FormBaccarat();
                gameForm.Username = username;  // ตั้งค่า username ผ่าน Public Field
                this.Hide();
                gameForm.Show();
            }
            else
            {
                MessageBox.Show("ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง!");
            }

        }

        private void d(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {



        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void FormLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // หากกด Enter ให้คลิกปุ่ม Login
                button1.PerformClick();

            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // สร้างฟอร์ม Register และซ่อนฟอร์ม Login
            FormRegister registerForm = new FormRegister();
            this.Hide();  // ซ่อนหน้า Login
            registerForm.Show();  // เปิดหน้า Register

        }
    }
    }


    