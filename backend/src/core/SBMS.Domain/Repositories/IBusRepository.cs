namespace SBMS.Domain.Repositories
{
    public interface IBusRepository
    {
        Task<IList<BusModel>> GetBusesAll();
        Task<BusModel> GetBusByPlate(string plate);
        Task<ResponseBaseModel> CreateBus(BusModel busModel);
        Task<ResponseBaseModel> UpdateBus(BusModel busModel);
        Task<ResponseBaseModel> DeleteBus(int id);
    }
}