using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
});

builder.Services.AddIdentity<AppUsuarios, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IPeliculaRepository, PeliculaRepository>();
builder.Services.AddScoped<IPeliculaService, PeliculaService>();
builder.Services.AddScoped<ITipoClasificacionRepository, TipoClasificacionRepository>();
builder.Services.AddScoped<ITipoClasificacionService, TipoClasificacionService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

var claveSecreta = builder.Configuration.GetValue<string>("ApiSettings:Secreta");

//Aqui se configura la autenticacion
builder.Services.AddAuthentication
    (   options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }

    ).AddJwtBearer
    ( 
        options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(claveSecreta)),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        }
    );

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = 
        "Autentication JWT usando el esquema Bearer, \r\n\r\n "+
        "Ingresa la palabra 'Bearer' seguido de un [espacio] y despues su token en el campo de abajo. \r\n\r\n"+
        "Ejemplo: \"Bearer tkljk125jhhk\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });

    options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1.0",
            Title = "NetMovies API",
            Description = "Api de Peliculas arquitectura limpia",
            TermsOfService = new Uri("https://www.google.com"),
            Contact = new OpenApiContact
            {
                Name = "Charly",
                Url = new Uri("https://www.google.com")
            },
            License = new OpenApiLicense
            {
                Name = "Licencia Personal",
                Url = new Uri("https://www.google.com")
            }
        }
    );

    //Esto hace quelos enums se muestren como strings en Swagger
    options.UseInlineDefinitionsForEnums();
});

//Soporte para CORS
//Se pueden Habilitar: 1-Un dominio especifico, 2-multiples los dominios,
//3-cualquier dominio (Tener en cuenta seguridad)
//Usamos de ejemplo el dominio: http://localhost:4200, se debe cambiar por el correcto
//Se usa (*) para todos los dominios
builder.Services.AddCors( p => p.AddPolicy("PoliticaCors", build =>
{
    build.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Si quieres usar Swagger SIEMPRE, incluso en producción, pon esto fuera del if:
app.UseSwagger();
app.UseSwaggerUI();

//Soporte para archivos estaticos como imagenes
app.UseStaticFiles();
app.UseHttpsRedirection();

//Soporte para CORS
app.UseCors("PoliticaCors");

//Soporte para authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
