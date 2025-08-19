namespace SBMS.Domain.Services
{
    public interface IDriverServices
    {
        Task<ResultModel<IList<DriverModel>>> GetDriversAll();
        Task<ResultModel<DriverModel>> GetDriverByDNI(int dni);
        Task<ResultModel<ResponseBaseModel>> CreateDriver(DriverModel driverModel);
        Task<ResultModel<ResponseBaseModel>> UpdateDriver(DriverModel driverModel);
        Task<ResultModel<ResponseBaseModel>> DeleteDriver(int id);
    }
}