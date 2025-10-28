using JobApplication.Models.Domain;

namespace JobApplication.Models.DTOs
{
    public class ApplicationJobResponseDto
    {
    public Guid Id { get; set; }
    public Guid ApplicantId { get; set; }
    public Guid JobId { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string? ResumeUrl { get; set; }
    public DateTime AppliedOn { get; set; } = DateTime.Now;

    }
}
