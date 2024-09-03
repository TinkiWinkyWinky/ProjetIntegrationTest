namespace ProjetIntegrationTest.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;
        public int LotId { get; set; }
        public Lot Lot { get; set; } = default!;
    }
}
