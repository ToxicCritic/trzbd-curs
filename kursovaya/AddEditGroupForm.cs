using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class AddEditGroupForm : Form
    {
        private readonly string connectionString = @"Server=ADCLG1;Database=Лифляндский_СпортШколаОлимпРезерва;Integrated Security=true;";

        public int TrainerID { get; private set; }
        public string Department { get; private set; }

        public AddEditGroupForm()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        public AddEditGroupForm(DataRow groupRow) : this()
        {
            if (groupRow != null)
            {
                TrainerID = (int)groupRow["TrainerID"];
                Department = groupRow["Department"].ToString();

                trainerComboBox.SelectedValue = TrainerID;
                departmentComboBox.SelectedValue = Department;
            }
        }

        private void LoadComboBoxData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlDataAdapter trainerAdapter = new SqlDataAdapter("SELECT id, TrainerFIO FROM Trainers", connection);
                DataTable trainerTable = new DataTable();
                trainerAdapter.Fill(trainerTable);
                trainerComboBox.DataSource = trainerTable;
                trainerComboBox.DisplayMember = "TrainerFIO";
                trainerComboBox.ValueMember = "id";

                SqlDataAdapter departmentAdapter = new SqlDataAdapter("SELECT DepartmentName FROM Departments", connection);
                DataTable departmentTable = new DataTable();
                departmentAdapter.Fill(departmentTable);
                departmentComboBox.DataSource = departmentTable;
                departmentComboBox.DisplayMember = "DepartmentName";
                departmentComboBox.ValueMember = "DepartmentName";
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            TrainerID = (int)trainerComboBox.SelectedValue;
            Department = departmentComboBox.SelectedValue.ToString();

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
            if (TrainerID == 0)
            {
                MessageBox.Show("Пожалуйста, выберите тренера.");
                return false;
            }
            if (string.IsNullOrEmpty(Department))
            {
                MessageBox.Show("Пожалуйста, выберите отдел.");
                return false;
            }
            return true;
        }

        private Label trainerLabel;
        private ComboBox trainerComboBox;
        private Label departmentLabel;
        private ComboBox departmentComboBox;
        private Button saveButton;
        private Button cancelButton;
    }
}
