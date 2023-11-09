// using PPM.Model;
// using System.Xml.Serialization;


// namespace PPM.Domain
// {
//     public class SaveAppData
//     {
//         public void SaveAppDataInXmlFile()
//         {
//             using (var writer = new StreamWriter("AllAppData.xml"))
//             {
//                 var serializer = new XmlSerializer(typeof(List<Project>));
//                 serializer.Serialize(writer, ProjectRepo.projectList);
//                 serializer = new XmlSerializer(typeof(List<Employee>));
//                 serializer.Serialize(writer, EmployeeRepo.employeeList);
//                 serializer = new XmlSerializer(typeof(List<Role>));
//                 serializer.Serialize(writer, RoleRepo.roleList);
//             }
//             Console.WriteLine("App data saved successfully.");

//         }
//     }
// }