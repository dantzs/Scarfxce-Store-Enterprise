using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SSE.Identidade.API.Data;
using SSE.Identidade.API.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Adicionar o serviço de logging
builder.Services.AddLogging();

//Configuração do Identity

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConncetion")));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//JWT

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearerOptions =>
{
    bearerOptions.RequireHttpsMetadata = true;
    bearerOptions.SaveToken = true;
    bearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = appSettings.ValidIn,
        ValidIssuer = appSettings.Sender
    };
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Scarfxce Store Enteprise Identity API",
        Description = "Esta API faz parte do curso ASP.NET Core Enterprise Application",
        Contact = new OpenApiContact() { Name = "Marcelo Henrique", Email = "marcelohrs2005@gmail.com" },
        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensouce.org/licenses/MIT") }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();


app.UseRouting();

app.MapControllers();

app.Run();

