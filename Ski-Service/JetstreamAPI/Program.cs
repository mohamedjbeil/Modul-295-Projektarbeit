using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JetstreamAPI.Data;
using JetstreamAPI.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Konfiguration aus appsettings.json laden
var configuration = builder.Configuration;

// Datenbankverbindung einrichten
builder.Services.AddDbContext<JetstreamDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("JetstreamDB")));

// Identity für Benutzerverwaltung hinzufügen
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<JetstreamDbContext>()
    .AddDefaultTokenProviders();

// JWT Authentifizierung einrichten
var jwtSecret = configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT Secret wurde nicht gesetzt!");
var key = Encoding.ASCII.GetBytes(jwtSecret);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Autorisierung aktivieren
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireLoggedIn", policy => policy.RequireAuthenticatedUser());
});

// Swagger/OpenAPI hinzufügen
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Jetstream API", Version = "v1" });
});

var app = builder.Build();

// Middleware-Konfiguratio
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>{
        
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SkiService API v1");
        c.RoutePrefix = "swagger";
    
    });

}

/*app.UseSwagger();
app.UseSwaggerUI();
*/
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
