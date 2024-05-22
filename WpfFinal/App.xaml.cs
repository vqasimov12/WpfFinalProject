using System.Windows;
using WpfFinal.DataBases;
using SimpleInjector;
using WpfFinal.Models.People;
using WpfFinal.Views.Windows;
using WpfFinal.Views.Pages.CommonPages;
using WpfFinal.ViewModels.CommonViewModels;
using WpfFinal.ViewModels;
using WpfFinal.ViewModels.UserViewModels;
using WpfFinal.Views.Windows.UserWindows;
using WpfFinal.Views.Windows.AdminWindows;
using WpfFinal.Views.Pages.UserPages;
using WpfFinal.Views.Windows.CommonWindows;
using WpfFinal.ViewModels.AdminViewModels;
using WpfFinal.Views.Pages.AdminPages;
namespace WpfFinal;
public partial class App : Application
{
    public static Container Container { get; set; } = new Container();
    public App()
    {
        Container.RegisterSingleton<AppDbContext>();
        Container.RegisterSingleton<Admin>();
        Container.RegisterSingleton<MainWindowView>();
        RegisterViews();
        RegisterViewModels();
    }
    void RegisterViews()
    {
        Container.RegisterSingleton<LoginPageView>();
        Container.RegisterSingleton<SignupPageView>();
        Container.RegisterSingleton<UserDashBoardWindowView>();
        Container.RegisterSingleton<AdminDashBoardWindowView>();
        Container.RegisterSingleton<AllBooksPageView>();
        Container.RegisterSingleton<AllBooksAdminPageView>();
        Container.RegisterSingleton<ReturnBookPageView>();
        Container.RegisterSingleton<TakeBookPageView>();
        Container.RegisterSingleton<HistoryPageView>();
        Container.RegisterSingleton<AllActiveBooksPageView>();
        Container.RegisterSingleton<AllUsersPageView>();
        Container.Register<ShowBookDetailsWindowView>();
        Container.Register<EditUserWindowView>();
        Container.Register<AddBookWindowView>();

    }
    void RegisterViewModels()
    {
        Container.RegisterSingleton<BaseViewModel>();
        Container.RegisterSingleton<ReturnBookPageViewModel>();
        Container.RegisterSingleton<LoginPageViewModel>();
        Container.RegisterSingleton<UserHistoryPageViewModel>();
        Container.RegisterSingleton<TakeBookPageViewModel>();
        Container.RegisterSingleton<AllBooksPageViewModel>();
        Container.RegisterSingleton<SignupPageViewModel>();
        Container.RegisterSingleton<UserDashBoardWindowViewModel>();
        Container.RegisterSingleton<AdminDashBoardWindowViewModel>();
        Container.RegisterSingleton<ShowBookDetailsWindowViewModel>();
        Container.RegisterSingleton<AllActiveBooksPageViewModel>();
        Container.RegisterSingleton<EditUserWindowViewModel>();
        Container.RegisterSingleton<AllUsersPageViewModel>();
        Container.RegisterSingleton<AddBookWindowViewModel>();
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        var view = Container.GetInstance<MainWindowView>();
        view.DataContext = Container.GetInstance<LoginPageViewModel>();
        view.ShowDialog();
        base.OnStartup(e);
        Current.Shutdown();
    }

}
