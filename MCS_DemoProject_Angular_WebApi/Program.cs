using MCS_DemoProject_Angular_WebApi.Interfaces;
using MCS_DemoProject_Angular_WebApi.Models;
using MCS_DemoProject_Angular_WebApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuration connection string information
builder.Services.AddDbContext<MCSContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MCSConString")));
//Resolve DI

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });
});
builder.Services.AddScoped<IUsers<User>, UserRepo>();

var app = builder.Build();

app.UseCors();
// Add Swagger generation and UI
app.UseSwagger();
app.UseSwaggerUI();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name V1");
    c.RoutePrefix = string.Empty; // Serve the Swagger UI at the root URL
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
