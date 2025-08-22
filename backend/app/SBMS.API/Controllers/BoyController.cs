namespace SBMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoyController : ControllerBase
    {
        private readonly ILogger<BoyController> _logger;
        private readonly IBoyServices _boyServices;

        public BoyController(ILogger<BoyController> logger,
            IBoyServices boyServices)
        {
            _logger = logger;
            _boyServices = boyServices;
        }

        /// <summary>
        /// Api para consultar los chicos(as).
        /// Retorna un objeto ResultModel con los registros de la tabla Boys.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetBoysAll()
        {
            var result = await _boyServices.GetBoysAll();

            return Ok(result);
        }

        /// <summary>
        /// Api para buscar un chico(a) por su ID.
        /// Retorna un objeto ResultModel con los datos del chico(a) solicitado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoyById(int id)
        {
            var result = await _boyServices.GetBoyById(id);

            if (result.Data == null) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Api para agregar un modelo de tipo chico(a).
        /// Retorna un objeto ResultModel<ResponseBaseModel> con el id del chico(a) agregado.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateBoy([FromBody] BoyModel boyModel)
        {
            var result = await _boyServices.CreateBoy(boyModel);

            return Ok(result);
        }

        /// <summary>
        /// Api para modificar el modelo del chico(a).
        /// Retorna un objeto ResultModel<ResponseBaseModel> con el id del chico(a) modificado.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateBoy([FromBody] BoyModel boyModel)
        {
            var result = await _boyServices.UpdateBoy(boyModel);

            if (result.Data != null && result.Data.Id == 0) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Api para eliminar al(a la) chico(a).
        /// Retorna un objeto ResultModel<ResponseBaseModel> con el id del chico(a) eliminado.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoy(int id)
        {
            var result = await _boyServices.DeleteBoy(id);

            if (result.Data != null && result.Data.Id == 0) return NotFound();

            return Ok(result);
        }
    }
}