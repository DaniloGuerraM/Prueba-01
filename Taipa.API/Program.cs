using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Taipa.API.Negocio.Implementaciones;
using Taipa.API.Negocio.Interface;
using Taipa.API.Repositorio.Implementaciones;
using Taipa.API.Repositorio.Interface;
using Taipa.App.Perfiles; // Asegurate de tener este namespace
using Taipa.App.Repositorio;
using Taipa.App.Repositorio.Contexto;
using Taipa.App.Negocio.Implementaciones;
using Taipa.App.Negocio.Interface;
using ITaipa.App.Repositorio.Interface;
using Taipa.App.Perfiles; // Asegurate de tener este namespace

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Configuración de servicios
// -------------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Manejo de enums como strings (útil para serialización/deserialización limpia)
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -------------------------
// Configuración de CORS
// -------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// -------------------------
// Configuración de AutoMapper
// -------------------------
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MapperPerfil>();
}, typeof(Program).Assembly);

// -------------------------
// Configuración del DbContext (PostgreSQL)
// -------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppContexto>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// -------------------------
// Inyección de dependencias
// -------------------------
builder.Services.AddScoped<IVentaNegocio, VentaNegocio>();
builder.Services.AddScoped<IVentaRepositorio, VentaRepositorio>();
builder.Services.AddScoped<IMetodosPagoNegocio, MetodosPagoNegocio>();
builder.Services.AddScoped<IMetodosPagoRepositorio, MetodosPagoRepositorio>();

builder.Services.AddScoped<Taipa.API.Negocio.Interface.IProductoNegocio, Taipa.API.Negocio.Implementaciones.ProductoNegocio>();
builder.Services.AddScoped<Taipa.API.Repositorio.Interface.IProductoRepositorio, Taipa.API.Repositorio.Implementaciones.ProductoRepositorio>();

builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IClienteNegocio, ClienteNegocio>();

builder.Services.AddScoped<IInventarioNegocio, InventarioNegocio>();
builder.Services.AddScoped<IInventarioRepositorio, InventarioRepositorio>();

builder.Services.AddScoped<IOrdenNegocio, OrdenNegocio>();
builder.Services.AddScoped<IOrdenRepositorio, OrdenRepositorio>();

builder.Services.AddScoped<ITransactionRepositorio, TransactionRepositorio>();

// -------------------------
// Construcción de la aplicación
// -------------------------
var app = builder.Build();

// -------------------------
// Pipeline HTTP
// -------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();


// -------------------------
// Aplicar migraciones automáticamente
// -------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<AppContexto>();
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error al aplicar migraciones");
    }
}

app.Run();