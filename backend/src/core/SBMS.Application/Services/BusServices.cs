namespace SBMS.Application.Services
{
    public class BusServices : IBusServices
    {
        private readonly ILogger<BusServices> _logger;
        private readonly IBusRepository _busRepository;

        public BusServices(ILogger<BusServices> logger,
            IBusRepository busRepository)
        {
            _logger = logger;
            _busRepository = busRepository;
        }

        public async Task<ResultModel<IList<BusModel>>> GetBusesAll()
        {
            var result = new ResultModel<IList<BusModel>>();

            try
            {
                result.Data = await _busRepository.GetBusesAll();
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

        public async Task<ResultModel<BusModel>> GetBusById(int id)
        {
            var result = new ResultModel<BusModel>();

            try
            {
                result.Data = await _busRepository.GetBusById(id);
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

        public async Task<ResultModel<ResponseBaseModel>> CreateBus(BusModel busModel)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                // Para validar que la patente no esté siendo usado por otro micro
                var busPlateExist = await _busRepository.GetBusByPlate(busModel.Plate);
                if (busPlateExist != null)
                {
                    throw new InvalidOperationException("Ya existe un micro con esta Patente");
                }

                result.Data = await _busRepository.CreateBus(busModel);
                result.Message = "Micro creado con éxito";
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

        public async Task<ResultModel<ResponseBaseModel>> UpdateBus(BusModel busModel)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                result.Data = await _busRepository.UpdateBus(busModel);
                result.Message = "Micro modificado con éxito";
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

        public async Task<ResultModel<ResponseBaseModel>> DeleteBus(int id)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                result.Data = await _busRepository.DeleteBus(id);
                result.Message = "Micro eliminado con éxito";
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