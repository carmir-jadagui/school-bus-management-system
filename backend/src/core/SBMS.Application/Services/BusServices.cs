namespace SBMS.Application.Services
{
    public class BusServices : IBusServices
    {
        private readonly ILogger<BusServices> _logger;
        private readonly IBusRepository _busRepository;
        private readonly IDriverServices _driverServices;
        private readonly IBoyServices _boyServices;

        public BusServices(ILogger<BusServices> logger,
            IBusRepository busRepository,
            IDriverServices driverServices,
            IBoyServices boyServices)
        {
            _logger = logger;
            _busRepository = busRepository;
            _driverServices = driverServices;
            _boyServices = boyServices;
        }

        public async Task<ResultModel<IList<BusModel>>> GetBusesAll()
        {
            var result = new ResultModel<IList<BusModel>>();

            try
            {
                result.Data = await _busRepository.GetBusesAll();
            }
            catch (SBMSPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.MessageLogger);
            }
            catch (Exception ex)
            {
                result.AddInternalError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<ResultModel<BusModel>> GetBusById(int id)
        {
            var result = new ResultModel<BusModel>();

            try
            {
                result.Data = await _busRepository.GetBusById(id);
            }
            catch (SBMSPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.MessageLogger);
            }
            catch (Exception ex)
            {
                result.AddInternalError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<ResultModel<ResponseBaseModel>> CreateBus(BusModel busModel)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                // Para dale el formato correcto a la patente
                busModel.Plate = NormalizePlate(busModel.Plate);

                // Para validar que la patente no esté siendo usado por otro micro
                var busPlateExist = await _busRepository.GetBusByPlate(busModel.Plate);
                if (busPlateExist != null)
                {
                    throw new InvalidOperationException("Ya existe un micro con esta Patente");
                }

                // Para validar si existen el chofes y chicos asignados
                await ValidateBusAssignments(busModel);

                result.Data = await _busRepository.CreateBus(busModel);
                result.Message = "Micro creado con éxito";
            }
            catch (SBMSInputDataException ex)
            {
                result.AddInputDataError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }
            catch (SBMSPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.MessageLogger);
            }
            catch (Exception ex)
            {
                result.AddInternalError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<ResultModel<ResponseBaseModel>> UpdateBus(BusModel busModel)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                // Para dale el formato correcto a la patente
                busModel.Plate = NormalizePlate(busModel.Plate);

                // Para validar si existen el chofes y chicos asignados
                await ValidateBusAssignments(busModel);

                result.Data = await _busRepository.UpdateBus(busModel);
                result.Message = "Micro modificado con éxito";
            }
            catch (SBMSInputDataException ex)
            {
                result.AddInputDataError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }
            catch (SBMSPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.MessageLogger);
            }
            catch (Exception ex)
            {
                result.AddInternalError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<ResultModel<ResponseBaseModel>> DeleteBus(int id)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                // Valida que no tenga asignaciones
                var busExist = await _busRepository.GetBusById(id);
                if (busExist != null)
                {
                    if (busExist.Driver != null || busExist.Boys.Count > 0)
                    {
                        throw new InvalidOperationException("No se puede eliminar este Micro, ya que tiene asignaciones");
                    }
                }
                
                result.Data = await _busRepository.DeleteBus(id);
                result.Message = "Micro eliminado con éxito";
            }
            catch (SBMSPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.MessageLogger);
            }
            catch (Exception ex)
            {
                result.AddInternalError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        // Para que la patente se guarde con el formato correcto, es decir,
        //con mayúsculas, guión y espacio, de acuerdo al formato correspondiente
        public string NormalizePlate(string plate)
        {
            if (string.IsNullOrWhiteSpace(plate))
                return plate;

            // Eliminamos espacios y guiones
            string cleaned = plate.Replace(" ", "").Replace("-", "").ToUpper();

            return cleaned.Length switch
            {
                6 => // Formato XXX-000
                    $"{cleaned.Substring(0, 3)}-{cleaned.Substring(3, 3)}",

                7 => // Formato XX123XX
                    $"{cleaned.Substring(0, 2)} {cleaned.Substring(2, 3)} {cleaned.Substring(5, 2)}",

                _ => throw new SBMSInputDataException("El valor no cumple los formatos permitidos")
            };
        }

        // Para validar si existen el chofes y chicos asignados
        private async Task ValidateBusAssignments(BusModel busModel)
        {
            // Para validar si tiene asignado chofer y si este existe
            if (busModel.Driver != null)
            {
                var driverExist = await _driverServices.GetDriverById(busModel.Driver.Id);
                if (driverExist.Data == null)
                {
                    throw new InvalidOperationException("El Chofer asignado no existe");
                }
            }

            // Para validar si tiene asignado chico(a)(s) y si existen
            if (busModel.Boys?.Count > 0)
            {
                var boys = await _boyServices.GetBoysAll();

                // Deja boysIds con sólo los ids
                var boysIds = boys.Data.Select(b => b.Id).ToHashSet();

                // Verificar si todos los IDs del busModel están en existingIds
                bool boysExist = busModel.Boys.All(bi => boysIds.Contains(bi.Id));

                if (!boysExist)
                {
                    throw new InvalidOperationException("Algun(os)(as) Chico(a)(s) asignado(s) no existe(n)");
                }
            }
        }
    }
}