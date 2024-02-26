using Bazar.Data;
using System.ComponentModel.DataAnnotations;

namespace Bazar.Models
{
    public class AdAddEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.AdNameMinLength)]
        [MaxLength(DataConstants.AdNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(DataConstants.AdDescriptionMinLength)]
        [MaxLength(DataConstants.AdDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }
    }
}
