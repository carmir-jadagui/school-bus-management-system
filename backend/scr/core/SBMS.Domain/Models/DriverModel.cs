namespace SBMS.Domain.Models;

public class DriverModel
{
    public int Id { get; set; }

    public int Dni { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Telephone { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}