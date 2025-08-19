namespace SBMS.Domain.Repositories
{
    public interface IPersonBaseRepository<T> where T : PersonBaseModel
    {
        Task<IList<T>> GetPersonAll();
        Task<T> GetPersonByDNI(int dni);
        Task<ResponseBaseModel> CreatePerson(T personModel);
        Task<ResponseBaseModel> UpdatePerson(T personModel);
        Task<ResponseBaseModel> DeletePerson(int id);
    }
}