using EcoSendWeb.App_Start;
using EcoSendWeb.Models.BO.Home;
using EcoSendWeb.Models.DAO;
using System.Collections.Generic;

namespace EcoSendWeb.Mapping.Service
{
    public class HomeMappingProfile : AutoMapper.Profile
    {
        public override string ProfileName
        {
            get
            {
                return "Service.HomeMappingProfile";
            }
        }

        public HomeMappingProfile()
        {
            //this.CreateMap<tblAirInfo, AirInfo>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.pk_id))
            //    .ForMember(dest => dest.Segment, opt => opt.MapFrom(src => src.segment))
            //    .ForMember(dest => dest.PartNumber, opt => opt.MapFrom(src => src.part_number))
            //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
            //    .ForMember(dest => dest.ATA, opt => opt.MapFrom(src => src.ata))
            //    .ForMember(dest => dest.ABC, opt => opt.MapFrom(src => src.abc))
            //    .ForMember(dest => dest.MfgName, opt => opt.MapFrom(src => src.mfg_name))
            //    .ForMember(dest => dest.TwlvMonthsTotalSales, opt => opt.MapFrom(src => src.twlv_months_total_sales))
            //    .ForMember(dest => dest.TwlvMonthsTotalQuotes, opt => opt.MapFrom(src => src.twlv_months_total_quotes))
            //    .ForMember(dest => dest.TwlvMonthsTotalNoBids, opt => opt.MapFrom(src => src.twlv_months_total_no_bids))
            //    .ForMember(dest => dest.ReparePrice, opt => opt.MapFrom(src => src.repare_price))
            //    .ForMember(dest => dest.SuggestedPurchasePrice, opt => opt.MapFrom(src => src.suggested_purchase_price))
            //    .ForMember(dest => dest.Mod, opt => opt.MapFrom(src => src.mod))
            //    .ForMember(dest => dest.Aircraft, opt => opt.MapFrom(src => src.aircraft))
            //    .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => MappingProfilesConfig.Mapper.Map<IEnumerable<CategoryInfo>>(src.tblCategoryInfoes)));

            //this.CreateMap<AirInfo, tblAirInfo>()
            //    .ForMember(dest => dest.pk_id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.segment, opt => opt.MapFrom(src => src.Segment))
            //    .ForMember(dest => dest.part_number, opt => opt.MapFrom(src => src.PartNumber))
            //    .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description))
            //    .ForMember(dest => dest.ata, opt => opt.MapFrom(src => src.ATA))
            //    .ForMember(dest => dest.abc, opt => opt.MapFrom(src => src.ABC))
            //    .ForMember(dest => dest.mfg_name, opt => opt.MapFrom(src => src.MfgName))
            //    .ForMember(dest => dest.twlv_months_total_sales, opt => opt.MapFrom(src => src.TwlvMonthsTotalSales))
            //    .ForMember(dest => dest.twlv_months_total_quotes, opt => opt.MapFrom(src => src.TwlvMonthsTotalQuotes))
            //    .ForMember(dest => dest.twlv_months_total_no_bids, opt => opt.MapFrom(src => src.TwlvMonthsTotalNoBids))
            //    .ForMember(dest => dest.repare_price, opt => opt.MapFrom(src => src.ReparePrice))
            //    .ForMember(dest => dest.suggested_purchase_price, opt => opt.MapFrom(src => src.SuggestedPurchasePrice))
            //    .ForMember(dest => dest.mod, opt => opt.MapFrom(src => src.Mod))
            //    .ForMember(dest => dest.aircraft, opt => opt.MapFrom(src => src.Aircraft))
            //    .ForMember(dest => dest.tblConditionsHistories, opt => opt.Ignore())
            //    .ForMember(dest => dest.tblCategoryInfoes, opt => opt.Ignore());

            //this.CreateMap<tblConditionsHistory, ConditionHistory>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.pk_id))
            //    .ForMember(dest => dest.AirInfoId, opt => opt.MapFrom(src => src.fk_airinfo_id))
            //    .ForMember(dest => dest.ConditionType, opt => opt.MapFrom(src => src.condition_type))
            //    .ForMember(dest => dest.MinPrice, opt => opt.MapFrom(src => src.min_price))
            //    .ForMember(dest => dest.MaxPrice, opt => opt.MapFrom(src => src.max_price))
            //    .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.updated))
            //    .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.comment));

            //this.CreateMap<ConditionHistory, tblConditionsHistory>()
            //    .ForMember(dest => dest.pk_id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.fk_airinfo_id, opt => opt.MapFrom(src => src.AirInfoId))
            //    .ForMember(dest => dest.condition_type, opt => opt.MapFrom(src => (int)src.ConditionType))
            //    .ForMember(dest => dest.min_price, opt => opt.MapFrom(src => src.MinPrice))
            //    .ForMember(dest => dest.max_price, opt => opt.MapFrom(src => src.MaxPrice))
            //    .ForMember(dest => dest.updated, opt => opt.MapFrom(src => src.Updated))
            //    .ForMember(dest => dest.comment, opt => opt.MapFrom(src => src.Comment))
            //    .ForMember(dest => dest.tblAirInfo, opt => opt.Ignore());

            //this.CreateMap<tblCategoryInfo, CategoryInfo>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.pk_id))
            //    .ForMember(dest => dest.AirInfoId, opt => opt.MapFrom(src => src.fk_airinfo_id))
            //    .ForMember(dest => dest.CategoryType, opt => opt.MapFrom(src => src.category_type))
            //    .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.company))
            //    .ForMember(dest => dest.ConditionType, opt => opt.MapFrom(src => src.condition_type))
            //    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.date))
            //    .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.qty))
            //    .ForMember(dest => dest.LtDays, opt => opt.MapFrom(src => src.lt_days))
            //    .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.unit_price))
            //    .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.serial_number))
            //    .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.comment));

            //this.CreateMap<CategoryInfo, tblCategoryInfo>()
            //    .ForMember(dest => dest.pk_id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.fk_airinfo_id, opt => opt.MapFrom(src => src.AirInfoId))
            //    .ForMember(dest => dest.category_type, opt => opt.MapFrom(src => (int)src.CategoryType))
            //    .ForMember(dest => dest.company, opt => opt.MapFrom(src => src.Company))
            //    .ForMember(dest => dest.condition_type, opt => opt.MapFrom(src => (int)src.ConditionType))
            //    .ForMember(dest => dest.date, opt => opt.MapFrom(src => src.Date))
            //    .ForMember(dest => dest.qty, opt => opt.MapFrom(src => src.Qty))
            //    .ForMember(dest => dest.lt_days, opt => opt.MapFrom(src => src.LtDays))
            //    .ForMember(dest => dest.unit_price, opt => opt.MapFrom(src => src.UnitPrice))
            //    .ForMember(dest => dest.serial_number, opt => opt.MapFrom(src => src.SerialNumber))
            //    .ForMember(dest => dest.comment, opt => opt.MapFrom(src => src.Comment))
            //    .ForMember(dest => dest.tblAirInfo, opt => opt.Ignore());

        }
    }
}