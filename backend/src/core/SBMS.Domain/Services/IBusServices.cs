namespace SBMS.Domain.Services
{
    public interface IBusServices
    {
        Task<ResultModel<IList<BusModel>>> GetBusesAll();
        Task<ResultModel<BusModel>> GetBusByPlate(string plate);
        Task<ResultModel<ResponseBaseModel>> CreateBus(BusModel busModel);
        Task<ResultModel<ResponseBaseModel>> UpdateBus(BusModel busModel);
        Task<ResultModel<ResponseBaseModel>> DeleteBus(int id);
    }
}