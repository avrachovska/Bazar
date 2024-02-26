using AutoMapper;
using Bazar.Data;
using Bazar.Models;
using Bazar.Services.Interfaces;

namespace Bazar.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BazarDbContext dbContext;
        private readonly IMapper mapper;

        public CategoryService(BazarDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public IEnumerable<CategoryDropdownViewModel> GetAllCategories()
        {
            var categories = this.dbContext.Categories.ToList();

            var viewModelCategories = this.mapper.Map<IEnumerable<CategoryDropdownViewModel>>(categories);

            return viewModelCategories;
        }
    }
}
