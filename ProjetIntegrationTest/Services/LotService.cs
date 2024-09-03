using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetIntegrationTest.Data;
using ProjetIntegrationTest.Models;
using ProjetIntegrationTest.ViewModels;

namespace ProjetIntegrationTest.Services
{
    public class LotService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public List<LotInfoVM> GetAllLots()
        {
            return _context.Lots
                .Select(l => new LotInfoVM
                {
                    Id = l.Id,
                    ArtistName = l.ArtistName,
                    Description = l.Description,
                    YearOfCreation = l.YearOfCreation,
                    MinEstimate = l.MinEstimate,
                    MaxEstimate = l.MaxEstimate,
                    Category = l.Category,
                    Dimension = l.Dimension,
                    Medium = l.Medium,
                    FilingDate = l.FilingDate
                }).ToList();
        }

        public Lot GetLotById(int id)
        {
            return _context.Lots.Include(l => l.Auctions).FirstOrDefault(l => l.Id == id);
        }

        public void CreateLot(Lot lot)
        {
            _context.Lots.Add(lot);
            _context.SaveChanges();
        }

        public void AssignLotToAuction(int lotId, int auctionId)
        {
            var lot = _context.Lots.Include(l => l.Auctions).FirstOrDefault(l => l.Id == lotId);
            var auction = _context.Auctions.Find(auctionId);

            if (lot != null && auction != null && !lot.Auctions.Contains(auction))
            {
                lot.Auctions.Add(auction);
                _context.SaveChanges();
            }
        }

        public void RemoveLotFromAuction(int lotId, int auctionId)
        {
            var lot = _context.Lots.Include(l => l.Auctions).FirstOrDefault(l => l.Id == lotId);
            var auction = _context.Auctions.Find(auctionId);

            if (lot != null && auction != null && lot.Auctions.Contains(auction))
            {
                lot.Auctions.Remove(auction);
                _context.SaveChanges();
            }
        }

        public void AssignSellerToLot(int lotId, int sellerId)
        {
            var lot = _context.Lots.Find(lotId);
            var seller = _context.Sellers.Find(sellerId);

            if (lot != null && seller != null)
            {
                lot.SellerId = sellerId;
                _context.SaveChanges();
            }
        }

        public void RemoveSellerFromLot(int lotId)
        {
            var lot = _context.Lots.Find(lotId);
            if (lot != null)
            {
                lot.SellerId = null;
                _context.SaveChanges();
            }
        }

        public void UpdateLot(Lot lot)
        {
            _context.Lots.Update(lot);
            _context.SaveChanges();
        }

        public void DeleteLot(int id)
        {
            var lot = _context.Lots.Find(id);
            if (lot != null)
            {
                _context.Lots.Remove(lot);
                _context.SaveChanges();
            }
        }
    }
}
