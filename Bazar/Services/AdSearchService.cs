using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bazar.Data;
using Bazar.Data.Models;
using Bazar.Models;
using Bazar.Services.Interfaces;

namespace Bazar.Services
{
    public class AdSearchService : IAdSearchService
    {
        private readonly BazarDbContext _context;
        private readonly IMapper _mapper;



        public AdSearchService(BazarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public List<AdViewModel> SearchAds(AdSearchViewModel searchModel, int page = 1, int pageSize = 10)
        {
            var query = _context.Ads
                .Include(ad => ad.Category)
                .Include(ad => ad.Owner)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.Title))
            {
                query = query.Where(ad => ad.Name.Contains(searchModel.Title));
            }

            if (!string.IsNullOrEmpty(searchModel.CategoryName))
            {
                query = query.Where(ad => ad.Category.Name.Contains(searchModel.CategoryName));
            }

            if (!string.IsNullOrEmpty(searchModel.OwnerUsername))
            {
                query = query.Where(ad => ad.Owner.UserName.Contains(searchModel.OwnerUsername));
            }

            if (searchModel.MinPrice.HasValue)
            {
                query = query.Where(ad => ad.Price >= searchModel.MinPrice);
            }

            if (searchModel.Price.HasValue)
            {
                query = query.Where(ad => ad.Price <= searchModel.Price);
            }

            if (searchModel.Date.HasValue)
            {
                query = query.Where(ad => ad.CreatedOn.Date == searchModel.Date.Value.Date);
            }
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<Ad> ads = query.ToList();
            List<AdViewModel> adViewModels = _mapper.Map<List<AdViewModel>>(ads);

            return adViewModels;
        }

        public int GetTotalAdsCount(AdSearchViewModel searchModel)
        {
            var query = _context.Ads.AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.Title))
            {
                query = query.Where(ad => ad.Name.Contains(searchModel.Title));
            }

            if (!string.IsNullOrEmpty(searchModel.CategoryName))
            {
                query = query.Where(ad => ad.Category.Name.Contains(searchModel.CategoryName));
            }

            if (!string.IsNullOrEmpty(searchModel.OwnerUsername))
            {
                query = query.Where(ad => ad.Owner.UserName.Contains(searchModel.OwnerUsername));
            }

            if (searchModel.MinPrice.HasValue)
            {
                query = query.Where(ad => ad.Price >= searchModel.MinPrice);
            }

            if (searchModel.Price.HasValue)
            {
                query = query.Where(ad => ad.Price <= searchModel.Price);
            }

            if (searchModel.Date.HasValue)
            {
                query = query.Where(ad => ad.CreatedOn.Date == searchModel.Date.Value.Date);
            }

            return query.Count();
        }
    }
}
