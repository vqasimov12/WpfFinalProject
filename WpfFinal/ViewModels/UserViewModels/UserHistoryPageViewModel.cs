using WpfFinal.DataBases;
using WpfFinal.Models.Others;
using WpfFinal.Models.People;

namespace WpfFinal.ViewModels.UserViewModels;
public class UserHistoryPageViewModel : BaseViewModel
{
    private User user;
    public User User { get => user; set { user = value; OnPropertyChanged(); } }
    public UserHistoryPageViewModel()
    {
        
    }
}
