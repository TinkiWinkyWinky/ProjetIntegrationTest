namespace ProjetIntegrationTest.ViewModels
{
    public class AuctionManagementVM
    {
        public List<LotInfoVM> AssignedLots { get; set; } = new List<LotInfoVM>();
        public List<LotInfoVM> AvailableLots { get; set; } = new List<LotInfoVM>();
        public int SelectedLotId { get; set; }
        public int AuctionId { get; set; }
    }
}
