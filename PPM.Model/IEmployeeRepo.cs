namespace PPM.Model
{
    public interface IEmployeeRepo
    {
        void AddEmployee(Employee employee);

        List<Employee> ListAllEmployees();

        Employee GetEmployee(int employeeId);

        void DeleteEmployee(int employeeId);
    }
}