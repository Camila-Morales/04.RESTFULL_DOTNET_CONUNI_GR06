using ConUni_Rest_Dotnet_Gr06.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConUni_Rest_Dotnet_Gr06.Models;

namespace ConUni_Rest_Dotnet_Gr06.Controllers
{
    public class ConversionController : ApiController
    {
        private readonly ConversionService _conversionService = new ConversionService();

        [HttpPost]
        [Route("api/conuni")]
        public IHttpActionResult Convertir(Conversion model)
        {
            if (model == null || string.IsNullOrEmpty(model.Tipo))
                return BadRequest("Datos incompletos");

            try
            {
                double resultado = _conversionService.Convertir(model.Tipo, model.Valor);
                return Ok(new { resultado });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
