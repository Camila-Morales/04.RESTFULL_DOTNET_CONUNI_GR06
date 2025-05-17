using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConUni_Rest_Dotnet_Gr06.Services
{
    public class ConversionService
    {
        public double Convertir(string tipo, double valor)
        {
            if (tipo == "cm_a_pulgadas")
                return valor / 2.54;
            else if (tipo == "pulgadas_a_cm")
                return valor * 2.54;
            else
                throw new ArgumentException("Tipo de conversión no válido");
        }
    }
}