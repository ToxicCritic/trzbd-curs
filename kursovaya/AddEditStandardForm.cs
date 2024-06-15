using System;
using System.Data;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class AddEditStandardForm : Form
    {
        public string Exercise { get; private set; }
        public string ResultForWomen { get; private set; }
        public string ResultForMen { get; private set; }

        public AddEditStandardForm()
        {
            InitializeComponent();
        }

        public AddEditStandardForm(DataRow standardRow) : this()
        {
            if (standardRow != null)
            {
                Exercise = standardRow["Exercise"].ToString();
                ResultForWomen = standardRow["Result_for_women"].ToString();
                ResultForMen = standardRow["Result_for_men"].ToString();

                exerciseTextBox.Text = Exercise;
                resultForWomenTextBox.Text = ResultForWomen;
                resultForMenTextBox.Text = ResultForMen;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Exercise = exerciseTextBox.Text;
            ResultForWomen = resultForWomenTextBox.Text;
            ResultForMen = resultForMenTextBox.Text;

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
            if (string.IsNullOrEmpty(Exercise))
            {
                MessageBox.Show("Упражнение является обязательным.");
                return false;
            }
            if (string.IsNullOrEmpty(ResultForWomen))
            {
                MessageBox.Show("Результат для женщин является обязательным.");
                return false;
            }
            if (string.IsNullOrEmpty(ResultForMen))
            {
                MessageBox.Show("Результат для мужчин является обязательным.");
                return false;
            }
            return true;
        }

        private Label exerciseLabel;
        private TextBox exerciseTextBox;
        private Label resultForWomenLabel;
        private TextBox resultForWomenTextBox;
        private Label resultForMenLabel;
        private TextBox resultForMenTextBox;
        private Button saveButton;
        private Button cancelButton;
    }
}
