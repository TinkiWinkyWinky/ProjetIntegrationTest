namespace ProjetIntegrationTest.Models
{
    public class Lot
    {
        public int Id { get; set; }
        public string ArtistName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int YearOfCreation { get; set; }
        public double MinEstimate { get; set; }
        public double MaxEstimate { get; set; }
        public string Category { get; set; } = default!;
        public string Dimension { get; set; } = default!;
        public string Medium { get; set; } = default!;
        public DateTime FilingDate { get; set; }
        public ICollection<Auction> Auctions { get; set; } = default!;
        public ICollection<Image> Images { get; set; } = default!;
        public int? SellerId { get; set; }
        public Seller? Seller { get; set; } = default!;
        public ICollection<Bid> Bets { get; set; } = default!;

    }
}
