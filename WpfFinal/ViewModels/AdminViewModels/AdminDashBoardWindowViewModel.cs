using System.Windows.Input;
using WpfFinal.ViewModels.CommonViewModels;
using WpfFinal.Views.Windows;
using WpfFinal.Commands;
using System.Windows;
using WpfFinal.Views.Windows.AdminWindows;
using System.Windows.Controls;
using WpfFinal.ViewModels.UserViewModels;
using WpfFinal.Views.Pages.AdminPages;

namespace WpfFinal.ViewModels.AdminViewModels;
public class AdminDashBoardWindowViewModel : DashBoardWindowBaseViewModel
{
    private Page currentPage;
    public Page CurrentPage { get => currentPage; set { currentPage = value; OnPropertyChanged(); } }

    public AdminDashBoardWindowViewModel()
    {
        CurrentPage = App.Container.GetInstance<AllBooksAdminPageView>();
        CurrentPage.DataContext = App.Container.GetInstance<AllBooksPageViewModel>();
        LogOutCommand = new RelayCommand(LogOut);
        CommandClose = new RelayCommand(CloseCommandExecute);
        AllBooksCommand = new RelayCommand(AllBooks);
        AllUsersCommand = new RelayCommand(AllUsersCommandExecute);
        ActiveBooksCommand = new RelayCommand(ActiveBooksCommandExecute);

    }

    #region AllBooks
    public ICommand AllBooksCommand { get; set; }
    public void AllBooks(object? obj)
    {
        CurrentPage = App.Container.GetInstance<AllBooksAdminPageView>();
        currentPage.DataContext = App.Container.GetInstance<AllBooksPageViewModel>();
    }
    #endregion

    #region AllUsersCommand
    public ICommand AllUsersCommand { get; set; }
    public void AllUsersCommandExecute(object?obj)
    {
        CurrentPage = App.Container.GetInstance<AllUsersPageView>();
        currentPage.DataContext = App.Container.GetInstance<AllUsersPageViewModel>();

    }
    #endregion

    #region ActiveBooksCommand
    public ICommand ActiveBooksCommand { get; set; }
    public void ActiveBooksCommandExecute(object?obj)
    {
        CurrentPage = App.Container.GetInstance<AllActiveBooksPageView>();
        var datacontext = App.Container.GetInstance<AllActiveBooksPageViewModel>();
        datacontext.Refresh();
        currentPage.DataContext = datacontext;

    }
    #endregion

    #region LogOutCommand
    public ICommand LogOutCommand { get; set; }
    public void LogOut(object? obj)
    {
        var window = App.Container.GetInstance<AdminDashBoardWindowView>();
        var NewWindow = App.Container.GetInstance<MainWindowView>();
        var datacontext = App.Container.GetInstance<LoginPageViewModel>();
        datacontext.Username = "";
        datacontext.Password = "";
        NewWindow.DataContext = datacontext;
        window.Visibility = Visibility.Hidden;
        NewWindow.ShowDialog();
    }
    #endregion

    #region CloseCommand
    public ICommand CommandClose { get; set; }
    public void CloseCommandExecute(object? obj)
    {
        App.Current.Shutdown();
    }
    #endregion
}
