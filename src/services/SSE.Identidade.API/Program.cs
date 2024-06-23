
using SSE.Identidade.API.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppConfiguration(builder.Configuration);
builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseAppConfiguration();
app.UseSwaggerConfiguration(app.Environment);

app.Run();