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
    public class LoginController : ApiController
    {
        private readonly LoginService _loginService = new LoginService();

        [HttpPost]
        [Route("api/login")]
        public IHttpActionResult IniciarSesion(Login model)
        {
            if (model == null)
                return BadRequest("Datos inválidos");

            bool valido = _loginService.ValidarCredenciales(model.Usuario, model.Contrasena);

            if (valido)
                return Ok(new { mensaje = "Login exitoso" });
            else
                return Unauthorized();
        }
    }
}
