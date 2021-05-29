using EcoSendWeb.App_Start;
using EcoSendWeb.Models.BO.Home;
using EcoSendWeb.Models.View.Home;
using System.Collections.Generic;

namespace EcoSendWeb.Mapping.View
{
    public class HomeMappingProfile : AutoMapper.Profile
    {
        public override string ProfileName
        {
            get
            {
                return "View.HomeMappingProfile";
            }
        }

        public HomeMappingProfile()
        {
            this.CreateMap<AirInfoVM, AirInfo>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => MappingProfilesConfig.Mapper.Map<IEnumerable<CategoryInfo>>(src.Categories)));

            this.CreateMap<AirInfo, AirInfoVM>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => MappingProfilesConfig.Mapper.Map<IEnumerable<CategoryVM>>(src.Categories)))
                .ForMember(dest => dest.StrReparePrice, opt => opt.Ignore())
                .ForMember(dest => dest.Conditions, opt => opt.Ignore())
                .ForMember(dest => dest.StrSuggestedPurchasePrice, opt => opt.Ignore())
                .ForMember(dest => dest.ModsList, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryNames, opt => opt.Ignore());

            this.CreateMap<ConditionHistory, ConditionVM>()
                .ForMember(dest => dest.StrUpdated, opt => opt.Ignore())
                .ForMember(dest => dest.StrChartUpdated, opt => opt.Ignore())
                .ForMember(dest => dest.StrMinPrice, opt => opt.Ignore())
                .ForMember(dest => dest.StrMaxPrice, opt => opt.Ignore());

            this.CreateMap<ConditionVM, ConditionHistory>()
                .ForMember(dest => dest.AirInfoId, opt => opt.Ignore());

            this.CreateMap<CategoryInfo, CategoryVM>()
                .ForMember(dest => dest.StrDate, opt => opt.Ignore());

            this.CreateMap<CategoryVM, CategoryInfo>();
        }

    }
}