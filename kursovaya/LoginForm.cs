using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace kursovaya
{
    public partial class LoginForm : Form
    {
        public static bool IsAdmin { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
        }



        private void loginButton_Click(object sender, EventArgs e)
        {
            string login = loginTextBox.Text;
            string password = passwordTextBox.Text;

            AuthenticationService authService = new AuthenticationService(Program.connectionStringColledge);

            if (authService.AuthenticateUser(login, password, out string status))
            {
                IsAdmin = (status == "admin");
                MessageBox.Show("Вход выполнен успешно!");
                MainForm mainForm = new MainForm(status, login);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Ошибка входа. Проверьте логин и пароль.");
            }
        }

        private Label loginLabel;
        private Label titleLabel;
        private Label passwordLabel;
        private TextBox loginTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
    }
}
