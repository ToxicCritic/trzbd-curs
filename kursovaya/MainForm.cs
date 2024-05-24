using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class MainForm : Form
    {
        private SqlConnection sqlConnection;

        public MainForm()
        {
            InitializeComponent();
            InitializeTabs();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        private void InitializeTabs()
        {
            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // Создаем вкладки для каждой таблицы
            tabControl.TabPages.Add(CreateTabPage("Athletes", "SELECT * FROM dbo.Athletes"));
            tabControl.TabPages.Add(CreateTabPage("Competitions", "SELECT * FROM dbo.Competitions"));
            tabControl.TabPages.Add(CreateTabPage("Competition Participating", "SELECT * FROM dbo.Competition_participating"));
            tabControl.TabPages.Add(CreateTabPage("Departments", "SELECT * FROM dbo.Departments"));
            tabControl.TabPages.Add(CreateTabPage("Groups", "SELECT * FROM dbo.Groups"));
            tabControl.TabPages.Add(CreateTabPage("Standards", "SELECT * FROM dbo.Standards"));
            tabControl.TabPages.Add(CreateTabPage("Standards Passing", "SELECT * FROM dbo.Standards_passing"));
            tabControl.TabPages.Add(CreateTabPage("Trainers", "SELECT * FROM dbo.Trainers"));
            tabControl.TabPages.Add(CreateTabPage("Trainings", "SELECT * FROM dbo.Trainings"));
            tabControl.TabPages.Add(CreateTabPage("Trophies", "SELECT * FROM dbo.Trophies"));

            this.Controls.Add(tabControl);
        }

        private TabPage CreateTabPage(string tableName, string selectQuery)
        {
            TabPage tabPage = new TabPage(tableName);

            DataGridView dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.ReadOnly = !LoginForm.IsAdmin;
            dataGridView.AllowUserToAddRows = LoginForm.IsAdmin;
            dataGridView.AllowUserToDeleteRows = LoginForm.IsAdmin;

            // Загрузка данных из таблицы
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectQuery, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;
            dataGridView.DataSource = bindingSource;

            if (LoginForm.IsAdmin)
            {
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                bindingSource.ListChanged += (sender, args) =>
                {
                    if (args.ListChangedType == ListChangedType.ItemAdded ||
                        args.ListChangedType == ListChangedType.ItemDeleted ||
                        args.ListChangedType == ListChangedType.ItemChanged)
                    {
                        sqlDataAdapter.Update(dataTable);
                    }
                };
            }

            tabPage.Controls.Add(dataGridView);
            return tabPage;
        }
    }
}
