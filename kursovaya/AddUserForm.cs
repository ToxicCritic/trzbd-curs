using System;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class AddUserForm : Form
    {
        public string Login => loginTextBox.Text;
        public string Password => passwordTextBox.Text;
        public string Status => statusComboBox.SelectedItem.ToString();

        public AddUserForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.loginTextBox = new TextBox { Text = "Логин", Dock = DockStyle.Top };
            this.passwordTextBox = new TextBox { Text = "Пароль", Dock = DockStyle.Top, UseSystemPasswordChar = true };
            this.statusComboBox = new ComboBox { Dock = DockStyle.Top };
            this.statusComboBox.Items.AddRange(new string[] { "admin", "user" });
            this.okButton = new Button { Text = "OK", DialogResult = DialogResult.OK, Dock = DockStyle.Bottom };
            this.cancelButton = new Button { Text = "Отмена", DialogResult = DialogResult.Cancel, Dock = DockStyle.Bottom };

            this.Controls.Add(loginTextBox);
            this.Controls.Add(passwordTextBox);
            this.Controls.Add(statusComboBox);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);

            this.Text = "Добавить пользователя";
        }

        private TextBox loginTextBox;
        private TextBox passwordTextBox;
        private ComboBox statusComboBox;
        private Button okButton;
        private Button cancelButton;
    }
}
