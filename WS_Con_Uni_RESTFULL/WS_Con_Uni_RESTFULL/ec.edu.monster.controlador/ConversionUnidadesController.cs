using Microsoft.AspNetCore.Mvc;
using WS_Con_Uni_RESTFULL.ec.edu.monster.modelo;

namespace WS_Con_Uni_RESTFULL.ec.edu.monster.controlador
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversionUnidadesController : ControllerBase
    {
        private readonly ConversionUnidadesModelo _modelo;

        public ConversionUnidadesController()
        {
            _modelo = new ConversionUnidadesModelo();
        }

        [HttpGet("inchesToCm/{inches}")]
        public ActionResult<double> InchesToCm(double inches)
        {
            try
            {
                double resultado = _modelo.ConvertInchesToCm(inches);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("cmToInches/{cm}")]
        public ActionResult<double> CmToInches(double cm)
        {
            try
            {
                double resultado = _modelo.ConvertCmToInches(cm);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
