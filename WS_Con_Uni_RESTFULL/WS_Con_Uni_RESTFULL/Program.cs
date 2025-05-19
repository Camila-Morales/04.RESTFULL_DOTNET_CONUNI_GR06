using Microsoft.Extensions.FileProviders;
using WS_Con_Uni_RESTFULL.ec.edu.monster.modelo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ConversionUnidadesModelo>();
builder.Services.AddTransient<LoginModelo>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configura `UseStaticFiles` para la carpeta predeterminada wwwroot (opcional)
app.UseStaticFiles();

// Configura `UseStaticFiles` para una carpeta personalizada
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "ec.edu.monster.vista")),
    RequestPath = "/vista"
});


app.UseAuthorization();

app.MapControllers();

app.Run();
