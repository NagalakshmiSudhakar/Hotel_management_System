using System;
using System.Collections.Generic;
using System.Text;


using HotelManagementSystem.Models;
using HotelManagementSystem.Repositories;

namespace HotelManagementSystem.Services
{
    public class RoomService
    {
        private readonly RoomRepository repository =
            new RoomRepository();

        public void AddRoom(Room room)
        {
            if (string.IsNullOrWhiteSpace(room.RoomNumber))
            {
                throw new ArgumentException("Room number is required.");
            }

            if (string.IsNullOrWhiteSpace(room.RoomType))
            {
                throw new ArgumentException("Room type is required.");
            }

            if (room.PricePerNight < 0)
            {
                throw new ArgumentException(
                    "Room price cannot be negative.");
            }

            repository.AddRoom(room);
        }

        public List<Room> GetAllRooms()
        {
            return repository.GetAllRooms();
        }
    }
}
