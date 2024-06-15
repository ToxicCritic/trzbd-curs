using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class AddEditTrainerForm : Form
    {
        public string TrainerFIO { get; private set; }
        public string Department { get; private set; }
        public string Job { get; private set; }
        public string Ranking { get; private set; }

        public AddEditTrainerForm()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        public AddEditTrainerForm(DataRow trainerRow) : this()
        {
            if (trainerRow != null)
            {
                TrainerFIO = trainerRow["TrainerFIO"].ToString();
                Department = trainerRow["Department"].ToString();
                Job = trainerRow["Job"].ToString();
                Ranking = trainerRow["Ranking"].ToString();

                trainerFIOTextBox.Text = TrainerFIO;
                departmentComboBox.SelectedValue = Department;
                jobTextBox.Text = Job;
                rankingTextBox.Text = Ranking;
            }
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
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            TrainerFIO = trainerFIOTextBox.Text;
            Department = departmentComboBox.SelectedValue.ToString();
            Job = jobTextBox.Text;
            Ranking = rankingTextBox.Text;

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
            if (string.IsNullOrEmpty(Job))
            {
                MessageBox.Show("Должность является обязательной.");
                return false;
            }
            return true;
        }

        private Label trainerFIOLabel;
        private TextBox trainerFIOTextBox;
        private Label departmentLabel;
        private ComboBox departmentComboBox;
        private Label jobLabel;
        private TextBox jobTextBox;
        private Label rankingLabel;
        private TextBox rankingTextBox;
        private Button saveButton;
        private Button cancelButton;
    }
}
