using AutoMapper;
using Prism.Application.DTOs;
using Prism.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.Application.Configuration
{
    public class AutomapConfig : Profile
    {
        public AutomapConfig()
        {
            CreateMap<AppUser, AccountDTO>()
                .ForMember(dest => dest.Phone, source => source.MapFrom(x => x.PhoneNumber))
                .ReverseMap();
        }
    }
}