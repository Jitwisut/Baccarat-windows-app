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

namespace game4
{
    public partial class FormRegister : Form
    {

        public FormRegister()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(username) || password.Length != 6)
            {
                MessageBox.Show("กรุณากรอกชื่อผู้ใช้ และรหัสผ่าน 6 หลัก!");
                return;
            }

            if (UserManager.IsUserExists(username))
            {
                MessageBox.Show("ชื่อผู้ใช้นี้มีอยู่แล้ว!");
                return;
            }

            UserManager.AddUser(username, password);
            MessageBox.Show("สมัครสมาชิกสำเร็จ!");

            FormLogin loginForm = new FormLogin();
            this.Hide();
            loginForm.Show();
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // หากกด Enter ให้คลิกปุ่ม Login
                button1.PerformClick();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormRegister_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                // หากกด Enter ให้คลิกปุ่ม Login
                button1.PerformClick();

            }
        }
    }
} 

   
