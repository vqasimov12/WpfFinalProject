using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfFinal.Commands;
using WpfFinal.DataBases;
using WpfFinal.ViewModels.CommonViewModels;
using WpfFinal.Views.Pages.UserPages;
using WpfFinal.Views.Windows;
using WpfFinal.Views.Windows.UserWindows;

namespace WpfFinal.ViewModels.UserViewModels;
public class UserDashBoardWindowViewModel : DashBoardWindowBaseViewModel
{
    private Page currentPage;

    public Page CurrentPage { get => currentPage; set { currentPage = value; OnPropertyChanged(); } }
    public UserDashBoardWindowViewModel()
    {
        CurrentPage = App.Container.GetInstance<AllBooksPageView>();
        CurrentPage.DataContext = App.Container.GetInstance<AllBooksPageViewModel>();
        AllBooksCommand = new RelayCommand(AllBooks);
        ReturnBookCommand = new RelayCommand(ReturnBook);
        TakeBookCommand = new RelayCommand(TakeBook);
        HistoryCommand = new RelayCommand(HistoryCommandExecute);
        LogOutCommand = new RelayCommand(LogOut);
        CommandClose = new RelayCommand(CloseCommandExecute);
    }

    #region CloseCommand
    public ICommand CommandClose { get; set; }
    public void CloseCommandExecute(object?obj)
    {
        App.Current.Shutdown();
    }

    #endregion

    #region AllBooks
    public ICommand AllBooksCommand { get; set; }
    public void AllBooks(object? obj)
    {
        CurrentPage = App.Container.GetInstance<AllBooksPageView>();
        currentPage.DataContext = App.Container.GetInstance<AllBooksPageViewModel>();
    }
    #endregion

    #region ReturnBookCommand
    public ICommand ReturnBookCommand { get; set; }
    public void ReturnBook(object? obj)
    {
        CurrentPage = App.Container.GetInstance<ReturnBookPageView>();
        var datacontext = App.Container.GetInstance<ReturnBookPageViewModel>();
        var data = App.Container.GetInstance<AppDbContext>();
        datacontext.User = data.CurrentUser;
        CurrentPage.DataContext = datacontext;
    }


    #endregion

    #region TakeBookCommand
    public ICommand TakeBookCommand { get; set; }
    public void TakeBook(object? obj)
    {
        CurrentPage = App.Container.GetInstance<TakeBookPageView>();
        var datacontext = App.Container.GetInstance<TakeBookPageViewModel>();
        datacontext.RefreshBooks();
        CurrentPage.DataContext = datacontext;
    }


    #endregion

    #region HistoryCommand
    public ICommand HistoryCommand { get; set; }
    public void HistoryCommandExecute(object? obj)
    {
        CurrentPage = App.Container.GetInstance<HistoryPageView>();
        var datacontext = App.Container.GetInstance<UserHistoryPageViewModel>();
        var data = App.Container.GetInstance<AppDbContext>();
        datacontext.User = data.CurrentUser;
        CurrentPage.DataContext = datacontext;
    }
    #endregion

    #region LogOutCommand
    public ICommand LogOutCommand { get; set; }

    public void LogOut(object? obj)
    {
        var window = App.Container.GetInstance<UserDashBoardWindowView>();
        var NewWindow = App.Container.GetInstance<MainWindowView>();
        var datacontext = App.Container.GetInstance<LoginPageViewModel>();
        datacontext.Username = "";
        datacontext.Password = "";
        NewWindow.DataContext = datacontext;
        window.Visibility=Visibility.Hidden;
        NewWindow.ShowDialog();
    }

    #endregion

}
