
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
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }

            if (booking.RoomId <= 0)
            {
                throw new ArgumentException(
                    "Room ID must be greater than zero.");
            }

            if (booking.GuestId <= 0)
            {
                throw new ArgumentException(
                    "Guest ID must be greater than zero.");
            }

            Room? room = roomRepository.GetRoomById(booking.RoomId);

            if (room == null)
            {
                throw new InvalidOperationException(
                    "Room does not exist.");
            }

            if (!guestRepository.GuestExists(booking.GuestId))
            {
                throw new InvalidOperationException(
                    "Guest does not exist.");
            }

            if (booking.CheckOutDate.Date <= booking.CheckInDate.Date)
            {
                throw new ArgumentException(
                    "Check-out date must be after check-in date.");
            }

            if (!Enum.IsDefined(
                typeof(BookingStatus), booking.BookingStatus))
            {
                throw new ArgumentException(
                    "Invalid booking status.");
            }

            if (booking.BookingStatus == BookingStatus.Confirmed &&
                !bookingRepository.IsRoomAvailable(
                    booking.RoomId,
                    booking.CheckInDate,
                    booking.CheckOutDate))
            {
                throw new InvalidOperationException(
                    "Room is already booked for the selected dates.");
            }

            // Calculate the number of nights
            int numberOfNights =
                (booking.CheckOutDate.Date -
                 booking.CheckInDate.Date).Days;

            // Calculate the total booking amount
            booking.TotalAmount =
                room.PricePerNight * numberOfNights;

            // Save the booking
            bookingRepository.AddBooking(booking);

            Console.WriteLine(
                $"Number of nights: {numberOfNights}");

            Console.WriteLine(
                $"Price per night: {room.PricePerNight:C}");

            Console.WriteLine(
                $"Total booking amount: {booking.TotalAmount:C}");
        }

        public List<Booking> GetAllBookings()
        {
            return bookingRepository.GetAllBookings();
        }
    }
}
