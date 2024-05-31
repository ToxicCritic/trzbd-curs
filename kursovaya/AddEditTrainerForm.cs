using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class AddEditTrainerForm : Form
    {
        private readonly string connectionString = @"Server=ADCLG1;Database=Лифляндский_СпортШколаОлимпРезерва;Integrated Security=true;";

        public string TrainerFIO { get; private set; }
        public string Department { get; private set; }

        public AddEditTrainerForm()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        public AddEditTrainerForm(DataRow trainerRow)
        {
            InitializeComponent();
            LoadComboBoxData();
            TrainerFIO = trainerRow["TrainerFIO"].ToString();
            Department = trainerRow["Department"].ToString();
            trainerFioTextBox.Text = TrainerFIO;
            departmentComboBox.SelectedValue = Department;
        }

        private void LoadComboBoxData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Загрузка данных для отделов
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
            TrainerFIO = trainerFioTextBox.Text;
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
            if (string.IsNullOrEmpty(TrainerFIO))
            {
                MessageBox.Show("ФИО тренера является обязательным.");
                return false;
            }
            if (string.IsNullOrEmpty(Department))
            {
                MessageBox.Show("Отдел является обязательным.");
                return false;
            }
            return true;
        }

        

        private Label trainerFioLabel;
        private TextBox trainerFioTextBox;
        private Label departmentLabel;
        private ComboBox departmentComboBox;
        private Button saveButton;
        private Button cancelButton;
    }
}
