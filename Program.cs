var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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
    endpoints.MapDelete("/employees", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Delete an Employee");
    });//End DELETE
});//End UseEndpoints

app.Run();
