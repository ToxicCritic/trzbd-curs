using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class AddEditStandardPassingForm : Form
    {
        private readonly string connectionString = @"Server=ADCLG1;Database=Лифляндский_СпортШколаОлимпРезерва;Integrated Security=true;";

        public int AthleteID { get; private set; }
        public int StandardID { get; private set; }
        public string Result { get; private set; }
        public DateTime Date { get; private set; }

        public AddEditStandardPassingForm()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        public AddEditStandardPassingForm(DataRow standardPassingRow)
        {
            InitializeComponent();
            LoadComboBoxData();

            AthleteID = (int)standardPassingRow["AthleteID"];
            StandardID = (int)standardPassingRow["StandardID"];
            Result = standardPassingRow["Result"].ToString();
            Date = (DateTime)standardPassingRow["Date"];

            athleteComboBox.SelectedValue = AthleteID;
            standardComboBox.SelectedValue = StandardID;
            resultTextBox.Text = Result;
            datePicker.Value = Date;
        }

        private void LoadComboBoxData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Загрузка данных для атлетов
                SqlDataAdapter athleteAdapter = new SqlDataAdapter("SELECT id, AthleteFIO FROM Athletes", connection);
                DataTable athleteTable = new DataTable();
                athleteAdapter.Fill(athleteTable);
                athleteComboBox.DataSource = athleteTable;
                athleteComboBox.DisplayMember = "AthleteFIO";
                athleteComboBox.ValueMember = "id";

                // Загрузка данных для стандартов
                SqlDataAdapter standardAdapter = new SqlDataAdapter("SELECT id, Exercise FROM Standards", connection);
                DataTable standardTable = new DataTable();
                standardAdapter.Fill(standardTable);
                standardComboBox.DataSource = standardTable;
                standardComboBox.DisplayMember = "Exercise";
                standardComboBox.ValueMember = "id";
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            AthleteID = (int)athleteComboBox.SelectedValue;
            StandardID = (int)standardComboBox.SelectedValue;
            Result = resultTextBox.Text;
            Date = datePicker.Value.Date;

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
            if (AthleteID <= 0)
            {
                MessageBox.Show("Пожалуйста, выберите атлета.");
                return false;
            }
            if (StandardID <= 0)
            {
                MessageBox.Show("Пожалуйста, выберите стандарт.");
                return false;
            }
            if (string.IsNullOrEmpty(Result))
            {
                MessageBox.Show("Результат является обязательным.");
                return false;
            }
            return true;
        }

        

        private Label athleteLabel;
        private ComboBox athleteComboBox;
        private Label standardLabel;
        private ComboBox standardComboBox;
        private Label resultLabel;
        private TextBox resultTextBox;
        private Label dateLabel;
        private DateTimePicker datePicker;
        private Button saveButton;
        private Button cancelButton;
    }
}
