using WpfFinal.Services;

namespace WpfFinal.Models.Others;
public class BorrowedBook:NotifyPropertyChangedService
{
    private Book book;
    private DateTime issueDate;
    private DateTime returnDate;

    public Book Book { get => book; set { book = value; OnPropertyChanged(); } }
    public DateTime IssueDate { get => issueDate; set { issueDate = value;OnPropertyChanged(); }}
    public DateTime ReturnDate { get => returnDate; set { returnDate = value;OnPropertyChanged(); }}
    public BorrowedBook()
    {
        IssueDate = DateTime.Now;
    }
}
