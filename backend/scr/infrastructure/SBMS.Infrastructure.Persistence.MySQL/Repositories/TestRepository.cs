namespace SBMS.Infrastructure.Persistence.MySQL.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly SBMSContext _dbContext;

        public TestRepository(SBMSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<TestModel>> GetTestAll()
        {
            try
            {
                var query = from t in _dbContext.Tests
                            orderby t.Id
                            select new TestModel
                            {
                                Id = t.Id,
                                Message = t.Message,
                            };

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: GetTestAll");
            }
        }
    }
}