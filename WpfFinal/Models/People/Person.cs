using MaterialDesignThemes.Wpf;
using System.Windows;
using WpfFinal.Services;
using WpfFinal.ViewModels.AdminViewModels;
using WpfFinal.ViewModels.UserViewModels;
using WpfFinal.Views.Pages.AdminPages;
using WpfFinal.Views.Pages.UserPages;
using WpfFinal.Views.Windows;
using WpfFinal.Views.Windows.UserWindows;

namespace WpfFinal.Models.People;

public class Person : NotifyPropertyChangedService
{
    private string name = "";
    private string email = "";
    private string password = "";

    public Person()
    {
        Name = "";
        Email = "";
    }
    public Person(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
    public string Name { get => name; set { name = value; OnPropertyChanged(); } }
    public string Email { get => email; set { email = value; OnPropertyChanged(); } }
    public string Password { get => password; set { password = value; OnPropertyChanged(); } }
}

