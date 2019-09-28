using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.ViewModels
{
    using System.Windows.Controls;

    using Prism.Commands;
    using Prism.Mvvm;

    using ZbW.Testing.Dms.Client.Views;

    internal class MainViewModel : BindableBase
    {
        private readonly MainView _mainView;
        private string _benutzer;

        private UserControl _content;

        public MainViewModel(string benutzername, MainView mainView)
        {
            this._mainView = mainView;
            Benutzer = benutzername;
            CmdNavigateToSearch = new DelegateCommand(OnCmdNavigateToSearch);
            CmdNavigateToDocumentDetail = new DelegateCommand(OnCmdNavigateToDocumentDetail);
            CmdLogout = new DelegateCommand(OnCmdLogout);
            this.NavigateToStartPage();
        }

        private void NavigateToStartPage()
        {
            this.NavigateToSearch();
        }

        private void OnCmdLogout()
        {
            UserService userService = new UserService();
            userService.RemoveUserName();

            LoginView loginView =new LoginView();
            loginView.Show();

            this._mainView.Close();
        }

        public string Benutzer
        {
            get
            {
                return _benutzer;
            }

            set
            {
                SetProperty(ref _benutzer, value);
            }
        }

        public UserControl Content
        {
            get
            {
                return _content;
            }

            set
            {
                SetProperty(ref _content, value);
            }
        }

        public DelegateCommand CmdNavigateToSearch { get; }

        public DelegateCommand CmdNavigateToDocumentDetail { get; }

        public DelegateCommand CmdLogout { get; }

        private void OnCmdNavigateToSearch()
        {
            NavigateToSearch();
        }

        private void OnCmdNavigateToDocumentDetail()
        {
            Content = new DocumentDetailView(Benutzer, NavigateToSearch);
        }

        private void NavigateToSearch()
        {
            Content = new SearchView();
        }
    }
}