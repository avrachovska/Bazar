using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bazar.Data.Models
{
    public class Ad
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
        public decimal Price { get; set; }

        [Required]
        public string OwnerId { get; set; }
        public IdentityUser Owner { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
