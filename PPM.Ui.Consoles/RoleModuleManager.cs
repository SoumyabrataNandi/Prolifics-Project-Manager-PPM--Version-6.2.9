using System;
using PPM.Domain;
using PPM.Model;

namespace PPM.Ui.Consoles
{
    public class RoleModuleManager
    {
        static RoleRepo roleRepo = new();
        static ValidationCheck validationCheck = new();
        public void RoleModule()
        {
            while (true)
            {
                // Role menu options
                Console.WriteLine();
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>> Role Menu <<<<<<<<<<<<<<<<<<<<<<<");
                Console.WriteLine(">>                                                    <<");
                Console.WriteLine(">>             1. Add Role                            <<");
                Console.WriteLine(">>             2. List of All Roles                   <<");
                Console.WriteLine(">>             3. Get Role by Id                      <<");
                Console.WriteLine(">>             4. Delete Role                         <<");
                Console.WriteLine(">>             5. Return to Main Menu                 <<");
                Console.WriteLine(">>                                                    <<");
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<");
                Console.WriteLine();

                Console.WriteLine("Enter Your Menu Choice Option:");
                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Role role = new();
                        Console.WriteLine("Enter Role Id:");
                        int roleId = Convert.ToInt32(Console.ReadLine());

                        if(validationCheck.IdNotPutZero(roleId))
                        {
                            Console.WriteLine("You Can't Enter Zero Id");
                            break;
                        }

                        if (validationCheck.IsRoleIdValid(roleId))
                        {
                            Console.WriteLine("Role Id is Already Present.....");
                            break;
                        }
                        role.RoleId = roleId;

                        System.Console.WriteLine("Enter Role Name:");
                        string roleName = Console.ReadLine()!;

                        if (!validationCheck.IsValidName(roleName))
                        {
                            Console.WriteLine("Please Don't Put It Blank ");
                            break;
                        }
                        role.RoleName = roleName;
                        roleRepo.AddRole(role);
                        Console.WriteLine("Role Add Successfuly");
                        break;

                    case "2":
                        Console.Clear();
                        if (roleRepo.ListAllRoles().Count == 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("No Data Found");
                            Console.WriteLine();
                            break;
                        }
                        // Display all data
                        foreach (var item in roleRepo.ListAllRoles())
                        {
                            Console.WriteLine("*******************************************************************************************************");
                            Console.WriteLine("                                                                                                       ");
                            Console.WriteLine($"                         Role Id: {item.RoleId} Role Name: {item.RoleName}                            ");
                            Console.WriteLine("                                                                                                       ");
                            Console.WriteLine("*******************************************************************************************************");
                            Console.WriteLine();
                        }
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Enter Role Id:");
                        int roleIdforFind = Convert.ToInt32(Console.ReadLine());

                        if (!validationCheck.IsRoleIdValid(roleIdforFind) == true)
                        {
                            Console.WriteLine("Role Id is Not Present.....");
                            break;
                        }
                        Console.Clear();
                        Console.WriteLine("*******************************************************************************************************");
                        Console.WriteLine("                                                                                                       ");
                        Console.WriteLine($"      Role Id: {roleRepo.GetRole(roleIdforFind).RoleId} Role Name: {roleRepo.GetRole(roleIdforFind).RoleName}                            ");
                        Console.WriteLine("                                                                                                       ");
                        Console.WriteLine("*******************************************************************************************************");
                        Console.WriteLine();
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("Enter Role Id:");
                        int roleIdforDelete = Convert.ToInt32(Console.ReadLine());

                        if (!validationCheck.IsRoleIdValid(roleIdforDelete))
                        {
                            Console.WriteLine("Role Id is Not Present.....");
                            break;
                        }
                        if (validationCheck.RoleInProject(roleIdforDelete))
                        {
                            Console.WriteLine("Role Present In a Project You Can't Delete This Role ....");
                            break;
                        }

                        roleRepo.DeleteRole(roleIdforDelete);
                        Console.WriteLine("Role Delete Successfuly");
                        break;

                    case "5":
                        return;

                    default:
                        System.Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
        }
    }
}