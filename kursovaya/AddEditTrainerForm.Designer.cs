namespace kursovaya
{
    partial class AddEditTrainerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.trainerFIOLabel = new System.Windows.Forms.Label();
            this.trainerFIOTextBox = new System.Windows.Forms.TextBox();
            this.departmentLabel = new System.Windows.Forms.Label();
            this.departmentComboBox = new System.Windows.Forms.ComboBox();
            this.jobLabel = new System.Windows.Forms.Label();
            this.jobTextBox = new System.Windows.Forms.TextBox();
            this.rankingLabel = new System.Windows.Forms.Label();
            this.rankingTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trainerFIOLabel
            // 
            this.trainerFIOLabel.AutoSize = true;
            this.trainerFIOLabel.Location = new System.Drawing.Point(12, 15);
            this.trainerFIOLabel.Name = "trainerFIOLabel";
            this.trainerFIOLabel.Size = new System.Drawing.Size(33, 14);
            this.trainerFIOLabel.TabIndex = 0;
            this.trainerFIOLabel.Text = "ФИО";
            // 
            // trainerFIOTextBox
            // 
            this.trainerFIOTextBox.Location = new System.Drawing.Point(150, 12);
            this.trainerFIOTextBox.Name = "trainerFIOTextBox";
            this.trainerFIOTextBox.Size = new System.Drawing.Size(200, 22);
            this.trainerFIOTextBox.TabIndex = 1;
            // 
            // departmentLabel
            // 
            this.departmentLabel.AutoSize = true;
            this.departmentLabel.Location = new System.Drawing.Point(12, 41);
            this.departmentLabel.Name = "departmentLabel";
            this.departmentLabel.Size = new System.Drawing.Size(42, 14);
            this.departmentLabel.TabIndex = 2;
            this.departmentLabel.Text = "Отдел";
            // 
            // departmentComboBox
            // 
            this.departmentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.departmentComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.departmentComboBox.FormattingEnabled = true;
            this.departmentComboBox.Location = new System.Drawing.Point(150, 38);
            this.departmentComboBox.Name = "departmentComboBox";
            this.departmentComboBox.Size = new System.Drawing.Size(200, 22);
            this.departmentComboBox.TabIndex = 3;
            // 
            // jobLabel
            // 
            this.jobLabel.AutoSize = true;
            this.jobLabel.Location = new System.Drawing.Point(12, 67);
            this.jobLabel.Name = "jobLabel";
            this.jobLabel.Size = new System.Drawing.Size(59, 14);
            this.jobLabel.TabIndex = 4;
            this.jobLabel.Text = "Должность";
            // 
            // jobTextBox
            // 
            this.jobTextBox.Location = new System.Drawing.Point(150, 64);
            this.jobTextBox.Name = "jobTextBox";
            this.jobTextBox.Size = new System.Drawing.Size(200, 22);
            this.jobTextBox.TabIndex = 5;
            // 
            // rankingLabel
            // 
            this.rankingLabel.AutoSize = true;
            this.rankingLabel.Location = new System.Drawing.Point(12, 93);
            this.rankingLabel.Name = "rankingLabel";
            this.rankingLabel.Size = new System.Drawing.Size(47, 14);
            this.rankingLabel.TabIndex = 6;
            this.rankingLabel.Text = "Рейтинг";
            // 
            // rankingTextBox
            // 
            this.rankingTextBox.Location = new System.Drawing.Point(150, 90);
            this.rankingTextBox.Name = "rankingTextBox";
            this.rankingTextBox.Size = new System.Drawing.Size(200, 22);
            this.rankingTextBox.TabIndex = 7;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(275, 125);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(15, 125);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddEditTrainerForm
            // 
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(362, 160);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.rankingTextBox);
            this.Controls.Add(this.rankingLabel);
            this.Controls.Add(this.jobTextBox);
            this.Controls.Add(this.jobLabel);
            this.Controls.Add(this.departmentComboBox);
            this.Controls.Add(this.departmentLabel);
            this.Controls.Add(this.trainerFIOTextBox);
            this.Controls.Add(this.trainerFIOLabel);
            this.Font = new System.Drawing.Font("Bahnschrift", 9F);
            this.Name = "AddEditTrainerForm";
            this.Text = "Добавить/Редактировать тренера";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
