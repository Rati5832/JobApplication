namespace JobApplication.Models.DTOs
{
    public class JobResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public int Salary { get; set; }
        public string Location { get; set; }
        public Guid PostedById { get; set; }
    }
}
