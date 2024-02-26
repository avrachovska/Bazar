using System.ComponentModel.DataAnnotations;

namespace Bazar.Models
{
    public class AdSearchViewModel
    {
        public string? Title { get; set; }
        public string? CategoryName { get; set; }
        public string? OwnerUsername { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? Price { get; set; }
        public DateTime? Date { get; set; }
        public IEnumerable<AdViewModel>? Ads { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public List<CategoryDropdownViewModel> Categories { get; set; }                                                                                         
    }
}
