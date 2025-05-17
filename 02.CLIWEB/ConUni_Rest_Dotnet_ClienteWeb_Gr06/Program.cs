using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient(); // Para consumir APIs externas
builder.Services.AddSingleton(builder.Configuration); // Acceder a la config en cualquier parte


var app = builder.Build();

Console.WriteLine($"ContentRootPath: {app.Environment.ContentRootPath}");
Console.WriteLine($"WebRootPath: {app.Environment.WebRootPath}");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Requiere HTTPS en producci�n
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Para cargar im�genes, CSS, JS, etc.

app.UseRouting();
app.UseAuthorization(); // Solo si est�s usando autenticaci�n

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();