namespace SBMS.Domain.Repositories
{
    public interface IDriverRepository
    {
        Task<IList<DriverModel>> GetDriversAll();
        Task<DriverModel> GetDriverByDNI(int dni);
        Task<ResponseBaseModel> CreateDriver(DriverModel driverModel);
        Task<ResponseBaseModel> UpdateDriver(DriverModel driverModel);
        Task<ResponseBaseModel> DeleteDriver(int id);
    }
}