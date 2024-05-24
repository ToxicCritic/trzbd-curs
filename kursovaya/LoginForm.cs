using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class LoginForm : Form
    {
        public static bool IsAdmin { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = this.Controls["txtUsername"].Text;
            string password = this.Controls["txtPassword"].Text;

            if (IsValidUser(username, password, out bool isAdmin))
            {
                IsAdmin = isAdmin;
                this.Hide();
                MainForm mainForm = new MainForm();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private bool IsValidUser(string username, string password, out bool isAdmin)
        {
            // Пример проверки (лучше использовать хеширование паролей и безопасное хранение)
            if (username == "admin" && password == "1")
            {
                isAdmin = true;
                return true;
            }
            else if (username == "user" && password == "userpassword")
            {
                isAdmin = false;
                return true;
            }

            isAdmin = false;
            return false;
        }
    }

}
