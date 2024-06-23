using SSE.WebApp.MVC.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddMvcConfiguration();

var app = builder.Build();


app.UseMvcConfiguration(app.Environment);

app.Run();
