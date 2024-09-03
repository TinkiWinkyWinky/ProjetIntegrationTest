namespace ProjetIntegrationTest.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; } = default!;
        public int LotId { get; set; } = default!;
        public Lot Lot { get; set; } = default!;
    }
}
