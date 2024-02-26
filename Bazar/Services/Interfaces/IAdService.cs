using Bazar.Models;

namespace Bazar.Services.Interfaces;

public interface IAdService
{
    IEnumerable<AdViewModel> GetAllAds(int page = 1, int pageSize = 10);
    IEnumerable<AdViewModel> My_Ads(string userId, int page = 1, int pageSize = 10);
    void AddAd(AdAddEditViewModel model, string userId);
    void AddToCart(int adId, string userId);
    AdAddEditViewModel GetAdForEdit(int adId);
    List<CategoryDropdownViewModel> GetAllCategories();
    void EditAd(AdAddEditViewModel model);
    void RemoveFromCart(int adId, string userId);
    IEnumerable<AdViewModel> GetCartItems(string username);
    void DeleteAd(int adId);
    int GetTotalAdsCount();
}