namespace PPM.Model
{
    public interface IRoleRepo
    {
        void AddRole(Role role);

        List<Role> ListAllRoles();

        Role GetRole(int roleId);

        void DeleteRole(int roleId);
    }
}