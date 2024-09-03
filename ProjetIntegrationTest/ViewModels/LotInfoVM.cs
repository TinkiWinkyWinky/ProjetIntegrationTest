namespace ProjetIntegrationTest.ViewModels
{
    public class LotInfoVM
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
    }
}
