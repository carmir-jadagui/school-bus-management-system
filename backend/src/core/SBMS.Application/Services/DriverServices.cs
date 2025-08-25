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
                _logger.LogError(ex, ex.MessageLogger);
            }
            catch (Exception ex)
            {
                result.AddInternalError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<ResultModel<DriverModel>> GetDriverById(int id)
        {
            var result = new ResultModel<DriverModel>();

            try
            {
                result.Data = await _driverRepository.GetPersonById(id);
            }
            catch (SBMSPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.MessageLogger);
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
                _logger.LogError(ex, ex.MessageLogger);
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
                    throw new InvalidOperationException("Ya existe un chofer con este DNI");
                }

                result.Data = await _driverRepository.CreatePerson(driverModel);
                result.Message = "Chofer creado con éxito";
            }
            catch (SBMSPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.MessageLogger);
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
                result.Message = "Chofer modificado con éxito";
            }
            catch (SBMSPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.MessageLogger);
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
                result.Message = "Chofer eliminado con éxito";
            }
            catch (SBMSPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.MessageLogger);
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