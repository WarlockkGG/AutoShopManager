// Models/Sale.cs
using AutoShopManager.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoShopManager.Models
{
    public class Sale
    {
        [Key] public int SaleID { get; set; }

        public int CarID { get; set; }
        public int ClientID { get; set; }
        public int EmployeeID { get; set; } // FK

        public DateTime SaleDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        [Required] public string ContractNumber { get; set; } = string.Empty;

        public virtual Car Car { get; set; } = null!;
        public virtual Client Client { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
    }
}