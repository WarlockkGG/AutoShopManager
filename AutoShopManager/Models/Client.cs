// Models/Client.cs
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AutoShopManager.Models
{
    public class Client
    {
        [Key] public int ClientID { get; set; }
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PassportData { get; set; } = string.Empty;

        public virtual ICollection<Sale> Purchases { get; set; } = new List<Sale>();
        public string GetFullName() => $"{LastName} {FirstName} {MiddleName}".Trim();
    }
}