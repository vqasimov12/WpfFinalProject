using System.Collections.ObjectModel;
using WpfFinal.Models.Others;
namespace WpfFinal.Models.People;
public class User : Person
{
    private ObservableCollection<BorrowedBook> activeBooks;
    private ObservableCollection<BorrowedBook> allBooks;
    public ObservableCollection<BorrowedBook> AllBooks { get => allBooks; set { allBooks = value; OnPropertyChanged(); } }
    public ObservableCollection<BorrowedBook> ActiveBooks { get => activeBooks; set { activeBooks = value; OnPropertyChanged(); } }
    public User()
    {

    }
    public void Setuser(User other)
    {
        Name=other.Name;
        Email=other.Email;
        Password=other.Password;
        ActiveBooks=other.ActiveBooks;
        AllBooks=other.AllBooks;
    }
    public static bool operator ==(User left, User right)
    {
        return (left?.Name == right?.Name&&left?.Email ==right?.Email &&left?.Password ==right?.Password);
    }
    public static bool operator !=(User left, User right)
    {
        return !(left?.Name == right?.Name && left?.Email == right?.Email && left?.Password == right?.Password);
    }

}
