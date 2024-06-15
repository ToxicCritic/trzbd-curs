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
            this.fioLabel = new System.Windows.Forms.Label();
            this.fioTextBox = new System.Windows.Forms.TextBox();
            this.departmentLabel = new System.Windows.Forms.Label();
            this.departmentComboBox = new System.Windows.Forms.ComboBox();
            this.groupLabel = new System.Windows.Forms.Label();
            this.groupComboBox = new System.Windows.Forms.ComboBox();
            this.trainerLabel = new System.Windows.Forms.Label();
            this.trainerComboBox = new System.Windows.Forms.ComboBox();
            this.rankingLabel = new System.Windows.Forms.Label();
            this.rankingTextBox = new System.Windows.Forms.TextBox();
            this.heightsLabel = new System.Windows.Forms.Label();
            this.heightsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.weightsLabel = new System.Windows.Forms.Label();
            this.weightsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.educationBeginLabel = new System.Windows.Forms.Label();
            this.educationBeginPicker = new System.Windows.Forms.DateTimePicker();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.heightsNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightsNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // fioLabel
            // 
            this.fioLabel.AutoSize = true;
            this.fioLabel.Location = new System.Drawing.Point(12, 15);
            this.fioLabel.Name = "fioLabel";
            this.fioLabel.Size = new System.Drawing.Size(33, 14);
            this.fioLabel.TabIndex = 0;
            this.fioLabel.Text = "ФИО";
            // 
            // fioTextBox
            // 
            this.fioTextBox.Location = new System.Drawing.Point(120, 12);
            this.fioTextBox.Name = "fioTextBox";
            this.fioTextBox.Size = new System.Drawing.Size(200, 22);
            this.fioTextBox.TabIndex = 1;
            // 
            // departmentLabel
            // 
            this.departmentLabel.AutoSize = true;
            this.departmentLabel.Location = new System.Drawing.Point(12, 42);
            this.departmentLabel.Name = "departmentLabel";
            this.departmentLabel.Size = new System.Drawing.Size(68, 14);
            this.departmentLabel.TabIndex = 2;
            this.departmentLabel.Text = "Отделение";
            // 
            // departmentComboBox
            // 
            this.departmentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.departmentComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.departmentComboBox.Location = new System.Drawing.Point(120, 39);
            this.departmentComboBox.Name = "departmentComboBox";
            this.departmentComboBox.Size = new System.Drawing.Size(200, 22);
            this.departmentComboBox.TabIndex = 3;
            // 
            // groupLabel
            // 
            this.groupLabel.AutoSize = true;
            this.groupLabel.Location = new System.Drawing.Point(12, 69);
            this.groupLabel.Name = "groupLabel";
            this.groupLabel.Size = new System.Drawing.Size(46, 14);
            this.groupLabel.TabIndex = 4;
            this.groupLabel.Text = "Группа";
            // 
            // groupComboBox
            // 
            this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.groupComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupComboBox.Location = new System.Drawing.Point(120, 66);
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Size = new System.Drawing.Size(200, 22);
            this.groupComboBox.TabIndex = 5;
            // 
            // trainerLabel
            // 
            this.trainerLabel.AutoSize = true;
            this.trainerLabel.Location = new System.Drawing.Point(12, 96);
            this.trainerLabel.Name = "trainerLabel";
            this.trainerLabel.Size = new System.Drawing.Size(46, 14);
            this.trainerLabel.TabIndex = 6;
            this.trainerLabel.Text = "Тренер";
            // 
            // trainerComboBox
            // 
            this.trainerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trainerComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.trainerComboBox.Location = new System.Drawing.Point(120, 93);
            this.trainerComboBox.Name = "trainerComboBox";
            this.trainerComboBox.Size = new System.Drawing.Size(200, 22);
            this.trainerComboBox.TabIndex = 7;
            // 
            // rankingLabel
            // 
            this.rankingLabel.AutoSize = true;
            this.rankingLabel.Location = new System.Drawing.Point(12, 123);
            this.rankingLabel.Name = "rankingLabel";
            this.rankingLabel.Size = new System.Drawing.Size(51, 14);
            this.rankingLabel.TabIndex = 8;
            this.rankingLabel.Text = "Рейтинг";
            // 
            // rankingTextBox
            // 
            this.rankingTextBox.Location = new System.Drawing.Point(120, 120);
            this.rankingTextBox.Name = "rankingTextBox";
            this.rankingTextBox.Size = new System.Drawing.Size(200, 22);
            this.rankingTextBox.TabIndex = 9;
            // 
            // heightsLabel
            // 
            this.heightsLabel.AutoSize = true;
            this.heightsLabel.Location = new System.Drawing.Point(12, 149);
            this.heightsLabel.Name = "heightsLabel";
            this.heightsLabel.Size = new System.Drawing.Size(32, 14);
            this.heightsLabel.TabIndex = 10;
            this.heightsLabel.Text = "Рост";
            // 
            // heightsNumericUpDown
            // 
            this.heightsNumericUpDown.Location = new System.Drawing.Point(120, 147);
            this.heightsNumericUpDown.Maximum = new decimal(new int[] {
            220,
            0,
            0,
            0});
            this.heightsNumericUpDown.Name = "heightsNumericUpDown";
            this.heightsNumericUpDown.Size = new System.Drawing.Size(200, 22);
            this.heightsNumericUpDown.TabIndex = 11;
            // 
            // weightsLabel
            // 
            this.weightsLabel.AutoSize = true;
            this.weightsLabel.Location = new System.Drawing.Point(12, 175);
            this.weightsLabel.Name = "weightsLabel";
            this.weightsLabel.Size = new System.Drawing.Size(27, 14);
            this.weightsLabel.TabIndex = 12;
            this.weightsLabel.Text = "Вес";
            // 
            // weightsNumericUpDown
            // 
            this.weightsNumericUpDown.Location = new System.Drawing.Point(120, 173);
            this.weightsNumericUpDown.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.weightsNumericUpDown.Name = "weightsNumericUpDown";
            this.weightsNumericUpDown.Size = new System.Drawing.Size(200, 22);
            this.weightsNumericUpDown.TabIndex = 13;
            // 
            // educationBeginLabel
            // 
            this.educationBeginLabel.AutoSize = true;
            this.educationBeginLabel.Location = new System.Drawing.Point(12, 201);
            this.educationBeginLabel.Name = "educationBeginLabel";
            this.educationBeginLabel.Size = new System.Drawing.Size(104, 14);
            this.educationBeginLabel.TabIndex = 14;
            this.educationBeginLabel.Text = "Начало обучения";
            // 
            // educationBeginPicker
            // 
            this.educationBeginPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.educationBeginPicker.Location = new System.Drawing.Point(120, 199);
            this.educationBeginPicker.Name = "educationBeginPicker";
            this.educationBeginPicker.Size = new System.Drawing.Size(200, 22);
            this.educationBeginPicker.TabIndex = 15;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(245, 225);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 16;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 225);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 17;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddEditAthleteForm
            // 
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(334, 261);
            this.Controls.Add(this.fioLabel);
            this.Controls.Add(this.fioTextBox);
            this.Controls.Add(this.departmentLabel);
            this.Controls.Add(this.departmentComboBox);
            this.Controls.Add(this.groupLabel);
            this.Controls.Add(this.groupComboBox);
            this.Controls.Add(this.trainerLabel);
            this.Controls.Add(this.trainerComboBox);
            this.Controls.Add(this.rankingLabel);
            this.Controls.Add(this.rankingTextBox);
            this.Controls.Add(this.heightsLabel);
            this.Controls.Add(this.heightsNumericUpDown);
            this.Controls.Add(this.weightsLabel);
            this.Controls.Add(this.weightsNumericUpDown);
            this.Controls.Add(this.educationBeginLabel);
            this.Controls.Add(this.educationBeginPicker);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AddEditAthleteForm";
            this.Text = "Add/Edit Athlete";
            ((System.ComponentModel.ISupportInitialize)(this.heightsNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightsNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}