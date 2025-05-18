using System.Diagnostics;
using System.Text;
using ConUni_Rest_Dotnet_ClienteWeb_Gr06.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConUni_Rest_Dotnet_ClienteWeb_Gr06.Controllers
{
    public class HomeController : Controller
    {
        private readonly string apiBaseUrl = "https://localhost:44364/api";


        public IActionResult Index()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{apiBaseUrl}/login", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Conversion");
                }

                model.Resultado = response.IsSuccessStatusCode ? "Login exitoso" : "Credenciales inválidas";
            }
            return View(model);
        }
    }
}
