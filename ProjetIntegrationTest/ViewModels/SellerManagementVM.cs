namespace ProjetIntegrationTest.ViewModels
{
    public class SellerManagementVM
    {
        public List<LotInfoVM> AssignedLots { get; set; } = new List<LotInfoVM>();
        public List<LotInfoVM> AvailableLots { get; set; } = new List<LotInfoVM>();
        public int SelectedLotId { get; set; }
        public int SellerId { get; set; }
    }
}
