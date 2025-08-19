using SBMS.Base.Models;

namespace SBMS.Infrastructure.Persistence.MySQL.Repositories
{
    public class BoyRepository : IBoyRepository
    {
        private readonly SBMSContext _dbContext;

        public BoyRepository(SBMSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<BoyModel>> GetBoysAll()
        {
            try
            {
                var query = from b in _dbContext.Boys
                            orderby b.Id
                            select new BoyModel
                            {
                                Id = b.Id,
                                Dni = b.Dni,
                                FirstName = b.FirstName,
                                LastName = b.LastName,
                                Gender = b.Gender,
                                Age = b.Age
                            };

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: GetBoysAll");
            }
        }

        public async Task<BoyModel> GetBoyByDNI(int dni)
        {
            try
            {
                var query = from b in _dbContext.Boys
                            where b.Dni == dni
                            select new BoyModel
                            {
                                Id = b.Id,
                                Dni = b.Dni,
                                FirstName = b.FirstName,
                                LastName = b.LastName,
                                Gender = b.Gender,
                                Age = b.Age
                            };

                var result = await query.FirstOrDefaultAsync();

                if (result == null)
                {
                    throw new SBMSPersistenceException("No se encontró el DNI");
                }

                return result;
            }
            catch (SBMSPersistenceException ex)
            {
                throw new SBMSPersistenceException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: GetBoyByDNI");
            }
        }

        public async Task<ResponseBaseModel> AddBoy(BoyModel boyModel)
        {
            var result = new ResponseBaseModel();

            try
            {
                var boy = new Boy()
                {
                    Dni = boyModel.Dni,
                    FirstName = boyModel.FirstName,
                    LastName = boyModel.LastName,
                    Gender = boyModel.Gender,
                    Age = boyModel.Age,
                    CreatedAt = boyModel.CreatedAt,
                    UpdatedAt = boyModel.UpdatedAt
                };

                _dbContext.Boys.Add(boy);
                await _dbContext.SaveChangesAsync();

                result.Id = boy.Id;

                return result;
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: AddBoy");
            }
        }
    }
}