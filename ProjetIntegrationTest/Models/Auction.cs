namespace ProjetIntegrationTest.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public int BiddingInterval { get; set; }
        public int ClosingInterval { get; set; }
        public int TimeIncrementPerBet { get; set; }
        public ICollection<Lot> Lots { get; set; } = default!;
    }
}
