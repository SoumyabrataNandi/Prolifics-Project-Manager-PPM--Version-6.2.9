// Create namespace for the Project
namespace PPM.Model
{
    public class Employee
    {
        // Creating Properties
        // Use auto-implemented properties if you don't need custom logic in getters or setters.
        public int EmployeeId { get; set; }

        // Use nullable reference types for nullable string properties(That's why we are using string?)
        public string? EmployeeFirstName { get; set; }

        // Use nullable reference types for nullable string properties(That's why we are using string?)
        public string? EmployeeLastName { get; set; }

        // Use nullable reference types for nullable string properties(That's why we are using string?)
        public string? EmployeeEmail { get; set; }

        public double EmployeeMobileNumber { get; set; }

        // Use nullable reference types for nullable string properties(That's why we are using string?)
        public string? EmployeeAddress { get; set; }

        public int EmployeeRoleId { get; set; }

    }
}