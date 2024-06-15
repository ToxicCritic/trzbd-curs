using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace kursovaya
{
    public partial class MainForm : Form
    {
        private SqlConnection sqlConnection;
        private DataGridView usersDataGridView;
        private string username;
        private string userStatus;
        private Label welcomeLabel;

        Dictionary<string, string> tableNamesRus = new Dictionary<string, string>
        {
            { "Athletes", "Атлеты" },
            { "Competitions", "Соревнования" },
            { "Competition Participating", "Участие в соревнованиях" },
            { "Departments", "Отделы" },
            { "Groups", "Группы" },
            { "Standards", "Стандарты" },
            { "Standards Passing", "Прохождение стандартов" },
            { "Trainers", "Тренеры" },
            { "Trainings", "Тренировки" },
            { "Trophies", "Трофеи" },
            { "Users", "Пользователи" }
        };

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
            sqlConnection = new SqlConnection(Program.connectionStringColledge);
            sqlConnection.Open();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            Application.Exit();
        }

        private void InitializeTabs(string userStatus)
        {
            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);

            // Используем таблицы для загрузки данных с нужными заголовками
            tabControl.TabPages.Add(CreateTabPage("Athletes", "SELECT * FROM Athletes", AddEditAthlete,
                                                  "Department", "Departments", "DepartmentName", "DepartmentName", 2,
                                                  "GroupID", "Groups", "GroupId", "GroupId", 3,
                                                  "TrainerID", "Trainers", "id", "TrainerFIO", 4));
            tabControl.TabPages.Add(CreateTabPage("Competitions", "SELECT * FROM Competitions", AddEditCompetition));
            tabControl.TabPages.Add(CreateTabPage("Competition Participating", "SELECT * FROM Competition_participating", AddEditCompetitionParticipating,
                                                  "CompetitionID", "Competitions", "id", "CompetitionName", 1,
                                                  "AthleteID", "Athletes", "id", "AthleteFIO", 2));
            tabControl.TabPages.Add(CreateTabPage("Departments", "SELECT * FROM Departments", AddEditDepartment));
            tabControl.TabPages.Add(CreateTabPage("Groups", "SELECT * FROM Groups", AddEditGroup,
                                                  "TrainerID", "Trainers", "id", "TrainerFIO", 1,
                                                  "Department", "Departments", "DepartmentName", "DepartmentName", 2));
            tabControl.TabPages.Add(CreateTabPage("Standards", "SELECT * FROM Standards", AddEditStandard));
            tabControl.TabPages.Add(CreateTabPage("Standards Passing", "SELECT * FROM Standards_passing", AddEditStandardPassing,
                                                  "AthleteID", "Athletes", "id", "AthleteFIO", 1,
                                                  "StandardID", "Standards", "id", "Exercise", 2));
            tabControl.TabPages.Add(CreateTabPage("Trainers", "SELECT * FROM Trainers", AddEditTrainer,
                                                  "Department", "Departments", "DepartmentName", "DepartmentName", 2));
            tabControl.TabPages.Add(CreateTabPage("Trainings", "SELECT * FROM Trainings", AddEditTraining,
                                                  "TrainerID", "Trainers", "id", "TrainerFIO", 1, 
                                                  "Department", "Departments", "DepartmentName", "DepartmentName", 2,
                                                  "GroupID", "Groups", "GroupId", "GroupId", 3));
            tabControl.TabPages.Add(CreateTabPage("Trophies", "SELECT * FROM Trophies", AddEditTrophy,
                                                  "CompetitionID", "Competitions", "id", "CompetitionName", 1,
                                                  "AthleteID", "Athletes", "id", "AthleteFIO"));

            if (userStatus == "admin")
            {
                tabControl.TabPages.Add(CreateUsersTabPage());
            }

            tabControl.TabPages.Add(CreateQueryTabPage());
            tabControl.TabPages.Add(CreateReportTabPage());

            this.Controls.Add(tabControl);
        }

        private TabPage CreateTabPage(string tableName, string selectQuery, Action<DataTable, DataRow, SqlDataAdapter> addEditHandler = null,
                              string foreignKeyField1 = null, string referenceTable1 = null, string referenceField1 = null, string displayMember1 = null, int? position1 = null,
                              string foreignKeyField2 = null, string referenceTable2 = null, string referenceField2 = null, string displayMember2 = null, int? position2 = null,
                              string foreignKeyField3 = null, string referenceTable3 = null, string referenceField3 = null, string displayMember3 = null, int? position3 = null)
        {
            string tabPageTitle = tableNamesRus.ContainsKey(tableName) ? tableNamesRus[tableName] : tableName;

            TabPage tabPage = new TabPage(tabPageTitle);

            ToolStrip toolStrip = new ToolStrip();
            ToolStripButton addButton = new ToolStripButton("Добавить");
            ToolStripButton deleteButton = new ToolStripButton("Удалить");
            ToolStripButton updateButton = new ToolStripButton("Изменить");
            tabPage.BackColor = Color.Honeydew;
            toolStrip.BackColor = Color.GhostWhite;
            toolStrip.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            toolStrip.Items.Add(addButton);
            toolStrip.Items.Add(deleteButton);
            toolStrip.Items.Add(updateButton);

            DataGridView dataGridView = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 300,
                ReadOnly = !LoginForm.IsAdmin,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoGenerateColumns = false,
               
            };
            dataGridView.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);

            CustomizeDataGridView(dataGridView);

            SqlDataAdapter localAdapter = new SqlDataAdapter(selectQuery, sqlConnection);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(localAdapter);
            DataTable localTable = new DataTable();
            localAdapter.Fill(localTable);

            localAdapter.InsertCommand = commandBuilder.GetInsertCommand();
            localAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            localAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();

            BindingSource bindingSource = new BindingSource
            {
                DataSource = localTable
            };
            dataGridView.DataSource = bindingSource;

            foreach (DataColumn column in localTable.Columns)
            {
                if ((foreignKeyField1 != null && column.ColumnName == foreignKeyField1) ||
                    (foreignKeyField2 != null && column.ColumnName == foreignKeyField2) ||
                    (foreignKeyField3 != null && column.ColumnName == foreignKeyField3))
                {
                    continue;
                }

                dataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = column.ColumnName,
                    HeaderText = column.ColumnName
                });
            }

            if (foreignKeyField1 != null && referenceTable1 != null && referenceField1 != null && displayMember1 != null)
            {
                DataTable referenceData1 = GetReferenceData(referenceTable1, referenceField1, displayMember1);
                DataGridViewComboBoxColumn comboColumn1 = new DataGridViewComboBoxColumn
                {
                    DataPropertyName = foreignKeyField1,
                    HeaderText = displayMember1,
                    DataSource = referenceData1,
                    ValueMember = referenceField1,
                    DisplayMember = displayMember1,
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
                };
                if (position1.HasValue && position1.Value < dataGridView.Columns.Count)
                {
                    dataGridView.Columns.Insert(position1.Value, comboColumn1);
                }
                else
                {
                    dataGridView.Columns.Add(comboColumn1);
                }
            }

            if (foreignKeyField2 != null && referenceTable2 != null && referenceField2 != null && displayMember2 != null)
            {
                DataTable referenceData2 = GetReferenceData(referenceTable2, referenceField2, displayMember2);
                DataGridViewComboBoxColumn comboColumn2 = new DataGridViewComboBoxColumn
                {
                    DataPropertyName = foreignKeyField2,
                    HeaderText = displayMember2,
                    DataSource = referenceData2,
                    ValueMember = referenceField2,
                    DisplayMember = displayMember2,
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
                };
                if (position2.HasValue && position2.Value < dataGridView.Columns.Count)
                {
                    dataGridView.Columns.Insert(position2.Value, comboColumn2);
                }
                else
                {
                    dataGridView.Columns.Add(comboColumn2);
                }
            }

            if (foreignKeyField3 != null && referenceTable3 != null && referenceField3 != null && displayMember3 != null)
            {
                DataTable referenceData3 = GetReferenceData(referenceTable3, referenceField3, displayMember3);
                DataGridViewComboBoxColumn comboColumn3 = new DataGridViewComboBoxColumn
                {
                    DataPropertyName = foreignKeyField3,
                    HeaderText = displayMember3,
                    DataSource = referenceData3,
                    ValueMember = referenceField3,
                    DisplayMember = displayMember3,
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
                };
                if (position3.HasValue && position3.Value < dataGridView.Columns.Count)
                {
                    dataGridView.Columns.Insert(position3.Value, comboColumn3);
                }
                else
                {
                    dataGridView.Columns.Add(comboColumn3);
                }
            }

            if (LoginForm.IsAdmin)
            {
                addButton.Click += (sender, e) => addEditHandler?.Invoke(localTable, null, localAdapter);
                updateButton.Click += (sender, e) =>
                {
                    if (dataGridView.SelectedRows.Count > 0)
                    {
                        DataRow row = ((DataRowView)dataGridView.SelectedRows[0].DataBoundItem).Row;
                        addEditHandler?.Invoke(localTable, row, localAdapter);
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, выберите строку для редактирования.");
                    }
                };

                deleteButton.Click += (sender, e) => DeleteRow(dataGridView, localTable, localAdapter);
            }

            tabPage.Controls.Add(toolStrip);
            tabPage.Controls.Add(dataGridView);
            return tabPage;
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private DataTable GetReferenceData(string referenceTable, string referenceField, string displayField)
        {
            DataTable referenceTableData = new DataTable();
            string query = $"SELECT {referenceField}, {displayField} FROM {referenceTable}";
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection))
            {
                adapter.Fill(referenceTableData);
            }
            return referenceTableData;
        }

        private void DeleteRow(DataGridView dataGridView, DataTable dataTable, SqlDataAdapter sqlDataAdapter)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        dataGridView.Rows.Remove(row);
                    }
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            sqlDataAdapter.Update(dataTable);
        }

        private DataGridView CustomizeDataGridView(DataGridView dataGridView)
        {
            // Настройки для DataGridView
            dataGridView.BackgroundColor = Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(150, 255, 194);
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.Honeydew;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 10);

            return dataGridView;
        }

        private void AddEditAthlete(DataTable dataTable, DataRow existingRow, SqlDataAdapter localAdapter)
        {
            using (AddEditAthleteForm form = existingRow == null ? new AddEditAthleteForm() : new AddEditAthleteForm(existingRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (existingRow == null) 
                    {
                        string insertQuery = "INSERT INTO Athletes (AthleteFIO, Department, GroupID, TrainerID, Ranking, Heights, Weights, Education_begin) VALUES (@AthleteFIO, @Department, @GroupID, @TrainerID, @Ranking, @Heights, @Weights, @Education_begin); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@AthleteFIO", form.FIO);
                            cmd.Parameters.AddWithValue("@Department", form.Department);
                            cmd.Parameters.AddWithValue("@GroupID", form.GroupID);
                            cmd.Parameters.AddWithValue("@TrainerID", form.TrainerID);
                            cmd.Parameters.AddWithValue("@Ranking", form.Ranking);
                            cmd.Parameters.AddWithValue("@Heights", form.Heights);
                            cmd.Parameters.AddWithValue("@Weights", form.Weights);
                            cmd.Parameters.AddWithValue("@Education_begin", form.EducationBegin);

                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                // Создаем новую строку и добавляем в DataTable после успешного выполнения команды вставки
                                DataRow newRow = dataTable.NewRow();
                                newRow["id"] = Convert.ToInt32(newId);
                                newRow["AthleteFIO"] = form.FIO;
                                newRow["Department"] = form.Department;
                                newRow["GroupID"] = form.GroupID;
                                newRow["TrainerID"] = form.TrainerID;
                                newRow["Ranking"] = form.Ranking;
                                newRow["Heights"] = form.Heights;
                                newRow["Weights"] = form.Weights;
                                newRow["Education_begin"] = form.EducationBegin;
                            }
                        }
                    }
                    else 
                    {
                        existingRow["AthleteFIO"] = form.FIO;
                        existingRow["Department"] = form.Department;
                        existingRow["GroupID"] = form.GroupID;
                        existingRow["TrainerID"] = form.TrainerID;
                        existingRow["Ranking"] = form.Ranking;
                        existingRow["Heights"] = form.Heights;
                        existingRow["Weights"] = form.Weights;
                        existingRow["Education_begin"] = form.EducationBegin;
                    }

                    localAdapter.Update(dataTable);
                    dataTable.Clear();
                    localAdapter.Fill(dataTable);
                }
            }
        }

        private void AddEditCompetitionParticipating(DataTable dataTable, DataRow existingRow, SqlDataAdapter localAdapter)
        {
            using (AddEditCompetitionParticipatingForm form = existingRow == null ? new AddEditCompetitionParticipatingForm() : new AddEditCompetitionParticipatingForm(existingRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (existingRow == null) // Adding new row
                    {
                        string insertQuery = "INSERT INTO Competition_participating (CompetitionID, AthleteID, Category) VALUES (@CompetitionID, @AthleteID, @Category); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@CompetitionID", form.CompetitionID);
                            cmd.Parameters.AddWithValue("@AthleteID", form.AthleteID);
                            cmd.Parameters.AddWithValue("@Category", form.Category);

                            // Выполняем запрос и получаем новый ID
                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                // Создаем новую строку и добавляем в DataTable после успешного выполнения команды вставки
                                DataRow newRow = dataTable.NewRow();
                                newRow["id"] = Convert.ToInt32(newId);
                                newRow["CompetitionID"] = form.CompetitionID;
                                newRow["AthleteID"] = form.AthleteID;
                                newRow["Category"] = form.Category;
                            }
                        }
                    }
                    else // Editing existing row
                    {
                        existingRow["CompetitionID"] = form.CompetitionID;
                        existingRow["AthleteID"] = form.AthleteID;
                        existingRow["Category"] = form.Category;
                    }

                    localAdapter.Update(dataTable);
                    dataTable.Clear();
                    localAdapter.Fill(dataTable);
                }
            }
        }


        private void AddEditCompetition(DataTable dataTable, DataRow existingRow, SqlDataAdapter localAdapter)
        {
            using (AddEditCompetitionForm form = existingRow == null ? new AddEditCompetitionForm() : new AddEditCompetitionForm(existingRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (existingRow == null)
                    {
                        string insertQuery = "INSERT INTO Competitions (CompetitionName, CompetitionPlace, CompetitionDate) VALUES (@CompetitionName, @CompetitionPlace, @CompetitionDate); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@CompetitionName", form.CompetitionName);
                            cmd.Parameters.AddWithValue("@CompetitionPlace", form.CompetitionPlace);
                            cmd.Parameters.AddWithValue("@CompetitionDate", form.CompetitionDate);

                            // Выполняем запрос и получаем новый ID
                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                // Создаем новую строку и добавляем в DataTable после успешного выполнения команды вставки
                                DataRow newRow = dataTable.NewRow();
                                newRow["id"] = Convert.ToInt32(newId);
                                newRow["CompetitionName"] = form.CompetitionName;
                                newRow["CompetitionPlace"] = form.CompetitionPlace;
                                newRow["CompetitionDate"] = form.CompetitionDate;
                            }
                        }
                    }
                    else
                    {
                        existingRow["CompetitionName"] = form.CompetitionName;
                        existingRow["CompetitionPlace"] = form.CompetitionPlace;
                        existingRow["CompetitionDate"] = form.CompetitionDate;
                    }

                    localAdapter.Update(dataTable);
                    dataTable.Clear();
                    localAdapter.Fill(dataTable);
                }
            }
        }


        private void AddEditDepartment(DataTable dataTable, DataRow existingRow, SqlDataAdapter localAdapter)
        {
            using (AddEditDepartmentForm form = existingRow == null ? new AddEditDepartmentForm() : new AddEditDepartmentForm(existingRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (existingRow == null)
                    {
                        string insertQuery = "INSERT INTO Departments (DepartmentName, Building) VALUES (@DepartmentName, @Building); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@DepartmentName", form.DepartmentName);
                            cmd.Parameters.AddWithValue("@Building", form.Building);

                            // Выполняем запрос и получаем новый ID
                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                // Создаем новую строку и добавляем в DataTable после успешного выполнения команды вставки
                                DataRow newRow = dataTable.NewRow();
                                newRow["id"] = Convert.ToInt32(newId);
                                newRow["DepartmentName"] = form.DepartmentName;
                                newRow["Building"] = form.Building;
                            }
                        }
                    }
                    else
                    {
                        existingRow["DepartmentName"] = form.DepartmentName;
                        existingRow["Building"] = form.Building;
                    }

                    localAdapter.Update(dataTable);
                    dataTable.Clear();
                    localAdapter.Fill(dataTable);
                }
            }
        }


        private void AddEditGroup(DataTable dataTable, DataRow existingRow, SqlDataAdapter localAdapter)
        {
            using (AddEditGroupForm form = existingRow == null ? new AddEditGroupForm() : new AddEditGroupForm(existingRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (existingRow == null) 
                    {
                        string insertQuery = "INSERT INTO Groups (TrainerID, Department) VALUES (@TrainerID, @Department); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@Department", form.Department);
                            cmd.Parameters.AddWithValue("@TrainerID", form.TrainerID);

                            // Выполняем запрос и получаем новый ID
                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                // Создаем новую строку и добавляем в DataTable после успешного выполнения команды вставки
                                DataRow newRow = dataTable.NewRow();
                                newRow["GroupId"] = Convert.ToInt32(newId);
                                newRow["Department"] = form.Department;
                                newRow["TrainerID"] = form.TrainerID;

                            }
                        }
                    }
                    else 
                    {
                        existingRow["TrainerID"] = form.TrainerID;
                        existingRow["Department"] = form.Department;
                    }

                    localAdapter.Update(dataTable);
                    dataTable.Clear();
                    localAdapter.Fill(dataTable);
                }
            }
        }


        private void AddEditStandard(DataTable dataTable, DataRow existingRow, SqlDataAdapter localAdapter)
        {
            using (AddEditStandardForm form = existingRow == null ? new AddEditStandardForm() : new AddEditStandardForm(existingRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (existingRow == null)
                    {
                        string insertQuery = "INSERT INTO Standards (Exercise, Result_for_women, Result_for_men) VALUES (@Exercise, @Result_for_women, @Result_for_men); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@Exercise", form.Exercise);
                            cmd.Parameters.AddWithValue("@Result_for_women", form.ResultForWomen);
                            cmd.Parameters.AddWithValue("@Result_for_men", form.ResultForMen);

                            // Выполняем запрос и получаем новый ID
                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                // Создаем новую строку и добавляем в DataTable после успешного выполнения команды вставки
                                DataRow newRow = dataTable.NewRow();
                                newRow["id"] = Convert.ToInt32(newId);
                                newRow["Exercise"] = form.Exercise;
                                newRow["Result_for_women"] = form.ResultForWomen;
                                newRow["Result_for_men"] = form.ResultForMen;
                            }
                        }
                    }
                    else
                    {
                        existingRow["Exercise"] = form.Exercise;
                        existingRow["Result_for_women"] = form.ResultForWomen;
                        existingRow["Result_for_men"] = form.ResultForMen;
                    }

                    localAdapter.Update(dataTable);
                    dataTable.Clear();
                    localAdapter.Fill(dataTable);
                }
            }
        }


        private void AddEditStandardPassing(DataTable dataTable, DataRow existingRow, SqlDataAdapter localAdapter)
        {
            using (AddEditStandardPassingForm form = existingRow == null ? new AddEditStandardPassingForm() : new AddEditStandardPassingForm(existingRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (existingRow == null) 
                    {
                        string insertQuery = "INSERT INTO Standards_passing (AthleteID, StandardID, Result, Date) VALUES (@AthleteID, @StandardID, @Result, @Date); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@AthleteID", form.AthleteID);
                            cmd.Parameters.AddWithValue("@StandardID", form.StandardID);
                            cmd.Parameters.AddWithValue("@Result", form.Result);
                            cmd.Parameters.AddWithValue("@Date", form.Date);

                            // Выполняем запрос и получаем новый ID
                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                // Создаем новую строку и добавляем в DataTable после успешного выполнения команды вставки
                                DataRow newRow = dataTable.NewRow();
                                newRow["id"] = Convert.ToInt32(newId);
                                newRow["AthleteID"] = form.AthleteID;
                                newRow["StandardID"] = form.StandardID;
                                newRow["Result"] = form.Result;
                                newRow["Date"] = form.Date;
                                dataTable.Rows.Add(newRow);
                            }
                        }
                    }
                    else 
                    {
                        existingRow["AthleteID"] = form.AthleteID;
                        existingRow["StandardID"] = form.StandardID;
                        existingRow["Result"] = form.Result;
                        existingRow["Date"] = form.Date;
                    }

                    localAdapter.Update(dataTable);
                    dataTable.Clear();
                    localAdapter.Fill(dataTable);
                }
            }
        }


        private void AddEditTrainer(DataTable dataTable, DataRow existingRow, SqlDataAdapter localAdapter)
        {
            using (AddEditTrainerForm form = existingRow == null ? new AddEditTrainerForm() : new AddEditTrainerForm(existingRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (existingRow == null) 
                    {
                        string insertQuery = "INSERT INTO Trainers (TrainerFIO, Department, Job, Ranking) VALUES (@TrainerFIO, @Department, @Job, @Ranking); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@TrainerFIO", form.TrainerFIO);
                            cmd.Parameters.AddWithValue("@Department", form.Department);
                            cmd.Parameters.AddWithValue("@Job", form.Job);
                            cmd.Parameters.AddWithValue("@Ranking", form.Ranking);
                            // Выполняем запрос и получаем новый ID
                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                // Создаем новую строку и добавляем в DataTable после успешного выполнения команды вставки
                                DataRow newRow = dataTable.NewRow();
                                newRow["id"] = Convert.ToInt32(newId);
                                newRow["TrainerFIO"] = form.TrainerFIO;
                                newRow["Department"] = form.Department;
                            }
                        }
                    }
                    else
                    {
                        existingRow["TrainerFIO"] = form.TrainerFIO;
                        existingRow["Department"] = form.Department;
                        existingRow["Job"] = form.Job;
                        existingRow["Ranking"] = form.Ranking;
                    }

                    localAdapter.Update(dataTable);
                    dataTable.Clear();
                    localAdapter.Fill(dataTable);
                }
            }
        }

        private void AddEditTraining(DataTable dataTable, DataRow existingRow, SqlDataAdapter localAdapter)
        {
            using (AddEditTrainingForm form = existingRow == null ? new AddEditTrainingForm() : new AddEditTrainingForm(existingRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (existingRow == null)
                    {
                        string insertQuery = "INSERT INTO Trainings (Department, GroupID, TrainerID, TrainingTime, TrainingDate) VALUES (@Department, @GroupID, @TrainerID, @TrainingTime, @TrainingDate); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@Department", form.Department);
                            cmd.Parameters.AddWithValue("@GroupID", form.GroupID);
                            cmd.Parameters.AddWithValue("@TrainerID", form.TrainerID);
                            cmd.Parameters.AddWithValue("@TrainingTime", form.TrainingTime);
                            cmd.Parameters.AddWithValue("@TrainingDate", form.TrainingDate);

                            // Выполняем запрос и получаем новый ID
                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                // Создаем новую строку и добавляем в DataTable после успешного выполнения команды вставки
                                DataRow newRow = dataTable.NewRow();
                                newRow["id"] = Convert.ToInt32(newId);
                                newRow["Department"] = form.Department;
                                newRow["GroupID"] = form.GroupID;
                                newRow["TrainerID"] = form.TrainerID;
                                newRow["TrainingTime"] = form.TrainingTime;
                                newRow["TrainingDate"] = form.TrainingDate;
                            }
                        }
                    }
                    else 
                    {
                        existingRow["Department"] = form.Department;
                        existingRow["GroupID"] = form.GroupID;
                        existingRow["TrainerID"] = form.TrainerID;
                        existingRow["TrainingTime"] = form.TrainingTime;
                        existingRow["TrainingDate"] = form.TrainingDate;
                    }

                    localAdapter.Update(dataTable);
                    dataTable.Clear();
                    localAdapter.Fill(dataTable);
                }
            }
        }

        private void AddEditTrophy(DataTable dataTable, DataRow existingRow, SqlDataAdapter localAdapter)
        {
            using (AddEditTrophyForm form = existingRow == null ? new AddEditTrophyForm() : new AddEditTrophyForm(existingRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (existingRow == null)
                    {
                        string insertQuery = "INSERT INTO Trophies (TrophyName, CompetitionID, AthleteID) VALUES (@TrophyName, @CompetitionID, @AthleteID); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@TrophyName", form.TrophyName);
                            cmd.Parameters.AddWithValue("@CompetitionID", form.CompetitionID);
                            cmd.Parameters.AddWithValue("@AthleteID", form.AthleteID);

                            // Выполняем запрос и получаем новый ID
                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                // Создаем новую строку и добавляем в DataTable после успешного выполнения команды вставки
                                DataRow newRow = dataTable.NewRow();
                                newRow["id"] = Convert.ToInt32(newId);
                                newRow["TrophyName"] = form.TrophyName;
                                newRow["CompetitionID"] = form.CompetitionID;
                                newRow["AthleteID"] = form.AthleteID;
                            }
                        }
                    }
                    else
                    {
                        existingRow["TrophyName"] = form.TrophyName;
                        existingRow["CompetitionID"] = form.CompetitionID;
                        existingRow["AthleteID"] = form.AthleteID;
                    }

                    localAdapter.Update(dataTable);
                    dataTable.Clear();
                    localAdapter.Fill(dataTable);
                }
            }
        }

        private TabPage CreateUsersTabPage()
        {
            TabPage tabPage = new TabPage("Пользователи");

            ToolStrip toolStrip = new ToolStrip();
            ToolStripButton addButton = new ToolStripButton("Добавить");
            ToolStripButton deleteButton = new ToolStripButton("Удалить");
            ToolStripButton changePasswordButton = new ToolStripButton("Изменить пароль");

            tabPage.BackColor = Color.Honeydew;
            toolStrip.BackColor = Color.GhostWhite;
            toolStrip.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));

            toolStrip.Items.Add(addButton);
            toolStrip.Items.Add(deleteButton);
            toolStrip.Items.Add(changePasswordButton);

            usersDataGridView = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 300,
                ReadOnly = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = true
            };

            CustomizeDataGridView(usersDataGridView);
            addButton.Click += AddUserButton_Click;
            deleteButton.Click += DeleteUserButton_Click;
            changePasswordButton.Click += ChangePasswordButton_Click;

            tabPage.Controls.Add(toolStrip);
            tabPage.Controls.Add(usersDataGridView);

            LoadUsers(usersDataGridView);

            return tabPage;
        }


        private void LoadUsers(DataGridView dataGridView)
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionStringColledge))
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

                    // Хэшируем пароль перед сохранением
                    string hashedPassword = AuthenticationService.HashPassword(password);

                    using (SqlConnection connection = new SqlConnection(Program.connectionStringColledge))
                    {
                        connection.Open();
                        string query = "INSERT INTO Users (Login, PasswordHash, Status) VALUES (@Login, @PasswordHash, @Status)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Login", login);
                            command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                            command.Parameters.AddWithValue("@Status", status);
                            command.ExecuteNonQuery();
                        }
                    }

                    LoadUsers(usersDataGridView);
                }
            }
        }

        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.SelectedRows.Count > 0)
            {
                int userId = (int)usersDataGridView.SelectedRows[0].Cells["ID"].Value;
                using (ChangePasswordForm changePasswordForm = new ChangePasswordForm())
                {
                    if (changePasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        string newPassword = changePasswordForm.NewPassword;
                        // Хэшируем новый пароль перед сохранением
                        string hashedPassword = AuthenticationService.HashPassword(newPassword);

                        using (SqlConnection connection = new SqlConnection(Program.connectionStringColledge))
                        {
                            connection.Open();
                            string query = "UPDATE Users SET PasswordHash = @PasswordHash WHERE ID = @UserID";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                                command.Parameters.AddWithValue("@UserID", userId);
                                command.ExecuteNonQuery();
                            }
                        }

                        LoadUsers(usersDataGridView);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для изменения пароля.");
            }
        }

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.SelectedRows.Count > 0)
            {
                int userId = (int)usersDataGridView.SelectedRows[0].Cells["ID"].Value;
                using (SqlConnection connection = new SqlConnection(Program.connectionStringColledge))
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

        private TabPage CreateQueryTabPage()
        {
            TabPage tabPage = new TabPage("Запросы");

            Label queryLabel = new Label
            {
                Text = "Выберите запрос:",
                Location = new Point(10, 10),
                AutoSize = true
            };

            ComboBox queryComboBox = new ComboBox
            {
                Location = new Point(120, 10),
                Width = 400,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.GhostWhite,
                Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)))
            };

            queryComboBox.Items.AddRange(new string[]
            {
                "Все атлеты с тренерами и департаментами",
                "Все соревнования и участвующие атлеты",
                "Все трофеи и информация о соревнованиях и атлетах",
                "Количество атлетов в каждом департаменте",
                "Все тренировки с департаментом, группой и тренером"
            });

            Button executeButton = new Button
            {
                Text = "Выполнить",
                Location = new Point(530, 10),
                Width = 80,
                BackColor = System.Drawing.SystemColors.ActiveBorder
            };

            DataGridView resultsDataGridView = new DataGridView
            {
                Location = new Point(10, 50),
                Width = 750,
                Height = 400,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            CustomizeDataGridView(resultsDataGridView);

            tabPage.BackColor = Color.Honeydew;

            executeButton.Click += (sender, e) => ExecuteSelectedQuery(queryComboBox, resultsDataGridView);

            tabPage.Controls.Add(queryLabel);
            tabPage.Controls.Add(queryComboBox);
            tabPage.Controls.Add(executeButton);
            tabPage.Controls.Add(resultsDataGridView);

            return tabPage;
        }

        private void ExecuteSelectedQuery(ComboBox queryComboBox, DataGridView resultsDataGridView)
        {
            string query = string.Empty;
            switch (queryComboBox.SelectedIndex)
            {
                case 0:
                    query = @"
                SELECT 
                    a.id AS AthleteID,
                    a.AthleteFIO,
                    t.TrainerFIO,
                    d.DepartmentName
                FROM Athletes a
                INNER JOIN Trainers t ON a.TrainerID = t.id
                INNER JOIN Departments d ON a.Department = d.DepartmentName";
                    break;
                case 1:
                    query = @"
                SELECT 
                    c.CompetitionName,
                    a.AthleteFIO,
                    cp.Category
                FROM Competition_participating cp
                INNER JOIN Competitions c ON cp.CompetitionID = c.id
                INNER JOIN Athletes a ON cp.AthleteID = a.id";
                    break;
                case 2:
                    query = @"
                SELECT 
                    t.TrophyName,
                    c.CompetitionName,
                    a.AthleteFIO
                FROM Trophies t
                INNER JOIN Competitions c ON t.CompetitionID = c.id
                INNER JOIN Athletes a ON t.AthleteID = a.id";
                    break;
                case 3:
                    query = @"
                SELECT 
                    d.DepartmentName,
                    COUNT(a.id) AS AthleteCount
                FROM Departments d
                LEFT JOIN Athletes a ON d.DepartmentName = a.Department
                GROUP BY d.DepartmentName";
                    break;
                case 4:
                    query = @"
                SELECT 
                    tr.TrainingDate,
                    tr.TrainingTime,
                    d.DepartmentName,
                    g.GroupId,
                    t.TrainerFIO
                FROM Trainings tr
                INNER JOIN Departments d ON tr.Department = d.DepartmentName
                INNER JOIN Groups g ON tr.GroupID = g.GroupId
                INNER JOIN Trainers t ON tr.TrainerID = t.id";
                    break;
            }

            if (!string.IsNullOrEmpty(query))
            {
                ExecuteQuery(query, resultsDataGridView);
            }
        }

        private void ExecuteQuery(string query, DataGridView resultsDataGridView)
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionStringColledge))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    resultsDataGridView.DataSource = dataTable;
                }
            }
        }

        private TabPage CreateReportTabPage()
        {
            TabPage tabPage = new TabPage("Отчеты");
            tabPage.BackColor = Color.Honeydew;

            Label reportLabel = new Label
            {
                Text = "Выберите отчет:",
                Location = new Point(10, 10),
                AutoSize = true
            };

            ComboBox reportComboBox = new ComboBox
            {
                Location = new Point(120, 10),
                Width = 400,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.GhostWhite,
                Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)))
            };

            reportComboBox.Items.AddRange(new string[]
            {
        "Количество атлетов в каждом департаменте",
        "Количество трофеев по соревнованиям"
            });

            Button generateButton = new Button
            {
                Text = "Сформировать отчет",
                Width = 100,
                Location = new Point(530, 10),
                BackColor = System.Drawing.SystemColors.ActiveBorder
            };

            Chart reportChart = new Chart
            {
                Location = new Point(10, 50),
                Width = 750,
                Height = 400
            };

            generateButton.Click += (sender, e) => GenerateReport(reportComboBox, reportChart);

            tabPage.Controls.Add(reportLabel);
            tabPage.Controls.Add(reportComboBox);
            tabPage.Controls.Add(generateButton);
            tabPage.Controls.Add(reportChart);

            return tabPage;
        }

        private void GenerateReport(ComboBox reportComboBox, Chart reportChart)
        {
            string query = string.Empty;
            switch (reportComboBox.SelectedIndex)
            {
                case 0:
                    query = @"
                SELECT 
                    d.DepartmentName,
                    COUNT(a.id) AS AthleteCount
                FROM Departments d
                LEFT JOIN Athletes a ON d.DepartmentName = a.Department
                GROUP BY d.DepartmentName";
                    break;
                case 1:
                    query = @"
                SELECT 
                    c.CompetitionName,
                    COUNT(t.id) AS TrophyCount
                FROM Trophies t
                INNER JOIN Competitions c ON t.CompetitionID = c.id
                GROUP BY c.CompetitionName";
                    break;
            }

            if (!string.IsNullOrEmpty(query))
            {
                using (SqlConnection connection = new SqlConnection(Program.connectionStringColledge))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        reportChart.Series.Clear();
                        reportChart.ChartAreas.Clear();
                        reportChart.ChartAreas.Add(new ChartArea());

                        Series series = new Series
                        {
                            Name = "Data",
                            IsValueShownAsLabel = true,
                            ChartType = SeriesChartType.Column,
                            Color = Color.Green // Устанавливаем цвет графика на зеленый
                        };

                        reportChart.Series.Add(series);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            series.Points.AddXY(row[0].ToString(), Convert.ToInt32(row[1]));
                        }

                        reportChart.Invalidate();
                    }
                }
            }
        }
    }
}
