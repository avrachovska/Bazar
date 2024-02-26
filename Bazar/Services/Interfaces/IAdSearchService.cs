using Bazar.Data.Models;
using Bazar.Models;

namespace Bazar.Services.Interfaces
{
    public interface IAdSearchService
    {
        List<AdViewModel> SearchAds(AdSearchViewModel searchModel, int page = 1, int pageSize = 10);
        int GetTotalAdsCount(AdSearchViewModel searchModel);
    }
}
