using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AutoMapper;
using Dal;
using Dto.models;

namespace DTO.Classes
{
    public class CopyRightDProfile : Profile
    {
        public CopyRightDProfile()
        {
            CreateMap<Dal.Models.Task, Tasks>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.StatusNavigation))
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.AssignedToNavigation))
                .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.PriorityNavigation))
                .ReverseMap()
                .ForMember(dest => dest.StatusNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Id))
                .ForMember(dest => dest.AssignedToNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.PriorityNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.Id))
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.AssignedTo.UserId))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Project.ProjectId));

            CreateMap<Dal.Models.User, User>().ReverseMap();
            CreateMap<Dal.Models.Project, Projects>().ReverseMap();
            CreateMap<Dal.Models.Lead, Leads>().ReverseMap();
            CreateMap<Dal.Models.Document, Documents>().ReverseMap();
            CreateMap<Dal.Models.Document, Documents>().ReverseMap();
            CreateMap<Dal.Models.Customer, Customers>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.StatusNavigation))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Id))
                .ForMember(dest => dest.StatusNavigation, opt => opt.Ignore());
            CreateMap<Dal.Models.RoleCode, RoleCode>().ReverseMap();
            CreateMap<Dal.Models.Communication, Communications>().ReverseMap();
            CreateMap<Dal.Models.StatusCodeProject, StatusCodeProject>().ReverseMap();
            CreateMap<Dal.Models.PriorityCode, Dto.models.PriorityCode>().ReverseMap();
            CreateMap<Dal.Models.StatusCodeUser, Dto.models.StatusCodeUser>().ReverseMap();
        }
    }
}
