namespace SBMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly ITestServices _testServices;

        public TestController(ILogger<TestController> logger,
            ITestServices testServices)
        {
            _logger = logger;
            _testServices = testServices;
        }

        /// <summary>
        /// Api para probar que el back funciona.
        /// Retorna un string hardcodeado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("TestBackEnd")]
        public string GetString()
        {
            var result = "Hola mundo";

            return (result);
        }

        /// <summary>
        /// Api para probar la conexión con la BD.
        /// Retorna un objeto ResulModel con los registros de la tabla Test.
        /// </summary>
        /// <returns></returns>
        [HttpGet("TestBD")]
        public async Task<IActionResult> GetTestAll()
        {
            var result = await _testServices.GetTestAll();

            return Ok(result);
        }
    }
}