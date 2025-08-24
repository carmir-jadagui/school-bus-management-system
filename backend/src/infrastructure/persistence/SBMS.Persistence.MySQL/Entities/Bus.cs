namespace SBMS.Persistence.MySQL.Entities;

public partial class Bus
{
    public int Id { get; set; }

    public string Plate { get; set; }

    public string Brand { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Boy> Boys { get; set; } = new List<Boy>();

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
}