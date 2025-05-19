using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConUni_Rest_Dotnet_ClienteWeb_Gr06.ec.edu.monster.models;
using Newtonsoft.Json;

namespace ConUni_Rest_Dotnet_ClienteWeb_Gr06
{
    class Program
    {
        private static readonly HttpClientHandler handler = new HttpClientHandler
        {
            // Ignora la validación del certificado (solo para desarrollo)
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };

        private static readonly HttpClient client = new HttpClient(handler);
        private static bool isAuthenticated = false;

        static async Task Main(string[] args)
        {
            client.BaseAddress = new Uri("https://localhost:44364/"); // Ajusta el puerto según tu servidor

            Console.WriteLine("Cliente de ConUni REST - Iniciando...");
            await LoginProcess();
            if (isAuthenticated)
            {
                await ConversionProcess();
            }
            Console.WriteLine("Proceso terminado. Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }

        static async Task LoginProcess()
        {
            Console.WriteLine("\n--- Proceso de Login ---");
            Console.Write("Usuario: ");
            string usuario = Console.ReadLine();
            Console.Write("Contraseña: ");
            string contrasena = Console.ReadLine();

            var loginModel = new Login { Usuario = usuario, Contrasena = contrasena };
            var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("api/login", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Login exitoso!");
                    isAuthenticated = true;
                }
                else
                {
                    Console.WriteLine($"Login fallido: {responseContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error de conexión: {ex.Message}");
                Console.WriteLine("Asegúrate de que el servidor esté corriendo y el puerto sea correcto.");
            }
        }

        static async Task ConversionProcess()
        {
            Console.WriteLine("\n--- Proceso de Conversión ---");
            Console.Write("Valor a convertir: ");
            if (!double.TryParse(Console.ReadLine(), out double valor))
            {
                Console.WriteLine("Valor inválido. Debe ser un número.");
                return;
            }

            Console.Write("Tipo de conversión (cm_a_pulgadas o pulgadas_a_cm): ");
            string tipo = Console.ReadLine();

            var conversionModel = new Conversion { Valor = valor, Tipo = tipo };
            var content = new StringContent(JsonConvert.SerializeObject(conversionModel), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/conuni", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                dynamic result = JsonConvert.DeserializeObject(responseContent);
                Console.WriteLine($"Resultado: {result.resultado}");
            }
            else
            {
                Console.WriteLine($"Error: {responseContent}");
            }
        }
    }
}
