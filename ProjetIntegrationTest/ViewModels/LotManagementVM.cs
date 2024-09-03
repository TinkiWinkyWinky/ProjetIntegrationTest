namespace ProjetIntegrationTest.ViewModels
{
    public class LotManagementVM
    {
        public List<AuctionInfoVM> AssignedAuctions { get; set; } = new List<AuctionInfoVM>();
        public List<AuctionInfoVM> AvailableAuctions { get; set; } = new List<AuctionInfoVM>();
        public List<SellerInfoVM> AvailableSellers { get; set; } = new List<SellerInfoVM>();
        public int SelectedAuctionId { get; set; }
        public int LotId { get; set; }
        public int SelectedSellerId { get; set; }
    }
}
