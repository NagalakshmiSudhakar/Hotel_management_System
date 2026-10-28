
using HotelManagementSystem.Models;
using HotelManagementSystem.Services;

RoomService roomService = new RoomService();
GuestService guestService = new GuestService();
BookingService bookingService = new BookingService();

bool running = true;

while (running)
{
    Console.WriteLine("\n===== HOTEL MANAGEMENT SYSTEM =====");
    Console.WriteLine("1. Add Room");
    Console.WriteLine("2. View All Rooms");
    Console.WriteLine("3. Add Guest");
    Console.WriteLine("4. View All Guests");
    Console.WriteLine("5. Add Booking");
    Console.WriteLine("6. View All Bookings");
    Console.WriteLine("7. Cancel Booking");
    Console.WriteLine("0. Exit");
    Console.Write("Enter your choice: ");

    string? choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                Console.Write("Enter Room Number: ");
                string roomNumber = Console.ReadLine() ?? "";

                Console.Write("Enter Room Type: ");
                string roomType = Console.ReadLine() ?? "";

                Console.Write("Enter Price Per Night: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.WriteLine("Invalid price.");
                    break;
                }

                Room room = new Room
                {
                    RoomNumber = roomNumber,
                    RoomType = roomType,
                    PricePerNight = price,
                    RoomStatus = RoomStatus.Available
                };

                roomService.AddRoom(room);
                Console.WriteLine("Room added successfully!");
                break;

            case "2":
                Console.WriteLine("\n===== ALL ROOMS =====");

                foreach (Room item in roomService.GetAllRooms())
                {
                    Console.WriteLine(
                        $"{item.RoomId} | {item.RoomNumber} | " +
                        $"{item.RoomType} | {item.PricePerNight} | " +
                        $"{item.RoomStatus}");
                }
                break;

            case "3":
                Console.Write("Enter Guest Name: ");
                string guestName = Console.ReadLine() ?? "";

                Console.Write("Enter Phone Number: ");
                string phoneNumber = Console.ReadLine() ?? "";

                Console.Write("Enter Email: ");
                string email = Console.ReadLine() ?? "";

                Console.Write("Enter Address: ");
                string address = Console.ReadLine() ?? "";

                Guest guest = new Guest
                {
                    GuestName = guestName,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    Address = address
                };

                guestService.AddGuest(guest);
                Console.WriteLine("Guest added successfully!");
                break;

            case "4":
                Console.WriteLine("\n===== ALL GUESTS =====");

                foreach (Guest item in guestService.GetAllGuests())
                {
                    Console.WriteLine(
                        $"{item.GuestId} | {item.GuestName} | " +
                        $"{item.PhoneNumber} | {item.Email} | " +
                        $"{item.Address}");
                }
                break;

            case "5":
                Console.Write("Enter Room ID: ");
                if (!int.TryParse(Console.ReadLine(), out int roomId))
                {
                    Console.WriteLine("Invalid Room ID.");
                    break;
                }

                Console.Write("Enter Guest ID: ");
                if (!int.TryParse(Console.ReadLine(), out int guestId))
                {
                    Console.WriteLine("Invalid Guest ID.");
                    break;
                }

                Console.Write("Enter Check-in Date (yyyy-MM-dd): ");
                if (!DateTime.TryParse(
                    Console.ReadLine(), out DateTime checkIn))
                {
                    Console.WriteLine("Invalid check-in date.");
                    break;
                }

                Console.Write("Enter Check-out Date (yyyy-MM-dd): ");
                if (!DateTime.TryParse(
                    Console.ReadLine(), out DateTime checkOut))
                {
                    Console.WriteLine("Invalid check-out date.");
                    break;
                }
                Booking booking = new Booking
                {
                    RoomId = roomId,
                    GuestId = guestId,
                    CheckInDate = checkIn,
                    CheckOutDate = checkOut,
                    BookingStatus = BookingStatus.Confirmed
                };

                bookingService.AddBooking(booking);
                Console.WriteLine("Booking added successfully!");
                break;

            case "6":
                Console.WriteLine("\n===== ALL BOOKINGS =====");

                foreach (Booking item in bookingService.GetAllBookings())
                {
                    Console.WriteLine(
                        $"{item.BookingId} | Room: {item.RoomId} | " +
                        $"Guest: {item.GuestId} | " +
                        $"Check-in: {item.CheckInDate:yyyy-MM-dd} | " +
                        $"Check-out: {item.CheckOutDate:yyyy-MM-dd} | " +
                        $"Amount: {item.TotalAmount} | " +
                        $"Status: {item.BookingStatus}");
                }
                break;

            case "7":
                Console.Write("Enter Booking ID to cancel: ");

                if (!int.TryParse(Console.ReadLine(), out int bookingId))
                {
                    Console.WriteLine("Invalid Booking ID.");
                    break;
                }

                bookingService.CancelBooking(bookingId);
                break;

            case "0":
                running = false;
                Console.WriteLine("Thank you for using the system!");
                break;

            default:
                Console.WriteLine("Invalid choice. Please select 0-6.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
