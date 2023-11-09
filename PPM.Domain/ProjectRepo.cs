using System.Data.SqlClient;
using PPM.Model;

namespace PPM.Domain
{
    public class ProjectRepo : IProjectRepo
    {
        ConnectionString connectionString = new();
        ValidationCheck validationCheck = new();
        public void AddProject(Project project)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();
                // Prepare the SQL INSERT statement
                string insertSql = "INSERT INTO Projects (ProjectId, ProjectName, ProjectStartDate, ProjectEndDate) " +
                                   "VALUES (@ProjectId, @ProjectName, @ProjectStartDate, @ProjectEndDate);";

                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", project.ProjectId);
                    command.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                    command.Parameters.AddWithValue("@ProjectStartDate", project.ProjectStartDate);
                    command.Parameters.AddWithValue("@ProjectEndDate", project.ProjectEndDate);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Project> ListAllProjectsWithoutEmployeeDetails()
        {
            List<Project> projectList = new();
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();

                // Prepare the SQL SELECT statement
                string selectSql = "SELECT ProjectId, ProjectName, ProjectStartDate, ProjectEndDate FROM Projects";

                using (SqlCommand command = new SqlCommand(selectSql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            Project project = new();
                            project.ProjectId = (int)reader["ProjectId"];
                            project.ProjectName = (string)reader["ProjectName"];
                            project.ProjectStartDate = (DateTime)reader["ProjectStartDate"];
                            project.ProjectEndDate = (DateTime)reader["ProjectEndDate"];
                            projectList.Add(project);
                        }
                    }
                }
            }
            return projectList;
        }

        public void DeleteProject(int projectId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();
                // Delete the project
                string deleteSql = "DELETE FROM Projects WHERE ProjectId = @ProjectId";
                string deleteSql2 = "DELETE FROM ProjectEmployees WHERE ProjectId = @ProjectId";

                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", projectId);
                    command.ExecuteNonQuery();
                }
                using (SqlCommand command = new SqlCommand(deleteSql2, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", projectId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddEmployeeToExistingProject(int projectId, int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();
                // Add the employee to the project
                string insertSql = "INSERT INTO ProjectEmployees (ProjectId , EmployeeId) VALUES (@ProjectId , @EmployeeId)";
                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", projectId);
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEmployeeFromProject(int projectId, int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();

                // Remove the employee from the project
                string deleteSql = "DELETE FROM ProjectEmployees WHERE ProjectId = @ProjectId AND EmployeeId = @EmployeeId";

                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", projectId);
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Project ViewProjectDetail(int projectId)
        {
            Project project = new();
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();

                // Retrieve project details
                string selectSql = "SELECT ProjectName, ProjectStartDate, ProjectEndDate FROM Projects WHERE ProjectId = @ProjectId";

                using (SqlCommand command = new SqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", projectId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                        project.ProjectName = (string)reader["ProjectName"];
                        project.ProjectStartDate = (DateTime)reader["ProjectStartDate"];
                        project.ProjectEndDate = (DateTime)reader["ProjectEndDate"];

                        project.ProjectEmployees = GetEmployeesInProject(projectId);
                        }
                    }
                }
            }
            return project;
        }

        public List<int> GetEmployeesInProject(int projectId)
        {
            List<int> employeeIds = new();
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();
                string selectSql = "SELECT EmployeeId FROM ProjectEmployees WHERE ProjectId = @ProjectId";
                using (SqlCommand command = new SqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", projectId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeeIds.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            return employeeIds;
        }
    }
}
