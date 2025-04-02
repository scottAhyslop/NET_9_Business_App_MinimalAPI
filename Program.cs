using Microsoft.AspNetCore.Mvc;
using NET_9_Business_App_MinimalAPI.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

/*//Register services with DI
builder.Services.AddRouting(options =>
{

    options.ConstraintMap.Add("pos", typeof(CustomConstraint));
});*///Custom Constraint service registration

//always be last
var app = builder.Build();

app.UseRouting();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    //DEFAULT landing endpoint
    endpoints.MapGet("/", async (HttpContext context) =>
    {
        context.Response.Headers["Content-Type"] = "text/html";
        await context.Response.WriteAsync($"<h2>Test Data</h2><h3>Your page has loaded properly</h3><h4>Your endpoints are avilable for data...</h4>");
        await context.Response.WriteAsync($"The Method is: {context.Request.Method}<br/>");
        await context.Response.WriteAsync($"The URL is: {context.Request.Path}<br/>");
        await context.Response.WriteAsync($"<br/><b>Headers</b>: <br/>");
        await context.Response.WriteAsync($"<ul>");

        foreach (var key in context.Request.Headers.Keys)
        {
            await context.Response.WriteAsync($"<li><b>{key}</b>: {context.Request.Headers[key]}</li>");
        }
        await context.Response.WriteAsync($"</ul>");
    });//End Default

    /*
        //GET /employees
        endpoints.MapGet("/employees", async (HttpContext context) =>
        {
            //get all employees
            var employees = EmployeesRepository.GetEmployees();//get a list of employees
            context.Response.StatusCode = 201;
            await context.Response.WriteAsync($"<table>");
            await context.Response.WriteAsync($"<tr><header><b><h2>Employee List</b>:<h2> </tr></header><br/>");
            await context.Response.WriteAsync($"<tr><header><td><b>Name</b></td><td><b>Position</b><td><b>Salary</b></td></tr></header>");
            foreach (var employee in employees)//display each employee in the list
            {
                await context.Response.WriteAsync($"<tr><td>{employee.EmployeeFirstName} {employee.EmployeeLastName}</td><td>{employee.EmployeePosition}</td><td>${employee.EmployeeSalary}</td></tr>");//display each employee's info
            }
            await context.Response.WriteAsync($"</table>");

        });//End GET employees*///End GET Employees pre-Model

    //GET /employees/id
    _ = endpoints.MapGet("/employees/{id:int}", ([AsParameters] GetEmployeeParameters param) =>
    {
        var employee = EmployeesRepository.GetEmployeeById(param.EmployeeId);
        if (employee is not null)
        {
            employee.EmployeeFirstName = param.EmployeeLastName;
            employee.EmployeePosition = param.EmployeePosition;
        }
       

        return employee;
    });//End GET EmployeeById

    //POST /employees Create an employee
    endpoints.MapPost("/employees", async (HttpContext context) =>
     {
         using var reader = new StreamReader(context.Request.Body);
         var body = await reader.ReadToEndAsync();
         var employee = JsonSerializer.Deserialize<Employee>(body);
         try
         {
             if (employee is not null)
             {
                 EmployeesRepository.AddEmployee(employee);
                 context.Response.StatusCode = 201;
                 await context.Response.WriteAsync($"Employee: {employee.EmployeeFirstName} {employee.EmployeeLastName} added. Records updated.");

             }
             else if (employee is null || employee.EmployeeId <= 0)
             {
                 context.Response.StatusCode = 400;
                 await context.Response.WriteAsync("Bad response to your request");
                 //TODO redirect home after displaying timed error message and log error deets
                 return;
             }
         }
         catch (Exception ex)
         {
             await context.Response.WriteAsync(ex.ToString());
             context.Response.StatusCode = 400;
         }//end try/catch     
     });//End POST

    //PUT /employees  Update an Employee
    endpoints.MapPut("/employees", async (HttpContext context) =>
    {
        //get a list of employees
        using var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();
        var employee = JsonSerializer.Deserialize<Employee>(body);

        //Update employee with "PUT" info from request
        var result = EmployeesRepository.UpdateEmployee(employee);
        if (result)
        {
            //context.Response.StatusCode = 204;
            await context.Response.WriteAsync("Employee updated successfully");
            return;
        }
        else
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("No employee found");
        }
    });//End PUT

    //DELETE /employees Delete Employee by Id
    endpoints.MapDelete("/employees/{EmployeeId:int}", async (HttpContext context) =>
    {
        var id = context.Request.RouteValues["EmployeeId"];
       
       var employeeId = int.Parse(id.ToString());
       
        var result = EmployeesRepository.DeleteEmployee(employeeId);

        if (result)//if there's a valid result from the EmployeeRepository
        {
            await context.Response.WriteAsync($"Employee {result} deleted. Records updated.");
        }
        else//otherewise, if no employee found, produce code and inform user
        {
            context.Response.StatusCode = 404;//not found
            await context.Response.WriteAsync("Employee not found.  Records unchanged.");
        }        
    });//End DELETE

//Sample Examples

 /*   //GET /default size in categories
    endpoints.MapGet("/{category=shirts}/{size=medium}/{id?}", async (HttpContext context) =>
    {
        await context.Response.WriteAsync($"Get Category: {context.Request.RouteValues["category"]} " +
            $"\nin Size: {context.Request.RouteValues["size"]} ");
    });//End GET default size in categories  */

 /* //GET /employee/id in position by CustomConstraint
    endpoints.MapGet("/employees/positions/{positions:pos}", async (HttpContext context) =>
    {
        await context.Response.WriteAsync($"Get Employees with a position of:  {context.Request.RouteValues["positions"]}");
    });//End GET employee by Id*/

});//End UseEndpoints
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.Run();

class GetEmployeeParameters
{
    [FromRoute]
    public int EmployeeId { get; set; }
    [FromQuery]
    public string EmployeeLastName { get; set; }
    [FromHeader]
    public string EmployeePosition { get; set; }
}

//NOTE:Default values have to start first, and optional values are found at the end of the arguments