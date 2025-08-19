namespace SBMS.Persistence.MySQL.Repositories
{
    public class BusRepository : IBusRepository
    {
        private readonly SBMSContext _dbContext;

        public BusRepository(SBMSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<BusModel>> GetBusesAll()
        {
            try
            {
                var query = from b in _dbContext.Buses
                            orderby b.Id
                            select new BusModel
                            {
                                Id = b.Id,
                                Plate = b.Plate,
                                Brand = b.Brand,
                                CreatedAt = b.CreatedAt,
                                UpdatedAt = b.UpdatedAt
                            };

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: GetBusesAll");
            }
        }

        public async Task<BusModel> GetBusByPlate(string plate)
        {
            try
            {
                var query = from b in _dbContext.Buses
                            where b.Plate == plate
                            select new BusModel
                            {
                                Id = b.Id,
                                Plate = b.Plate,
                                Brand = b.Brand,
                                CreatedAt = b.CreatedAt,
                                UpdatedAt = b.UpdatedAt
                            };

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: GetBusByPlate");
            }
        }

        public async Task<ResponseBaseModel> CreateBus(BusModel busModel)
        {
            var result = new ResponseBaseModel();

            try
            {
                var bus = new Bus()
                {
                    Plate = busModel.Plate,
                    Brand = busModel.Brand,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _dbContext.Buses.Add(bus);
                await _dbContext.SaveChangesAsync();

                result.Id = bus.Id;

                return result;
            }
            catch (Exception)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: CreateBus");
            }
        }

        public async Task<ResponseBaseModel> UpdateBus(BusModel busModel)
        {
            var result = new ResponseBaseModel();

            try
            {
                var bus = await _dbContext.Buses.FirstOrDefaultAsync(x => x.Id == busModel.Id);

                if (bus != null)
                {
                    bus.Brand = busModel.Brand;
                    bus.UpdatedAt = DateTime.Now;

                    await _dbContext.SaveChangesAsync();

                    result.Id = bus.Id;
                }

                return result;
            }
            catch (Exception)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: UpdateBus");
            }
        }

        public async Task<ResponseBaseModel> DeleteBus(int id)
        {
            var result = new ResponseBaseModel();

            try
            {
                var bus = await _dbContext.Buses.FirstOrDefaultAsync(x => x.Id == id);

                if (bus != null)
                {
                    _dbContext.Buses.Remove(bus);
                    await _dbContext.SaveChangesAsync();

                    result.Id = id;
                }

                return result;
            }
            catch (Exception)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: DeleteBus");
            }
        }
    }
}