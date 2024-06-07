using System;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class ChangePasswordForm : Form
    {
        public string NewPassword { get; private set; }

        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            NewPassword = newPasswordTextBox.Text;

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
            if (string.IsNullOrEmpty(NewPassword))
            {
                MessageBox.Show("Пароль является обязательным.");
                return false;
            }
            return true;
        }



        private Label newPasswordLabel;
        private TextBox newPasswordTextBox;
        private Button saveButton;
        private Button cancelButton;
    }
}
