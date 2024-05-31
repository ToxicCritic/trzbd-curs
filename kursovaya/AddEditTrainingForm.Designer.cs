namespace kursovaya
{
    partial class AddEditTrainingForm
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
            this.departmentLabel = new System.Windows.Forms.Label();
            this.departmentComboBox = new System.Windows.Forms.ComboBox();
            this.groupLabel = new System.Windows.Forms.Label();
            this.groupComboBox = new System.Windows.Forms.ComboBox();
            this.trainerLabel = new System.Windows.Forms.Label();
            this.trainerComboBox = new System.Windows.Forms.ComboBox();
            this.trainingTimeLabel = new System.Windows.Forms.Label();
            this.trainingTimePicker = new System.Windows.Forms.DateTimePicker();
            this.trainingDateLabel = new System.Windows.Forms.Label();
            this.trainingDatePicker = new System.Windows.Forms.DateTimePicker();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // departmentLabel
            // 
            this.departmentLabel.AutoSize = true;
            this.departmentLabel.Location = new System.Drawing.Point(12, 15);
            this.departmentLabel.Name = "departmentLabel";
            this.departmentLabel.Size = new System.Drawing.Size(42, 14);
            this.departmentLabel.TabIndex = 0;
            this.departmentLabel.Text = "Отдел";
            // 
            // departmentComboBox
            // 
            this.departmentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.departmentComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.departmentComboBox.FormattingEnabled = true;
            this.departmentComboBox.Location = new System.Drawing.Point(150, 12);
            this.departmentComboBox.Name = "departmentComboBox";
            this.departmentComboBox.Size = new System.Drawing.Size(200, 22);
            this.departmentComboBox.TabIndex = 1;
            // 
            // groupLabel
            // 
            this.groupLabel.AutoSize = true;
            this.groupLabel.Location = new System.Drawing.Point(12, 42);
            this.groupLabel.Name = "groupLabel";
            this.groupLabel.Size = new System.Drawing.Size(46, 14);
            this.groupLabel.TabIndex = 2;
            this.groupLabel.Text = "Группа";
            // 
            // groupComboBox
            // 
            this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.groupComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupComboBox.FormattingEnabled = true;
            this.groupComboBox.Location = new System.Drawing.Point(150, 39);
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Size = new System.Drawing.Size(200, 22);
            this.groupComboBox.TabIndex = 3;
            // 
            // trainerLabel
            // 
            this.trainerLabel.AutoSize = true;
            this.trainerLabel.Location = new System.Drawing.Point(12, 69);
            this.trainerLabel.Name = "trainerLabel";
            this.trainerLabel.Size = new System.Drawing.Size(46, 14);
            this.trainerLabel.TabIndex = 4;
            this.trainerLabel.Text = "Тренер";
            // 
            // trainerComboBox
            // 
            this.trainerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trainerComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.trainerComboBox.FormattingEnabled = true;
            this.trainerComboBox.Location = new System.Drawing.Point(150, 66);
            this.trainerComboBox.Name = "trainerComboBox";
            this.trainerComboBox.Size = new System.Drawing.Size(200, 22);
            this.trainerComboBox.TabIndex = 5;
            // 
            // trainingTimeLabel
            // 
            this.trainingTimeLabel.AutoSize = true;
            this.trainingTimeLabel.Location = new System.Drawing.Point(12, 96);
            this.trainingTimeLabel.Name = "trainingTimeLabel";
            this.trainingTimeLabel.Size = new System.Drawing.Size(111, 14);
            this.trainingTimeLabel.TabIndex = 6;
            this.trainingTimeLabel.Text = "Время тренировки";
            // 
            // trainingTimePicker
            // 
            this.trainingTimePicker.CustomFormat = "HH:mm";
            this.trainingTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.trainingTimePicker.Location = new System.Drawing.Point(150, 93);
            this.trainingTimePicker.Name = "trainingTimePicker";
            this.trainingTimePicker.ShowUpDown = true;
            this.trainingTimePicker.Size = new System.Drawing.Size(200, 22);
            this.trainingTimePicker.TabIndex = 7;
            // 
            // trainingDateLabel
            // 
            this.trainingDateLabel.AutoSize = true;
            this.trainingDateLabel.Location = new System.Drawing.Point(12, 123);
            this.trainingDateLabel.Name = "trainingDateLabel";
            this.trainingDateLabel.Size = new System.Drawing.Size(34, 14);
            this.trainingDateLabel.TabIndex = 8;
            this.trainingDateLabel.Text = "Дата";
            // 
            // trainingDatePicker
            // 
            this.trainingDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.trainingDatePicker.Location = new System.Drawing.Point(150, 120);
            this.trainingDatePicker.Name = "trainingDatePicker";
            this.trainingDatePicker.Size = new System.Drawing.Size(200, 22);
            this.trainingDatePicker.TabIndex = 9;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(275, 146);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(15, 146);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddEditTrainingForm
            // 
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(362, 181);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.trainingDatePicker);
            this.Controls.Add(this.trainingDateLabel);
            this.Controls.Add(this.trainingTimePicker);
            this.Controls.Add(this.trainingTimeLabel);
            this.Controls.Add(this.trainerComboBox);
            this.Controls.Add(this.trainerLabel);
            this.Controls.Add(this.groupComboBox);
            this.Controls.Add(this.groupLabel);
            this.Controls.Add(this.departmentComboBox);
            this.Controls.Add(this.departmentLabel);
            this.Font = new System.Drawing.Font("Bahnschrift", 9F);
            this.Name = "AddEditTrainingForm";
            this.Text = "Добавить/Редактировать тренировку";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}