using EcoSendWeb.Models.BO.Account;
using EcoSendWeb.Models.DAO;
using System;

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


            //this.CreateMap<tblRegisteredPerson, RegisteredPerson>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.pk_id))
            //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
            //    .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.registration_date))
            //    .ForMember(dest => dest.IsValid, opt => opt.MapFrom(src => src.is_valid))
            //    .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.is_admin))
            //    .ForMember(dest => dest.Password, opt => opt.Ignore());

            //this.CreateMap<RegisteredPerson, tblRegisteredPerson>()
            //    .ForMember(dest => dest.pk_id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
            //    .ForMember(dest => dest.registration_date, opt => opt.MapFrom(src => src.RegistrationDate))
            //    .ForMember(dest => dest.is_valid, opt => opt.MapFrom(src => src.IsValid))
            //    .ForMember(dest => dest.is_admin, opt => opt.MapFrom(src => src.IsAdmin))
            //    .ForMember(dest => dest.tblLoginInfoes, opt => opt.Ignore())
            //    .ForMember(dest => dest.passwordHash, opt => opt.Ignore());


            //this.CreateMap<tblLoginInfo, LoginInfo>()
            //    .ForMember(dest => dest.SessionId, opt => opt.MapFrom(src => src.pk_session))
            //    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.fk_user_id))
            //    .ForMember(dest => dest.LoginDate, opt => opt.MapFrom(src => src.login_date))
            //    .ForMember(dest => dest.UserAddress, opt => opt.MapFrom(src => src.user_address));

            //this.CreateMap<LoginInfo, tblLoginInfo>()
            //    .ForMember(dest => dest.pk_session, opt => opt.MapFrom(src => src.SessionId))
            //    .ForMember(dest => dest.fk_user_id, opt => opt.MapFrom(src => src.UserId))
            //    .ForMember(dest => dest.login_date, opt => opt.MapFrom(src => src.LoginDate))
            //    .ForMember(dest => dest.user_address, opt => opt.MapFrom(src => src.UserAddress))
            //    .ForMember(dest => dest.is_active, opt => opt.Ignore())
            //    .ForMember(dest => dest.tblRegisteredPerson, opt => opt.Ignore())
            //    .ForMember(dest => dest.tblUploadInfoes, opt => opt.Ignore());

            //this.CreateMap<tblUploadInfo, UploadInfo>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.pk_id))
            //    .ForMember(dest => dest.SessionId, opt => opt.MapFrom(src => src.fk_session))
            //    .ForMember(dest => dest.PartNumberCount, opt => opt.MapFrom(src => src.part_number_count))
            //    .ForMember(dest => dest.PartNumbers, opt => opt.MapFrom(src => src.part_numbers))
            //    .ForMember(dest => dest.UploadDate, opt => opt.MapFrom(src => src.upload_date));

            //this.CreateMap<UploadInfo, tblUploadInfo>()
            //    .ForMember(dest => dest.pk_id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.fk_session, opt => opt.MapFrom(src => src.SessionId))
            //    .ForMember(dest => dest.part_number_count, opt => opt.MapFrom(src => src.PartNumberCount))
            //    .ForMember(dest => dest.part_numbers, opt => opt.MapFrom(src => src.PartNumbers))
            //    .ForMember(dest => dest.upload_date, opt => opt.MapFrom(src => src.UploadDate))
            //    .ForMember(dest => dest.tblLoginInfo, opt => opt.Ignore());
        }
    }
}