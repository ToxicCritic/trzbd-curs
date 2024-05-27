using System;
using System.Windows.Forms;

namespace kursovaya
{
    partial class AddEditAthleteForm
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
            this.fioTextBox = new TextBox();
            this.departmentComboBox = new ComboBox();
            this.groupComboBox = new ComboBox();
            this.trainerComboBox = new ComboBox();
            this.rankingTextBox = new TextBox();
            this.heightsNumericUpDown = new NumericUpDown();
            this.weightsNumericUpDown = new NumericUpDown();
            this.educationBeginPicker = new DateTimePicker();
            this.saveButton = new Button();
            this.cancelButton = new Button();

            this.SuspendLayout();

            // fioTextBox
            this.fioTextBox.Location = new System.Drawing.Point(12, 12);
            this.fioTextBox.Name = "fioTextBox";
            this.fioTextBox.Size = new System.Drawing.Size(200, 20);
            this.fioTextBox.TabIndex = 0;

            // departmentComboBox
            this.departmentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.departmentComboBox.Location = new System.Drawing.Point(12, 38);
            this.departmentComboBox.Name = "departmentComboBox";
            this.departmentComboBox.Size = new System.Drawing.Size(200, 21);
            this.departmentComboBox.TabIndex = 1;

            // groupComboBox
            this.groupComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.groupComboBox.Location = new System.Drawing.Point(12, 65);
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Size = new System.Drawing.Size(200, 21);
            this.groupComboBox.TabIndex = 2;

            // trainerComboBox
            this.trainerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.trainerComboBox.Location = new System.Drawing.Point(12, 92);
            this.trainerComboBox.Name = "trainerComboBox";
            this.trainerComboBox.Size = new System.Drawing.Size(200, 21);
            this.trainerComboBox.TabIndex = 3;

            // rankingTextBox
            this.rankingTextBox.Location = new System.Drawing.Point(12, 119);
            this.rankingTextBox.Name = "rankingTextBox";
            this.rankingTextBox.Size = new System.Drawing.Size(200, 20);
            this.rankingTextBox.TabIndex = 4;

            // heightsNumericUpDown
            this.heightsNumericUpDown.Location = new System.Drawing.Point(12, 145);
            this.heightsNumericUpDown.Name = "heightsNumericUpDown";
            this.heightsNumericUpDown.Size = new System.Drawing.Size(200, 20);
            this.heightsNumericUpDown.TabIndex = 5;

            // weightsNumericUpDown
            this.weightsNumericUpDown.Location = new System.Drawing.Point(12, 171);
            this.weightsNumericUpDown.Name = "weightsNumericUpDown";
            this.weightsNumericUpDown.Size = new System.Drawing.Size(200, 20);
            this.weightsNumericUpDown.TabIndex = 6;

            // educationBeginPicker
            this.educationBeginPicker.Format = DateTimePickerFormat.Short;
            this.educationBeginPicker.Location = new System.Drawing.Point(12, 197);
            this.educationBeginPicker.Name = "educationBeginPicker";
            this.educationBeginPicker.Size = new System.Drawing.Size(200, 20);
            this.educationBeginPicker.TabIndex = 7;

            // saveButton
            this.saveButton.Location = new System.Drawing.Point(12, 223);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new EventHandler(this.SaveButton_Click);

            // cancelButton
            this.cancelButton.Location = new System.Drawing.Point(137, 223);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new EventHandler(this.CancelButton_Click);

            // AddEditAthleteForm
            this.ClientSize = new System.Drawing.Size(224, 258);
            this.Controls.Add(this.fioTextBox);
            this.Controls.Add(this.departmentComboBox);
            this.Controls.Add(this.groupComboBox);
            this.Controls.Add(this.trainerComboBox);
            this.Controls.Add(this.rankingTextBox);
            this.Controls.Add(this.heightsNumericUpDown);
            this.Controls.Add(this.weightsNumericUpDown);
            this.Controls.Add(this.educationBeginPicker);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Name = "AddEditAthleteForm";
            this.Text = "Add/Edit Athlete";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}