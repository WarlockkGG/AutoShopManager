// Models/Car.cs
using AutoShopManager.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoShopManager.Models
{
    public class Car
    {
        [Key] public int CarID { get; set; }
        [Required] public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }

        [Required, StringLength(17, MinimumLength = 17)]
        public string VIN { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;
        public string BodyType { get; set; } = string.Empty;

        [Column(TypeName = "decimal(4, 2)")] // Виправлення попередження Decimal
        public decimal EngineVolume { get; set; }
        public string FuelType { get; set; } = string.Empty;
        public int Mileage { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public CarStatus Status { get; set; } = CarStatus.Available;

        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

        public string GetFullName() => $"{Brand} {Model} ({Year})";
    }
}