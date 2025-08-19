namespace SBMS.Application.Services
{
    public class BoyServices : IBoyServices
    {
        private readonly ILogger<BoyServices> _logger;
        private readonly IBoyRepository _boyRepository;

        public BoyServices(ILogger<BoyServices> logger,
            IBoyRepository boyRepository)
        {
            _logger = logger;
            _boyRepository = boyRepository;
        }

        public async Task<ResultModel<IList<BoyModel>>> GetBoysAll()
        {
            var result = new ResultModel<IList<BoyModel>>();

            try
            {
                result.Data = await _boyRepository.GetBoysAll();
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
                result.Data = await _boyRepository.GetBoyByDNI(dni);
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
                var boyDNIExist = await _boyRepository.GetBoyByDNI(boyModel.Dni);
                if (boyDNIExist != null)
                {
                    throw new InvalidOperationException("A boy with this DNI already exists");
                }

                result.Data = await _boyRepository.CreateBoy(boyModel);
                result.Message = "Boy added successfully";
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
                result.Data = await _boyRepository.UpdateBoy(boyModel);
                result.Message = "Boy updated successfully";
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
                result.Data = await _boyRepository.DeleteBoy(id);
                result.Message = "Boy deleted successfully";
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