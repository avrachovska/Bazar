using Bazar.Models;

namespace Bazar.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDropdownViewModel> GetAllCategories();
    }
}
