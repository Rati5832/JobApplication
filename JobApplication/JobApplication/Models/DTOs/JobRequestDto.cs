namespace JobApplication.Models.DTOs
{
    public class JobRequestDto
    {
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public int Salary { get; set; }
        public string Location { get; set; }
    }
}
