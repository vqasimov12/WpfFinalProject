using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfFinal.Commands;
using WpfFinal.DataBases;
using WpfFinal.Models.People;
using WpfFinal.Services;
using WpfFinal.Views.Windows.AdminWindows;

namespace WpfFinal.ViewModels.AdminViewModels;
public class AllUsersPageViewModel : BaseViewModel
{
    private ObservableCollection<User> users;
    public ObservableCollection<User> Users { get => users; set { users = value; OnPropertyChanged(); } }

    public AllUsersPageViewModel()
    {
        Users = App.Container.GetInstance<AppDbContext>().Users;
        EditCommand = new RelayCommand(EditCommandExecute);
        RemoveCommand = new RelayCommand(Remove);
    }

    #region EditCommand
    public ICommand EditCommand { get; set; }
    public void EditCommandExecute(object? obj)
    {

        var window = App.Container.GetInstance<EditUserWindowView>();
        var datacontext = App.Container.GetInstance<EditUserWindowViewModel>();
        datacontext.User = obj as User;
        datacontext.Temp = new();
        datacontext.Temp.Setuser(datacontext.User);
        window.DataContext = datacontext;
        window.ShowDialog();
    }

    #endregion

    #region RemoveCommand
    public ICommand RemoveCommand { get; set; }
    public void Remove(object? obj)
    {
        User a = obj as User;
        if (a is  null)
            return;
        var data = App.Container.GetInstance<AppDbContext>();
        data.RemoveUser(a.Name);
        SaveChangesToFileService.Save();
        //data.ReadUsersFromFile();
    }
    #endregion
}
