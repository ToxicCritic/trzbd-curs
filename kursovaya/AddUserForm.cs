using System;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class AddUserForm : Form
    {
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Status { get; private set; }

        public AddUserForm()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Login = loginTextBox.Text;
            Password = passwordTextBox.Text;
            Status = statusComboBox.SelectedItem.ToString();

            if (ValidateForm())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(Login))
            {
                MessageBox.Show("Логин является обязательным.");
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Пароль является обязательным.");
                return false;
            }
            if (string.IsNullOrEmpty(Status))
            {
                MessageBox.Show("Статус является обязательным.");
                return false;
            }
            return true;
        }



        private Label loginLabel;
        private TextBox loginTextBox;
        private Label passwordLabel;
        private TextBox passwordTextBox;
        private Label statusLabel;
        private ComboBox statusComboBox;
        private Button saveButton;
        private Button cancelButton;
    }
}
