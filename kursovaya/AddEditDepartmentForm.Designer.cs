namespace kursovaya
{
    partial class AddEditDepartmentForm
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
            this.departmentNameLabel = new System.Windows.Forms.Label();
            this.departmentNameTextBox = new System.Windows.Forms.TextBox();
            this.buildingLabel = new System.Windows.Forms.Label();
            this.buildingTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // departmentNameLabel
            // 
            this.departmentNameLabel.AutoSize = true;
            this.departmentNameLabel.Location = new System.Drawing.Point(12, 15);
            this.departmentNameLabel.Name = "departmentNameLabel";
            this.departmentNameLabel.Size = new System.Drawing.Size(102, 14);
            this.departmentNameLabel.TabIndex = 0;
            this.departmentNameLabel.Text = "Название отдела";
            // 
            // departmentNameTextBox
            // 
            this.departmentNameTextBox.Location = new System.Drawing.Point(150, 12);
            this.departmentNameTextBox.Name = "departmentNameTextBox";
            this.departmentNameTextBox.Size = new System.Drawing.Size(200, 22);
            this.departmentNameTextBox.TabIndex = 1;
            // 
            // buildingLabel
            // 
            this.buildingLabel.AutoSize = true;
            this.buildingLabel.Location = new System.Drawing.Point(12, 41);
            this.buildingLabel.Name = "buildingLabel";
            this.buildingLabel.Size = new System.Drawing.Size(48, 14);
            this.buildingLabel.TabIndex = 2;
            this.buildingLabel.Text = "Здание";
            // 
            // buildingTextBox
            // 
            this.buildingTextBox.Location = new System.Drawing.Point(150, 38);
            this.buildingTextBox.Name = "buildingTextBox";
            this.buildingTextBox.Size = new System.Drawing.Size(200, 22);
            this.buildingTextBox.TabIndex = 3;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(275, 64);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 64);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddEditDepartmentForm
            // 
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(362, 99);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.buildingTextBox);
            this.Controls.Add(this.buildingLabel);
            this.Controls.Add(this.departmentNameTextBox);
            this.Controls.Add(this.departmentNameLabel);
            this.Font = new System.Drawing.Font("Bahnschrift", 9F);
            this.Name = "AddEditDepartmentForm";
            this.Text = "Добавить/Редактировать отдел";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}