using System;
using System.Collections.Generic;
using System.Text;


using HotelManagementSystem.Models;
using HotelManagementSystem.Repositories;

namespace HotelManagementSystem.Services
{
    public class GuestService
    {
        private readonly GuestRepository repository =
            new GuestRepository();

        public void AddGuest(Guest guest)
        {
            if (string.IsNullOrWhiteSpace(guest.GuestName))
            {
                throw new ArgumentException(
                    "Guest name is required.");
            }

            if (string.IsNullOrWhiteSpace(guest.PhoneNumber))
            {
                throw new ArgumentException(
                    "Phone number is required.");
            }

            repository.AddGuest(guest);
        }

        public List<Guest> GetAllGuests()
        {
            return repository.GetAllGuests();
        }
    }
}
