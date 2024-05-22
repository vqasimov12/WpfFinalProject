using System.Collections.ObjectModel;
using System.Data;
using WpfFinal.DataBases;
using WpfFinal.Models.Others;
using WpfFinal.Services;

namespace WpfFinal.ViewModels.AdminViewModels;
public class AllActiveBooksPageViewModel : BaseViewModel
{
    private ObservableCollection<BorrowedBook> allBooks;
    public ObservableCollection< BorrowedBook> AllBooks { get => allBooks; set { allBooks = value; OnPropertyChanged(); } }
    public AllActiveBooksPageViewModel()
    {
        AllBooks = new();
        Refresh();
    }
    public void Refresh()
    {
        var data = App.Container.GetInstance<AppDbContext>();
        //SaveChangesToFileService.Save();
        //data.ReadUsersFromFile();
        foreach (var u in data.Users)
            foreach (var b in u.ActiveBooks)
                AllBooks.Add(b);
    }

}
