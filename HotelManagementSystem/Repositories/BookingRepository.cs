using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.Data.SqlClient;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Repositories
{
    public class BookingRepository
    {
        private readonly DatabaseConnection database =
            new DatabaseConnection();

        public void AddBooking(Booking booking)
        {
            using SqlConnection connection = database.GetConnection();

            string query = @"
                INSERT INTO Bookings
                (RoomId, GuestId, CheckInDate, CheckOutDate,
                 TotalAmount, BookingStatus)
                VALUES
                (@RoomId, @GuestId, @CheckInDate, @CheckOutDate,
                 @TotalAmount, @BookingStatus)";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RoomId", booking.RoomId);
            command.Parameters.AddWithValue("@GuestId", booking.GuestId);
            command.Parameters.AddWithValue("@CheckInDate", booking.CheckInDate.Date);
            command.Parameters.AddWithValue("@CheckOutDate", booking.CheckOutDate.Date);
            command.Parameters.AddWithValue("@TotalAmount", booking.TotalAmount);
            command.Parameters.AddWithValue(
                "@BookingStatus", booking.BookingStatus.ToString());

            connection.Open();
            command.ExecuteNonQuery();

            Console.WriteLine("Booking added successfully!");
        }

        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();

            using SqlConnection connection = database.GetConnection();

            string query = "SELECT * FROM Bookings";

            using SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                bookings.Add(new Booking
                {
                    BookingId = Convert.ToInt32(reader["BookingId"]),
                    RoomId = Convert.ToInt32(reader["RoomId"]),
                    GuestId = Convert.ToInt32(reader["GuestId"]),
                    CheckInDate = Convert.ToDateTime(reader["CheckInDate"]),
                    CheckOutDate = Convert.ToDateTime(reader["CheckOutDate"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    BookingStatus = Enum.Parse<BookingStatus>(
                        reader["BookingStatus"].ToString()!)
                });
            }

            return bookings;
        }
    }
}

