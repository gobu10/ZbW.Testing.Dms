using System;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.ViewModels
{
    using System.Windows;

    using Prism.Commands;
    using Prism.Mvvm;

    using ZbW.Testing.Dms.Client.Views;

    public class LoginViewModel : BindableBase
    {
        private readonly LoginView _loginView;

        private string _benutzername;

        private UserService _userService;

        public LoginViewModel(LoginView loginView)
        {
            _loginView = loginView;
            _userService = new UserService();

            CmdLogin = new DelegateCommand(OnCmdLogin, OnCanLogin);
            CmdAbbrechen = new DelegateCommand(OnCmdAbbrechen);

            var username = this._userService.GetUsername();
            this.AutoLoginIfPossible(username);
        }

        public DelegateCommand CmdAbbrechen { get; }

        public DelegateCommand CmdLogin { get; }

        public string Benutzername
        {
            get
            {
                return _benutzername;
            }

            set
            {
                if (SetProperty(ref _benutzername, value))
                {
                    CmdLogin.RaiseCanExecuteChanged();
                }
            }
        }

        private void AutoLoginIfPossible(String username)
        {
            if (!String.IsNullOrEmpty(username))
            {
                Benutzername = username;
                this.OnCmdLogin();
            }
        }
        private bool OnCanLogin()
        {
            return !string.IsNullOrEmpty(Benutzername);
        }

        private void OnCmdAbbrechen()
        {
            Application.Current.Shutdown();
        }

        private void OnCmdLogin()
        {
            if (string.IsNullOrEmpty(Benutzername))
            {
                MessageBox.Show("Bitte tragen Sie einen Benutzernamen ein...");
                return;
            }

            this._userService.SaveUsername(Benutzername);

            var searchView = new MainView(Benutzername);
            searchView.Show();

            _loginView.Close();
        }
    }
}