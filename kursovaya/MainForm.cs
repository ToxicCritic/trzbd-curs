using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class MainForm : Form
    {
        private SqlConnection sqlConnection;
        private DataGridView usersDataGridView;
        private string username;
        private string userStatus;
        private Label welcomeLabel;
        private readonly string connectionString = @"Server=ADCLG1;Database=Лифляндский_СпортШколаОлимпРезерва;Integrated Security=true;";

        public MainForm(string status, string username)
        {
            this.userStatus = status;
            this.username = username;
            InitializeComponent();
            InitializeSqlConnection();
            InitializeTabs(status);
            this.welcomeLabel.Text = $"Здравствуй, {username} ({userStatus})";
        }

        private void InitializeSqlConnection()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

       

        private void InitializeTabs(string userStatus)
        {
            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0); // Устанавливаем нижнее расположение, чтобы не перекрывать приветственное сообщение

            // Создаем вкладки для каждой таблицы
            tabControl.TabPages.Add(CreateTabPage("Athletes", "SELECT * FROM dbo.Athletes", AddEditAthlete));
            tabControl.TabPages.Add(CreateTabPage("Competitions", "SELECT * FROM dbo.Competitions"));
            tabControl.TabPages.Add(CreateTabPage("Competition Participating", "SELECT * FROM dbo.Competition_participating", null, "AthleteID", "Athletes", "id"));
            tabControl.TabPages.Add(CreateTabPage("Departments", "SELECT * FROM dbo.Departments"));
            tabControl.TabPages.Add(CreateTabPage("Groups", "SELECT * FROM dbo.Groups"));
            tabControl.TabPages.Add(CreateTabPage("Standards", "SELECT * FROM dbo.Standards"));
            tabControl.TabPages.Add(CreateTabPage("Standards Passing", "SELECT * FROM dbo.Standards_passing", null, "AthleteID", "Athletes", "id"));
            tabControl.TabPages.Add(CreateTabPage("Trainers", "SELECT * FROM dbo.Trainers"));
            tabControl.TabPages.Add(CreateTabPage("Trainings", "SELECT * FROM dbo.Trainings", null, "TrainerID", "Trainers", "id"));
            tabControl.TabPages.Add(CreateTabPage("Trophies", "SELECT * FROM dbo.Trophies"));

            if (userStatus == "admin")
            {
                tabControl.TabPages.Add(CreateUsersTabPage());
            }

            this.Controls.Add(tabControl);
        }

        private TabPage CreateTabPage(string tableName, string selectQuery, Action<DataTable> addHandler = null, string foreignKeyField = null, string referenceTable = null, string referenceField = null)
        {
            TabPage tabPage = new TabPage(tableName);

            ToolStrip toolStrip = new ToolStrip();
            ToolStripButton addButton = new ToolStripButton("Добавить");
            ToolStripButton deleteButton = new ToolStripButton("Удалить");
            ToolStripButton updateButton = new ToolStripButton("Изменить");

            toolStrip.Items.Add(addButton);
            toolStrip.Items.Add(deleteButton);
            toolStrip.Items.Add(updateButton);

            DataGridView dataGridView = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 300,
                ReadOnly = !LoginForm.IsAdmin,
                AllowUserToAddRows = LoginForm.IsAdmin,
                AllowUserToDeleteRows = LoginForm.IsAdmin
            };

            // Загрузка данных из таблицы
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectQuery, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            BindingSource bindingSource = new BindingSource
            {
                DataSource = dataTable
            };
            dataGridView.DataSource = bindingSource;

            if (foreignKeyField != null && referenceTable != null && referenceField != null)
            {
                DataGridViewComboBoxColumn comboColumn = new DataGridViewComboBoxColumn
                {
                    HeaderText = foreignKeyField,
                    DataPropertyName = foreignKeyField,
                    DataSource = GetReferenceData(referenceTable, referenceField),
                    ValueMember = referenceField,
                    DisplayMember = referenceField
                };
                dataGridView.Columns.Add(comboColumn);
            }

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

                if (addHandler != null)
                {
                    addButton.Click += (sender, e) => addHandler(dataTable);
                }
                else
                {
                    addButton.Click += (sender, e) => AddRow(dataTable);
                }

                deleteButton.Click += (sender, e) => DeleteRow(dataGridView, dataTable, sqlDataAdapter);
                updateButton.Click += (sender, e) => UpdateRow(dataGridView, sqlDataAdapter);
            }

            tabPage.Controls.Add(toolStrip);
            tabPage.Controls.Add(dataGridView);
            return tabPage;
        }

        private DataTable GetReferenceData(string tableName, string fieldName)
        {
            DataTable referenceData = new DataTable();
            string query = $"SELECT {fieldName} FROM dbo.{tableName}";
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(referenceData);
            }
            return referenceData;
        }

        private void AddRow(DataTable dataTable)
        {
            DataRow newRow = dataTable.NewRow();
            // Предполагается, что добавление строки будет обрабатываться через DataGridView
            dataTable.Rows.Add(newRow);
        }

        private void DeleteRow(DataGridView dataGridView, DataTable dataTable, SqlDataAdapter sqlDataAdapter)
        {
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    dataGridView.Rows.Remove(row);
                }
            }
            sqlDataAdapter.Update(dataTable);
        }

        private void UpdateRow(DataGridView dataGridView, SqlDataAdapter sqlDataAdapter)
        {
            // Здесь предполагается, что изменения уже были внесены в DataGridView и нужно просто обновить данные
            sqlDataAdapter.Update((DataTable)dataGridView.DataSource);
        }

        private void AddEditAthlete(DataTable dataTable)
        {
            using (AddEditAthleteForm form = new AddEditAthleteForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    DataRow newRow = dataTable.NewRow();
                    newRow["FIO"] = form.FIO;
                    newRow["Department"] = form.DepartmentID;
                    newRow["GroupID"] = form.GroupID;
                    newRow["TrainerID"] = form.TrainerID;
                    newRow["Ranking"] = form.Ranking;
                    newRow["Heights"] = form.Heights;
                    newRow["Weights"] = form.Weights;
                    newRow["Education_begin"] = form.EducationBegin;
                    dataTable.Rows.Add(newRow);
                }
            }
        }

        private TabPage CreateUsersTabPage()
        {
            TabPage tabPage = new TabPage("Пользователи");

            ToolStrip toolStrip = new ToolStrip();
            ToolStripButton addButton = new ToolStripButton("Добавить");
            ToolStripButton deleteButton = new ToolStripButton("Удалить");
            ToolStripButton updateButton = new ToolStripButton("Изменить");

            toolStrip.Items.Add(addButton);
            toolStrip.Items.Add(deleteButton);
            toolStrip.Items.Add(updateButton);

            usersDataGridView = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 300,
                ReadOnly = false,
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true
            };

            addButton.Click += AddUserButton_Click;
            deleteButton.Click += DeleteUserButton_Click;
            updateButton.Click += UpdateUserButton_Click;

            tabPage.Controls.Add(toolStrip);
            tabPage.Controls.Add(usersDataGridView);

            LoadUsers(usersDataGridView);

            return tabPage;
        }

        private void LoadUsers(DataGridView dataGridView)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView.DataSource = dataTable;
            }
        }

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            using (AddUserForm addUserForm = new AddUserForm())
            {
                if (addUserForm.ShowDialog() == DialogResult.OK)
                {
                    string login = addUserForm.Login;
                    string password = addUserForm.Password;
                    string status = addUserForm.Status;

                    AuthenticationService authService = new AuthenticationService(connectionString);
                    authService.AddUser(login, password, status);

                    LoadUsers(usersDataGridView);
                }
            }
        }

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.SelectedRows.Count > 0)
            {
                int userId = (int)usersDataGridView.SelectedRows[0].Cells["ID"].Value;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Users WHERE ID = @UserID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userId);
                        command.ExecuteNonQuery();
                    }
                }

                LoadUsers(usersDataGridView);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для удаления.");
            }
        }

        private void UpdateUserButton_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.SelectedRows.Count > 0)
            {
                int userId = (int)usersDataGridView.SelectedRows[0].Cells["ID"].Value;
                string newStatus = (string)usersDataGridView.SelectedRows[0].Cells["Status"].Value == "admin" ? "user" : "admin";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Users SET Status = @Status WHERE ID = @UserID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", newStatus);
                        command.Parameters.AddWithValue("@UserID", userId);
                        command.ExecuteNonQuery();
                    }
                }

                LoadUsers(usersDataGridView);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для обновления.");
            }
        }
    }
}
