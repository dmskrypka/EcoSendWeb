using EcoSendWeb.Models.BO.Parcel;
using EcoSendWeb.Models.View.Parcel;
using System.Web.Mvc;

namespace EcoSendWeb.Mapping.View
{
    public class ParcelMappingProfile : AutoMapper.Profile
    {
        public override string ProfileName
        {
            get
            {
                return "View.ParcelMappingProfile";
            }
        }

        public ParcelMappingProfile()
        {
            this.CreateMap<ParcelBO, ParcelVM>()
                .ForMember(dest => dest.RecipientFullName, opt => opt.MapFrom(src => $"{src.RecipientFirstName} {src.RecipientLastName}"))
                .ForMember(dest => dest.SenderFullName, opt => opt.MapFrom(src => $"{src.SenderFirstName} {src.SenderLastName}"))
                .ForMember(dest => dest.PackTypes, opt => opt.Ignore())
                ;

            this.CreateMap<PackBO, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => $"{src.Name} ({src.Price:#.00} UAH)"))
                .ForAllOtherMembers(opt => opt.Ignore())
                ;

            this.CreateMap<NewParcelVM, ParcelBO>()
                .ForMember(dest => dest.RecipientFirstName, opt => opt.MapFrom(src => src.RecipientFirstName))
                .ForMember(dest => dest.RecipientLastName, opt => opt.MapFrom(src => src.RecipientLastName))
                .ForMember(dest => dest.RecipientEmail, opt => opt.MapFrom(src => src.RecipientEmail))
                .ForMember(dest => dest.DestCity, opt => opt.MapFrom(src => src.DestCity))
                .ForMember(dest => dest.DestStreet, opt => opt.MapFrom(src => src.DestStreet))
                .ForMember(dest => dest.DestPostalCode, opt => opt.MapFrom(src => src.DestPostalCode))
                .ForMember(dest => dest.DestCountry, opt => opt.MapFrom(src => src.DestCountry))
                .ForMember(dest => dest.PackType, opt => opt.MapFrom(src => src.PackType))
                .ForAllOtherMembers(opt => opt.Ignore())
                ;

            this.CreateMap<MovementBO, MovementVM>();

        }
    }
}