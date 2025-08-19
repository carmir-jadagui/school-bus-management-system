namespace SBMS.Domain.Repositories
{
    public interface IBoyRepository
    {
        Task<IList<BoyModel>> GetBoysAll();
        Task<BoyModel> GetBoyByDNI(int dni);
        Task<ResponseBaseModel> CreateBoy(BoyModel boyModel);
        Task<ResponseBaseModel> UpdateBoy(BoyModel boyModel);
    }
}