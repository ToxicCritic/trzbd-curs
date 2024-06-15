using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursovaya
{
    static class Program
    {
        public static string connectionStringHome = "Data Source=ADCLG1; Initial Catalog=Лифляндский_СпортШколаОлимпРезерва; " +
            "Integrated Security=True; Trusted_Connection=True; TrustServerCertificate=true;";
        public static string connectionStringColledge = "Server=localhost\\SQLEXPRESS01;Database='SHOR';Trusted_Connection=True;";
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
