using System.Data.SqlClient;
using PPM.Model;

namespace PPM.Domain
{
    public class EmployeeRepo : IEmployeeRepo
    {
        ValidationCheck validationCheck = new();
        ConnectionString connectionString = new();

        public void AddEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();
                // Prepare the SQL INSERT statement
                string insertSql = "INSERT INTO Employees (EmployeeId, EmployeeFirstName, EmployeeLastName, EmployeeMobileNumber, EmployeeAddress, EmployeeRoleId) " +
                                   "VALUES (@EmployeeId, @EmployeeFirstName, @EmployeeLastName, @EmployeeMobileNumber, @EmployeeAddress, @EmployeeRoleId)";

                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    command.Parameters.AddWithValue("@EmployeeFirstName", employee.EmployeeFirstName);
                    command.Parameters.AddWithValue("@EmployeeLastName", employee.EmployeeLastName);
                    command.Parameters.AddWithValue("@EmployeeMobileNumber", employee.EmployeeMobileNumber);
                    command.Parameters.AddWithValue("@EmployeeAddress", employee.EmployeeAddress);
                    command.Parameters.AddWithValue("@EmployeeRoleId", employee.EmployeeRoleId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Employee> ListAllEmployees()
        {
            List<Employee> employeeList = new();
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();

                // Prepare the SQL SELECT statement
                string selectSql = "SELECT * FROM Employees";

                using (SqlCommand command = new SqlCommand(selectSql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee employee = new();
                            employee.EmployeeId = (int)reader["EmployeeId"];
                            employee.EmployeeFirstName = (string)reader["EmployeeFirstName"];
                            employee.EmployeeLastName = (string)reader["EmployeeLastName"];
                            employee.EmployeeMobileNumber = (long)reader["EmployeeMobileNumber"];
                            employee.EmployeeAddress = (string)reader["EmployeeAddress"];
                            employee.EmployeeRoleId = (int)reader["EmployeeRoleId"];

                            employeeList.Add(employee);
                        }
                    }
                }
            }return employeeList;
        }

        public Employee GetEmployee(int employeeId)
        {
            Employee employee = new();
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();

                // Prepare the SQL SELECT statement
                string selectSql = "SELECT * FROM Employees WHERE EmployeeId = @EmployeeId";

                using (SqlCommand command = new SqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        employee.EmployeeId = (int)reader["EmployeeId"];
                        employee.EmployeeFirstName = (string)reader["EmployeeFirstName"];
                        employee.EmployeeLastName = (string)reader["EmployeeLastName"];
                        employee.EmployeeMobileNumber = (long)reader["EmployeeMobileNumber"];
                        employee.EmployeeAddress = (string)reader["EmployeeAddress"];
                        employee.EmployeeRoleId = (int)reader["EmployeeRoleId"];
                    }
                }
            }
            return employee;
        }

        public void DeleteEmployee(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();

                // Prepare the SQL DELETE statement
                string deleteSql = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";

                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
