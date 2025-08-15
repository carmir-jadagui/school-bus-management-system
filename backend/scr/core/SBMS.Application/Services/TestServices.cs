namespace SBMS.Application.Services
{
    public class TestServices : ITestServices
    {
        private readonly ILogger<TestServices> _logger;
        private readonly ITestRepository _testRepository;

        public TestServices(ILogger<TestServices> logger,
            ITestRepository testRepository)
        {
            _logger = logger;
            _testRepository = testRepository;
        }

        public async Task<ResultModel<IList<TestModel>>> GetTestAll()
        {
            var result = new ResultModel<IList<TestModel>>();

            try
            {
                result.Data = await _testRepository.GetTestAll();
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