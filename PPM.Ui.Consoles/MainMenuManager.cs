using PPM.Domain;

namespace PPM.Ui.Consoles
{
    public class MainMenuManager
    {
        static ProjectModuleManager projectModuleManager = new();
        static EmployeeModuleManager employeeModuleManager = new();
        static RoleModuleManager roleModuleManager = new();

        public void MainMenu()
        {
            // Use try-catch-finaly block for exception handling
            try
            {
                // Creating menu with infinite while loop
                while (true)
                {
                    // Giving Welcome Message
                    Console.WriteLine("Welcome to Prolifics Project Manager(PPM)");
                    // All menu options
                    Console.WriteLine();
                    Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>> Menu <<<<<<<<<<<<<<<<<<<<<<<");
                    Console.WriteLine(">>                                               <<");
                    Console.WriteLine(">>             1. Project Module                 <<");
                    Console.WriteLine(">>             2. Employee Module                <<");
                    Console.WriteLine(">>             3. Role Module                    <<");
                    Console.WriteLine(">>             4. Exit                           <<");
                    Console.WriteLine(">>                                               <<");
                    Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<");
                    Console.WriteLine();

                    // Taking input from user
                    Console.WriteLine("Enter Your Menu Choice Option:");
                    string choice = Console.ReadLine()!;

                    switch (choice)
                    {
                        case "1":
                            Console.Clear();
                            projectModuleManager.ProjectModule();
                            break;

                        case "2":
                            Console.Clear();
                            employeeModuleManager.EmployeeModule();
                            break;

                        case "3":
                            Console.Clear();
                            roleModuleManager.RoleModule();
                            break;

                        case "4":
                            return;

                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid Choice");
                            break;
                    }
                }
            }
            catch (FormatException exp)
            {
                Console.WriteLine("Exception ocurred in the Application. Check the exception details below:");
                Console.WriteLine("Message  : {0}.", exp.Message);
                Console.WriteLine();
            }
            catch (Exception exp)
            {
                Console.WriteLine("Exception ocurred in the Application. Check the exception details below:");
                Console.WriteLine("Message : {0}", exp.Message);
                Console.WriteLine();
            }
            // If any exception occurs or not this finally block will be executed any show this message before exit from application
            finally
            {
                Console.WriteLine("Thank You for using Prolifics Project Manager(PPM)");
            }
        }
    }
}