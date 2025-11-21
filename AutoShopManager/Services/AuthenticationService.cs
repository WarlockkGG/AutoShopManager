// Services/AuthenticationService.cs
using AutoShopManager.Data;
using AutoShopManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AutoShopManager.Services
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Имитация Хеширования (используйте реальную библиотеку, например BCrypt.Net, в продакшене)
        private string HashPassword(string password)
        {
            return $"HASH_{password.GetHashCode()}";
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            return storedHash == $"HASH_{password.GetHashCode()}";
        }

        // Создание начального администратора (Для первого запуска)
        public void SeedInitialAdmin(string username, string password, string firstName)
        {
            // Используем Try-Catch для надежности, так как это работа с БД
            try
            {
                if (!_context.Users.Any())
                {
                    var adminEmployee = new Employee
                    {
                        FirstName = firstName,
                        LastName = "Системный",
                        Position = "Администратор",
                        // Инициализируем обязательные строковые поля
                        Phone = "N/A",
                        Email = "N/A"
                    };
                    _context.Employees.Add(adminEmployee);
                    _context.SaveChanges();

                    var adminUser = new User
                    {
                        EmployeeID = adminEmployee.EmployeeID,
                        Username = username,
                        PasswordHash = HashPassword(password),
                        Role = UserRole.Administrator,
                        IsActive = true
                    };
                    _context.Users.Add(adminUser);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки инициализации БД, если она возникнет
                Console.WriteLine($"Ошибка инициализации администратора: {ex.Message}");
            }
        }

        // Основной метод аутентификации
        public User Authenticate(string username, string password)
        {
            SeedInitialAdmin("admin", "123", "Петр"); // Создаем, если не существует

            var user = _context.Users
                .Include(u => u.Employee)
                .FirstOrDefault(u => u.Username.ToLower() == username.ToLower());

            if (user == null || !user.IsActive) return null;

            if (VerifyPasswordHash(password, user.PasswordHash))
            {
                user.LastLogin = DateTime.Now;
                _context.SaveChanges();
                // TODO: Создать сессию пользователя (SessionManager)
                return user;
            }

            return null;
        }
    }
}