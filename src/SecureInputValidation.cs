using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SafeVault
{
    public class SecureInputValidation
    {
        public bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            return Regex.IsMatch(username, @"^[a-zA-Z0-9_]{3,20}$");
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"
            );
        }

        public SqlCommand GetUserByUsername(string username, SqlConnection connection)
        {
            string query = @"
                SELECT UserId, Username, Email
                FROM Users
                WHERE Username = @Username";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", username);

            return command;
        }
    }
}