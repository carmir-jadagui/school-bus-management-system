namespace SBMS.Persistence.MySQL.Repositories
{
    public class BoyRepository : IPersonBaseRepository<BoyModel>
    {
        private readonly SBMSContext _dbContext;

        public BoyRepository(SBMSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<BoyModel>> GetPersonAll()
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
                                Age = b.Age,
                                CreatedAt = b.CreatedAt,
                                UpdatedAt = b.UpdatedAt
                            };

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: GetBoysAll", ex.Message);
            }
        }

        public async Task<BoyModel> GetPersonById(int id)
        {
            try
            {
                var query = from b in _dbContext.Boys
                            where b.Id == id
                            select new BoyModel
                            {
                                Id = b.Id,
                                Dni = b.Dni,
                                FirstName = b.FirstName,
                                LastName = b.LastName,
                                Gender = b.Gender,
                                Age = b.Age,
                                CreatedAt = b.CreatedAt,
                                UpdatedAt = b.UpdatedAt
                            };

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: GetPersonById", ex.Message);
            }
        }

        public async Task<BoyModel> GetPersonByDNI(int dni)
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
                                Age = b.Age,
                                CreatedAt = b.CreatedAt,
                                UpdatedAt = b.UpdatedAt
                            };

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: GetBoyByDNI", ex.Message);
            }
        }

        public async Task<ResponseBaseModel> CreatePerson(BoyModel boyModel)
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
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _dbContext.Boys.Add(boy);
                await _dbContext.SaveChangesAsync();

                result.Id = boy.Id;

                return result;
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: CreateBoy", ex.Message);
            }
        }

        public async Task<ResponseBaseModel> UpdatePerson(BoyModel boyModel)
        {
            var result = new ResponseBaseModel();

            try
            {
                var boy = await _dbContext.Boys.FirstOrDefaultAsync(x => x.Id == boyModel.Id);

                if (boy != null)
                {
                    boy.FirstName = boyModel.FirstName;
                    boy.LastName = boyModel.LastName;
                    boy.Gender = boyModel.Gender;
                    boy.Age = boyModel.Age;
                    boy.UpdatedAt = DateTime.Now;

                    await _dbContext.SaveChangesAsync();

                    result.Id = boy.Id;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: UpdateBoy", ex.Message);
            }
        }

        public async Task<ResponseBaseModel> DeletePerson(int id)
        {
            var result = new ResponseBaseModel();

            try
            {
                var boy = await _dbContext.Boys.FirstOrDefaultAsync(x => x.Id == id);

                if (boy != null)
                {
                    _dbContext.Boys.Remove(boy);
                    await _dbContext.SaveChangesAsync();

                    result.Id = id;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: DeleteBoy", ex.Message);
            }
        }
    }
}