using EcoSendWeb.Models.BO.Account;
using EcoSendWeb.Models.DAO;

namespace EcoSendWeb.Mapping.Service
{
    public class AccountMappingProfile : AutoMapper.Profile
    {
        public override string ProfileName
        {
            get
            {
                return "Service.AccountMappingProfile";
            }
        }

        public AccountMappingProfile()
        {
            this.CreateMap<tblUser, UserBO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.pk_user))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.first_name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.last_name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.phone))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.city))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.street))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.postal_code))
                .ForMember(dest => dest.Pasport, opt => opt.MapFrom(src => src.pasport))
                .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => src.last_login))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.created_date))
                .ForMember(dest => dest.Points, opt => opt.Ignore())
                ;

        }
    }
}