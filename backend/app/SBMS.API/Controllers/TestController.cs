using Microsoft.AspNetCore.Mvc;

namespace SBMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Api para probar que el back funciona.
        /// Retorna un string hardcodeado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Test")]
        public string GetString()
        {
            var result = "Hola mundo";

            return (result);
        }
    }
}