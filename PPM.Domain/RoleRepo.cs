using System.Data.SqlClient;
using PPM.Model;


namespace PPM.Domain
{
    public class RoleRepo : IRoleRepo
    {
        ConnectionString connectionString = new();
        ValidationCheck validationCheck = new();

        public void AddRole(Role role)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();
                // Prepare the SQL INSERT statement
                string insertSql = "INSERT INTO Roles (RoleId,RoleName) VALUES (@RoleId,@RoleName);";

                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@RoleName", role.RoleName);
                    command.Parameters.AddWithValue("@RoleId", role.RoleId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Role> ListAllRoles()
        {
            List<Role> roleList = new();
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();

                // Prepare the SQL SELECT statement
                string selectSql = "SELECT RoleId, RoleName FROM Roles";

                using (SqlCommand command = new SqlCommand(selectSql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                           Role role = new();

                            role.RoleId = (int)reader["RoleId"];
                            role.RoleName = (string)reader["RoleName"];
                            roleList.Add(role);
                        }
                    }
                }
            }return roleList;
        }

        public Role GetRole(int roleId)
        {
            Role role = new();
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();

                // Prepare the SQL SELECT statement
                string selectSql = "SELECT RoleId, RoleName FROM Roles WHERE RoleId = @RoleId";

                using (SqlCommand command = new SqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@RoleId", roleId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        role.RoleId = (int)reader["RoleId"];
                        role.RoleName = (string)reader["RoleName"];

                    }return role;
                }
            }
        }

        public void DeleteRole(int roleId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Connection()))
            {
                connection.Open();

                // Delete the role
                string deleteSql = "DELETE FROM Roles WHERE RoleId = @RoleId";

                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
