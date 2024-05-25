using System.Windows.Forms;

namespace kursovaya
{
    partial class AddUserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

            this.loginTextBox = new TextBox { Text = "Логин", Dock = DockStyle.Top };
            this.passwordTextBox = new TextBox { Text = "Пароль", Dock = DockStyle.Top, UseSystemPasswordChar = false };
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

        #endregion
    }
}