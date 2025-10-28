using JobApplication.Models.Domain;

namespace JobApplication.Models.DTOs
{
    public class ApplicationJobRequestDto
    {
        public Guid JobId { get; set; }
        public string? ResumeUrl { get; set; }
    }
}
