using BackendTareas.Context;
using BackendTareas.Repositorio;
using BackendTareas.Repositorio.IRepositorio;
using BackendTareas.TareasMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200")  // URL de tu frontend en Angular
              .AllowAnyHeader()  // Permitir cualquier encabezado
              .AllowAnyMethod(); // Permitir cualquier método HTTP (GET, POST, PUT, DELETE, etc.)
    });
});


// Agregar servicios al contenedor (dependencias)
builder.Services.AddControllers();


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ConSQLite");

//Agregar repositorios con el paron repositorio
builder.Services.AddScoped<ITareaRepositorio, Tarearepositorio>();

// Agregar el Automapper
builder.Services.AddAutoMapper(typeof(TareasMapper));

// registrar el servicio para conexion
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Usar CORS
app.UseCors("AllowAngularOrigins");  // Habilitar la política CORS


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
