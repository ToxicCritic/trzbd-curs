using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace kursovaya
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.welcomeLabel = new Label();
            this.SuspendLayout();

            // 
            // welcomeLabel
            // 
            this.welcomeLabel.AutoSize = true;
            this.welcomeLabel.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.welcomeLabel.Location = new Point(10, 570);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new Size(250, 19);
            this.welcomeLabel.TabIndex = 0;
            this.welcomeLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            // 
            // MainForm
            // 
            this.ClientSize = new Size(800, 600);
            this.Controls.Add(this.welcomeLabel);
            this.Name = "MainForm";
            this.Text = "Главная форма";
            this.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

