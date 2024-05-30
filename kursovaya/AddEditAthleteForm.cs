using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class AddEditAthleteForm : Form
    {

        public string FIO { get; private set; }
        public string Department { get; private set; }
        public int GroupID { get; private set; }
        public int TrainerID { get; private set; }
        public string Ranking { get; private set; }
        public int Heights { get; private set; }
        public int Weights { get; private set; }
        public DateTime EducationBegin { get; private set; }

        public AddEditAthleteForm()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        public AddEditAthleteForm(DataRow athleteRow)
        {
            InitializeComponent();
            LoadComboBoxData();
            FIO = athleteRow["AthleteFIO"].ToString();
            Department = athleteRow["Department"].ToString();
            GroupID = (int)athleteRow["GroupID"];
            TrainerID = (int)athleteRow["TrainerID"];
            Ranking = athleteRow["Ranking"].ToString();
            Heights = (int)athleteRow["Heights"];
            Weights = (int)athleteRow["Weights"];
            EducationBegin = (DateTime)athleteRow["Education_begin"];

            fioTextBox.Text = FIO;
            departmentComboBox.SelectedValue = Department;
            groupComboBox.SelectedValue = GroupID;
            trainerComboBox.SelectedValue = TrainerID;
            rankingTextBox.Text = Ranking;
            heightsNumericUpDown.Value = Heights;
            weightsNumericUpDown.Value = Weights;
            educationBeginPicker.Value = EducationBegin;
        }

        private void LoadComboBoxData()
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionStringColledge))
            {
                connection.Open();

                SqlDataAdapter departmentAdapter = new SqlDataAdapter("SELECT DepartmentName FROM Departments", connection);
                DataTable departmentTable = new DataTable();
                departmentAdapter.Fill(departmentTable);
                departmentComboBox.DataSource = departmentTable;
                departmentComboBox.DisplayMember = "DepartmentName";
                departmentComboBox.ValueMember = "DepartmentName";

                SqlDataAdapter groupAdapter = new SqlDataAdapter("SELECT GroupId FROM Groups", connection);
                DataTable groupTable = new DataTable();
                groupAdapter.Fill(groupTable);
                groupComboBox.DataSource = groupTable;
                groupComboBox.DisplayMember = "GroupId";
                groupComboBox.ValueMember = "GroupId";

                SqlDataAdapter trainerAdapter = new SqlDataAdapter("SELECT id, TrainerFIO FROM Trainers", connection);
                DataTable trainerTable = new DataTable();
                trainerAdapter.Fill(trainerTable);
                trainerComboBox.DataSource = trainerTable;
                trainerComboBox.DisplayMember = "TrainerFIO";
                trainerComboBox.ValueMember = "id";
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            FIO = fioTextBox.Text;
            Department = departmentComboBox.SelectedValue.ToString();
            GroupID = (int)groupComboBox.SelectedValue;
            TrainerID = (int)trainerComboBox.SelectedValue;
            Ranking = rankingTextBox.Text;
            Heights = (int)heightsNumericUpDown.Value;
            Weights = (int)weightsNumericUpDown.Value;
            EducationBegin = educationBeginPicker.Value;

            if (ValidateForm())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(FIO))
            {
                MessageBox.Show("FIO is required.");
                return false;
            }
            if (string.IsNullOrEmpty(Department))
            {
                MessageBox.Show("Department is required.");
                return false;
            }
            if (GroupID == 0)
            {
                MessageBox.Show("GroupID is required.");
                return false;
            }
            if (TrainerID == 0)
            {
                MessageBox.Show("TrainerID is required.");
                return false;
            }
            return true;
        }

        private Label fioLabel;
        private TextBox fioTextBox;
        private Label departmentLabel;
        private ComboBox departmentComboBox;
        private Label groupLabel;
        private ComboBox groupComboBox;
        private Label trainerLabel;
        private ComboBox trainerComboBox;
        private Label rankingLabel;
        private TextBox rankingTextBox;
        private Label heightsLabel;
        private NumericUpDown heightsNumericUpDown;
        private Label weightsLabel;
        private NumericUpDown weightsNumericUpDown;
        private Label educationBeginLabel;
        private DateTimePicker educationBeginPicker;
        private Button saveButton;
        private Button cancelButton;
    }
}
