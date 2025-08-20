namespace SBMS.Domain.Models;

public class BusModel : AuditModel
{
    public int Id { get; set; }
    private string _plate = null!;
    public string Plate
    {
        get => _plate;
        set => _plate = value.ToUpper(); // siempre se guarda en mayúsculas
    }
    public string? Brand { get; set; }
}