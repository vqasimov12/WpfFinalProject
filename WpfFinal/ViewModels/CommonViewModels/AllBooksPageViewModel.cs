using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfFinal.Commands;
using WpfFinal.DataBases;
using WpfFinal.Models.Others;
using WpfFinal.Services;
using WpfFinal.ViewModels.AdminViewModels;
using WpfFinal.ViewModels.CommonViewModels;
using WpfFinal.Views.Windows.AdminWindows;
using WpfFinal.Views.Windows.CommonWindows;
namespace WpfFinal.ViewModels.UserViewModels;
public class AllBooksPageViewModel : BaseViewModel
{
    private ObservableCollection<Book> books;
    public ObservableCollection<Book> Books { get => books; set { books = value; OnPropertyChanged(); } }
    public AllBooksPageViewModel()
    {
        var data = App.Container.GetInstance<AppDbContext>();
        Books = data.Books;
        ShowDetailsCommand = new RelayCommand(ShowDetails);
        EditBookCommand = new RelayCommand(EditBookCommandEcecute, EditBookCommandCanExecute);
        AddBookCommand = new RelayCommand(AddBookCommandExecute);
        RemoveCommand = new RelayCommand(Remove, EditBookCommandCanExecute);
    }

    #region ShowDetailsCommand
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
    #endregion

    #region EditBookCommand
    public ICommand EditBookCommand { get; set; }

    public bool EditBookCommandCanExecute(object? obj)
    {
        var a = obj as Book;
        return a is not null;
    }
    public void EditBookCommandEcecute(object? obj)
    {
        var book = obj as Book;
        if (book is null) return;
        Book NewBook = new();
        NewBook.SetBook(book);
        var window = App.Container.GetInstance<AddBookWindowView>();
        var datacontext = App.Container.GetInstance<AddBookWindowViewModel>();
        datacontext.Book = NewBook;
        datacontext.ButtonContent = "Edit";
        datacontext.LabelText = "Edit Book";
        window.DataContext = datacontext;
        window.ShowDialog();
        if (datacontext.dialogResult == true)
        {
            var data = App.Container.GetInstance<AppDbContext>();
            data.RemoveBook(book);
            book.SetBook(NewBook);
            data.AddBook(book, book.Count);
            SaveChangesToFileService.Save();
            data.ReadAllBooks();
            Books = data.Books;
        }
    }
    #endregion

    #region AddBookCommand
    public ICommand AddBookCommand { get; set; }
    public void AddBookCommandExecute(object? obj)
    {
        var NewBook = new Book();
        NewBook.Image = "../../../Images/DefaultBook.png";
        var window = App.Container.GetInstance<AddBookWindowView>();
        var datacontext = App.Container.GetInstance<AddBookWindowViewModel>();
        datacontext.Book = NewBook;
        datacontext.ButtonContent = "Add";
        datacontext.LabelText = "Add Book";
        window.DataContext = datacontext;
        window.ShowDialog();
        if (datacontext.dialogResult == true)
        {
            var data = App.Container.GetInstance<AppDbContext>();
            data.AddBook(NewBook, NewBook.Count);
            SaveChangesToFileService.Save();
            data.ReadAllBooks();
            Books = data.Books;
        }
    }
    #endregion

    #region RemoveCommand 
    public ICommand RemoveCommand { get; set; } 
    public void Remove(object? obj)
    {
        var book = obj as Book;
        if (book is null)
            return;
        var data = App.Container.GetInstance<AppDbContext>();
        data.RemoveBook(book);
        SaveChangesToFileService.Save();
    }
    #endregion
}
