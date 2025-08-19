namespace SBMS.Domain.Repositories
{
    public interface ITestRepository
    {
        Task<IList<TestModel>> GetTestAll();
    }
}