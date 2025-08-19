namespace SBMS.Domain.Repositories
{
    public interface IBoyRepository
    {
        Task<IList<BoyModel>> GetBoysAll();
        Task<BoyModel> GetBoyByDNI(int dni);
        Task<ResponseBaseModel> AddBoy(BoyModel boyModel);
    }
}