namespace SBMS.Persistence.MySQL.Entities;

public partial class BusesDriver
{
    public int BusId { get; set; }

    public int DriversId { get; set; }

    public virtual Bus Bus { get; set; }

    public virtual Driver Drivers { get; set; }
}