namespace Models;

public class Loan
{
    public int Id { get; set; } // Первинний ключ
    public DateTime LoanDate { get; set; } // Дата позики
    public DateTime? ReturnDate { get; set; } // Дата повернення (може бути null, якщо книга ще не повернена)

    // Зв’язок з книгою (зовнішній ключ)
    public int BookId { get; set; }
    public Book Book { get; set; } // Навігаційна властивість до книги

    // Зв’язок з читачем (зовнішній ключ)
    public int ReaderId { get; set; }
    public Reader Reader { get; set; } // Навігаційна властивість до читача
}