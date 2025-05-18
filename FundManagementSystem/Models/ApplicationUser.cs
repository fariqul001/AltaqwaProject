using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FundManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public decimal TotalFundAmount { get; set; } = 0;
    }
}
