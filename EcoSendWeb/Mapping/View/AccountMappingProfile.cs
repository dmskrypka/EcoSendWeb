using EcoSendWeb.Models.BO.Account;
using EcoSendWeb.Models.BO.Parcel;
using EcoSendWeb.Models.View.Account;
using EcoSendWeb.Models.View.Parcel;

namespace EcoSendWeb.Mapping.View
{
    public class AccountMappingProfile : AutoMapper.Profile
    {
        public override string ProfileName
        {
            get
            {
                return "View.AccountMappingProfile";
            }
        }

        public AccountMappingProfile()
        {
            this.CreateMap<RegistrationVM, UserBO>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.LastLogin, opt => opt.Ignore())
                .ForMember(dest => dest.Points, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore());

            this.CreateMap<UserBO, UserVM>();

            this.CreateMap<UserVM, UserBO>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Points, opt => opt.Ignore())
                .ForMember(dest => dest.LastLogin, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                ;

            this.CreateMap<PackBO, PackVM>();

        }
    }
}