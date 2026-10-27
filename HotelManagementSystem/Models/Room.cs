using System;
using System.Collections.Generic;
using System.Text;
namespace HotelManagementSystem.Models
    {
        public class Room
        {
            public int RoomId { get; set; }

            public string RoomNumber { get; set; } = string.Empty;

            public string RoomType { get; set; } = string.Empty;

            public decimal PricePerNight { get; set; }

            public RoomStatus RoomStatus { get; set; }
        }

        public enum RoomStatus
        {
            Available,
            Occupied,
            Maintenance
        }
}

