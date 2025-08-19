namespace SBMS.Domain.Repositories
{
    public interface IBoyRepository
    {
        Task<IList<BoyModel>> GetBoysAll();
        Task<ResponseBaseModel> AddBoy(BoyModel boyModel);
    }
}