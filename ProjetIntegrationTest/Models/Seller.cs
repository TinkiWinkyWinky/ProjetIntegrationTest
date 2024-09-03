namespace ProjetIntegrationTest.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public ICollection<Lot> Lots { get; set; } = default!;
    }
}
