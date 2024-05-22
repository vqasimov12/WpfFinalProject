using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using WpfFinal.Commands;
using WpfFinal.DataBases;
using WpfFinal.Models.People;
using WpfFinal.Services;

namespace WpfFinal.ViewModels.CommonViewModels;
public class SignupPageViewModel : BaseViewModel
{
    bool isOtpCodeVisible;
    private bool secondR;
    private string oTPCode;
    private User user = new();

    public User User { get => user; set { user = value; OnPropertyChanged(); } }
    bool SecondR { get => secondR; set { secondR = value; OnPropertyChanged(); } }
    public string OTPCode
    {
        get => oTPCode;
        set
        {
            oTPCode = value; OnPropertyChanged();
        }
    }
    public bool IsOtpCodeVisible { get => isOtpCodeVisible; set { isOtpCodeVisible = value; OnPropertyChanged(); } }

    public ICommand RegisterCommand { get; set; }
    public ICommand LoginCommand { get; set; }

    public SignupPageViewModel()
    {
        RegisterCommand = new RelayCommand(RegisterCommandExecute, CanRegisterButtonExecute);
        LoginCommand = new RelayCommand(loginCommandExecute);
    }

    public void loginCommandExecute(object? obj)
    {
        Page? page = obj as Page;
        if (page is not null && page.NavigationService.CanGoBack)
        {
            User = new();
            IsOtpCodeVisible = false;
            OTPCode = "";
            SecondR = false;
            page.NavigationService.GoBack();
        }
    }

    public bool CanRegisterButtonExecute(object? obj)
    {
        if (SecondR == true)
            return OTPCode == MailService.OTPCode;
        else
            if (User.Name != "" && User.Password.Length > 5 &&
               Regex.IsMatch(User.Email, "^[a-zA-Z0-9.]+@[a-zA-Z0-9.]+\\.[a-zA-Z]{2,4}$"))
            return true;
        else
            return false;
    }
    public void RegisterCommandExecute(object? obj)
    {
        if (!IsOtpCodeVisible)
        {
            IsOtpCodeVisible = true;
            if (SecondR == false)
            {
                MailService.SendMail(User.Email);
                SecondR = true;
            }
        }

        else
        {
            var data = App.Container.GetInstance<AppDbContext>();
            if (data.UserExist(User.Name))
                MessageBox.Show("This Username has already been used change another one", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                User.ActiveBooks = new();
                User.AllBooks = new();
                data.AddUser(User);

                User = new();
                IsOtpCodeVisible = false;
                OTPCode = "";
                SecondR = false;
            }
        }
    }

}
