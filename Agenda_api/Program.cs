//using Agenda_api.Data;
//using Agenda_api.Models;
//using Agenda_api.Repository;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using System.Text;
//using AutoMapper;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(setupAction =>
//{
//    setupAction.AddSecurityDefinition("Agenda_apiBearerAuth", new OpenApiSecurityScheme()
//    {
//        Type = SecuritySchemeType.Http,
//        Scheme = "Bearer",
//        Description = "Aca pegar el toke generado la logearse."
//    });
//    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement 
//    { 
//        { new OpenApiSecurityScheme 
//             { 
//                Reference = new OpenApiReference 
//                { 
//                Type= ReferenceType.SecurityScheme,Id="AgendaApiBearerAuth"} },new List<string>() }}
//    );
//});

// #region
// builder.Services.AddSingleton<UserRepository>();               //ESTO NO SE PARA QUE SE USA PERO ESTABA EN EL NOTION DEL PROFE... MAS ADELANTE SEGURAMENTE VEA PARA QUE SIRVE. IMPORTANTE VER LO DE SINGELTON CREO QUE ES DEL TOKEN.
// builder.Services.AddSingleton<ContactRepository>();           // Esto es para que el ContactRepository y el UserRepository puedan ser usados como servicios y yo pueda llamar a los metodos (Para que yo los pueda inyectar) en este caso como Singelton.
// #endregion

////Add Context
//builder.Services.AddDbContext<AgendaApiContext>(dbContextOptions => dbContextOptions.UseSqlite(
//    builder.Configuration["ConnectionStrings:AgendaAPIDBConnectionString"]));   //Esta es la coneccion a la base de datos (esto trabaja con el appsettings.json.

//var app = builder.Build();

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer()

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Agenda_api.Controllers;
using Agenda_api.Repository;
using Agenda_api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using Agenda_api.Repository.Interfaces;
using AutoMapper;
using Agenda_api.Profiles;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("AgendaApiBearerAuth", new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Ac� pegar el token generado al loguearse."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "AgendaApiBearerAuth" } //Tiene que coincidir con el id seteado arriba en la definici�n
                }, new List<string>() }
    });
});

builder.Services.AddDbContext<AgendaApiContext>(dbContextOptions => dbContextOptions.UseSqlite(
    builder.Configuration["ConnectionStrings:AgendaAPIDBConnectionString"]));

builder.Services.AddAuthentication("Bearer") //"Bearer" es el tipo de auntenticaci�n que tenemos que elegir despu�s en PostMan para pasarle el token
    .AddJwtBearer(options => //Ac� definimos la configuraci�n de la autenticaci�n. le decimos qu� cosas queremos comprobar. La fecha de expiraci�n se valida por defecto.
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    }
);


//Cors para que el navegador pueda acceder a los endpoints
builder.Services.AddCors(options => options.AddPolicy("AllowWebapp",
                                    builder =>builder.AllowAnyOrigin()
                                              .AllowAnyHeader()
                                              .AllowAnyMethod()));

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new ContactProfile());
    cfg.AddProfile(new UserProfile());
});
var mapper = config.CreateMapper();

#region DependencyInjections
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IMapper, Mapper>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowWebapp");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
