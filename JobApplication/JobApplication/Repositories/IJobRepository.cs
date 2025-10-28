using JobApplication.Models.Domain;
using JobApplication.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Repositories
{
    public interface IJobRepository
    {
        Task<List<Job>> GetAllJobsAsync(string? filterOn, string? filterQuery,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 30);

        Task<Job?> GetJobByIdAsync(Guid id);

        Task<Job> CreateJobAsync(Job jobRequest);

        Task<Job?> UpdateJobAsync(Guid id, Job job);
        Task<Job?> DeleteJobAsync(Guid id);
    }
}
