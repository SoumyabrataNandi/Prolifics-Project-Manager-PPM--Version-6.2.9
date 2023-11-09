using PPM.Domain;
using PPM.Model;

namespace PPM.Ui.Consoles
{
    public class EmployeeModuleManager
    {
        static EmployeeRepo employeeRepo = new();
        static ValidationCheck validationCheck = new();

        public void EmployeeModule()
        {
            while (true)
            {
                // Employee menu options
                Console.WriteLine();
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>> Employee Menu <<<<<<<<<<<<<<<<<<<<<<<");
                Console.WriteLine(">>                                                        <<");
                Console.WriteLine(">>             1. Add Employee                            <<");
                Console.WriteLine(">>             2. List of All Employees                   <<");
                Console.WriteLine(">>             3. Get Employee by Id                      <<");
                Console.WriteLine(">>             4. Delete Employee                         <<");
                Console.WriteLine(">>             5. Return to Main Menu                     <<");
                Console.WriteLine(">>                                                        <<");
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
                Console.WriteLine();

                Console.WriteLine("Enter Your Menu Choice Option:");
                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Employee employee = new();
                        Console.WriteLine("Enter Employee Id: ");
                        int employeeId = Convert.ToInt32(Console.ReadLine());

                        if(validationCheck.IdNotPutZero(employeeId))
                        {
                            Console.WriteLine("You Can't Enter Zero Id");
                            break;
                        }

                        if (validationCheck.EmployeeExists(employeeId))
                        {
                            Console.WriteLine("Employee Id is Already Present....");
                            break;
                        }
                        employee.EmployeeId = employeeId;

                        Console.WriteLine("Enter Employee First Name: ");
                        string employeeFirstName = Console.ReadLine()!;

                        if (!validationCheck.IsValidName(employeeFirstName))
                        {
                            Console.WriteLine("Please Don't Put It Blank ");
                            break;
                        }
                        employee.EmployeeFirstName = employeeFirstName;

                        Console.WriteLine("Enter Employee Last Name: ");
                        string employeeLastName = Console.ReadLine()!;

                        if (!validationCheck.IsValidName(employeeLastName))
                        {
                            Console.WriteLine("Please Don't Put It Blank ");
                            break;
                        }
                        employee.EmployeeLastName = employeeLastName;

                        Console.WriteLine("Enter Employee Email: ");
                        string employeeEmail = Console.ReadLine()!;

                        if (!validationCheck.IsValidEmail(employeeEmail))
                        {
                            Console.WriteLine("...Invalid Email Format... Please Enter Valid Email...");
                            break;
                        }
                        employee.EmployeeEmail = employeeEmail;
                        Console.WriteLine("Enter Employee Mobile Number: ");
                        long employeeMobileNumber = Convert.ToInt64(Console.ReadLine());
                        if (!validationCheck.IsValidMobileNumber(employeeMobileNumber.ToString()))
                        {
                            Console.WriteLine("...Invalid Mobile Number Format... Please Enter Valid Mobile Number...");
                            break;
                        }
                        employee.EmployeeMobileNumber = employeeMobileNumber;
                        Console.WriteLine("Enter Employee Address: ");
                        string employeeAddress = Console.ReadLine()!;

                        if (!validationCheck.IsValidName(employeeAddress))
                        {
                            Console.WriteLine("Please Don't Put It Blank ");
                            break;
                        }
                        employee.EmployeeAddress = employeeAddress;

                        Console.WriteLine("Enter Role Id: ");
                        int employeeRoleId = Convert.ToInt32(Console.ReadLine());

                        if (!validationCheck.IsRoleIdValid(employeeRoleId))
                        {
                            Console.WriteLine("...Invalid Role Id... Please Enter Valid Role Id...");
                            break;
                        }
                        employee.EmployeeRoleId = employeeRoleId;

                        // Add all data into List
                        employeeRepo.AddEmployee(employee);
                        Console.WriteLine("Employee Add Successfully");
                        break;

                    case "2":
                        Console.Clear();
                        if (employeeRepo.ListAllEmployees().Count == 0)
                        {
                            Console.WriteLine("No Data Found");
                            Console.WriteLine();
                            break;
                        }
                        foreach (var item in employeeRepo.ListAllEmployees())
                        {
                            Console.WriteLine("************************************************************************************************");
                            Console.WriteLine($"            Employee Id: {item.EmployeeId} Employee Name: {item.EmployeeFirstName}             ");
                            Console.WriteLine($"                     Employee Last Name: {item.EmployeeLastName}                               ");
                            Console.WriteLine($"                  Employee Mobile Number: {item.EmployeeMobileNumber}                          ");
                            Console.WriteLine($"        Employee Address: {item.EmployeeAddress}   Employee Role Id: {item.EmployeeRoleId}     ");
                            Console.WriteLine("************************************************************************************************");
                            Console.WriteLine();
                        }
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Enter Employee Id:");
                        int employeeIdforFind = Convert.ToInt32(Console.ReadLine());

                        if (!validationCheck.EmployeeExists(employeeIdforFind))
                        {
                            Console.WriteLine("Employee Id is Not Present....");
                            break;
                        }


                        Console.WriteLine("************************************************************************************************");
                        Console.WriteLine($"    Employee Id: {employeeRepo.GetEmployee(employeeIdforFind).EmployeeId} Employee Name: {employeeRepo.GetEmployee(employeeIdforFind).EmployeeFirstName}   ");
                        Console.WriteLine($"                      Employee Last Name: {employeeRepo.GetEmployee(employeeIdforFind).EmployeeLastName}                     ");
                        Console.WriteLine($"                  Employee Mobile Number: {employeeRepo.GetEmployee(employeeIdforFind).EmployeeMobileNumber}                  ");
                        Console.WriteLine($"    Employee Address: {employeeRepo.GetEmployee(employeeIdforFind).EmployeeAddress}  Employee Role Id: {employeeRepo.GetEmployee(employeeIdforFind).EmployeeRoleId} ");
                        Console.WriteLine("************************************************************************************************");
                        Console.WriteLine();
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("Enter Employee Id:");
                        int employeeIdforDelete = Convert.ToInt32(Console.ReadLine());

                        if (!validationCheck.EmployeeExists(employeeIdforDelete))
                        {
                            Console.WriteLine("Employee Id is Not Present....");
                            break;
                        }
                        if (validationCheck.EmployeeInProject(employeeIdforDelete))
                        {
                            Console.WriteLine("Employee Present In a Project You Can't Delete Employee....");
                            break;
                        }

                        employeeRepo.DeleteEmployee(employeeIdforDelete);
                        Console.WriteLine("Employee Delete Successfuly");
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