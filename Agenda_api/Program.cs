using Agenda_api.Models;
using Agenda_api.Repository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

 #region
 builder.Services.AddSingleton<UserRepository>();               //ESTO NO SE PARA QUE SE USA PERO ESTABA EN EL NOTION DEL PROFE... MAS ADELANTE SEGURAMENTE VEA PARA QUE SIRVE. IMPORTANTE VER LO DE SINGELTON CREO QUE ES DEL TOKEN.
 builder.Services.AddSingleton<ContactRepository>();
 #endregion

//Add Context
builder.Services.AddDbContext<AgendaDbContext>(dbContextOptions => dbContextOptions.UseSqlite(
    builder.Configuration["ConnectionStrings:AgendaAPIDBConnectionString"]));   //Esta es la coneccion a la base de datos (esto trabaja con el appsettings.json.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
