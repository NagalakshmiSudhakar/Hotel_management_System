
using Microsoft.Data.SqlClient;

namespace HotelManagementSystem.Repositories
{
    public class DatabaseConnection
    {
        private readonly string connectionString =
            @"Server=localhost;Database=HotelManagementSystemDB;Integrated Security=True;TrustServerCertificate=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
