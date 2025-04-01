using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using NET_9_Business_App_MinimalAPI.CustomConstraints;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{

    options.ConstraintMap.Add("pos", typeof(CustomConstraint));
});

//always be last
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

//Sample Example

 /*   //GET /default size in categories
    endpoints.MapGet("/{category=shirts}/{size=medium}/{id?}", async (HttpContext context) =>
    {
        await context.Response.WriteAsync($"Get Category: {context.Request.RouteValues["category"]} " +
            $"\nin Size: {context.Request.RouteValues["size"]} ");
    });//End GET default size in categories  */

    //GET /employee/id
    endpoints.MapGet("/employees/{id:int}", async (HttpContext context) =>
    {
        await context.Response.WriteAsync($"Have received Employee with EmployeeId of: {context.Request.RouteValues["id"]}");
    });//End GET employee by Id

    //GET /employee/name
    endpoints.MapGet("/employees/{name}", async (HttpContext context) =>
    {
        await context.Response.WriteAsync($"Have received Employee named: {context.Request.RouteValues["name"]}");
    });//End GET employee by Name

    //GET /employee/id in position by CustomConstraint
    endpoints.MapGet("/employees/positions/{positions:pos}", async (HttpContext context) =>
    {
        await context.Response.WriteAsync($"Get Employees with a position of:  {context.Request.RouteValues["positions"]}");
    });//End GET employee by Id

});//End UseEndpoints

app.Run();

//NOTE:Default values have to start first, and optional values are found at the end