using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.Data.SqlClient;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Repositories
{
    public class RoomRepository
    {
        private readonly DatabaseConnection database =
            new DatabaseConnection();

        public void AddRoom(Room room)
        {
            using SqlConnection connection = database.GetConnection();

            string query = @"INSERT INTO Rooms
                (RoomNumber, RoomType, PricePerNight, RoomStatus)
                VALUES
                (@RoomNumber, @RoomType, @PricePerNight, @RoomStatus)";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
            command.Parameters.AddWithValue("@RoomType", room.RoomType);
            command.Parameters.AddWithValue("@PricePerNight", room.PricePerNight);
            command.Parameters.AddWithValue("@RoomStatus", room.RoomStatus.ToString());

            connection.Open();
            command.ExecuteNonQuery();

            Console.WriteLine("Room added successfully!");
        }

        public List<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();

            using SqlConnection connection = database.GetConnection();

            string query = "SELECT * FROM Rooms";

            using SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                rooms.Add(new Room
                {
                    RoomId = Convert.ToInt32(reader["RoomId"]),
                    RoomNumber = reader["RoomNumber"].ToString()!,
                    RoomType = reader["RoomType"].ToString()!,
                    PricePerNight = Convert.ToDecimal(reader["PricePerNight"]),
                    RoomStatus = Enum.Parse<RoomStatus>(
                        reader["RoomStatus"].ToString()!)
                });
            }

            return rooms;
        }
    }
}
