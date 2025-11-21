// Models/Employee.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AutoShopManager.Models
{
    public class Employee
    {
        [Key] public int EmployeeID { get; set; }
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        [Required] public string Position { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime HireDate { get; set; } = DateTime.Today;

        public virtual User User { get; set; } = null!; // Навігаційна властивість 1:1
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public string GetFullName() => $"{LastName} {FirstName} {MiddleName}".Trim();
    }
}