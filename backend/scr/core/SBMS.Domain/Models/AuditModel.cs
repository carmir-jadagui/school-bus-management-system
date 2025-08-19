namespace SBMS.Domain.Models
{
    public class AuditModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}