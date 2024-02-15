using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bazar.Data.Models
{
    public class AdBuyer
    {
        [Required]
        public string BuyerId { get; set; }
        public IdentityUser Buyer { get; set; }

        [Required]
        public int AdId { get; set; }
        public Ad Ad { get; set; }
    }
}
