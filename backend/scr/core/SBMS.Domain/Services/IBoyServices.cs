namespace SBMS.Domain.Services
{
    public interface IBoyServices
    {
        Task<ResultModel<IList<BoyModel>>> GetBoysAll();
        Task<ResultModel<ResponseBaseModel>> AddBoy(BoyModel boyModel);
    }
}