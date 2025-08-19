namespace SBMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusController : ControllerBase
    {
        private readonly ILogger<BusController> _logger;
        private readonly IBusServices _busServices;

        public BusController(ILogger<BusController> logger,
            IBusServices busServices)
        {
            _logger = logger;
            _busServices = busServices;
        }

        /// <summary>
        /// Api para consultar los micros.
        /// Retorna un objeto ResultModel con los registros de la tabla Buses.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetBusesAll()
        {
            var result = await _busServices.GetBusesAll();

            return Ok(result);
        }

        /// <summary>
        /// Api para buscar un micro por su Patente.
        /// Retorna un objeto ResultModel con los datos del micro solicitado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{plate}")]
        public async Task<IActionResult> GetBusByPlate(string plate)
        {
            var result = await _busServices.GetBusByPlate(plate);

            if (result.Data == null) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Api para agregar un modelo de tipo micro.
        /// Retorna un objeto ResultModel<ResponseBaseModel> con el id del micro agregado.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateBus([FromBody] BusModel busModel)
        {
            var result = await _busServices.CreateBus(busModel);

            return Ok(result);
        }

        /// <summary>
        /// Api para modificar el modelo del micro.
        /// Retorna un objeto ResultModel<ResponseBaseModel> con el id del micro modificado.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateBus([FromBody] BusModel busModel)
        {
            var result = await _busServices.UpdateBus(busModel);

            if (result.Data != null && result.Data.Id == 0) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Api para eliminar un micro.
        /// Retorna un objeto ResultModel<ResponseBaseModel> con el id del micro eliminado.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBus(int id)
        {
            var result = await _busServices.DeleteBus(id);

            if (result.Data != null && result.Data.Id == 0) return NotFound();

            return Ok(result);
        }
    }
}