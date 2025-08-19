namespace SBMS.Infrastructure.Persistence.MySQL.Entities;

public partial class Bus
{
    public int Id { get; set; }

    public string Plate { get; set; } = null!;

    public string? Brand { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}