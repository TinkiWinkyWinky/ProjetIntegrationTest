using Microsoft.AspNetCore.Identity;
using ProjetIntegrationTest.Data;
using ProjetIntegrationTest.Models;
using ProjetIntegrationTest.ViewModels;

namespace ProjetIntegrationTest.Services
{
    public class SellerService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public List<SellerInfoVM> GetAllSellers()
        {
            return _context.Sellers
                .Select(s => new SellerInfoVM
                {
                    Id = s.Id,
                    LastName = s.LastName,
                    FirstName = s.FirstName,
                    PhoneNumber = s.PhoneNumber,
                    Email = s.Email
                }).ToList();
        }

        public Seller GetSellerById(int id)
        {
            return _context.Sellers.Find(id);
        }

        public void CreateSeller(Seller seller)
        {
            _context.Sellers.Add(seller);
            _context.SaveChanges();
        }

        public void AssignLotToSeller(int sellerId, int lotId)
        {
            var seller = _context.Sellers.Find(sellerId);
            var lot = _context.Lots.Find(lotId);

            if (seller != null && lot != null && lot.SellerId == null)
            {
                lot.SellerId = sellerId;
                _context.Lots.Update(lot);
                _context.SaveChanges();
            }
        }

        public void RemoveLotFromSeller(int sellerId, int lotId)
        {
            var lot = _context.Lots.Find(lotId);
            if (lot != null && lot.SellerId == sellerId)
            {
                lot.SellerId = null;
                _context.Lots.Update(lot);
                _context.SaveChanges();
            }
        }

        public void UpdateSeller(Seller seller)
        {
            _context.Sellers.Update(seller);
            _context.SaveChanges();
        }

        public void DeleteSeller(int id)
        {
            var lots = _context.Lots.Where(l => l.SellerId == id).ToList();
            foreach (var lot in lots)
            {
                lot.SellerId = null;
            }

            _context.SaveChanges();

            var seller = _context.Sellers.Find(id);
            if (seller != null)
            {
                _context.Sellers.Remove(seller);
                _context.SaveChanges();
            }
        }
    }
}
