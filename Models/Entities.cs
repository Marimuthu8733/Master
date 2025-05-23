namespace Tour_Management_.Net_8.Models
{
    public class UserInfo
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Password { get; set; }
        public DateTime Dob { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }

    public class Tour
    {
        public int TourId { get; set; }
        public string? TourName { get; set; }
        public string? Place { get; set; }
        public int Days { get; set; }
        public decimal Price { get; set; }
        public string? Locations { get; set; }
        public string? TourInfo { get; set; }
        public string? Pic { get; set; }
    }

    public class Booking
    {
        public int BookingId { get; set; } // New primary key
        public int TourId { get; set; }
        public string? TourName { get; set; }
        public string? Place { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
    }
}
