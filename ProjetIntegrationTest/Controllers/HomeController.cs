using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProjetIntegrationTest.Data;
using ProjetIntegrationTest.Models;
using ProjetIntegrationTest.Services;
using ProjetIntegrationTest.ViewModels;
using System.Diagnostics;

namespace ProjetIntegrationTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly AuctionService _auctionService;
        private readonly LotService _lotService;
        private readonly SellerService _sellerService;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, AuctionService auctionService, LotService lotService, SellerService sellerService)
        {
            _logger = logger;
            _context = context;
            _auctionService = auctionService;
            _lotService = lotService;
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult AllAuctions()
        {
            var auctions = _auctionService.GetAllAuctions();
            return View(auctions);
        }

        [HttpGet]
        public IActionResult CreateAuction()
        {
            return View(new AuctionInfoVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAuction(AuctionInfoVM auctionInfoVM)
        {
            if (ModelState.IsValid)
            {
                var auction = new Auction
                {
                    OpeningDate = auctionInfoVM.OpeningDate,
                    ClosingDate = auctionInfoVM.ClosingDate,
                    BiddingInterval = auctionInfoVM.BiddingInterval,
                    ClosingInterval = auctionInfoVM.ClosingInterval,
                    TimeIncrementPerBet = auctionInfoVM.TimeIncrementPerBet,
                    Lots = new List<Lot>()
                };

                _auctionService.CreateAuction(auction);

                return RedirectToAction(nameof(ManageLots), new { id = auction.Id });
            }

            return View(auctionInfoVM);
        }

        [HttpGet]
        public IActionResult ManageLots(int id)
        {
            var auction = _auctionService.GetAuctionById(id);
            if (auction == null)
            {
                return NotFound();
            }

            var allLots = _lotService.GetAllLots();
            var assignedLots = auction.Lots.ToList();

            var viewModel = new AuctionManagementVM
            {
                AuctionId = id,
                AssignedLots = assignedLots.Select(l => new LotInfoVM
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
                }).ToList(),
                AvailableLots = allLots
                    .Where(l => !assignedLots.Any(al => al.Id == l.Id))
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
                    }).ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult EditAuction(int id)
        {
            var auction = _auctionService.GetAuctionById(id);
            if (auction == null)
            {
                return NotFound();
            }

            var viewModel = new AuctionInfoVM
            {
                Id = auction.Id,
                OpeningDate = auction.OpeningDate,
                ClosingDate = auction.ClosingDate,
                BiddingInterval = auction.BiddingInterval,
                ClosingInterval = auction.ClosingInterval,
                TimeIncrementPerBet = auction.TimeIncrementPerBet
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAuction(AuctionInfoVM vm)
        {
            if (ModelState.IsValid)
            {
                var auction = _auctionService.GetAuctionById(vm.Id);
                if (auction == null)
                {
                    return NotFound();
                }

                auction.OpeningDate = vm.OpeningDate;
                auction.ClosingDate = vm.ClosingDate;
                auction.BiddingInterval = vm.BiddingInterval;
                auction.ClosingInterval = vm.ClosingInterval;
                auction.TimeIncrementPerBet = vm.TimeIncrementPerBet;

                _auctionService.UpdateAuction(auction);

                return RedirectToAction(nameof(ManageLots), new { id = auction.Id });
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignLot(AuctionManagementVM model)
        {
            _auctionService.AssignLotToAuction(model.AuctionId, model.SelectedLotId);
            return RedirectToAction(nameof(ManageLots), new { id = model.AuctionId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveLot(AuctionManagementVM model)
        {
            _auctionService.RemoveLotFromAuction(model.AuctionId, model.SelectedLotId);
            return RedirectToAction(nameof(ManageLots), new { id = model.AuctionId });
        }

        [HttpPost]
        public IActionResult DeleteAuction(int id)
        {
            _auctionService.DeleteAuction(id);
            return RedirectToAction("AllAuctions");
        }

        [HttpGet]
        public IActionResult AllLots()
        {
            var lots = _lotService.GetAllLots();
            return View(lots);
        }

        [HttpGet]
        public IActionResult CreateLot()
        {
            return View(new LotInfoVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateLot(LotInfoVM model)
        {
            if (ModelState.IsValid)
            {
                var lot = new Lot
                {
                    ArtistName = model.ArtistName,
                    Description = model.Description,
                    YearOfCreation = model.YearOfCreation,
                    MinEstimate = model.MinEstimate,
                    MaxEstimate = model.MaxEstimate,
                    Category = model.Category,
                    Dimension = model.Dimension,
                    Medium = model.Medium,
                    FilingDate = model.FilingDate
                };

                _lotService.CreateLot(lot);

                return RedirectToAction("ManageAuctions", new { id = lot.Id });
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteLot(int id)
        {
            _lotService.DeleteLot(id);
            return RedirectToAction("AllLots");
        }

        [HttpGet]
        public IActionResult EditLot(int id)
        {
            var lot = _lotService.GetLotById(id);
            if (lot == null)
            {
                return NotFound();
            }

            var viewModel = new LotInfoVM
            {
                Id = lot.Id,
                ArtistName = lot.ArtistName,
                Description = lot.Description,
                YearOfCreation = lot.YearOfCreation,
                MinEstimate = lot.MinEstimate,
                MaxEstimate = lot.MaxEstimate,
                Category = lot.Category,
                Dimension = lot.Dimension,
                Medium = lot.Medium,
                FilingDate = lot.FilingDate
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditLot(LotInfoVM vm)
        {
            if (ModelState.IsValid)
            {
                var lot = _lotService.GetLotById(vm.Id);
                if (lot == null)
                {
                    return NotFound();
                }

                lot.ArtistName = vm.ArtistName;
                lot.Description = vm.Description;
                lot.YearOfCreation = vm.YearOfCreation;
                lot.MinEstimate = vm.MinEstimate;
                lot.MaxEstimate = vm.MaxEstimate;
                lot.Category = vm.Category;
                lot.Dimension = vm.Dimension;
                lot.Medium = vm.Medium;
                lot.FilingDate = vm.FilingDate;

                _lotService.UpdateLot(lot);

                return RedirectToAction(nameof(ManageAuctions), new { id = lot.Id });
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult ManageAuctions(int id)
        {
            var lot = _lotService.GetLotById(id);
            if (lot == null)
            {
                return NotFound();
            }

            var allAuctions = _auctionService.GetAllAuctions();
            var assignedAuctions = lot.Auctions.ToList();

            var viewModel = new LotManagementVM
            {
                LotId = lot.Id,
                AssignedAuctions = assignedAuctions.Select(a => new AuctionInfoVM
                {
                    Id = a.Id,
                    OpeningDate = a.OpeningDate,
                    ClosingDate = a.ClosingDate,
                    BiddingInterval = a.BiddingInterval,
                    ClosingInterval = a.ClosingInterval,
                    TimeIncrementPerBet = a.TimeIncrementPerBet
                }).ToList(),
                AvailableAuctions = allAuctions
                    .Where(a => !assignedAuctions.Any(al => al.Id == a.Id))
                    .Select(a => new AuctionInfoVM
                    {
                        Id = a.Id,
                        OpeningDate = a.OpeningDate,
                        ClosingDate = a.ClosingDate,
                        BiddingInterval = a.BiddingInterval,
                        ClosingInterval = a.ClosingInterval,
                        TimeIncrementPerBet = a.TimeIncrementPerBet
                    }).ToList(),
                AvailableSellers = _sellerService.GetAllSellers()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignLotToAuction(int lotId, int auctionId)
        {
            _lotService.AssignLotToAuction(lotId, auctionId);
            return RedirectToAction("ManageAuctions", new { id = lotId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveLotFromAuction(int lotId, int auctionId)
        {
            _lotService.RemoveLotFromAuction(lotId, auctionId);
            return RedirectToAction("ManageAuctions", new { id = lotId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignSellerToLot(LotManagementVM model)
        {
            _lotService.AssignSellerToLot(model.LotId, model.SelectedSellerId);
            return RedirectToAction("ManageAuctions", new { id = model.LotId });
        }

        [HttpGet]
        public IActionResult AllSellers()
        {
            var sellers = _sellerService.GetAllSellers();
            return View(sellers);
        }

        [HttpGet]
        public IActionResult CreateSeller()
        {
            return View(new SellerInfoVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSeller(SellerInfoVM vm)
        {
            if (ModelState.IsValid)
            {
                var seller = new Seller
                {
                    LastName = vm.LastName,
                    FirstName = vm.FirstName,
                    PhoneNumber = vm.PhoneNumber,
                    Email = vm.Email
                };

                _sellerService.CreateSeller(seller);

                return RedirectToAction("ManageSellers", new { sellerId = seller.Id });
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult EditSeller(int id)
        {
            var seller = _sellerService.GetSellerById(id);
            if (seller == null)
            {
                return NotFound();
            }

            var viewModel = new SellerInfoVM
            {
                Id = seller.Id,
                LastName = seller.LastName,
                FirstName = seller.FirstName,
                PhoneNumber = seller.PhoneNumber,
                Email = seller.Email
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSeller(SellerInfoVM vm)
        {
            if (ModelState.IsValid)
            {
                var seller = _sellerService.GetSellerById(vm.Id);
                if (seller == null)
                {
                    return NotFound();
                }

                seller.LastName = vm.LastName;
                seller.FirstName = vm.FirstName;
                seller.PhoneNumber = vm.PhoneNumber;
                seller.Email = vm.Email;

                _sellerService.UpdateSeller(seller);

                return RedirectToAction("ManageSellers", new { sellerId = seller.Id });
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult DeleteSeller(int id)
        {
            _sellerService.DeleteSeller(id);
            return RedirectToAction("AllSellers");
        }

        [HttpGet]
        public IActionResult ManageSellers(int sellerId)
        {
            var seller = _context.Sellers.Find(sellerId);
            if (seller == null)
            {
                return NotFound();
            }

            var assignedLots = _context.Lots.Where(l => l.SellerId == sellerId).Select(l => new LotInfoVM
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

            var availableLots = _context.Lots.Where(l => l.SellerId == null).Select(l => new LotInfoVM
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

            var viewModel = new SellerManagementVM
            {
                AssignedLots = assignedLots,
                AvailableLots = availableLots,
                SellerId = sellerId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignLotToSeller(SellerManagementVM vm)
        {
            if (ModelState.IsValid)
            {
                _sellerService.AssignLotToSeller(vm.SellerId, vm.SelectedLotId);
                return RedirectToAction("ManageSellers", new { sellerId = vm.SellerId });
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveLotFromSeller(int sellerId, int lotId)
        {
            _sellerService.RemoveLotFromSeller(sellerId, lotId);
            return RedirectToAction("ManageSellers", new { sellerId });
        }
    }
}