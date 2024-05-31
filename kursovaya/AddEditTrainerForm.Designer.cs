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
            this.trainerFioLabel = new System.Windows.Forms.Label();
            this.trainerFioTextBox = new System.Windows.Forms.TextBox();
            this.departmentLabel = new System.Windows.Forms.Label();
            this.departmentComboBox = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trainerFioLabel
            // 
            this.trainerFioLabel.AutoSize = true;
            this.trainerFioLabel.Location = new System.Drawing.Point(12, 15);
            this.trainerFioLabel.Name = "trainerFioLabel";
            this.trainerFioLabel.Size = new System.Drawing.Size(33, 14);
            this.trainerFioLabel.TabIndex = 0;
            this.trainerFioLabel.Text = "ФИО";
            // 
            // trainerFioTextBox
            // 
            this.trainerFioTextBox.Location = new System.Drawing.Point(150, 12);
            this.trainerFioTextBox.Name = "trainerFioTextBox";
            this.trainerFioTextBox.Size = new System.Drawing.Size(200, 22);
            this.trainerFioTextBox.TabIndex = 1;
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
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(275, 65);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(15, 65);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddEditTrainerForm
            // 
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(362, 100);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.departmentComboBox);
            this.Controls.Add(this.departmentLabel);
            this.Controls.Add(this.trainerFioTextBox);
            this.Controls.Add(this.trainerFioLabel);
            this.Font = new System.Drawing.Font("Bahnschrift", 9F);
            this.Name = "AddEditTrainerForm";
            this.Text = "Добавить/Редактировать тренера";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}