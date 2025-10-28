using JobApplication.Models.Domain;

namespace JobApplication.Models.DTOs
{
    public class SavedJobsResponseDto
    {
        public Guid JobId { get; set; }
        public Guid UserId { get; set; }
        public string CompanyName { get; set; } = String.Empty;
        public string JobTitle { get; set; } = String.Empty;
        public DateTime SavedOn { get; set; } = DateTime.UtcNow;
    }
}
