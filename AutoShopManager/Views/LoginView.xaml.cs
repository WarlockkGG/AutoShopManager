// Views/LoginView.xaml.cs (оновлений)

using System.Windows;
using AutoShopManager.ViewModels;

namespace AutoShopManager.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();

            // Підписка на подію після завантаження DataContext
            this.Loaded += LoginView_Loaded;
        }

        private void LoginView_Loaded(object sender, RoutedEventArgs e)
        {
            // Отримуємо ViewModel (який був встановлений у App.xaml.cs)
            if (DataContext is LoginViewModel viewModel)
            {
                // Підписуємося на подію успішного входу
                viewModel.LoginSuccess += OnLoginSuccess;
            }
        }

        private void OnLoginSuccess()
        {
            // 1. Створення та відображення головного вікна
            // Примітка: В ідеалі це має робити DI-контейнер
            MainView mainView = new MainView();
            mainView.Show();

            // 2. Закриття поточного вікна входу
            this.Close();
        }
    }
}