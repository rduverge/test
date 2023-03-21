using CuentasPorCobrar.Shared;
using Microsoft.AspNetCore.Mvc.Formatters;
using static System.Console;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

using FluentValidation.AspNetCore;
using API.Middleware;
using Microsoft.AspNetCore.Mvc;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(doc =>
    {
        doc.SwaggerEndpoint("/swagger/v1/swagger.json", "Cuentas por Cobrar Service API Version 1");

        doc.SupportedSubmitMethods(new[]
        {
            SubmitMethod.Get, SubmitMethod.Post,
            SubmitMethod.Put, SubmitMethod.Delete
        });
    });
}


app.UseHttpsRedirection();

builder.WebHost.UseUrls("https://localhost:5002/");

//app.UseCors(configurePolicy: options =>
//{
//    options.WithMethods("GET", "POST", "PUT", "DELETE");
//    options.WithOrigins(
//        "https://localhost:5001"); //Allow requests from the MVC client
//});

//app.UseMiddleware<SecurityHeaders>();


app.UseAuthorization();
app.UseCors("CorsPolicy");



app.MapControllers();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;



try
{
    var context = services.GetRequiredService<CuentasporcobrardbContext>();
    context.Database.Migrate(); 

}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error ocurred during migration"); 

}


app.Run();
