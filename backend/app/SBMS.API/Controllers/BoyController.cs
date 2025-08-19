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
        /// Api para buscar un chico(a) por su DNI.
        /// Retorna un objeto ResultModel con los datos del chico(a) solicitado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{dni}")]
        public async Task<IActionResult> GetBoyByDNI(int dni)
        {
            var result = await _boyServices.GetBoyByDNI(dni);

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
    }
}