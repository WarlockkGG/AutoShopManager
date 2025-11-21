// ViewModels/LoginViewModel.cs
using AutoShopManager.Core;
using AutoShopManager.Services;
using AutoShopManager.Models;
using System.Windows.Input;

namespace AutoShopManager.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly AuthenticationService _authService;
        private string _username;
        private string _errorMessage;

        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => Set(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(AuthenticationService authService)
        {
            _authService = authService;
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object parameter)
        {
            // Простая проверка (пароль передается через parameter)
            return !string.IsNullOrWhiteSpace(Username) && parameter is string password && !string.IsNullOrWhiteSpace(password);
        }

        private void ExecuteLogin(object parameter)
        {
            string password = parameter as string;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Пожалуйста, заполните все поля.";
                return;
            }

            User authenticatedUser = _authService.Authenticate(Username, password);

            if (authenticatedUser != null)
            {
                ErrorMessage = $"Вход выполнен успешно! Роль: {authenticatedUser.Role}.";
                // TODO: Вызов навигации к MainView
            }
            else
            {
                ErrorMessage = "Неправильный логин или пароль, либо учетная запись деактивирована.";
            }
        }
    }
}