using System;
using System.Collections.Generic;
using System.Text;

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

            string query = @"INSERT INTO Guests
                (GuestName, PhoneNumber, Email, Address)
                VALUES
                (@GuestName, @PhoneNumber, @Email, @Address)";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@GuestName", guest.GuestName);
            command.Parameters.AddWithValue("@PhoneNumber", guest.PhoneNumber);
            command.Parameters.AddWithValue(
                "@Email", (object?)guest.Email ?? DBNull.Value);
            command.Parameters.AddWithValue(
                "@Address", (object?)guest.Address ?? DBNull.Value);

            connection.Open();
            command.ExecuteNonQuery();

            Console.WriteLine("Guest added successfully!");
        }

        public List<Guest> GetAllGuests()
        {
            List<Guest> guests = new List<Guest>();

            using SqlConnection connection = database.GetConnection();

            string query = "SELECT * FROM Guests";

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
    }
}