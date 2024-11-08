namespace LibrariAPI.ViewModels;

public class LoanViewModel
{
    public int Id { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int BookId { get; set; }
    public int ReaderId { get; set; }
}