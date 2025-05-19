
using System;
using System.ComponentModel.DataAnnotations;

namespace FundManagementSystem.Models
{
    public class FundRequest
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } // Foreign key to ApplicationUser

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be positive.")]
        public decimal Amount { get; set; }

        [Required]
        public string Month { get; set; }

        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

        public DateTime RequestedAt { get; set; } = DateTime.Now;
    }
}
