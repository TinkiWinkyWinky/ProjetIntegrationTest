using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProjetIntegrationTest.Data;
using ProjetIntegrationTest.Models;
using ProjetIntegrationTest.ViewModels;

namespace ProjetIntegrationTest.Services
{
    public class AuctionService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public List<AuctionInfoVM> GetAllAuctions()
        {
            return _context.Auctions
                .Select(a => new AuctionInfoVM
                {
                    Id = a.Id,
                    OpeningDate = a.OpeningDate,
                    ClosingDate = a.ClosingDate,
                    BiddingInterval = a.BiddingInterval,
                    ClosingInterval = a.ClosingInterval,
                    TimeIncrementPerBet = a.TimeIncrementPerBet
                }).ToList();
        }

        public Auction GetAuctionById(int id)
        {
            return _context.Auctions.Include(a => a.Lots).FirstOrDefault(a => a.Id == id);
        }

        public void CreateAuction(Auction auction)
        {
            _context.Auctions.Add(auction);
            _context.SaveChanges();
        }

        public void UpdateAuction(Auction auction)
        {
            _context.Auctions.Update(auction);
            _context.SaveChanges();
        }

        public void DeleteAuction(int id)
        {
            var auction = _context.Auctions.Find(id);
            if (auction != null)
            {
                _context.Auctions.Remove(auction);
                _context.SaveChanges();
            }
        }

        public void AssignLotToAuction(int auctionId, int lotId)
        {
            var auction = _context.Auctions.Include(a => a.Lots).FirstOrDefault(a => a.Id == auctionId);
            var lot = _context.Lots.Find(lotId);

            if (auction != null && lot != null && !auction.Lots.Contains(lot))
            {
                auction.Lots.Add(lot);
                _context.SaveChanges();
            }
        }

        public void RemoveLotFromAuction(int auctionId, int lotId)
        {
            var auction = _context.Auctions.Include(a => a.Lots).FirstOrDefault(a => a.Id == auctionId);
            var lot = auction?.Lots.FirstOrDefault(l => l.Id == lotId);

            if (auction != null && lot != null)
            {
                auction.Lots.Remove(lot);
                _context.SaveChanges();
            }
        }
    }
}
