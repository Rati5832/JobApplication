namespace JobApplication.Models.Domain
{
    public class ApplicationJob
    {
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }

        public Guid JobId { get; set; }
        public Job Job { get; set; }
        public string? ResumeUrl { get; set; }
        public DateTime AppliedOn { get; set; } = DateTime.UtcNow;
    }
}
