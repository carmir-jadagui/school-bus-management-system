namespace SBMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly ILogger<DriverController> _logger;
        private readonly IDriverServices _driverServices;

        public DriverController(ILogger<DriverController> logger,
            IDriverServices driverServices)
        {
            _logger = logger;
            _driverServices = driverServices;
        }

        /// <summary>
        /// Api para consultar los choferes.
        /// Retorna un objeto ResultModel con los registros de la tabla Drivers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDriversAll()
        {
            var result = await _driverServices.GetDriversAll();

            return Ok(result);
        }

        /// <summary>
        /// Api para buscar un chofer por su DNI.
        /// Retorna un objeto ResultModel con los datos del chofer solicitado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{dni}")]
        public async Task<IActionResult> GetDriverByDNI(int dni)
        {
            var result = await _driverServices.GetDriverByDNI(dni);

            if (result.Data == null) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Api para agregar un modelo de tipo chofer.
        /// Retorna un objeto ResultModel<ResponseBaseModel> con el id del chofer agregado.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateDriver([FromBody] DriverModel driverModel)
        {
            var result = await _driverServices.CreateDriver(driverModel);

            return Ok(result);
        }

        /// <summary>
        /// Api para modificar el modelo del chofer.
        /// Retorna un objeto ResultModel<ResponseBaseModel> con el id del chofer modificado.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateDriver([FromBody] DriverModel driverModel)
        {
            var result = await _driverServices.UpdateDriver(driverModel);

            if (result.Data != null && result.Data.Id == 0) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Api para eliminar al chofer.
        /// Retorna un objeto ResultModel<ResponseBaseModel> con el id del chofer eliminado.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var result = await _driverServices.DeleteDriver(id);

            if (result.Data != null && result.Data.Id == 0) return NotFound();

            return Ok(result);
        }
    }
}