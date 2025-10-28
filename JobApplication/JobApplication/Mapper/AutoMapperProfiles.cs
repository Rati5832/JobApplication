using AutoMapper;
using JobApplication.Models.Domain;
using JobApplication.Models.DTOs;

namespace JobApplication.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Job, JobResponseDto>().ReverseMap();
            CreateMap<Job, JobRequestDto>().ReverseMap();
            CreateMap<Job, UpdateJobDto>().ReverseMap();

            CreateMap<ApplicationJob, UpdateJobDto>().ReverseMap();

            CreateMap<ApplicationJob, ApplicationJobResponseDto>()
                .ForMember(destination => destination.JobTitle, existing => existing.MapFrom(src => src.Job.Title))
                .ForMember(destination => destination.CompanyName, existing => existing.MapFrom(src => src.Job.CompanyName))
                .ReverseMap();

            CreateMap<SavedJob, SavedJobsRequestDto>().ReverseMap();
            CreateMap<SavedJob, SavedJobsResponseDto>()
                .ForMember(destination => destination.CompanyName, existing => existing.MapFrom(src => src.Job.CompanyName))
                .ForMember(destination => destination.JobTitle, existing => existing.MapFrom(src => src.Job.Title))
                .ReverseMap();

           CreateMap<ApplicationUser, ApplicationUserResponseDto>().ReverseMap();


        }

    }
}
