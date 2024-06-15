using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class AddEditTrainingForm : Form
    {
        private readonly string connectionString = @"Server=ADCLG1;Database=Лифляндский_СпортШколаОлимпРезерва;Integrated Security=true;";

        public string Department { get; private set; }
        public int GroupID { get; private set; }
        public int TrainerID { get; private set; }
        public TimeSpan TrainingTime { get; private set; }
        public DateTime TrainingDate { get; private set; }

        public AddEditTrainingForm()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        public AddEditTrainingForm(DataRow trainingRow)
        {
            InitializeComponent();
            LoadComboBoxData();

            TrainerID = (int)trainingRow["TrainerID"];
            Department = trainingRow["Department"].ToString();
            GroupID = (int)trainingRow["GroupID"];
            TrainingTime = (TimeSpan)trainingRow["TrainingTime"];
            TrainingDate = (DateTime)trainingRow["TrainingDate"];

            trainerComboBox.SelectedValue = TrainerID;
            departmentComboBox.SelectedValue = Department;
            groupComboBox.SelectedValue = GroupID;
            trainingTimePicker.Value = DateTime.Today.Add(TrainingTime);
            trainingDatePicker.Value = TrainingDate;
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

                // Загрузка данных для групп
                SqlDataAdapter groupAdapter = new SqlDataAdapter("SELECT GroupId FROM Groups", connection);
                DataTable groupTable = new DataTable();
                groupAdapter.Fill(groupTable);
                groupComboBox.DataSource = groupTable;
                groupComboBox.DisplayMember = "GroupId";
                groupComboBox.ValueMember = "GroupId";

                // Загрузка данных для тренеров
                SqlDataAdapter trainerAdapter = new SqlDataAdapter("SELECT id, TrainerFIO FROM Trainers", connection);
                DataTable trainerTable = new DataTable();
                trainerAdapter.Fill(trainerTable);
                trainerComboBox.DataSource = trainerTable;
                trainerComboBox.DisplayMember = "TrainerFIO";
                trainerComboBox.ValueMember = "id";
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Department = departmentComboBox.SelectedValue.ToString();
            GroupID = (int)groupComboBox.SelectedValue;
            TrainerID = (int)trainerComboBox.SelectedValue;
            TrainingTime = trainingTimePicker.Value.TimeOfDay;
            TrainingDate = trainingDatePicker.Value.Date;

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
            if (string.IsNullOrEmpty(Department))
            {
                MessageBox.Show("Отдел является обязательным.");
                return false;
            }
            if (GroupID <= 0)
            {
                MessageBox.Show("Пожалуйста, выберите группу.");
                return false;
            }
            if (TrainerID <= 0)
            {
                MessageBox.Show("Пожалуйста, выберите тренера.");
                return false;
            }
            return true;
        }

        

        private Label departmentLabel;
        private ComboBox departmentComboBox;
        private Label groupLabel;
        private ComboBox groupComboBox;
        private Label trainerLabel;
        private ComboBox trainerComboBox;
        private Label trainingTimeLabel;
        private DateTimePicker trainingTimePicker;
        private Label trainingDateLabel;
        private DateTimePicker trainingDatePicker;
        private Button saveButton;
        private Button cancelButton;
    }
}
