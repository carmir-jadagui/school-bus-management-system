namespace SBMS.Domain.Services
{
    public interface ITestServices
    {
        Task<ResultModel<IList<TestModel>>> GetTestAll();
    }
}