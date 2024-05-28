using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class AddEditCompetitionParticipatingForm : Form
    {
        public int CompetitionID { get; private set; }
        public int AthleteID { get; private set; }
        public string Category { get; private set; }

        public AddEditCompetitionParticipatingForm()
        {
            InitializeComponent();
            LoadCompetitionData();
            LoadAthleteData();
        }

        public AddEditCompetitionParticipatingForm(DataRow competitionParticipatingRow)
        {
            InitializeComponent();
            LoadCompetitionData();
            LoadAthleteData();

            CompetitionID = (int)competitionParticipatingRow["CompetitionID"];
            AthleteID = (int)competitionParticipatingRow["AthleteID"];
            Category = competitionParticipatingRow["Category"].ToString();

            competitionComboBox.SelectedValue = CompetitionID;
            athleteComboBox.SelectedValue = AthleteID;
            categoryTextBox.Text = Category;
        }

        private void LoadCompetitionData()
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionStringColledge))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT id, CompetitionName FROM Competitions";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    competitionComboBox.DataSource = dataTable;
                    competitionComboBox.DisplayMember = "CompetitionName";
                    competitionComboBox.ValueMember = "id";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading competition data: {ex.Message}");
                }
            }
        }

        private void LoadAthleteData()
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionStringColledge))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT id, FIO FROM Athletes";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    athleteComboBox.DataSource = dataTable;
                    athleteComboBox.DisplayMember = "FIO";
                    athleteComboBox.ValueMember = "id";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading athlete data: {ex.Message}");
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            CompetitionID = (int)competitionComboBox.SelectedValue;
            AthleteID = (int)athleteComboBox.SelectedValue;
            Category = categoryTextBox.Text;

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
            if (CompetitionID <= 0)
            {
                MessageBox.Show("Пожалуйста, выберите соревнование.");
                return false;
            }
            if (AthleteID <= 0)
            {
                MessageBox.Show("Пожалуйста, выберите атлета.");
                return false;
            }
            if (string.IsNullOrEmpty(Category))
            {
                MessageBox.Show("Категория является обязательным полем.");
                return false;
            }
            return true;
        }

        private Label competitionLabel;
        private ComboBox competitionComboBox;
        private Label athleteLabel;
        private ComboBox athleteComboBox;
        private Label categoryLabel;
        private TextBox categoryTextBox;
        private Button saveButton;
        private Button cancelButton;
    }
}
