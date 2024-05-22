using WpfFinal.Services;

namespace WpfFinal.Models.Others;
public class Book : NotifyPropertyChangedService
{
    private string bookName;
    private string authorName;
    private int count;
    private bool status;
    private string image= "../../../Images/DefaultBook.png";
    private string bookGanre;
    public void SetBook(Book b)
    {
        BookGanre=b.BookGanre;
        BookName=b.BookName;
        Count=b.Count;
        Status=b.Status;
        Image=b.Image;
        Availability=b.Availability;
        AuthorName = b.AuthorName;
    }
    public string BookName { get => bookName; set { bookName = value; OnPropertyChanged(); } }
    public string AuthorName { get => authorName; set { authorName = value; OnPropertyChanged(); } }
    public int Count { get => count; set { count = value; OnPropertyChanged(); } }
    public bool Status { get => status; set { status = value; OnPropertyChanged(); } }
    public string BookGanre { get => bookGanre; set { bookGanre = value; OnPropertyChanged(); } }
    public string Image { get => image; set { image = value; OnPropertyChanged(); } }
    public string Availability
    {
        get => (Count>0 ? "Availabile" : "Not Availabile");
        set => OnPropertyChanged(); 
    }
}