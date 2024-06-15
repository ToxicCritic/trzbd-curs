using System;
using System.Data;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class AddEditDepartmentForm : Form
    {
        public string DepartmentName { get; private set; }
        public string Building { get; private set; }

        public AddEditDepartmentForm()
        {
            InitializeComponent();
        }

        public AddEditDepartmentForm(DataRow departmentRow) : this()
        {
            if (departmentRow != null)
            {
                DepartmentName = departmentRow["DepartmentName"].ToString();
                Building = departmentRow["Building"].ToString();

                departmentNameTextBox.Text = DepartmentName;
                buildingTextBox.Text = Building;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DepartmentName = departmentNameTextBox.Text;
            Building = buildingTextBox.Text;

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
            if (string.IsNullOrEmpty(DepartmentName))
            {
                MessageBox.Show("Название отдела является обязательным.");
                return false;
            }
            if (string.IsNullOrEmpty(Building))
            {
                MessageBox.Show("Здание является обязательным.");
                return false;
            }
            return true;
        }

        private Label departmentNameLabel;
        private TextBox departmentNameTextBox;
        private Label buildingLabel;
        private TextBox buildingTextBox;
        private Button saveButton;
        private Button cancelButton;
    }
}
