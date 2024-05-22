using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using WpfFinal.Commands;
using WpfFinal.Views.Pages.CommonPages;
using WpfFinal.DataBases;
using WpfFinal.Views.Windows;
using WpfFinal.ViewModels.UserViewModels;
using WpfFinal.Views.Windows.UserWindows;
using WpfFinal.Views.Windows.AdminWindows;
using WpfFinal.Views.Pages.UserPages;
using WpfFinal.ViewModels.AdminViewModels;
using WpfFinal.Views.Pages.AdminPages;

namespace WpfFinal.ViewModels.CommonViewModels;
public class LoginPageViewModel : BaseViewModel
{
    private string username;
    private string password;
    bool a = true;
    bool u = true;
    //private string displayPassword;

    //public string DisplayPassword
    //{
    //    get => displayPassword; set
    //    {
    //        Password = value;
    //        displayPassword = Asterix(value);
    //        OnPropertyChanged();
    //    }
    //}

    public string Password
    {
        get => password;
        set
        {
            if (password != value)
            {
                password = value;
                OnPropertyChanged();
            }
        }
    }
    public string Username { get => username; set { username = value; OnPropertyChanged(); } }
    public ICommand LoginCommand { get; set; }
    public ICommand SignUpCommand { get; set; }
    public LoginPageViewModel()
    {
        SignUpCommand = new RelayCommand(SignUpCommandExcute);
        LoginCommand = new RelayCommand(Login);
    }

    public void Login(object? obj)
    {
        var database = App.Container.GetInstance<AppDbContext>();

        if (Username == "admin")
            if (Password == "admin")
            {
                var window = App.Container.GetInstance<MainWindowView>();
                var window1 = App.Container.GetInstance<AdminDashBoardWindowView>();
                var datacontext = App.Container.GetInstance<AdminDashBoardWindowViewModel>();
                datacontext.CurrentPage = App.Container.GetInstance<AllBooksAdminPageView>();
                datacontext.CurrentPage.DataContext = App.Container.GetInstance<AllBooksPageViewModel>();
                window1.DataContext = datacontext;
                window.Visibility = Visibility.Hidden;
                window1.WindowState = WindowState.Normal;
                window1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                if (a)
                {
                    window1.ShowDialog();
                    a = false;
                }
                else
                    window1.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Incorrect Password", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Password = "";
            }


        else if (database.UserExist(Username))
        {
            var user = database.CheckUser(Username, Password);

            if (user is null)
            {
                MessageBox.Show("Incorrect Username", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Password = "";
            }
            else
            {
                var data = App.Container.GetInstance<AppDbContext>();
                data.CurrentUser = user;
                var window = App.Container.GetInstance<MainWindowView>();
                var window1 = App.Container.GetInstance<UserDashBoardWindowView>();
                var datacontext = App.Container.GetInstance<UserDashBoardWindowViewModel>();
                datacontext.CurrentPage = App.Container.GetInstance<AllBooksPageView>();
                datacontext.CurrentPage.DataContext = App.Container.GetInstance<AllBooksPageViewModel>();
                window1.DataContext = datacontext;
                window.Visibility = Visibility.Hidden;
                window1.WindowState = WindowState.Normal;
                window1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                if (u)
                {
                    window1.ShowDialog();
                    u = false;
                }
                else
                    window1.Visibility = Visibility.Visible;
            }
        }

        else
        {
            MessageBox.Show("User can not be found", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            Username = "";
            Password = "";
        }
    }

    public void SignUpCommandExcute(object? obj)
    {
        if (obj is Page page)
        {
            var view = App.Container.GetInstance<SignupPageView>();
            view.DataContext = App.Container.GetInstance<SignupPageViewModel>();
            page.NavigationService.Navigate(view);
        }
    }

}
