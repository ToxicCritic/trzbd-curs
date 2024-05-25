using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursovaya
{
    static class Program
    {
        public static string connectionStringHome = "Server=192.168.0.101;Database='SportsSchool';" +
            "User Id='sa';Password='yourStrong(!)Password';TrustServerCertificate=True;";
        public static string connectionStringColledge = "Data Source=ADCLG1; Initial Catalog=Лифляндский_СпортШколаОлимпРезерва; " +
            "Integrated Security=True; Trusted_Connection=True; TrustServerCertificate=true;";
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
