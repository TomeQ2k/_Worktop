using AutoMapper;
using Worktop.Core.Common.Helpers;
using Worktop.Core.Domain.Entities;
using Worktop.WebApp.ViewModels;
using Worktop.WebApp.ViewModels.Components;

namespace Worktop.WebApp.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, ProfileViewModel>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(u => u.Job.Title))
                .ForMember(dest => dest.TotalSalary, opt => opt.MapFrom(u => u.Job.Salary * (decimal)u.SalaryBonus));
            CreateMap<User, WorkerDetailsViewModel>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(u => u.Job.Title))
                .ForMember(dest => dest.BaseSalary, opt => opt.MapFrom(u => u.Job.Salary))
                .ForMember(dest => dest.TotalSalary, opt => opt.MapFrom(u => u.Job.Salary * (decimal)u.SalaryBonus));

            CreateMap<TaskItem, EditTaskViewModel>();

            CreateMap<OpinionCreatorViewModel, Opinion>()
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore());

            CreateMap<Article, EditArticleViewModel>()
                .ForMember(dest => dest.ArticleTitle, opt => opt.MapFrom(a => a.Title))
                .ForMember(dest => dest.Title, opt => opt.Ignore());

            CreateMap<MailViewModel, Mail>();

            CreateMap<MessageFormViewModel, Message>();

            CreateMap<ChatRoom, EditRoomViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(vm => vm.Id ?? Utils.Id()))
                .ForMember(dest => dest.IsPassword, opt => opt.MapFrom(vm => !string.IsNullOrEmpty(vm.Password)));
        }
    }
}