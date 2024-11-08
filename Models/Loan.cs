namespace Models;

public class Loan
{
    public int Id { get; set; } // ��������� ����
    public DateTime LoanDate { get; set; } // ���� ������
    public DateTime? ReturnDate { get; set; } // ���� ���������� (���� ���� null, ���� ����� �� �� ���������)

    // ������ � ������ (������� ����)
    public int BookId { get; set; }
    public Book Book { get; set; } // ���������� ���������� �� �����

    // ������ � ������� (������� ����)
    public int ReaderId { get; set; }
    public Reader Reader { get; set; } // ���������� ���������� �� ������
}