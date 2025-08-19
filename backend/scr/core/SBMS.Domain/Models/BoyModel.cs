namespace SBMS.Domain.Models;

public class BoyModel
{
    public int Id { get; set; }

    public int Dni { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    private string _gender = null!;
    public string Gender
    {
        get => _gender;
        set => _gender = value.ToUpper(); // siempre se guarda en mayúsculas
    }

    public int Age { get; set; }
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}