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
            LoadComboBoxData();
        }

        public AddEditCompetitionParticipatingForm(DataRow competitionParticipatingRow)
        {
            InitializeComponent();
            LoadComboBoxData();

            CompetitionID = (int)competitionParticipatingRow["CompetitionID"];
            AthleteID = (int)competitionParticipatingRow["AthleteID"];
            Category = competitionParticipatingRow["Category"].ToString();

            competitionComboBox.SelectedValue = CompetitionID;
            athleteComboBox.SelectedValue = AthleteID;
            categoryTextBox.Text = Category;
        }

        private void LoadComboBoxData()
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionStringColledge))
            {
                connection.Open();

                // Загрузка данных для соревнований
                SqlDataAdapter competitionAdapter = new SqlDataAdapter("SELECT id, CompetitionName FROM Competitions", connection);
                DataTable competitionTable = new DataTable();
                competitionAdapter.Fill(competitionTable);
                competitionComboBox.DataSource = competitionTable;
                competitionComboBox.DisplayMember = "CompetitionName";
                competitionComboBox.ValueMember = "id";

                // Загрузка данных для атлетов
                SqlDataAdapter athleteAdapter = new SqlDataAdapter("SELECT id, AthleteFIO FROM Athletes", connection);
                DataTable athleteTable = new DataTable();
                athleteAdapter.Fill(athleteTable);
                athleteComboBox.DataSource = athleteTable;
                athleteComboBox.DisplayMember = "AthleteFIO";
                athleteComboBox.ValueMember = "id";
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
