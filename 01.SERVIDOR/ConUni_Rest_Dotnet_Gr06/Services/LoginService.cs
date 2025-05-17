using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConUni_Rest_Dotnet_Gr06.Services
{
    public class LoginService
    {
        public bool ValidarCredenciales(string usuario, string clave)
        {
            return usuario == "MONSTER" && clave == "MONSTER9";
        }
    }
}