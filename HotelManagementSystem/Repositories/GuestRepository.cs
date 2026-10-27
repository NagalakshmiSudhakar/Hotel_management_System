
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Repositories
{
    public class GuestRepository
    {
        private readonly DatabaseConnection database =
            new DatabaseConnection();

        public void AddGuest(Guest guest)
        {
            using SqlConnection connection = database.GetConnection();

            string query = @"
                INSERT INTO Guests
                    (GuestName, PhoneNumber, Email, Address)
                VALUES
                    (@GuestName, @PhoneNumber, @Email, @Address)";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@GuestName", guest.GuestName);
            command.Parameters.AddWithValue("@PhoneNumber", guest.PhoneNumber);

            command.Parameters.AddWithValue(
                "@Email",
                string.IsNullOrWhiteSpace(guest.Email)
                    ? DBNull.Value
                    : guest.Email);

            command.Parameters.AddWithValue(
                "@Address",
                string.IsNullOrWhiteSpace(guest.Address)
                    ? DBNull.Value
                    : guest.Address);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<Guest> GetAllGuests()
        {
            List<Guest> guests = new List<Guest>();

            using SqlConnection connection = database.GetConnection();

            string query = @"
                SELECT GuestId, GuestName, PhoneNumber, Email, Address
                FROM Guests";

            using SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                guests.Add(new Guest
                {
                    GuestId = Convert.ToInt32(reader["GuestId"]),
                    GuestName = reader["GuestName"].ToString()!,
                    PhoneNumber = reader["PhoneNumber"].ToString()!,
                    Email = reader["Email"] == DBNull.Value
                        ? string.Empty
                        : reader["Email"].ToString()!,
                    Address = reader["Address"] == DBNull.Value
                        ? string.Empty
                        : reader["Address"].ToString()!
                });
            }

            return guests;
        }

        public bool GuestExists(int guestId)
        {
            using SqlConnection connection = database.GetConnection();

            string query = "SELECT COUNT(1) FROM Guests WHERE GuestId = @GuestId";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@GuestId", guestId);

            connection.Open();

            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }
    }
}
