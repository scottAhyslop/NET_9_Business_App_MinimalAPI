namespace NET_9_Business_App_MinimalAPI.Models
{
    //create sample employee class and create a constructor for the basic employee using params
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeePosition { get; set; }
        public double EmployeeSalary { get; set; }

        public Employee(int employeeId, string employeeFirstName, string employeeLastName, string employeePosition, double employeeSalary)
        {
            EmployeeId = employeeId;
            EmployeeFirstName = employeeFirstName;
            EmployeeLastName = employeeLastName;
            EmployeePosition = employeePosition;
            EmployeeSalary = employeeSalary;
        }
    }//end of Employee class

 
   
}
