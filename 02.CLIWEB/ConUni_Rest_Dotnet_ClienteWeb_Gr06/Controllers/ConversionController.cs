using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;
using ConUni_Rest_Dotnet_ClienteWeb_Gr06.Models;
using Newtonsoft.Json;
using System.Text;

namespace ConUni_Rest_Dotnet_ClienteWeb_Gr06.Controllers
{
    public class ConversionController : Controller
    {
        private readonly string apiBaseUrl = "https://localhost:44364/api";


        public IActionResult Index()
        {
            return View(new ConversionModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(ConversionModel model)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{apiBaseUrl}/conuni", content);

                if (response.IsSuccessStatusCode)
                {
                    var data = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                    model.Resultado = $"Resultado: {data.resultado}";
                }
                else
                {
                    model.Resultado = "Error en la conversión";
                }
            }
            return View(model);
        }

    }
}
