using System.ComponentModel.DataAnnotations;

namespace FundManagementSystem.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string NID { get; set; } = string.Empty;

        public string PaymentProofImagePath { get; set; } = string.Empty;

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }

}





