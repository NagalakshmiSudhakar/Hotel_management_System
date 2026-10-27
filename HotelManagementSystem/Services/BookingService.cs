
using System;
using System.Collections.Generic;
using HotelManagementSystem.Models;
using HotelManagementSystem.Repositories;

namespace HotelManagementSystem.Services
{
    public class BookingService
    {
        private readonly BookingRepository bookingRepository = new();
        private readonly RoomRepository roomRepository = new();
        private readonly GuestRepository guestRepository = new();

        public void AddBooking(Booking booking)
        {
            // 1. Check whether the booking object is valid
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }

            // 2. Validate Room ID
            if (booking.RoomId <= 0)
            {
                throw new ArgumentException(
                    "Room ID must be greater than zero.");
            }

            // 3. Validate Guest ID
            if (booking.GuestId <= 0)
            {
                throw new ArgumentException(
                    "Guest ID must be greater than zero.");
            }

            // 4. Check whether the room exists
            if (!roomRepository.RoomExists(booking.RoomId))
            {
                throw new InvalidOperationException(
                    "Room does not exist.");
            }

            // 5. Check whether the guest exists
            if (!guestRepository.GuestExists(booking.GuestId))
            {
                throw new InvalidOperationException(
                    "Guest does not exist.");
            }

            // 6. Validate booking dates
            if (booking.CheckOutDate.Date <= booking.CheckInDate.Date)
            {
                throw new ArgumentException(
                    "Check-out date must be after check-in date.");
            }

            // 7. Validate total amount
            if (booking.TotalAmount < 0)
            {
                throw new ArgumentException(
                    "Total amount cannot be negative.");
            }

            // 8. Validate booking status
            if (!Enum.IsDefined(
                typeof(BookingStatus), booking.BookingStatus))
            {
                throw new ArgumentException(
                    "Invalid booking status.");
            }

            // 9. Check room availability for the selected dates
            if (booking.BookingStatus == BookingStatus.Confirmed &&
                !bookingRepository.IsRoomAvailable(
                    booking.RoomId,
                    booking.CheckInDate,
                    booking.CheckOutDate))
            {
                throw new InvalidOperationException(
                    "Room is already booked for the selected dates.");
            }

            // 10. Save the booking
            bookingRepository.AddBooking(booking);
        }

        public List<Booking> GetAllBookings()
        {
            return bookingRepository.GetAllBookings();
        }
    }
}
