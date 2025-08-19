namespace SBMS.Domain.Models;

public class BusModel
{
    public int Id { get; set; }

    private string _plate = null!;
    public string Plate
    {
        get => _plate;
        set => _plate = value.ToUpper(); // siempre se guarda en mayúsculas
    }
    public string? Brand { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}