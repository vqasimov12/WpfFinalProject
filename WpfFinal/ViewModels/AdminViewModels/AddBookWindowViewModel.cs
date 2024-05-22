using Microsoft.Win32;
using System.IO;
using System.Windows.Input;
using WpfFinal.Commands;
using WpfFinal.DataBases;
using WpfFinal.Models.Others;
using WpfFinal.Services;

namespace WpfFinal.ViewModels.AdminViewModels;
public class AddBookWindowViewModel:BaseViewModel
{
    public bool dialogResult { get; set; } = false; 
    private Book book;
    private string buttonContent;
    private string labelText;

    public Book Book { get => book; set { book = value; OnPropertyChanged(); } }
    public string ButtonContent { get => buttonContent; set { buttonContent = value; OnPropertyChanged(); }}
    public string LabelText { get => labelText; set { labelText = value; OnPropertyChanged(); }}
    public AddBookWindowViewModel()
    {
        SelectImageCommand = new RelayCommand(SelectImage);
        AddBookCommand = new RelayCommand(AddBookCommandExecute,AddBookCommandCanExeCute);
    }

    #region SelectImageCommand
    public ICommand SelectImageCommand { get; set; }
    public void SelectImage(object?obj)
    {
        var dialog = new OpenFileDialog();
        dialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

        if (dialog.ShowDialog() == true)
        {
            var originalFileName = dialog.FileName;
            using FileStream originalFile = new FileStream(originalFileName, FileMode.Open);
            var copyFileName =
                Directory.GetCurrentDirectory().Split("\\bin")[0]
                + "\\Images\\"
                + Guid.NewGuid().ToString().Replace("-", "") + Random.Shared.Next(10000, 1000000000) + originalFileName.Split("\\").Last();

            using FileStream copyFile = new FileStream(copyFileName, FileMode.Create);
            originalFile.CopyTo(copyFile);
            copyFile.Close();

            Book.Image= copyFileName;
        }


    }
    #endregion
    
    #region AddBookCommand
    public ICommand AddBookCommand { get; set; }

    public bool AddBookCommandCanExeCute(object?obj)
    {
        return Book.AuthorName?.Length >= 3 && Book.BookGanre?.Length >= 3 && Book.BookName?.Length >= 3&&Book.Count>0;
    }
    public void AddBookCommandExecute(object?obj)
    {
        dialogResult = true;
        Close(obj);
    }
    #endregion
}
