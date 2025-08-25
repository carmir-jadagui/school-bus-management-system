namespace SBMS.Persistence.MySQL.Entities;

public partial class BusesBoy
{
    public int BusId { get; set; }

    public int BoysId { get; set; }

    public virtual Boy Boys { get; set; }

    public virtual Bus Bus { get; set; }
}