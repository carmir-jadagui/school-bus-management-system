namespace SBMS.Application.Services
{
    public class DriverServices : IDriverServices
    {
        private readonly ILogger<DriverServices> _logger;
        private readonly IPersonBaseRepository<DriverModel> _driverRepository;

        public DriverServices(ILogger<DriverServices> logger,
            IPersonBaseRepository<DriverModel> driverRepository)
        {
            _logger = logger;
            _driverRepository = driverRepository;
        }

        public async Task<ResultModel<IList<DriverModel>>> GetDriversAll()
        {
            var result = new ResultModel<IList<DriverModel>>();

            try
            {
                result.Data = await _driverRepository.GetPersonAll();
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
                result.Data = await _driverRepository.GetPersonByDNI(dni);
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
                var driverDNIExist = await _driverRepository.GetPersonByDNI(driverModel.Dni);
                if (driverDNIExist != null)
                {
                    throw new InvalidOperationException("A driver with this DNI already exists");
                }

                result.Data = await _driverRepository.CreatePerson(driverModel);
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
                result.Data = await _driverRepository.UpdatePerson(driverModel);
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
                result.Data = await _driverRepository.DeletePerson(id);
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