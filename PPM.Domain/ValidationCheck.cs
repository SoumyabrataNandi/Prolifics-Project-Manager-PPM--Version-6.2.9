using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace PPM.Domain
{
    public class ValidationCheck
    {
        ConnectionString connectionString = new();
        public bool IsValidEmail(string email)
        {
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            return Regex.IsMatch(email, pattern);
        }
        // public bool IsValidRole(int roleId)
        // {
        //     return RoleRepo.roleList.Exists(r => r.RoleId == roleId);
        // }
        public bool IsValidMobileNumber(string mobileNumber)
        {
            string pattern = @"^\d{10}$";

            return Regex.IsMatch(mobileNumber, pattern);
        }
        public bool EmployeeExists(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();
                // Check if an employee with the given ID already exists in the database
                string selectSql = "SELECT 1 FROM Employees WHERE EmployeeId = @EmployeeId";

                using (SqlCommand command = new SqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
        // public bool IsValidDate(string date)
        // {
        //     return DateOnly.TryParse(date, out _);
        // }

        public bool ProjectExists(int projectId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();
                // Check if an employee with the given ID already exists in the database
                string selectSql = "SELECT 1 FROM Projects WHERE ProjectId = @ProjectId";

                using (SqlCommand command = new SqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", projectId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
        public bool IsRoleIdValid(int roleId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();
                // Check if an employee with the given ID already exists in the database
                string selectSql = "SELECT 1 FROM Roles WHERE RoleId = @RoleId";

                using (SqlCommand command = new SqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@RoleId", roleId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }

            }
        }
        public bool IsValidName(string name)
        {
            if (name == null)
            {
                Console.WriteLine("Please Don't Put It Blank ");
                return false;
            }
            return true;
        }
        public bool EmployeeInProject(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();
                string selectSql = "SELECT 1 FROM ProjectEmployees WHERE EmployeeId = @EmployeeId";
                using (SqlCommand command = new SqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
        public bool RoleInProject(int roleId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();
                string selectSql = "SELECT EmployeeId FROM Employees WHERE EmployeeRoleId = @RoleId";
                using (SqlCommand command = new SqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                        int employeeId = (int)reader["EmployeeId"];
                        return EmployeeInProject(employeeId);
                        }
                    }
                }
            }return false;
        }
        public bool IdNotPutZero(int id)
        {
            if(id == 0)
            {
                return true;
            }
            return false;
        }
    }
}
