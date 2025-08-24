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
                                Driver = b.BusesDriver != null && b.BusesDriver.Drivers != null ? new DriverModel
                                {
                                    Id = b.BusesDriver.DriversId,
                                    FirstName = b.BusesDriver.Drivers.FirstName,
                                    LastName = b.BusesDriver.Drivers.LastName
                                } : null,
                                Boys = b.BusesBoys.Select(bb => new BoyModel
                                {
                                    Id = bb.BoysId,
                                    Dni = bb.Boys.Dni,
                                    FirstName = bb.Boys.FirstName,
                                    LastName = bb.Boys.LastName,
                                    Gender = bb.Boys.Gender,
                                    Age = bb.Boys.Age,
                                }).ToList()
                            };

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: GetBusesAll", ex.Message);
            }
        }

        public async Task<BusModel> GetBusById(int id)
        {
            try
            {
                var query = from b in _dbContext.Buses
                            where b.Id == id
                            select new BusModel
                            {
                                Id = b.Id,
                                Plate = b.Plate,
                                Brand = b.Brand,
                                Driver = b.BusesDriver != null && b.BusesDriver.Drivers != null ? new DriverModel
                                {
                                    Id = b.BusesDriver.DriversId,
                                    FirstName = b.BusesDriver.Drivers.FirstName,
                                    LastName = b.BusesDriver.Drivers.LastName
                                } : null,
                                Boys = b.BusesBoys.Select(bb => new BoyModel
                                {
                                    Id = bb.BoysId,
                                    Dni = bb.Boys.Dni,
                                    FirstName = bb.Boys.FirstName,
                                    LastName = bb.Boys.LastName,
                                    Gender = bb.Boys.Gender,
                                    Age = bb.Boys.Age,
                                }).ToList()
                            };

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: GetBusById", ex.Message);
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
                                Driver = b.BusesDriver != null && b.BusesDriver.Drivers != null ? new DriverModel
                                {
                                    Id = b.BusesDriver.DriversId,
                                    FirstName = b.BusesDriver.Drivers.FirstName,
                                    LastName = b.BusesDriver.Drivers.LastName
                                } : null,
                                Boys = b.BusesBoys.Select(bb => new BoyModel
                                {
                                    Id = bb.BoysId,
                                    Dni = bb.Boys.Dni,
                                    FirstName = bb.Boys.FirstName,
                                    LastName = bb.Boys.LastName,
                                    Gender = bb.Boys.Gender,
                                    Age = bb.Boys.Age,
                                }).ToList()
                            };

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: GetBusByPlate", ex.Message);
            }
        }

        public async Task<ResponseBaseModel> CreateBus(BusModel busModel)
        {
            // se aplica Transaction / Rollback
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            var result = new ResponseBaseModel();

            try
            {
                // Para guardar los datos de micro
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

                // Para guardar la relación entre el micro y el chofer
                if (busModel.Driver != null)
                {
                    var busDriver = new BusesDriver()
                    {
                        BusId = bus.Id,
                        DriversId = busModel.Driver.Id
                    };

                    _dbContext.BusesDrivers.Add(busDriver);
                };

                // Para guardar la relación entre el micro y el(los) chico(a)(s)
                if (busModel.Boys?.Count > 0)
                {
                    foreach (var boy in busModel.Boys)
                    {
                        var busBoy = new BusesBoy()
                        {
                            BusId = bus.Id,
                            BoysId = boy.Id
                        };

                        _dbContext.BusesBoys.Add(busBoy);
                    }
                };

                // Guardar las relaciones
                await _dbContext.SaveChangesAsync();

                // Commit de la transacción
                await transaction.CommitAsync();

                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new SBMSPersistenceException("Persistence Layer Failure: CreateBus", ex.Message);
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
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: UpdateBus", ex.Message);
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
            catch (Exception ex)
            {
                throw new SBMSPersistenceException("Persistence Layer Failure: DeleteBus", ex.Message);
            }
        }
    }
}