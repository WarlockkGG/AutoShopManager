// App.xaml.cs
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using AutoShopManager.Data;
using AutoShopManager.Services;
using AutoShopManager.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AutoShopManager
{
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            // 1. Регистрация DbContext (SQLite)
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite("Data Source=AutoShop.db");
            });

            // 2. Регистрация сервисов (один экземпляр для каждого вызова)
            services.AddTransient<AuthenticationService>();

            // 3. Регистрация ViewModel
            services.AddTransient<LoginViewModel>();

            // 4. Регистрация View (окно входа)
            services.AddSingleton<Views.LoginView>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Получение и отображение стартового окна (LoginView)
            var loginView = serviceProvider.GetService<Views.LoginView>();
            // Установка DataContext через DI
            loginView.DataContext = serviceProvider.GetService<LoginViewModel>();
            loginView.Show();

            base.OnStartup(e);
        }
    }
}