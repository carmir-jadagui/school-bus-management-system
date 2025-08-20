namespace SBMS.Domain.Models
{
    public class PersonBaseModel : AuditModel
    {
        public int Id { get; set; }
        public int Dni { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
    }
}