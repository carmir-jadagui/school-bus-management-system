namespace SBMS.Persistence.MySQL.Entities;

public partial class Bus
{
    public int Id { get; set; }

    public string Plate { get; set; }

    public string Brand { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<BusesBoy> BusesBoys { get; set; } = new List<BusesBoy>();

    public virtual BusesDriver BusesDriver { get; set; }
}