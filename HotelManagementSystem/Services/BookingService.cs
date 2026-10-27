
using System;
using System.Collections.Generic;
using HotelManagementSystem.Models;
using HotelManagementSystem.Repositories;

namespace HotelManagementSystem.Services
{
    public class BookingService
    {
        private readonly BookingRepository bookingRepository =
            new BookingRepository();

        private readonly RoomRepository roomRepository =
            new RoomRepository();

        private readonly GuestRepository guestRepository =
            new GuestRepository();

        public void AddBooking(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            if (booking.RoomId <= 0)
                throw new ArgumentException("Invalid room ID.");

            if (booking.GuestId <= 0)
                throw new ArgumentException("Invalid guest ID.");

            if (!roomRepository.RoomExists(booking.RoomId))
                throw new ArgumentException("Room does not exist.");

            if (!guestRepository.GuestExists(booking.GuestId))
                throw new ArgumentException("Guest does not exist.");

            if (booking.CheckOutDate.Date <= booking.CheckInDate.Date)
                throw new ArgumentException(
                    "Check-out must be after check-in.");

            if (booking.TotalAmount < 0)
                throw new ArgumentException(
                    "Booking amount cannot be negative.");

            if (!Enum.IsDefined(typeof(BookingStatus), booking.BookingStatus))
                throw new ArgumentException("Invalid booking status.");

            bookingRepository.AddBooking(booking);
        }

        public List<Booking> GetAllBookings()
        {
            return bookingRepository.GetAllBookings();
        }
    }
}
