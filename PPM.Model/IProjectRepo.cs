namespace PPM.Model
{
    public interface IProjectRepo
    {
        void AddProject(Project project);

        void AddEmployeeToExistingProject(int projectId, int employeeId);

        List<Project> ListAllProjectsWithoutEmployeeDetails();

        Project ViewProjectDetail(int projectId);

        void DeleteEmployeeFromProject(int projectId, int employeeId);

        void DeleteProject(int projectId);
    }
}