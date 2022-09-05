using AutoMapper;
using RP_Ovning15.Core.Dto;
using RP_Ovning15.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace RP_Ovning15.Data.Data
{
    public class LmsMappings : Profile
    {
        public LmsMappings()
        {
            CreateMap<Course, CourseDto>()
                .ForMember(d => d.Title, f => f.MapFrom(o => o.Title))
                .ForMember(d => d.StartDate, f => f.MapFrom(o => o.StartDate));
            CreateMap<Course, CourseDto>()
                .ForMember(d => d.Title, f => f.MapFrom(o => o.Title))
                .ForMember(d => d.StartDate, f => f.MapFrom(o => o.StartDate)).ReverseMap();

            CreateMap<Module, ModuleDto>()
                .ForMember(d => d.Title, f => f.MapFrom(o => o.Title))
                .ForMember(d => d.StartDate, f => f.MapFrom(o => o.StartDate));
            CreateMap<Module, ModuleDto>()
                .ForMember(d => d.Title, f => f.MapFrom(o => o.Title))
                .ForMember(d => d.StartDate, f => f.MapFrom(o => o.StartDate)).ReverseMap();
        }

    }
}
