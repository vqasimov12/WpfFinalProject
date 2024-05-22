using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using WpfFinal.Models.Others;
using WpfFinal.Models.People;
using WpfFinal.Services;

namespace WpfFinal.DataBases;
public class AppDbContext : NotifyPropertyChangedService
{
    string directoryPath = "../../../DataBases/JsonFiles";
    private ObservableCollection<Book>? books;
    private User currentUser;
    private ObservableCollection<User> users;

    public User CurrentUser { get => currentUser; set { currentUser = value; OnPropertyChanged(); } }
    public AppDbContext()
    {
        ReadAllBooks();
        ReadUsersFromFile();
    }
    #region Users
    public ObservableCollection<User> Users { get => users; set { users = value; OnPropertyChanged(); } }

    public bool NameExists(string name)
    {
        var user = Users.FirstOrDefault(u => u.Name == name);
        if (user is not null)
            return true;
        return false;
    }
    public void AddUser(User user)
    {
        Users.Add(user);
        SaveUsersToFile();
    }
    public void RemoveUser(User user) => Users.Remove(user);
    public void RemoveUser(string name)
    {
        var user = Users.FirstOrDefault(x => x.Name == name);
        if (user is not null)
            Users.Remove(user);
    }
    public void SaveUsersToFile()
    {
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
        var text = JsonSerializer.Serialize(Users, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(directoryPath + "/Users.json", text);
    }
    public void ReadUsersFromFile()
    {
        if (!Directory.Exists(directoryPath))
            return;
        if (!File.Exists(directoryPath + "/Users.json"))
            return;
        var text = File.ReadAllText(directoryPath + "/Users.json");
        Users = JsonSerializer.Deserialize<ObservableCollection<User>>(text)!;
    }
    public bool UserExist(string name) => Users.Any(u => u.Name == name);
    public User? CheckUser(string name, string password)=> Users.FirstOrDefault(u => u.Name == name && u.Password == password)!;

    #endregion

    #region Books
    public ObservableCollection<Book>? Books { get => books; set { books = value; OnPropertyChanged(); } }
    public void AddBook(Book book,int count=1)
    {
        if (BookExists(book.BookName))
        {
            foreach (var b in Books)
                if (b.BookName == book.BookName)
                    b.Count += count;
        }
        else
            Books.Add(book);
    }
    public bool BookExists(string bookName) => Books.Any(b => b.BookName == bookName);
    public void SaveBooks()
    {
        var file = directoryPath + "/Books.json";
        var text = JsonSerializer.Serialize(Books, new JsonSerializerOptions { WriteIndented = true });
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
        File.WriteAllText(file, text);
    }
    public void ReadAllBooks()
    {
        var file = directoryPath + "/Books.json";
        if (File.Exists(file))
        {
            var text = File.ReadAllText(file);
            Books = JsonSerializer.Deserialize<ObservableCollection<Book>>(text)!;
        }




    }
    public Book? SearchBook(string bookName) => Books.FirstOrDefault(b => b.BookName == bookName);
    public void RemoveBook(Book book)
    {
        var b = SearchBook(book.BookName);
        if (b is not null)
            Books.Remove(b);
        else
            MessageBox.Show("Invalid Book name", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
    }


    #endregion


}
