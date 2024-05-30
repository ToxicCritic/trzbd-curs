namespace kursovaya
{
    partial class AddEditCompetitionParticipatingForm
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
            this.categoryLabel = new System.Windows.Forms.Label();
            this.categoryTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // competitionLabel
            // 
            this.competitionLabel.AutoSize = true;
            this.competitionLabel.Location = new System.Drawing.Point(12, 15);
            this.competitionLabel.Name = "competitionLabel";
            this.competitionLabel.Size = new System.Drawing.Size(80, 13);
            this.competitionLabel.TabIndex = 0;
            this.competitionLabel.Text = "Соревнование";
            // 
            // competitionComboBox
            // 
            this.competitionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.competitionComboBox.FormattingEnabled = true;
            this.competitionComboBox.Location = new System.Drawing.Point(116, 12);
            this.competitionComboBox.Name = "competitionComboBox";
            this.competitionComboBox.Size = new System.Drawing.Size(200, 21);
            this.competitionComboBox.TabIndex = 1;
            // 
            // athleteLabel
            // 
            this.athleteLabel.AutoSize = true;
            this.athleteLabel.Location = new System.Drawing.Point(12, 42);
            this.athleteLabel.Name = "athleteLabel";
            this.athleteLabel.Size = new System.Drawing.Size(36, 13);
            this.athleteLabel.TabIndex = 2;
            this.athleteLabel.Text = "Атлет";
            // 
            // athleteComboBox
            // 
            this.athleteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.athleteComboBox.FormattingEnabled = true;
            this.athleteComboBox.Location = new System.Drawing.Point(116, 39);
            this.athleteComboBox.Name = "athleteComboBox";
            this.athleteComboBox.Size = new System.Drawing.Size(200, 21);
            this.athleteComboBox.TabIndex = 3;
            // 
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Location = new System.Drawing.Point(12, 69);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(60, 13);
            this.categoryLabel.TabIndex = 4;
            this.categoryLabel.Text = "Категория";
            // 
            // categoryTextBox
            // 
            this.categoryTextBox.Location = new System.Drawing.Point(116, 66);
            this.categoryTextBox.Name = "categoryTextBox";
            this.categoryTextBox.Size = new System.Drawing.Size(200, 20);
            this.categoryTextBox.TabIndex = 5;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(241, 100);
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
            // AddEditCompetitionParticipatingForm
            // 
            this.ClientSize = new System.Drawing.Size(334, 141);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.categoryTextBox);
            this.Controls.Add(this.categoryLabel);
            this.Controls.Add(this.athleteComboBox);
            this.Controls.Add(this.athleteLabel);
            this.Controls.Add(this.competitionComboBox);
            this.Controls.Add(this.competitionLabel);
            this.Name = "AddEditCompetitionParticipatingForm";
            this.Text = "Добавить/Редактировать участие в соревновании";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}