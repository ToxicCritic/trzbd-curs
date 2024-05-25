using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class MainForm : Form
    {
        private SqlConnection sqlConnection;
        private SqlDataAdapter usersDataAdapter;
        private DataTable usersDataTable;
        private BindingSource usersBindingSource;
        private DataGridView usersDataGridView;

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

            if (LoginForm.IsAdmin)
            {
                tabControl.TabPages.Add(CreateUsersTabPage());
            }

            this.Controls.Add(tabControl);
        }

        private TabPage CreateTabPage(string tableName, string selectQuery)
        {
            TabPage tabPage = new TabPage(tableName);

            DataGridView dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
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

        private TabPage CreateUsersTabPage()
        {
            TabPage tabPage = new TabPage("Пользователи");

            DataGridView dataGridView = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 200
            };

            Button addUserButton = new Button
            {
                Text = "Добавить пользователя",
                Dock = DockStyle.Top
            };

            Button deleteUserButton = new Button
            {
                Text = "Удалить пользователя",
                Dock = DockStyle.Top
            };

            Button updateUserButton = new Button
            {
                Text = "Изменить статус пользователя",
                Dock = DockStyle.Top
            };

            // Привязка событий кнопок к методам
            addUserButton.Click += AddUserButton_Click;
            deleteUserButton.Click += DeleteUserButton_Click;
            updateUserButton.Click += UpdateUserButton_Click;

            tabPage.Controls.Add(dataGridView);
            tabPage.Controls.Add(addUserButton);
            tabPage.Controls.Add(deleteUserButton);
            tabPage.Controls.Add(updateUserButton);

            LoadUsers(dataGridView);

            return tabPage;
        }

        private void LoadUsers(DataGridView dataGridView)
        {
            string query = "SELECT * FROM Users";
            usersDataAdapter = new SqlDataAdapter(query, sqlConnection);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(usersDataAdapter);
            usersDataTable = new DataTable();
            usersDataAdapter.Fill(usersDataTable);

            usersBindingSource = new BindingSource
            {
                DataSource = usersDataTable
            };
            dataGridView.DataSource = usersBindingSource;
        }

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            // Открываем форму для ввода данных нового пользователя
            using (AddUserForm addUserForm = new AddUserForm())
            {
                if (addUserForm.ShowDialog() == DialogResult.OK)
                {
                    string login = addUserForm.Login;
                    string password = addUserForm.Password;
                    string status = addUserForm.Status;

                    string passwordHash = HashPassword(password);

                    string query = "INSERT INTO Users (Login, PasswordHash, Status) VALUES (@Login, @PasswordHash, @Status)";
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@Login", login);
                        command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        command.Parameters.AddWithValue("@Status", status);
                        command.ExecuteNonQuery();
                    }

                    // Обновляем таблицу пользователей
                    LoadUsers(usersDataGridView);
                }
            }
        }

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.SelectedRows.Count > 0)
            {
                int userId = (int)usersDataGridView.SelectedRows[0].Cells["ID"].Value;

                string query = "DELETE FROM Users WHERE ID = @UserID";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.ExecuteNonQuery();
                }

                // Обновляем таблицу пользователей
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

                string query = "UPDATE Users SET Status = @Status WHERE ID = @UserID";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Status", newStatus);
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.ExecuteNonQuery();
                }

                // Обновляем таблицу пользователей
                LoadUsers(usersDataGridView);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для обновления.");
            }
        }

        private string HashPassword(string password)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
