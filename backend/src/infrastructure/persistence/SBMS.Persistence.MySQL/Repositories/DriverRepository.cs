namespace SBMS.Persistence.MySQL.Repositories
{
    public class DriverRepository : IPersonBaseRepository<DriverModel>
    {
        private readonly SBMSContext _dbContext;

        public DriverRepository(SBMSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<DriverModel>> GetPersonAll()
        {
            try
            {
                var query = from b in _dbContext.Drivers
                            orderby b.Id
                            select new DriverModel
                            {
                                Id = b.Id,
                                Dni = b.Dni,
                                FirstName = b.FirstName,
                                LastName = b.LastName,
                                Telephone = b.Telephone,
                                CreatedAt = b.CreatedAt,
                                UpdatedAt = b.UpdatedAt
                            };

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: GetDriversAll", ex.Message);
            }
        }

        public async Task<DriverModel> GetPersonById(int id)
        {
            try
            {
                var query = from b in _dbContext.Drivers
                            where b.Id == id
                            select new DriverModel
                            {
                                Id = b.Id,
                                Dni = b.Dni,
                                FirstName = b.FirstName,
                                LastName = b.LastName,
                                Telephone = b.Telephone,
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

        public async Task<DriverModel> GetPersonByDNI(int dni)
        {
            try
            {
                var query = from b in _dbContext.Drivers
                            where b.Dni == dni
                            select new DriverModel
                            {
                                Id = b.Id,
                                Dni = b.Dni,
                                FirstName = b.FirstName,
                                LastName = b.LastName,
                                Telephone = b.Telephone,
                                CreatedAt = b.CreatedAt,
                                UpdatedAt = b.UpdatedAt
                            };

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: GetDriverByDNI", ex.Message);
            }
        }

        public async Task<ResponseBaseModel> CreatePerson(DriverModel driverModel)
        {
            var result = new ResponseBaseModel();

            try
            {
                var driver = new Driver()
                {
                    Dni = driverModel.Dni,
                    FirstName = driverModel.FirstName,
                    LastName = driverModel.LastName,
                    Telephone = driverModel.Telephone,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _dbContext.Drivers.Add(driver);
                await _dbContext.SaveChangesAsync();

                result.Id = driver.Id;

                return result;
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: CreateDriver", ex.Message);
            }
        }

        public async Task<ResponseBaseModel> UpdatePerson(DriverModel driverModel)
        {
            var result = new ResponseBaseModel();

            try
            {
                var driver = await _dbContext.Drivers.FirstOrDefaultAsync(x => x.Id == driverModel.Id);

                if (driver != null)
                {
                    driver.FirstName = driverModel.FirstName;
                    driver.LastName = driverModel.LastName;
                    driver.Telephone = driverModel.Telephone;
                    driver.UpdatedAt = DateTime.Now;

                    await _dbContext.SaveChangesAsync();

                    result.Id = driver.Id;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: UpdateDriver", ex.Message);
            }
        }

        public async Task<ResponseBaseModel> DeletePerson(int id)
        {
            var result = new ResponseBaseModel();

            try
            {
                var driver = await _dbContext.Drivers.FirstOrDefaultAsync(x => x.Id == id);

                if (driver != null)
                {
                    _dbContext.Drivers.Remove(driver);
                    await _dbContext.SaveChangesAsync();

                    result.Id = id;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: DeleteDriver", ex.Message);
            }
        }
    }
}