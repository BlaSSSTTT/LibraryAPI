using Microsoft.EntityFrameworkCore;
using Models;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Reader> Readers { get; set; }
    public LibraryContext(){}
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=LibraryDBKPZ4;Trusted_Connection=True;");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Налаштування для Book
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Title).IsRequired();
            entity.Property(b => b.Author).IsRequired();
            entity.Property(b => b.Genre);

        });

        // Налаштування для Loan
        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.LoanDate).IsRequired();
            entity.Property(l => l.ReturnDate);

            // Обов'язковий зв'язок з Book
            entity.HasOne(l => l.Book)
                  .WithMany()
                  .HasForeignKey(l => l.BookId)
                  .IsRequired();

            // Обов'язковий зв'язок з Reader
            entity.HasOne(l => l.Reader)
                  .WithMany()
                  .HasForeignKey(l => l.ReaderId)
                  .IsRequired();
        });

        // Налаштування для Reader
        modelBuilder.Entity<Reader>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.FullName).IsRequired();
            entity.Property(r => r.Email);
        });
    }
}
