using Microsoft.AspNetCore.Mvc;
using WS_Con_Uni_RESTFULL.ec.edu.monster.modelo;

namespace WS_Con_Uni_RESTFULL.ec.edu.monster.controlador
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly LoginModelo _conversor;  
        public LoginController(ILogger<LoginController> logger, LoginModelo conversor)
        {
            _logger = logger; _conversor = conversor; // Inyecta la dependencia del conversor
        }

        [HttpGet("login/{usuario}/{password}")]
        public ActionResult<bool> Login(string usuario, string password)
        {
            try
            {
                bool result = _conversor.login(usuario, password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ingresar"); return BadRequest("Error de Inicio de Sesion");
            }
        }
    }
}
