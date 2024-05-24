using System.Windows.Forms;

namespace kursovaya
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle = new Label();
        private Label lblUsername = new Label();
        private Label lblPassword = new Label();
        private TextBox txtUsername = new TextBox();
        private TextBox txtPassword = new TextBox();
        private Button btnLogin = new Button();
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // Настройки формы
            this.Text = "Login";
            this.Size = new System.Drawing.Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Заголовок
            Label lblTitle = new Label();
            lblTitle.Text = "Login to Sports School";
            lblTitle.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new System.Drawing.Point(100, 30);
            this.Controls.Add(lblTitle);

            // Метка для имени пользователя
            Label lblUsername = new Label();
            lblUsername.Text = "Username:";
            lblUsername.Location = new System.Drawing.Point(50, 80);
            lblUsername.AutoSize = true;
            this.Controls.Add(lblUsername);

            // Текстовое поле для имени пользователя
            TextBox txtUsername = new TextBox();
            txtUsername.Name = "txtUsername";
            txtUsername.Location = new System.Drawing.Point(150, 80);
            txtUsername.Width = 200;
            this.Controls.Add(txtUsername);

            // Метка для пароля
            Label lblPassword = new Label();
            lblPassword.Text = "Password:";
            lblPassword.Location = new System.Drawing.Point(50, 120);
            lblPassword.AutoSize = true;
            this.Controls.Add(lblPassword);

            // Текстовое поле для пароля
            TextBox txtPassword = new TextBox();
            txtPassword.Name = "txtPassword";
            txtPassword.Location = new System.Drawing.Point(150, 120);
            txtPassword.Width = 200;
            txtPassword.PasswordChar = '*';
            this.Controls.Add(txtPassword);

            // Кнопка для входа
            Button btnLogin = new Button();
            btnLogin.Text = "Login";
            btnLogin.Location = new System.Drawing.Point(150, 160);
            btnLogin.Width = 100;
            btnLogin.Click += BtnLogin_Click;
            this.Controls.Add(btnLogin);
        }

        #endregion
    }
}