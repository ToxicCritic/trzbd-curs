namespace kursovaya
{
    partial class AddEditStandardForm
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
            this.exerciseLabel = new System.Windows.Forms.Label();
            this.exerciseTextBox = new System.Windows.Forms.TextBox();
            this.resultForWomenLabel = new System.Windows.Forms.Label();
            this.resultForWomenTextBox = new System.Windows.Forms.TextBox();
            this.resultForMenLabel = new System.Windows.Forms.Label();
            this.resultForMenTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // exerciseLabel
            // 
            this.exerciseLabel.AutoSize = true;
            this.exerciseLabel.Location = new System.Drawing.Point(12, 15);
            this.exerciseLabel.Name = "exerciseLabel";
            this.exerciseLabel.Size = new System.Drawing.Size(85, 14);
            this.exerciseLabel.TabIndex = 0;
            this.exerciseLabel.Text = "Название упр.";
            // 
            // exerciseTextBox
            // 
            this.exerciseTextBox.Location = new System.Drawing.Point(150, 12);
            this.exerciseTextBox.Name = "exerciseTextBox";
            this.exerciseTextBox.Size = new System.Drawing.Size(200, 22);
            this.exerciseTextBox.TabIndex = 1;
            // 
            // resultForWomenLabel
            // 
            this.resultForWomenLabel.AutoSize = true;
            this.resultForWomenLabel.Location = new System.Drawing.Point(12, 41);
            this.resultForWomenLabel.Name = "resultForWomenLabel";
            this.resultForWomenLabel.Size = new System.Drawing.Size(135, 14);
            this.resultForWomenLabel.TabIndex = 2;
            this.resultForWomenLabel.Text = "Результат для женщин";
            // 
            // resultForWomenTextBox
            // 
            this.resultForWomenTextBox.Location = new System.Drawing.Point(150, 38);
            this.resultForWomenTextBox.Name = "resultForWomenTextBox";
            this.resultForWomenTextBox.Size = new System.Drawing.Size(200, 22);
            this.resultForWomenTextBox.TabIndex = 3;
            // 
            // resultForMenLabel
            // 
            this.resultForMenLabel.AutoSize = true;
            this.resultForMenLabel.Location = new System.Drawing.Point(12, 67);
            this.resultForMenLabel.Name = "resultForMenLabel";
            this.resultForMenLabel.Size = new System.Drawing.Size(133, 14);
            this.resultForMenLabel.TabIndex = 4;
            this.resultForMenLabel.Text = "Результат для мужчин";
            // 
            // resultForMenTextBox
            // 
            this.resultForMenTextBox.Location = new System.Drawing.Point(150, 64);
            this.resultForMenTextBox.Name = "resultForMenTextBox";
            this.resultForMenTextBox.Size = new System.Drawing.Size(200, 22);
            this.resultForMenTextBox.TabIndex = 5;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(275, 90);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(15, 90);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddEditStandardForm
            // 
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(362, 125);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.resultForMenTextBox);
            this.Controls.Add(this.resultForMenLabel);
            this.Controls.Add(this.resultForWomenTextBox);
            this.Controls.Add(this.resultForWomenLabel);
            this.Controls.Add(this.exerciseTextBox);
            this.Controls.Add(this.exerciseLabel);
            this.Font = new System.Drawing.Font("Bahnschrift", 9F);
            this.Name = "AddEditStandardForm";
            this.Text = "Добавить/Редактировать стандарт";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}