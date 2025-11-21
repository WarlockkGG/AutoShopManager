// Models/User.cs
using AutoShopManager.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace AutoShopManager.Models
{
    public class User
    {
        [Key] public int UserID { get; set; }
        [Required] public string Username { get; set; } = string.Empty;
        [Required] public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastLogin { get; set; }

        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; } = null!; // Навігаційна властивість
    }
}