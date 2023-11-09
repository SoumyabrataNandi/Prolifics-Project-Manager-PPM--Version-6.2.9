using PPM.Domain;
using PPM.Model;

namespace PPM.Ui.Consoles
{
    public class ProjectModuleManager
    {
        static ProjectRepo projectRepo = new();
        static ValidationCheck validationCheck = new();
        EmployeeRepo employeeRepo  = new();
        RoleRepo roleRepo = new();

        public void ProjectModule()
        {
            while (true)
            {
                // Project menu options
                Console.WriteLine();
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>> Project Menu <<<<<<<<<<<<<<<<<<<<<<<");
                Console.WriteLine(">>                                                       <<");
                Console.WriteLine(">>            1. Add Project                             <<");
                Console.WriteLine(">>            2. List of All Projects                    <<");
                Console.WriteLine(">>            3. Add Employee Existing Project           <<");
                Console.WriteLine(">>            4. Get Project by Id With Employee Details <<");
                Console.WriteLine(">>            5. Delete Employee From Project            <<");
                Console.WriteLine(">>            6. Delete Project                          <<");
                Console.WriteLine(">>            7. Return to Main Menu                     <<");
                Console.WriteLine(">>                                                       <<");
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
                Console.WriteLine();

                Console.WriteLine("Enter Your Menu Choice Option:");
                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Project project = new();
                        Console.WriteLine("Enter Project Id: ");
                        int projectId = Convert.ToInt32(Console.ReadLine());

                        if(validationCheck.IdNotPutZero(projectId))
                        {
                            Console.WriteLine("You Can't Enter Zero Id");
                            break;
                        }

                        if (validationCheck.ProjectExists(projectId))
                        {
                            Console.WriteLine("Project Id is Already Present.....");
                            break;
                        }
                        project.ProjectId = projectId;

                        Console.WriteLine("Enter Project Name: ");
                        string projectName = Console.ReadLine()!;
                        
                        if (!validationCheck.IsValidName(projectName))
                        {
                            Console.WriteLine("Please Don't Put It Blank ");
                            break;
                        }
                        project.ProjectName = projectName;

                        Console.WriteLine("Enter Project Start Date (YYYY-MM-DD):");
                        DateTime projectStartDate = Convert.ToDateTime(Console.ReadLine()!);

                        Console.WriteLine("Enter Project End Date (YYYY-MM-DD):");
                        DateTime projectEndDate = Convert.ToDateTime(Console.ReadLine()!);

                        if (projectStartDate > projectEndDate)
                        {
                            Console.WriteLine("  End Date Should Be Lesser Than Start Date   ");
                            break;
                        }
                        project.ProjectStartDate = projectStartDate;
                        project.ProjectEndDate = projectEndDate;

                        projectRepo.AddProject(project);
                        Console.WriteLine();
                        Console.WriteLine("Project Add Successfuly");

                        Console.WriteLine();
                        Console.WriteLine("Do You Want AddEmployee to This Project...");
                        Console.WriteLine();
                        Console.WriteLine("Press 1...");
                        Console.WriteLine();
                        Console.WriteLine("Otherwise Press Any Key To Continue...");
                        string wantToAddOrNot = Console.ReadLine()!;

                        if (wantToAddOrNot == "1")
                        {
                            Console.WriteLine("How many Employee You Want To Add This Project:-");
                            int employeeAddToProjectCount = Convert.ToInt32(Console.ReadLine());

                            for (int i = 0; i < employeeAddToProjectCount; i++)
                            {
                                // Prompt user to enter employee ID
                                Console.WriteLine("Enter Employee Id ");
                                int employeeId = Convert.ToInt32(Console.ReadLine());

                                projectRepo.AddEmployeeToExistingProject(projectId, employeeId);
                            }
                        }
                        break;

                    case "2":
                        Console.Clear();
                        if (projectRepo.ListAllProjectsWithoutEmployeeDetails().Count == 0)
                        {
                            Console.WriteLine("No Data Found");
                            Console.WriteLine();
                            break;
                        }
                        // Display all data
                        foreach (var item in projectRepo.ListAllProjectsWithoutEmployeeDetails())
                        {
                            Console.WriteLine("************************************************************************************************");
                            Console.WriteLine("                                                                                                ");
                            Console.WriteLine($"             Project Id: {item.ProjectId} Project Name: {item.ProjectName}                     ");
                            Console.WriteLine($"       Project Start Date: {item.ProjectStartDate} Project End Date: {item.ProjectEndDate}     ");
                            Console.WriteLine("                                                                                                ");
                            Console.WriteLine("************************************************************************************************");
                            Console.WriteLine();
                        }
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Enter Project Id:");
                        int projectIdForAddEmployee = Convert.ToInt32(Console.ReadLine());

                        if (!validationCheck.ProjectExists(projectIdForAddEmployee))
                        {
                            Console.WriteLine("Project Id Is Not Found.....");
                            break;
                        }

                        Console.WriteLine("How many Employee You Want To Add This Project:-");
                        int employeeAddToProject = Convert.ToInt32(Console.ReadLine());

                        for (int i = 0; i < employeeAddToProject; i++)
                        {
                            // Prompt user to enter employee ID
                            Console.WriteLine("Enter Employee Id ");
                            int employeeIdForAddToProject = Convert.ToInt32(Console.ReadLine());

                            if (!validationCheck.EmployeeExists(employeeIdForAddToProject))
                            {
                                Console.WriteLine("Employee Is not Found");
                                break;
                            }

                            if (validationCheck.EmployeeInProject(employeeIdForAddToProject))
                            {
                                Console.WriteLine("Employee Is Already Add To This Project...");
                                break;
                            }

                            projectRepo.AddEmployeeToExistingProject(projectIdForAddEmployee, employeeIdForAddToProject);
                            Console.WriteLine("Employee added to project successfully");
                        }
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("Enter Project Id:");
                        int projectIdforFind = Convert.ToInt32(Console.ReadLine());

                        if (!validationCheck.ProjectExists(projectIdforFind))
                        {
                            Console.WriteLine("Project Id Is Not Found.....");
                            break;
                        }
                        var projectDetails = projectRepo.ViewProjectDetail(projectIdforFind);
                        Console.WriteLine("***************************************************************************************************************************************************");
                        Console.WriteLine($"Project Id: {projectIdforFind}  |   Project Name: {projectDetails.ProjectName}"
                        + $"Project Start Date: {projectDetails.ProjectStartDate}   |   Project End Date: {projectDetails.ProjectEndDate}");
                        Console.WriteLine();
                        if (projectDetails.ProjectEmployees!.Count == 0)
                        {
                            break;
                        }
                        Console.WriteLine("List of Employees in This Project");
                        string previousRole = "";

                        foreach (int employees in projectDetails.ProjectEmployees)
                        {
                            var employess = employeeRepo.GetEmployee(employees);
                            var roles = roleRepo.GetRole(employess.EmployeeRoleId);

                            if (roles.RoleName != previousRole)
                            {
                                Console.WriteLine($"       Employee Role: {roles.RoleName}      ");
                                Console.WriteLine("------------------------------------------------------------");
                                previousRole = roles.RoleName!;
                            }
                            
                            Console.WriteLine();
                            Console.WriteLine($"          Employee Id: {employess.EmployeeId}  |  Employee Name: {employess.EmployeeFirstName}      ");
                            Console.WriteLine($"          Employee Last Name: {employess.EmployeeLastName}       ");
                            Console.WriteLine($"          Employee Email: {employess.EmployeeEmail}         ");
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                        

                        Console.WriteLine("***************************************************************************************************************************************************");
                        Console.WriteLine();

                        break;

                    case "5":
                        Console.Clear();
                        Console.WriteLine("Enter Project Id:");
                        int projectIdForRemoveEmployee = Convert.ToInt32(Console.ReadLine());

                        if (!validationCheck.ProjectExists(projectIdForRemoveEmployee))
                        {
                            Console.WriteLine("Project Id Is Not Found.....");
                            break;
                        }

                        Console.WriteLine("Enter Employee Id ");
                        int employeeIdForRemoveFormProject = Convert.ToInt32(Console.ReadLine());

                        if (!validationCheck.EmployeeExists(employeeIdForRemoveFormProject))
                        {
                            Console.WriteLine("Employee Is not Found");
                            break;
                        }
                        if (!validationCheck.EmployeeInProject(employeeIdForRemoveFormProject))
                        {
                            Console.WriteLine("Employee Is not Present in this Project");
                            break;
                        }

                        projectRepo.DeleteEmployeeFromProject(projectIdForRemoveEmployee, employeeIdForRemoveFormProject);
                        Console.WriteLine("Employee Removed from Project Successfully");
                        break;

                    case "6":
                        Console.Clear();
                        Console.WriteLine("Enter Project Id:");
                        int projectIdforDelete = Convert.ToInt32(Console.ReadLine());

                        if (!validationCheck.ProjectExists(projectIdforDelete))
                        {
                            Console.WriteLine("Project Id Is Not Found.....");
                            break;
                        }

                        projectRepo.DeleteProject(projectIdforDelete);
                        Console.WriteLine("Project Delete Successfuly");
                        break;

                    case "7":
                        return;

                    default:
                        System.Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
        }
    }
}