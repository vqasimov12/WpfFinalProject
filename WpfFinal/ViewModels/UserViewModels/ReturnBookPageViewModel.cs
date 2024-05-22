using System.IO;
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
public class ReturnBookPageViewModel : BaseViewModel
{
    private User user ;
    public User User { get => user; set { user = value; OnPropertyChanged(); } }
    public ReturnBookPageViewModel()
    {
        ReturnCommand = new RelayCommand(ReturnCommandExecute, ReturnCommandCanExecute);
        ShowDetailsCommand = new RelayCommand(ShowDetails);
    }

    #region ReturnCommand
    public ICommand ReturnCommand { get; set; }
    public bool ReturnCommandCanExecute(object? obj)
    {
        var a = obj as BorrowedBook;
        return a is not null;
    }
    public void ReturnCommandExecute(object? obj)
    {
        var a = obj as BorrowedBook;
        if (a is not null)
        {
            a.ReturnDate = DateTime.Now;
            var data = App.Container.GetInstance<AppDbContext>();
            //a.Book.Count++;
            data.AddBook(a.Book);
            User.ActiveBooks.Remove(a);
            MessageBox.Show("Book returned successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            SaveChangesToFileService.Save();
        }
    }
    #endregion

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
        window.Show();
    }

}
