using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.Use(async (context, next) => 
{
    await next(context);
});

app.UseEndpoints(endpoints =>
{
    //GET /employees
    endpoints.MapGet("/employees", async (HttpContext context) =>
    { await context.Response.WriteAsync("Get Employees");
    });//End GET

    //POST /employees
    endpoints.MapPost("/employees", async (HttpContext context) =>
     { await context.Response.WriteAsync("Create an Employee");
     });//End POST

    //PUT /employees
    endpoints.MapPut("/employees", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Update an  Employee");
    });//End PUT

    //DELETE /employees
    endpoints.MapDelete("/employees/{position}/{id}", async (HttpContext context) =>
    {        
        await context.Response.WriteAsync($"Deleted the Employee: {context.Request.RouteValues["id"]}");
    });//End DELETE

    //GET /default size in categories
    endpoints.MapGet("/{category=shirts}/{size=medium}", async (HttpContext context) =>
    {
        await context.Response.WriteAsync($"Get Category: {context.Request.RouteValues["category"]} " +
            $"\nin Size: {context.Request.RouteValues["size"]} ");
    });//End GET default size in categories  

});//End UseEndpoints

app.Run();
