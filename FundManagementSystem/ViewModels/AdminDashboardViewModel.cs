using FundManagementSystem.Models;
using System.Collections.Generic;

namespace FundManagementSystem.ViewModels
{
    public class AdminDashboardViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public decimal TotalFunds { get; set; }
    }
}
