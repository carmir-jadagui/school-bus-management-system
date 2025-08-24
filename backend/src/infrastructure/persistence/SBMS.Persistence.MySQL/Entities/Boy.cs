namespace SBMS.Persistence.MySQL.Entities;

public partial class Boy
{
    public int Id { get; set; }

    public int Dni { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Gender { get; set; }

    public int Age { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Bus> Buses { get; set; } = new List<Bus>();
}
