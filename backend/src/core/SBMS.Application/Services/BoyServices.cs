namespace SBMS.Application.Services
{
    public class BoyServices : IBoyServices
    {
        private readonly ILogger<BoyServices> _logger;
        private readonly IPersonBaseRepository<BoyModel> _boyRepository;

        public BoyServices(ILogger<BoyServices> logger,
            IPersonBaseRepository<BoyModel> boyRepository)
        {
            _logger = logger;
            _boyRepository = boyRepository;
        }

        public async Task<ResultModel<IList<BoyModel>>> GetBoysAll()
        {
            var result = new ResultModel<IList<BoyModel>>();

            try
            {
                result.Data = await _boyRepository.GetPersonAll();
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

        public async Task<ResultModel<BoyModel>> GetBoyById(int id)
        {
            var result = new ResultModel<BoyModel>();

            try
            {
                result.Data = await _boyRepository.GetPersonById(id);
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

        public async Task<ResultModel<BoyModel>> GetBoyByDNI(int dni)
        {
            var result = new ResultModel<BoyModel>();

            try
            {
                result.Data = await _boyRepository.GetPersonByDNI(dni);
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

        public async Task<ResultModel<ResponseBaseModel>> CreateBoy(BoyModel boyModel)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                // Para validar que el DNI no esté siendo usado por otro(a) chico(a)
                var boyDNIExist = await _boyRepository.GetPersonByDNI(boyModel.Dni);
                if (boyDNIExist != null)
                {
                    throw new InvalidOperationException("Ya existe un chico(a) con este DNI");
                }

                result.Data = await _boyRepository.CreatePerson(boyModel);
                result.Message = "Chico(a) creado(a) con éxito";
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

        public async Task<ResultModel<ResponseBaseModel>> UpdateBoy(BoyModel boyModel)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                result.Data = await _boyRepository.UpdatePerson(boyModel);
                result.Message = "Chico(a) modificado(a) con éxito";
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

        public async Task<ResultModel<ResponseBaseModel>> DeleteBoy(int id)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                result.Data = await _boyRepository.DeletePerson(id);
                result.Message = "Chico(a) eliminado(a) con éxito";
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