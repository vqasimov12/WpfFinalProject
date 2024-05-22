using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfFinal.Commands;
using WpfFinal.DataBases;
using WpfFinal.Models.Others;
using WpfFinal.Models.People;
using WpfFinal.Services;
using WpfFinal.ViewModels.CommonViewModels;
using WpfFinal.Views.Windows.CommonWindows;

namespace WpfFinal.ViewModels.UserViewModels;
public class TakeBookPageViewModel : BaseViewModel
{
    private ObservableCollection<Book> books;

    public ObservableCollection<Book> Books { get => books; set { books = value; OnPropertyChanged(); } }
    public void RefreshBooks()
    {
        var data = App.Container.GetInstance<AppDbContext>();
        Books = new();
        foreach (var b in data.Books)
            Books.Add(b);
        for (int i=0;i<Books.Count;i++)
            if (Books[i].Count <= 0)
                Books.Remove(Books[i]);
    }
    public TakeBookPageViewModel()
    {
        TakeCommand = new RelayCommand(TakeCommandExecute, TakeCommandCanExecute);
        ShowDetailsCommand = new RelayCommand(ShowDetails);
        var data = App.Container.GetInstance<AppDbContext>();
        RefreshBooks();
    }
    public ICommand TakeCommand { get; set; }
    public bool TakeCommandCanExecute(object? obj)
    {
        var a = obj as Book;
        return a is not null;
    }
    public void TakeCommandExecute(object? obj)
    {
        var data = App.Container.GetInstance<AppDbContext>();
        var user = data.CurrentUser;
        if (user.ActiveBooks?.Count > 2)
        {
            MessageBox.Show("You have reached max level of 3 books \nPlease first return book", "Warning", MessageBoxButton.OK, MessageBoxImage.Stop);
            return;
        }
        var a = obj as Book;
        if (a is not null)
        {
            a.Count--;
            var borrowb = new BorrowedBook();
            borrowb.Book = a;
            borrowb.IssueDate = DateTime.Now;
            user.ActiveBooks?.Add(borrowb);
            user.AllBooks?.Add(borrowb);
            RefreshBooks();
            SaveChangesToFileService.Save();
        }
    }

    public ICommand ShowDetailsCommand { get; set; }
    public void ShowDetails(object? obj)
    {
        var book = obj as Book;
        if (book is null) return;
        var window = App.Container.GetInstance<ShowBookDetailsWindowView>();
        var datacontext = App.Container.GetInstance<ShowBookDetailsWindowViewModel>();
        datacontext.Book = book;
        window.DataContext = datacontext;
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.ShowDialog();
    }


}
