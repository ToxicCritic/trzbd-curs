using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;

namespace kursovaya
{
    public class AuthenticationService
    {
        private readonly string connectionString;

        public AuthenticationService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AuthenticateUser(string login, string password, out string status)
        {
            status = null;
            string passwordHash = HashPassword(password);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Status FROM Users WHERE Login = @Login AND PasswordHash = @PasswordHash";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            status = reader.GetString(0);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void AddUser(string login, string password, string status)
        {
            string passwordHash = HashPassword(password);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (Login, PasswordHash, Status) VALUES (@Login, @PasswordHash, @Status)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    command.Parameters.AddWithValue("@Status", status);
                    command.ExecuteNonQuery();
                }
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
