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
        /// Api para consultar los chicos.
        /// Retorna un objeto ResultModel con los registros de la tabla Boys.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBoysAll")]
        public async Task<IActionResult> GetBoysAll()
        {
            var result = await _boyServices.GetBoysAll();

            return Ok(result);
        }

        /// <summary>
        /// Api para agregar un modelo de tipo chico.
        /// Retorna un objeto ResultModel con los registros de la tabla Boys.
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddBoy")]
        public async Task<IActionResult> AddBoy([FromBody] BoyModel boyModel)
        {
            var result = await _boyServices.AddBoy(boyModel);

            return Ok(result);
        }
    }
}