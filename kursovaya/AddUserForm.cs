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
            
        }

        private TextBox loginTextBox;
        private TextBox passwordTextBox;
        private ComboBox statusComboBox;
        private Button okButton;
        private Button cancelButton;
    }
}
