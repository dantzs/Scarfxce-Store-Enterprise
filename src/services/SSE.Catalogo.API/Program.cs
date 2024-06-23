using Microsoft.EntityFrameworkCore;
using SSE.Catalogo.API.Data;
using SSE.Catalogo.API.Extensions;
using SSE.Catalogo.API.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();


var app = builder.Build();
app.UseSwaggerConfiguration(app.Environment);

app.Run();
