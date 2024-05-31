namespace kursovaya
{
    partial class AddEditGroupForm
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
            this.trainerLabel = new System.Windows.Forms.Label();
            this.trainerComboBox = new System.Windows.Forms.ComboBox();
            this.departmentLabel = new System.Windows.Forms.Label();
            this.departmentComboBox = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trainerLabel
            // 
            this.trainerLabel.AutoSize = true;
            this.trainerLabel.Location = new System.Drawing.Point(12, 15);
            this.trainerLabel.Name = "trainerLabel";
            this.trainerLabel.Size = new System.Drawing.Size(46, 14);
            this.trainerLabel.TabIndex = 0;
            this.trainerLabel.Text = "Тренер";
            // 
            // trainerComboBox
            // 
            this.trainerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trainerComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.trainerComboBox.FormattingEnabled = true;
            this.trainerComboBox.Location = new System.Drawing.Point(150, 12);
            this.trainerComboBox.Name = "trainerComboBox";
            this.trainerComboBox.Size = new System.Drawing.Size(200, 22);
            this.trainerComboBox.TabIndex = 1;
            // 
            // departmentLabel
            // 
            this.departmentLabel.AutoSize = true;
            this.departmentLabel.Location = new System.Drawing.Point(12, 42);
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
            this.departmentComboBox.Location = new System.Drawing.Point(150, 39);
            this.departmentComboBox.Name = "departmentComboBox";
            this.departmentComboBox.Size = new System.Drawing.Size(200, 22);
            this.departmentComboBox.TabIndex = 3;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(275, 66);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 66);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddEditGroupForm
            // 
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(362, 101);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.departmentComboBox);
            this.Controls.Add(this.departmentLabel);
            this.Controls.Add(this.trainerComboBox);
            this.Controls.Add(this.trainerLabel);
            this.Font = new System.Drawing.Font("Bahnschrift", 9F);
            this.Name = "AddEditGroupForm";
            this.Text = "Добавить/Редактировать группу";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}