// Create namespace for the Project
namespace PPM.Model
{
    // Creating Project Class
    public class Project
    {
        // Creating Properties
        // Use auto-implemented properties if you don't need custom logic in getters or setters.
        public int ProjectId { get; set; }

        // Use nullable reference types for nullable string properties(That's why we are using string?)
        public string? ProjectName { get; set; }

        public DateTime ProjectStartDate { get; set; }

        public DateTime ProjectEndDate { get; set; }

        // Create List because in one project may have multiple employees
        // Use nullable reference types for nullable List<> properties(That's why we are using List<>?)
        public List<int>? ProjectEmployees { get; set; } = new();
    }
}