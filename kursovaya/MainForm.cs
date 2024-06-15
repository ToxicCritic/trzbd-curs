using System;
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
        // tabPage.BackColor = Color.Honeydew;
        // toolStrip.BackColor = Color.GhostWhite;
        // toolStrip.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        private TabPage CreateTabPage(string tableName, string selectQuery, Action<DataTable, DataRow> addEditHandler = null,
                              string foreignKeyField1 = null, string referenceTable1 = null, string referenceField1 = null, string displayMember1 = null, int? position1 = null,
                              string foreignKeyField2 = null, string referenceTable2 = null, string referenceField2 = null, string displayMember2 = null, int? position2 = null,
                              string foreignKeyField3 = null, string referenceTable3 = null, string referenceField3 = null, string displayMember3 = null, int? position3 = null)
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
                AllowUserToDeleteRows = LoginForm.IsAdmin,
                AutoGenerateColumns = false
            };

            SqlDataAdapter localAdapter = new SqlDataAdapter(selectQuery, sqlConnection);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(localAdapter);
            DataTable localTable = new DataTable();
            localAdapter.Fill(localTable);

            // Установка команд вручную
            localAdapter.InsertCommand = sqlCommandBuilder.GetInsertCommand();
            localAdapter.UpdateCommand = sqlCommandBuilder.GetUpdateCommand();
            localAdapter.DeleteCommand = sqlCommandBuilder.GetDeleteCommand();

            BindingSource bindingSource = new BindingSource
            {
                DataSource = localTable
            };
            dataGridView.DataSource = bindingSource;

            // Добавление всех столбцов из DataTable в DataGridView, за исключением столбцов с выпадающими списками
            foreach (DataColumn column in localTable.Columns)
            {
                if ((foreignKeyField1 != null && column.ColumnName == foreignKeyField1) ||
                    (foreignKeyField2 != null && column.ColumnName == foreignKeyField2) ||
                    (foreignKeyField3 != null && column.ColumnName == foreignKeyField3))
                {
                    continue; // Пропускаем столбцы, которые будут заменены на выпадающие списки
                }

                dataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = column.ColumnName,
                    HeaderText = column.ColumnName
                });
            }

            // Добавление выпадающего списка для первого внешнего ключа
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

            // Добавление выпадающего списка для второго внешнего ключа
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

            // Добавление выпадающего списка для третьего внешнего ключа
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
                bindingSource.ListChanged += (sender, args) =>
                {
                    if (args.ListChangedType == ListChangedType.ItemAdded ||
                        args.ListChangedType == ListChangedType.ItemDeleted ||
                        args.ListChangedType == ListChangedType.ItemChanged)
                    {
                        localAdapter.Update(localTable);

                        // Перезагрузка данных для обновления ID
                        localTable.Clear();
                        localAdapter.Fill(localTable);
                    }
                };

                addButton.Click += (sender, e) => addEditHandler?.Invoke(localTable, null);
                updateButton.Click += (sender, e) =>
                {
                    if (dataGridView.SelectedRows.Count > 0)
                    {
                        DataRow row = ((DataRowView)dataGridView.SelectedRows[0].DataBoundItem).Row;
                        addEditHandler?.Invoke(localTable, row);
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

        private void AddEditAthlete(DataTable dataTable, DataRow dataRow)
        {
            using (AddEditAthleteForm form = new AddEditAthleteForm(dataRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (dataRow == null)
                    {
                        dataRow = dataTable.NewRow();
                        dataTable.Rows.Add(dataRow);
                    }

                    dataRow["AthleteFIO"] = form.FIO;
                    dataRow["Department"] = form.Department;
                    dataRow["GroupID"] = form.GroupID;
                    dataRow["TrainerID"] = form.TrainerID;
                    dataRow["Ranking"] = form.Ranking;
                    dataRow["Heights"] = form.Heights;
                    dataRow["Weights"] = form.Weights;
                    dataRow["Education_begin"] = form.EducationBegin;

                    if (dataRow.RowState == DataRowState.Added)
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
                                dataRow["id"] = Convert.ToInt32(newId);
                            }
                        }
                    }
                }
            }
        }


        private void AddEditCompetition(DataTable dataTable, DataRow dataRow)
        {
            using (AddEditCompetitionForm form = new AddEditCompetitionForm(dataRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (dataRow == null)
                    {
                        dataRow = dataTable.NewRow();
                        dataTable.Rows.Add(dataRow);
                    }

                    dataRow["CompetitionName"] = form.CompetitionName;
                    dataRow["CompetitionPlace"] = form.CompetitionPlace;
                    dataRow["CompetitionDate"] = form.CompetitionDate;

                    if (dataRow.RowState == DataRowState.Added)
                    {
                        string insertQuery = "INSERT INTO Competitions (CompetitionName, CompetitionPlace, CompetitionDate) VALUES (@CompetitionName, @CompetitionPlace, @CompetitionDate); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@CompetitionName", form.CompetitionName);
                            cmd.Parameters.AddWithValue("@CompetitionPlace", form.CompetitionPlace);
                            cmd.Parameters.AddWithValue("@CompetitionDate", form.CompetitionDate);

                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                dataRow["id"] = Convert.ToInt32(newId);
                            }
                        }
                    }
                }
            }
        }

        private void AddEditCompetitionParticipating(DataTable dataTable, DataRow dataRow)
        {
            using (AddEditCompetitionParticipatingForm form = new AddEditCompetitionParticipatingForm(dataRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (dataRow == null)
                    {
                        dataRow = dataTable.NewRow();
                        dataTable.Rows.Add(dataRow);
                    }

                    dataRow["CompetitionID"] = form.CompetitionID;
                    dataRow["AthleteID"] = form.AthleteID;
                    dataRow["Category"] = form.Category;

                    if (dataRow.RowState == DataRowState.Added)
                    {
                        string insertQuery = "INSERT INTO Competition_participating (CompetitionID, AthleteID, Category) VALUES (@CompetitionID, @AthleteID, @Category); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@CompetitionID", form.CompetitionID);
                            cmd.Parameters.AddWithValue("@AthleteID", form.AthleteID);
                            cmd.Parameters.AddWithValue("@Category", form.Category);

                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                dataRow["id"] = Convert.ToInt32(newId);
                            }
                        }
                    }
                }
            }
        }


        private void AddEditDepartment(DataTable dataTable, DataRow dataRow)
        {
            using (AddEditDepartmentForm form = new AddEditDepartmentForm(dataRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (dataRow == null)
                    {
                        dataRow = dataTable.NewRow();
                        dataTable.Rows.Add(dataRow);
                    }

                    dataRow["DepartmentName"] = form.DepartmentName;
                    dataRow["Building"] = form.Building;

                    if (dataRow.RowState == DataRowState.Added)
                    {
                        string insertQuery = "INSERT INTO Departments (DepartmentName, Building) VALUES (@DepartmentName, @Building); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@DepartmentName", form.DepartmentName);
                            cmd.Parameters.AddWithValue("@Building", form.Building);

                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                dataRow["DepartmentName"] = newId.ToString();
                            }
                        }
                    }
                }
            }
        }


        private void AddEditGroup(DataTable dataTable, DataRow dataRow)
        {
            using (AddEditGroupForm form = new AddEditGroupForm(dataRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (dataRow == null)
                    {
                        dataRow = dataTable.NewRow();
                        dataTable.Rows.Add(dataRow);
                    }

                    dataRow["TrainerID"] = form.TrainerID;
                    dataRow["Department"] = form.Department;

                    if (dataRow.RowState == DataRowState.Added)
                    {
                        string insertQuery = "INSERT INTO Groups (TrainerID, Department) VALUES (@TrainerID, @Department); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@TrainerID", form.TrainerID);
                            cmd.Parameters.AddWithValue("@Department", form.Department);

                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                dataRow["GroupID"] = Convert.ToInt32(newId);
                            }
                        }
                    }
                }
            }
        }

        private void AddEditStandard(DataTable dataTable, DataRow dataRow)
        {
            using (AddEditStandardForm form = new AddEditStandardForm(dataRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (dataRow == null)
                    {
                        dataRow = dataTable.NewRow();
                        dataTable.Rows.Add(dataRow);
                    }

                    dataRow["Exercise"] = form.Exercise;
                    dataRow["Result_for_women"] = form.ResultForWomen;
                    dataRow["Result_for_men"] = form.ResultForMen;

                    if (dataRow.RowState == DataRowState.Added)
                    {
                        string insertQuery = "INSERT INTO Standards (Exercise, Result_for_women, Result_for_men) VALUES (@Exercise, @Result_for_women, @Result_for_men); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@Exercise", form.Exercise);
                            cmd.Parameters.AddWithValue("@Result_for_women", form.ResultForWomen);
                            cmd.Parameters.AddWithValue("@Result_for_men", form.ResultForMen);

                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                dataRow["id"] = Convert.ToInt32(newId);
                            }
                        }
                    }
                }
            }
        }


        private void AddEditStandardPassing(DataTable dataTable, DataRow dataRow)
        {
            using (AddEditStandardPassingForm form = new AddEditStandardPassingForm(dataRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (dataRow == null)
                    {
                        dataRow = dataTable.NewRow();
                        dataTable.Rows.Add(dataRow);
                    }

                    dataRow["AthleteID"] = form.AthleteID;
                    dataRow["StandardID"] = form.StandardID;
                    dataRow["Result"] = form.Result;
                    dataRow["Date"] = form.Date;

                    if (dataRow.RowState == DataRowState.Added)
                    {
                        string insertQuery = "INSERT INTO Standards_passing (AthleteID, StandardID, Result, Date) VALUES (@AthleteID, @StandardID, @Result, @Date); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@AthleteID", form.AthleteID);
                            cmd.Parameters.AddWithValue("@StandardID", form.StandardID);
                            cmd.Parameters.AddWithValue("@Result", form.Result);
                            cmd.Parameters.AddWithValue("@Date", form.Date);

                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                dataRow["id"] = Convert.ToInt32(newId);
                            }
                        }
                    }
                }
            }
        }


        private void AddEditTrainer(DataTable dataTable, DataRow dataRow)
        {
            using (AddEditTrainerForm form = new AddEditTrainerForm(dataRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (dataRow == null)
                    {
                        dataRow = dataTable.NewRow();
                        dataTable.Rows.Add(dataRow);
                    }

                    dataRow["TrainerFIO"] = form.TrainerFIO;
                    dataRow["Department"] = form.Department;
                    dataRow["Job"] = form.Job;
                    dataRow["Ranking"] = form.Ranking;

                    if (dataRow.RowState == DataRowState.Added)
                    {
                        string insertQuery = "INSERT INTO Trainers (TrainerFIO, Department, Job, Ranking) VALUES (@TrainerFIO, @Department, @Job, @Ranking); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@TrainerFIO", form.TrainerFIO);
                            cmd.Parameters.AddWithValue("@Department", form.Department);
                            cmd.Parameters.AddWithValue("@Job", form.Job);
                            cmd.Parameters.AddWithValue("@Ranking", form.Ranking);

                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                dataRow["id"] = Convert.ToInt32(newId);
                            }
                        }
                    }
                }
            }
        }


        private void AddEditTraining(DataTable dataTable, DataRow dataRow)
        {
            using (AddEditTrainingForm form = new AddEditTrainingForm(dataRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (dataRow == null)
                    {
                        dataRow = dataTable.NewRow();
                        dataTable.Rows.Add(dataRow);
                    }

                    dataRow["Department"] = form.Department;
                    dataRow["GroupID"] = form.GroupID;
                    dataRow["TrainerID"] = form.TrainerID;
                    dataRow["TrainingTime"] = form.TrainingTime;
                    dataRow["TrainingDate"] = form.TrainingDate;

                    if (dataRow.RowState == DataRowState.Added)
                    {
                        string insertQuery = "INSERT INTO Trainings (Department, GroupID, TrainerID, TrainingTime, TrainingDate) VALUES (@Department, @GroupID, @TrainerID, @TrainingTime, @TrainingDate); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@Department", form.Department);
                            cmd.Parameters.AddWithValue("@GroupID", form.GroupID);
                            cmd.Parameters.AddWithValue("@TrainerID", form.TrainerID);
                            cmd.Parameters.AddWithValue("@TrainingTime", form.TrainingTime);
                            cmd.Parameters.AddWithValue("@TrainingDate", form.TrainingDate);

                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                dataRow["id"] = Convert.ToInt32(newId);
                            }
                        }
                    }
                }
            }
        }


        private void AddEditTrophy(DataTable dataTable, DataRow dataRow)
        {
            using (AddEditTrophyForm form = new AddEditTrophyForm(dataRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (dataRow == null)
                    {
                        dataRow = dataTable.NewRow();
                        dataTable.Rows.Add(dataRow);
                    }

                    dataRow["TrophyName"] = form.TrophyName;
                    dataRow["CompetitionID"] = form.CompetitionID;
                    dataRow["AthleteID"] = form.AthleteID;

                    if (dataRow.RowState == DataRowState.Added)
                    {
                        string insertQuery = "INSERT INTO Trophies (TrophyName, CompetitionID, AthleteID) VALUES (@TrophyName, @CompetitionID, @AthleteID); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@TrophyName", form.TrophyName);
                            cmd.Parameters.AddWithValue("@CompetitionID", form.CompetitionID);
                            cmd.Parameters.AddWithValue("@AthleteID", form.AthleteID);

                            object newId = cmd.ExecuteScalar();
                            if (newId != null && newId != DBNull.Value)
                            {
                                dataRow["id"] = Convert.ToInt32(newId);
                            }
                        }
                    }
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

        private SqlCommand GetInsertCommand(string tableName, SqlConnection connection)
        {
            switch (tableName)
            {
                case "Athletes":
                    return new SqlCommand("INSERT INTO Athletes (AthleteFIO, Department, GroupID, TrainerID, Ranking, Heights, Weights, Education_begin) VALUES (@AthleteFIO, @Department, @GroupID, @TrainerID, @Ranking, @Heights, @Weights, @Education_begin); SELECT SCOPE_IDENTITY();", connection);
                case "Competitions":
                    return new SqlCommand("INSERT INTO Competitions (CompetitionName, CompetitionPlace, CompetitionDate) VALUES (@CompetitionName, @CompetitionPlace, @CompetitionDate); SELECT SCOPE_IDENTITY();", connection);
                case "Competition Participating":
                    return new SqlCommand("INSERT INTO Competition_participating (CompetitionID, AthleteID, Category) VALUES (@CompetitionID, @AthleteID, @Category); SELECT SCOPE_IDENTITY();", connection);
                case "Departments":
                    return new SqlCommand("INSERT INTO Departments (DepartmentName, Building) VALUES (@DepartmentName, @Building); SELECT SCOPE_IDENTITY();", connection);
                case "Groups":
                    return new SqlCommand("INSERT INTO Groups (TrainerID, Department) VALUES (@TrainerID, @Department); SELECT SCOPE_IDENTITY();", connection);
                case "Standards":
                    return new SqlCommand("INSERT INTO Standards (Exercise, Result_for_women, Result_for_men) VALUES (@Exercise, @Result_for_women, @Result_for_men); SELECT SCOPE_IDENTITY();", connection);
                case "Standards Passing":
                    return new SqlCommand("INSERT INTO Standards_passing (AthleteID, StandardID, Result, Date) VALUES (@AthleteID, @StandardID, @Result, @Date); SELECT SCOPE_IDENTITY();", connection);
                case "Trainers":
                    return new SqlCommand("INSERT INTO Trainers (TrainerFIO, Department, Job, Ranking) VALUES (@TrainerFIO, @Department, @Job, @Ranking); SELECT SCOPE_IDENTITY();", connection);
                case "Trainings":
                    return new SqlCommand("INSERT INTO Trainings (Department, GroupID, TrainerID, TrainingTime, TrainingDate) VALUES (@Department, @GroupID, @TrainerID, @TrainingTime, @TrainingDate); SELECT SCOPE_IDENTITY();", connection);
                case "Trophies":
                    return new SqlCommand("INSERT INTO Trophies (TrophyName, CompetitionID, AthleteID) VALUES (@TrophyName, @CompetitionID, @AthleteID); SELECT SCOPE_IDENTITY();", connection);
                case "Users":
                    return new SqlCommand("INSERT INTO Users (Login, PasswordHash, Status) VALUES (@Login, @PasswordHash, @Status); SELECT SCOPE_IDENTITY();", connection);
                default:
                    throw new ArgumentException("Unknown table name");
            }
        }

        private SqlCommand GetUpdateCommand(string tableName, SqlConnection connection)
        {
            switch (tableName)
            {
                case "Athletes":
                    return new SqlCommand("UPDATE Athletes SET AthleteFIO = @AthleteFIO, Department = @Department, GroupID = @GroupID, TrainerID = @TrainerID, Ranking = @Ranking, Heights = @Heights, Weights = @Weights, Education_begin = @Education_begin WHERE id = @id", connection);
                case "Competitions":
                    return new SqlCommand("UPDATE Competitions SET CompetitionName = @CompetitionName, CompetitionPlace = @CompetitionPlace, CompetitionDate = @CompetitionDate WHERE id = @id", connection);
                case "Competition Participating":
                    return new SqlCommand("UPDATE Competition_participating SET CompetitionID = @CompetitionID, AthleteID = @AthleteID, Category = @Category WHERE id = @id", connection);
                case "Departments":
                    return new SqlCommand("UPDATE Departments SET DepartmentName = @DepartmentName, Building = @Building WHERE DepartmentName = @DepartmentName", connection);
                case "Groups":
                    return new SqlCommand("UPDATE Groups SET TrainerID = @TrainerID, Department = @Department WHERE GroupID = @GroupID", connection);
                case "Standards":
                    return new SqlCommand("UPDATE Standards SET Exercise = @Exercise, Result_for_women = @Result_for_women, Result_for_men = @Result_for_men WHERE id = @id", connection);
                case "Standards Passing":
                    return new SqlCommand("UPDATE Standards_passing SET AthleteID = @AthleteID, StandardID = @StandardID, Result = @Result, Date = @Date WHERE id = @id", connection);
                case "Trainers":
                    return new SqlCommand("UPDATE Trainers SET TrainerFIO = @TrainerFIO, Department = @Department, Job = @Job, Ranking = @Ranking WHERE id = @id", connection);
                case "Trainings":
                    return new SqlCommand("UPDATE Trainings SET Department = @Department, GroupID = @GroupID, TrainerID = @TrainerID, TrainingTime = @TrainingTime, TrainingDate = @TrainingDate WHERE id = @id", connection);
                case "Trophies":
                    return new SqlCommand("UPDATE Trophies SET TrophyName = @TrophyName, CompetitionID = @CompetitionID, AthleteID = @AthleteID WHERE id = @id", connection);
                case "Users":
                    return new SqlCommand("UPDATE Users SET Login = @Login, PasswordHash = @PasswordHash, Status = @Status WHERE id = @id", connection);
                default:
                    throw new ArgumentException("Unknown table name");
            }
        }

        private SqlCommand GetDeleteCommand(string tableName, SqlConnection connection)
        {
            switch (tableName)
            {
                case "Athletes":
                    return new SqlCommand("DELETE FROM Athletes WHERE id = @id", connection);
                case "Competitions":
                    return new SqlCommand("DELETE FROM Competitions WHERE id = @id", connection);
                case "Competition Participating":
                    return new SqlCommand("DELETE FROM Competition_participating WHERE id = @id", connection);
                case "Departments":
                    return new SqlCommand("DELETE FROM Departments WHERE DepartmentName = @DepartmentName", connection);
                case "Groups":
                    return new SqlCommand("DELETE FROM Groups WHERE GroupID = @GroupID", connection);
                case "Standards":
                    return new SqlCommand("DELETE FROM Standards WHERE id = @id", connection);
                case "Standards Passing":
                    return new SqlCommand("DELETE FROM Standards_passing WHERE id = @id", connection);
                case "Trainers":
                    return new SqlCommand("DELETE FROM Trainers WHERE id = @id", connection);
                case "Trainings":
                    return new SqlCommand("DELETE FROM Trainings WHERE id = @id", connection);
                case "Trophies":
                    return new SqlCommand("DELETE FROM Trophies WHERE id = @id", connection);
                case "Users":
                    return new SqlCommand("DELETE FROM Users WHERE id = @id", connection);
                default:
                    throw new ArgumentException("Unknown table name");
            }
        }

        private void AddParametersToCommand(SqlCommand command)
        {
            switch (command.CommandText)
            {
                case string s when s.Contains("Athletes"):
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4, "id"));
                    command.Parameters.Add(new SqlParameter("@AthleteFIO", SqlDbType.NVarChar, 50, "AthleteFIO"));
                    command.Parameters.Add(new SqlParameter("@Department", SqlDbType.NVarChar, 20, "Department"));
                    command.Parameters.Add(new SqlParameter("@GroupID", SqlDbType.Int, 4, "GroupID"));
                    command.Parameters.Add(new SqlParameter("@TrainerID", SqlDbType.Int, 4, "TrainerID"));
                    command.Parameters.Add(new SqlParameter("@Ranking", SqlDbType.NVarChar, 30, "Ranking"));
                    command.Parameters.Add(new SqlParameter("@Heights", SqlDbType.Int, 4, "Heights"));
                    command.Parameters.Add(new SqlParameter("@Weights", SqlDbType.Int, 4, "Weights"));
                    command.Parameters.Add(new SqlParameter("@Education_begin", SqlDbType.Date, 3, "Education_begin"));
                    break;
                case string s when s.Contains("Competitions"):
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4, "id"));
                    command.Parameters.Add(new SqlParameter("@CompetitionName", SqlDbType.NVarChar, 50, "CompetitionName"));
                    command.Parameters.Add(new SqlParameter("@CompetitionPlace", SqlDbType.NVarChar, 30, "CompetitionPlace"));
                    command.Parameters.Add(new SqlParameter("@CompetitionDate", SqlDbType.Date, 3, "CompetitionDate"));
                    break;
                case string s when s.Contains("Competition_participating"):
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4, "id"));
                    command.Parameters.Add(new SqlParameter("@CompetitionID", SqlDbType.Int, 4, "CompetitionID"));
                    command.Parameters.Add(new SqlParameter("@AthleteID", SqlDbType.Int, 4, "AthleteID"));
                    command.Parameters.Add(new SqlParameter("@Category", SqlDbType.NVarChar, 255, "Category"));
                    break;
                case string s when s.Contains("Departments"):
                    command.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 20, "DepartmentName"));
                    command.Parameters.Add(new SqlParameter("@Building", SqlDbType.NVarChar, 20, "Building"));
                    break;
                case string s when s.Contains("Groups"):
                    command.Parameters.Add(new SqlParameter("@GroupID", SqlDbType.Int, 4, "GroupID"));
                    command.Parameters.Add(new SqlParameter("@TrainerID", SqlDbType.Int, 4, "TrainerID"));
                    command.Parameters.Add(new SqlParameter("@Department", SqlDbType.NVarChar, 20, "Department"));
                    break;
                case string s when s.Contains("Standards"):
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4, "id"));
                    command.Parameters.Add(new SqlParameter("@Exercise", SqlDbType.NVarChar, 30, "Exercise"));
                    command.Parameters.Add(new SqlParameter("@Result_for_women", SqlDbType.NVarChar, 30, "Result_for_women"));
                    command.Parameters.Add(new SqlParameter("@Result_for_men", SqlDbType.NVarChar, 30, "Result_for_men"));
                    break;
                case string s when s.Contains("Standards_passing"):
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4, "id"));
                    command.Parameters.Add(new SqlParameter("@AthleteID", SqlDbType.Int, 4, "AthleteID"));
                    command.Parameters.Add(new SqlParameter("@StandardID", SqlDbType.Int, 4, "StandardID"));
                    command.Parameters.Add(new SqlParameter("@Result", SqlDbType.NVarChar, 20, "Result"));
                    command.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 3, "Date"));
                    break;
                case string s when s.Contains("Trainers"):
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4, "id"));
                    command.Parameters.Add(new SqlParameter("@TrainerFIO", SqlDbType.NVarChar, 50, "TrainerFIO"));
                    command.Parameters.Add(new SqlParameter("@Department", SqlDbType.NVarChar, 20, "Department"));
                    command.Parameters.Add(new SqlParameter("@Job", SqlDbType.NVarChar, 30, "Job"));
                    command.Parameters.Add(new SqlParameter("@Ranking", SqlDbType.NVarChar, 30, "Ranking"));
                    break;
                case string s when s.Contains("Trainings"):
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4, "id"));
                    command.Parameters.Add(new SqlParameter("@Department", SqlDbType.NVarChar, 20, "Department"));
                    command.Parameters.Add(new SqlParameter("@GroupID", SqlDbType.Int, 4, "GroupID"));
                    command.Parameters.Add(new SqlParameter("@TrainerID", SqlDbType.Int, 4, "TrainerID"));
                    command.Parameters.Add(new SqlParameter("@TrainingTime", SqlDbType.Time, 7, "TrainingTime"));
                    command.Parameters.Add(new SqlParameter("@TrainingDate", SqlDbType.Date, 3, "TrainingDate"));
                    break;
                case string s when s.Contains("Trophies"):
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4, "id"));
                    command.Parameters.Add(new SqlParameter("@TrophyName", SqlDbType.NVarChar, 30, "TrophyName"));
                    command.Parameters.Add(new SqlParameter("@CompetitionID", SqlDbType.Int, 4, "CompetitionID"));
                    command.Parameters.Add(new SqlParameter("@AthleteID", SqlDbType.Int, 4, "AthleteID"));
                    break;
                case string s when s.Contains("Users"):
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4, "id"));
                    command.Parameters.Add(new SqlParameter("@Login", SqlDbType.NVarChar, 50, "Login"));
                    command.Parameters.Add(new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 256, "PasswordHash"));
                    command.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 10, "Status"));
                    break;
                default:
                    throw new ArgumentException("Unknown command text");
            }
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
