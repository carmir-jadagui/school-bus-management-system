namespace SBMS.Domain.Services
{
    public interface IBoyServices
    {
        Task<ResultModel<IList<BoyModel>>> GetBoysAll();
        Task<ResultModel<BoyModel>> GetBoyByDNI(int dni);
        Task<ResultModel<ResponseBaseModel>> AddBoy(BoyModel boyModel);
    }
}