namespace SBMS.Infrastructure.Persistence.MySQL.Entities;

public partial class Test
{
    public int Id { get; set; }

    public string Message { get; set; } = null!;
}