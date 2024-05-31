namespace kursovaya
{
    partial class AddEditTrophyForm
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
            this.competitionLabel = new System.Windows.Forms.Label();
            this.competitionComboBox = new System.Windows.Forms.ComboBox();
            this.athleteLabel = new System.Windows.Forms.Label();
            this.athleteComboBox = new System.Windows.Forms.ComboBox();
            this.trophyNameLabel = new System.Windows.Forms.Label();
            this.trophyNameTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // competitionLabel
            // 
            this.competitionLabel.AutoSize = true;
            this.competitionLabel.Location = new System.Drawing.Point(12, 15);
            this.competitionLabel.Name = "competitionLabel";
            this.competitionLabel.Size = new System.Drawing.Size(86, 14);
            this.competitionLabel.TabIndex = 0;
            this.competitionLabel.Text = "Соревнование";
            // 
            // competitionComboBox
            // 
            this.competitionComboBox.BackColor = System.Drawing.Color.Honeydew;
            this.competitionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.competitionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.competitionComboBox.FormattingEnabled = true;
            this.competitionComboBox.Location = new System.Drawing.Point(150, 12);
            this.competitionComboBox.Name = "competitionComboBox";
            this.competitionComboBox.Size = new System.Drawing.Size(200, 22);
            this.competitionComboBox.TabIndex = 1;
            // 
            // athleteLabel
            // 
            this.athleteLabel.AutoSize = true;
            this.athleteLabel.Location = new System.Drawing.Point(12, 42);
            this.athleteLabel.Name = "athleteLabel";
            this.athleteLabel.Size = new System.Drawing.Size(43, 14);
            this.athleteLabel.TabIndex = 2;
            this.athleteLabel.Text = "Атлет:";
            // 
            // athleteComboBox
            // 
            this.athleteComboBox.BackColor = System.Drawing.Color.Honeydew;
            this.athleteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.athleteComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.athleteComboBox.FormattingEnabled = true;
            this.athleteComboBox.Location = new System.Drawing.Point(150, 39);
            this.athleteComboBox.Name = "athleteComboBox";
            this.athleteComboBox.Size = new System.Drawing.Size(200, 22);
            this.athleteComboBox.TabIndex = 3;
            // 
            // trophyNameLabel
            // 
            this.trophyNameLabel.AutoSize = true;
            this.trophyNameLabel.Location = new System.Drawing.Point(12, 69);
            this.trophyNameLabel.Name = "trophyNameLabel";
            this.trophyNameLabel.Size = new System.Drawing.Size(103, 14);
            this.trophyNameLabel.TabIndex = 4;
            this.trophyNameLabel.Text = "Название трофея";
            // 
            // trophyNameTextBox
            // 
            this.trophyNameTextBox.Location = new System.Drawing.Point(150, 66);
            this.trophyNameTextBox.Name = "trophyNameTextBox";
            this.trophyNameTextBox.Size = new System.Drawing.Size(200, 22);
            this.trophyNameTextBox.TabIndex = 5;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(275, 92);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 92);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddEditTrophyForm
            // 
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(362, 127);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.trophyNameTextBox);
            this.Controls.Add(this.trophyNameLabel);
            this.Controls.Add(this.athleteComboBox);
            this.Controls.Add(this.athleteLabel);
            this.Controls.Add(this.competitionComboBox);
            this.Controls.Add(this.competitionLabel);
            this.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "AddEditTrophyForm";
            this.Text = "Добавить/Редактировать трофей";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}