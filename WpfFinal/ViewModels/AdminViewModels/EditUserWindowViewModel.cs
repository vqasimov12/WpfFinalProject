using System.Windows;
using System.Windows.Input;
using WpfFinal.Commands;
using WpfFinal.Models.People;
using WpfFinal.Services;

namespace WpfFinal.ViewModels.AdminViewModels;
public class EditUserWindowViewModel : BaseViewModel
{
    private User user;
    private User temp;

    public User User { get => user; set { user = value; OnPropertyChanged(); } }
    public User Temp { get => temp; set { temp = value; OnPropertyChanged(); } }

    public EditUserWindowViewModel()
    {
        SaveChanges = new RelayCommand(SaveChangesExecute, SaveChangesCanExecute);
    }

    #region
    public ICommand SaveChanges { get; set; }
    public bool SaveChangesCanExecute(object?obj)=> !(User == Temp);
    public void SaveChangesExecute(object? obj)
    {
        User.Setuser(temp);
        SaveChangesToFileService.Save();
        Close(obj);
    }
    #endregion
}