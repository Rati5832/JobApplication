namespace JobApplication.Models.Domain
{
    public class SavedJob
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }
        public Job Job { get; set; }
        
        public Guid UserId { get; set; }
        public DateTime SavedOn { get; set; }

    }
}
