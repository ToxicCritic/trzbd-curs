namespace kursovaya
{
    partial class AddEditStandardPassingForm
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
            this.athleteLabel = new System.Windows.Forms.Label();
            this.athleteComboBox = new System.Windows.Forms.ComboBox();
            this.standardLabel = new System.Windows.Forms.Label();
            this.standardComboBox = new System.Windows.Forms.ComboBox();
            this.resultLabel = new System.Windows.Forms.Label();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.dateLabel = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // athleteLabel
            // 
            this.athleteLabel.AutoSize = true;
            this.athleteLabel.Location = new System.Drawing.Point(12, 15);
            this.athleteLabel.Name = "athleteLabel";
            this.athleteLabel.Size = new System.Drawing.Size(43, 14);
            this.athleteLabel.TabIndex = 0;
            this.athleteLabel.Text = "Атлет:";
            // 
            // athleteComboBox
            // 
            this.athleteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.athleteComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.athleteComboBox.FormattingEnabled = true;
            this.athleteComboBox.Location = new System.Drawing.Point(150, 12);
            this.athleteComboBox.Name = "athleteComboBox";
            this.athleteComboBox.Size = new System.Drawing.Size(200, 22);
            this.athleteComboBox.TabIndex = 1;
            // 
            // standardLabel
            // 
            this.standardLabel.AutoSize = true;
            this.standardLabel.Location = new System.Drawing.Point(12, 42);
            this.standardLabel.Name = "standardLabel";
            this.standardLabel.Size = new System.Drawing.Size(63, 14);
            this.standardLabel.TabIndex = 2;
            this.standardLabel.Text = "Стандарт:";
            // 
            // standardComboBox
            // 
            this.standardComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.standardComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.standardComboBox.FormattingEnabled = true;
            this.standardComboBox.Location = new System.Drawing.Point(150, 39);
            this.standardComboBox.Name = "standardComboBox";
            this.standardComboBox.Size = new System.Drawing.Size(200, 22);
            this.standardComboBox.TabIndex = 3;
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(12, 69);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(65, 14);
            this.resultLabel.TabIndex = 4;
            this.resultLabel.Text = "Результат:";
            // 
            // resultTextBox
            // 
            this.resultTextBox.Location = new System.Drawing.Point(150, 66);
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.Size = new System.Drawing.Size(200, 22);
            this.resultTextBox.TabIndex = 5;
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(12, 95);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(37, 14);
            this.dateLabel.TabIndex = 6;
            this.dateLabel.Text = "Дата:";
            // 
            // datePicker
            // 
            this.datePicker.Location = new System.Drawing.Point(150, 92);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(200, 22);
            this.datePicker.TabIndex = 7;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(275, 118);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(15, 118);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddEditStandardPassingForm
            // 
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(362, 153);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.resultTextBox);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.standardComboBox);
            this.Controls.Add(this.standardLabel);
            this.Controls.Add(this.athleteComboBox);
            this.Controls.Add(this.athleteLabel);
            this.Font = new System.Drawing.Font("Bahnschrift", 9F);
            this.Name = "AddEditStandardPassingForm";
            this.Text = "Добавить/Редактировать прохождение стандарта";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}