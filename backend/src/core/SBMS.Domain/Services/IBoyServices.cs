namespace SBMS.Domain.Services
{
    public interface IBoyServices
    {
        Task<ResultModel<IList<BoyModel>>> GetBoysAll();
        Task<ResultModel<BoyModel>> GetBoyById(int id);
        Task<ResultModel<ResponseBaseModel>> CreateBoy(BoyModel boyModel);
        Task<ResultModel<ResponseBaseModel>> UpdateBoy(BoyModel boyModel);
        Task<ResultModel<ResponseBaseModel>> DeleteBoy(int id);
    }
}