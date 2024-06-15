using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class AddEditTrophyForm : Form
    {
        public string TrophyName { get; private set; }
        public int CompetitionID { get; private set; }
        public int AthleteID { get; private set; }

        public AddEditTrophyForm()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        public AddEditTrophyForm(DataRow trophyRow) : this()
        {
            if (trophyRow != null)
            {
                TrophyName = trophyRow["TrophyName"].ToString();
                CompetitionID = (int)trophyRow["CompetitionID"];
                AthleteID = (int)trophyRow["AthleteID"];

                trophyNameTextBox.Text = TrophyName;
                competitionComboBox.SelectedValue = CompetitionID;
                athleteComboBox.SelectedValue = AthleteID;
            }
        }

        private void LoadComboBoxData()
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionStringColledge))
            {
                connection.Open();

                SqlDataAdapter competitionAdapter = new SqlDataAdapter("SELECT id, CompetitionName FROM Competitions", connection);
                DataTable competitionTable = new DataTable();
                competitionAdapter.Fill(competitionTable);
                competitionComboBox.DataSource = competitionTable;
                competitionComboBox.DisplayMember = "CompetitionName";
                competitionComboBox.ValueMember = "id";

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
            TrophyName = trophyNameTextBox.Text;
            CompetitionID = (int)competitionComboBox.SelectedValue;
            AthleteID = (int)athleteComboBox.SelectedValue;

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
            if (string.IsNullOrEmpty(TrophyName))
            {
                MessageBox.Show("Название трофея является обязательным.");
                return false;
            }
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
            return true;
        }

        private Label trophyNameLabel;
        private TextBox trophyNameTextBox;
        private Label competitionLabel;
        private ComboBox competitionComboBox;
        private Label athleteLabel;
        private ComboBox athleteComboBox;
        private Button saveButton;
        private Button cancelButton;
    }
}
