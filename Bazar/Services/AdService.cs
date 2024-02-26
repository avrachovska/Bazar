using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bazar.Data;
using Bazar.Data.Models;
using Bazar.Models;
using Bazar.Services.Interfaces;

namespace Bazar.Services
{
    public class AdService : IAdService
    {
        private readonly BazarDbContext _context;
        private readonly IMapper _mapper;

        public AdService(BazarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<AdViewModel> GetAllAds(int page = 1, int pageSize = 10)
        {
            var ads = _context.Ads
                .Include(a => a.Category)
                .Include(a => a.Owner)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return _mapper.Map<List<AdViewModel>>(ads);
        }

        public IEnumerable<AdViewModel> My_Ads(string userId, int page = 1, int pageSize = 10)
        {
            var ads = _context.Ads
                .Include(a => a.Category)
                .Include(a => a.Owner)
                .Where(a => a.Owner.Id == userId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return _mapper.Map<List<AdViewModel>>(ads);
        }

        public int GetTotalAdsCount()
        {
            return _context.Ads.Count();
        }

        public void AddAd(AdAddEditViewModel model, string userId)
        {
            var ad = _mapper.Map<Ad>(model);
            ad.OwnerId = userId;
            ad.CreatedOn = DateTime.Now;

            _context.Ads.Add(ad);
            _context.SaveChanges();
        }


        public void AddToCart(int adId, string userId)
        {
            var existingCartItem = _context.AdBuyers
                .FirstOrDefault(ab => ab.AdId == adId && ab.BuyerId == userId);

            if (existingCartItem != null)
            {
                return;
            }

            var adBuyer = new AdBuyer
            {
                AdId = adId,
                BuyerId = userId
            };

            _context.AdBuyers.Add(adBuyer);
            _context.SaveChanges();
        }


        public AdAddEditViewModel GetAdForEdit(int adId)
        {
            var ad = _context.Ads.Find(adId);
            return _mapper.Map<AdAddEditViewModel>(ad);
        }

        public void EditAd(AdAddEditViewModel model)
        {
            var ad = _context.Ads.Find(model.Id);
            _mapper.Map(model, ad);
            _context.SaveChanges();
        }

        public List<CategoryDropdownViewModel> GetAllCategories()
        {
            return _context.Categories
                .Select(c => new CategoryDropdownViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
        }

        public IEnumerable<AdViewModel> GetCartItems(string userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return new List<AdViewModel>();
            }

            var cartItemIds = _context.AdBuyers
                .Where(c => c.BuyerId == user.Id)
                .Select(c => c.AdId)
                .ToList();

            var cartAds = _context.Ads
                .Where(ad => cartItemIds.Contains(ad.Id))
                .ToList();

            var cartViewModels = _mapper.Map<List<Ad>, List<AdViewModel>>(cartAds);

            return cartViewModels;
        }


        public void RemoveFromCart(int adId, string userId)
        {
            var cartItem = _context.AdBuyers
                .FirstOrDefault(ab => ab.AdId == adId && ab.BuyerId == userId);

            // Ако обявата е намерена, изтриваме я
            if (cartItem != null)
            {
                _context.AdBuyers.Remove(cartItem);
                _context.SaveChanges();
            }
        }

        public void DeleteAd(int adId)
        {
            var ad = _context.Ads.Find(adId);
            _context.Ads.Remove(ad);
            _context.SaveChanges();
        }

    }
}
