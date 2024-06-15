using System;
using System.Data;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class AddEditCompetitionForm : Form
    {
        public string CompetitionName { get; private set; }
        public string CompetitionPlace { get; private set; }
        public DateTime CompetitionDate { get; private set; }

        public AddEditCompetitionForm()
        {
            InitializeComponent();
        }

        public AddEditCompetitionForm(DataRow competitionRow) : this()
        {
            if (competitionRow != null)
            {
                CompetitionName = competitionRow["CompetitionName"].ToString();
                CompetitionPlace = competitionRow["CompetitionPlace"].ToString();
                CompetitionDate = (DateTime)competitionRow["CompetitionDate"];

                competitionNameTextBox.Text = CompetitionName;
                competitionPlaceTextBox.Text = CompetitionPlace;
                competitionDatePicker.Value = CompetitionDate;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            CompetitionName = competitionNameTextBox.Text;
            CompetitionPlace = competitionPlaceTextBox.Text;
            CompetitionDate = competitionDatePicker.Value;

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
            if (string.IsNullOrEmpty(CompetitionName))
            {
                MessageBox.Show("Название соревнования является обязательным.");
                return false;
            }
            if (string.IsNullOrEmpty(CompetitionPlace))
            {
                MessageBox.Show("Место проведения соревнования является обязательным.");
                return false;
            }
            return true;
        }

        private Label competitionNameLabel;
        private TextBox competitionNameTextBox;
        private Label competitionPlaceLabel;
        private TextBox competitionPlaceTextBox;
        private Label competitionDateLabel;
        private DateTimePicker competitionDatePicker;
        private Button saveButton;
        private Button cancelButton;
    }
}
