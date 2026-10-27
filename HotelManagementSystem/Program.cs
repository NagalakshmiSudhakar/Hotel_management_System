
using HotelManagementSystem.Models;
using HotelManagementSystem.Services;

GuestService service = new GuestService();

try
{
    Guest guest = new Guest
    {
        GuestName = "Priya Kumar",
        PhoneNumber = "9876543210",
        Email = "priya@example.com",
        Address = "Madurai"
    };

    service.AddGuest(guest);

    foreach (Guest item in service.GetAllGuests())
    {
        Console.WriteLine(
            $"{item.GuestId} | {item.GuestName} | " +
            $"{item.PhoneNumber} | {item.Email} | " +
            $"{item.Address}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
