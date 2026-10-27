using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Models
{
    public class Guest
    {
        public int GuestId { get; set; }

        public string GuestName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }
}