namespace ProjetIntegrationTest.Models
{
    public class Customer : ApplicationUser
    {
        public int AddressId { get; set; }
        public Address Address { get; set; } = default!;
        public string ProfilePicture { get; set; } = default!;
        public ICollection<Bid> Bets { get; set; } =default!;
    }
}
