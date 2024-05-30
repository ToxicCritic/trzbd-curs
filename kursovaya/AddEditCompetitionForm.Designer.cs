namespace kursovaya
{
    partial class AddEditCompetitionForm
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
            this.competitionNameLabel = new System.Windows.Forms.Label();
            this.competitionNameTextBox = new System.Windows.Forms.TextBox();
            this.competitionPlaceLabel = new System.Windows.Forms.Label();
            this.competitionPlaceTextBox = new System.Windows.Forms.TextBox();
            this.competitionDateLabel = new System.Windows.Forms.Label();
            this.competitionDatePicker = new System.Windows.Forms.DateTimePicker();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // competitionNameLabel
            // 
            this.competitionNameLabel.AutoSize = true;
            this.competitionNameLabel.Location = new System.Drawing.Point(12, 15);
            this.competitionNameLabel.Name = "competitionNameLabel";
            this.competitionNameLabel.Size = new System.Drawing.Size(132, 13);
            this.competitionNameLabel.TabIndex = 0;
            this.competitionNameLabel.Text = "Название соревнования";
            // 
            // competitionNameTextBox
            // 
            this.competitionNameTextBox.Location = new System.Drawing.Point(151, 12);
            this.competitionNameTextBox.Name = "competitionNameTextBox";
            this.competitionNameTextBox.Size = new System.Drawing.Size(200, 20);
            this.competitionNameTextBox.TabIndex = 1;
            // 
            // competitionPlaceLabel
            // 
            this.competitionPlaceLabel.AutoSize = true;
            this.competitionPlaceLabel.Location = new System.Drawing.Point(12, 41);
            this.competitionPlaceLabel.Name = "competitionPlaceLabel";
            this.competitionPlaceLabel.Size = new System.Drawing.Size(102, 13);
            this.competitionPlaceLabel.TabIndex = 2;
            this.competitionPlaceLabel.Text = "Место проведения";
            // 
            // competitionPlaceTextBox
            // 
            this.competitionPlaceTextBox.Location = new System.Drawing.Point(151, 38);
            this.competitionPlaceTextBox.Name = "competitionPlaceTextBox";
            this.competitionPlaceTextBox.Size = new System.Drawing.Size(200, 20);
            this.competitionPlaceTextBox.TabIndex = 3;
            // 
            // competitionDateLabel
            // 
            this.competitionDateLabel.AutoSize = true;
            this.competitionDateLabel.Location = new System.Drawing.Point(12, 67);
            this.competitionDateLabel.Name = "competitionDateLabel";
            this.competitionDateLabel.Size = new System.Drawing.Size(33, 13);
            this.competitionDateLabel.TabIndex = 4;
            this.competitionDateLabel.Text = "Дата";
            // 
            // competitionDatePicker
            // 
            this.competitionDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.competitionDatePicker.Location = new System.Drawing.Point(151, 64);
            this.competitionDatePicker.Name = "competitionDatePicker";
            this.competitionDatePicker.Size = new System.Drawing.Size(200, 20);
            this.competitionDatePicker.TabIndex = 5;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(277, 100);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(15, 100);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddEditCompetitionForm
            // 
            this.ClientSize = new System.Drawing.Size(364, 141);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.competitionDatePicker);
            this.Controls.Add(this.competitionDateLabel);
            this.Controls.Add(this.competitionPlaceTextBox);
            this.Controls.Add(this.competitionPlaceLabel);
            this.Controls.Add(this.competitionNameTextBox);
            this.Controls.Add(this.competitionNameLabel);
            this.Name = "AddEditCompetitionForm";
            this.Text = "Добавить/Редактировать соревнование";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}