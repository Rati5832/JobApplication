using JobApplication.Models.Domain;

namespace JobApplication.Models.DTOs
{
    public class UpdateJobDto
    {
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public int Salary { get; set; }
        public string Location { get; set; }
    }
}
