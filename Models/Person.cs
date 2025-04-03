namespace NET_9_Business_App_MinimalAPI.Models
{
    //for Custom binding from query string and headers
    public class Person
    {

        public int EmployeeId { get; set; }
        public string? EmployeeLastName { get; set; }

        //Binds the EmployeeId and EmployeeLastName from the query string and headers
        public static ValueTask<Person> BindAsync(HttpContext context)
        {
            var idStr = context.Request.Query["EmployeeId"];
            var nameStr = context.Request.Headers["EmployeeLastName"];

            if (int.TryParse(idStr, out var id))
            {
                return new ValueTask<Person>(new Person
                {
                    EmployeeId = id,
                    EmployeeLastName = nameStr
                });
            }
            else
            {
                Person? person = new Person();
                return new ValueTask<Person?>(Task.FromResult<Person?>(person));
            }
        }//end BindAsync
    }//end Person class
}//end Namespace:Models
