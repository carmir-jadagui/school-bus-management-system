namespace SBMS.Persistence.MySQL.Entities;

public partial class Driver
{
    public int Id { get; set; }

    public int Dni { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Telephone { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual BusesDriver BusesDriver { get; set; }
}