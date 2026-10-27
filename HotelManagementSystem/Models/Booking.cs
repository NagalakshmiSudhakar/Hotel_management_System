using System;
using System.Collections.Generic;
using System.Text;


namespace HotelManagementSystem.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int RoomId { get; set; }

        public int GuestId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public decimal TotalAmount { get; set; }

        public BookingStatus BookingStatus { get; set; }
    }

    public enum BookingStatus
    {
        Confirmed,
        Cancelled,
        Completed
    }
}

