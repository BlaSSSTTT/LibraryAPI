namespace Models;

public class Book
{
    public int Id { get; set; } // Первинний ключ
    public string Author { get; set; }
    public string Title { get; set; } // Назва книги
    public string Genre { get; set; } // Жанр книги
}