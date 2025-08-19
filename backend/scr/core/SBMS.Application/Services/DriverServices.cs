using SBMS.Domain.Models;

namespace SBMS.Application.Services
{
    public class DriverServices : IDriverServices
    {
        private readonly ILogger<DriverServices> _logger;
        private readonly IDriverRepository _driverRepository;

        public DriverServices(ILogger<DriverServices> logger,
            IDriverRepository driverRepository)
        {
            _logger = logger;
            _driverRepository = driverRepository;
        }

        public async Task<ResultModel<IList<DriverModel>>> GetDriversAll()
        {
            var result = new ResultModel<IList<DriverModel>>();

            try
            {
                result.Data = await _driverRepository.GetDriversAll();
            }
            catch (SBMSPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                result.AddInternalError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<ResultModel<DriverModel>> GetDriverByDNI(int dni)
        {
            var result = new ResultModel<DriverModel>();

            try
            {
                result.Data = await _driverRepository.GetDriverByDNI(dni);
            }
            catch (SBMSPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                result.AddInternalError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<ResultModel<ResponseBaseModel>> CreateDriver(DriverModel driverModel)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                // Para validar que el DNI no esté siendo usado por chofer
                var driverDNIExist = await _driverRepository.GetDriverByDNI(driverModel.Dni);
                if (driverDNIExist != null)
                {
                    throw new InvalidOperationException("A driver with this DNI already exists");
                }

                result.Data = await _driverRepository.CreateDriver(driverModel);
                result.Message = "Driver added successfully";
            }
            catch (SBMSPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                result.AddInternalError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<ResultModel<ResponseBaseModel>> UpdateDriver(DriverModel driverModel)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                result.Data = await _driverRepository.UpdateDriver(driverModel);
                result.Message = "Driver updated successfully";
            }
            catch (SBMSPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                result.AddInternalError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<ResultModel<ResponseBaseModel>> DeleteDriver(int id)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                result.Data = await _driverRepository.DeleteDriver(id);
                result.Message = "Driver deleted successfully";
            }
            catch (SBMSPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                result.AddInternalError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }
    }
}