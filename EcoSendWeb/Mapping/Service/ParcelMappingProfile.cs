using EcoSendWeb.Models.BO.Parcel;
using EcoSendWeb.Models.DAO;
using System;
using System.Data;

namespace EcoSendWeb.Mapping.Service
{
    public class ParcelMappingProfile : AutoMapper.Profile
    {
        public override string ProfileName
        {
            get
            {
                return "Service.ParcelMappingProfile";
            }
        }

        public ParcelMappingProfile()
        {
            this.CreateMap<DataRow, ParcelBO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int)src["pk_parcel"]))
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => (Guid)src["fk_sender"]))
                .ForMember(dest => dest.SenderFirstName, opt => opt.MapFrom(src => (string)src["sender_first_name"]))
                .ForMember(dest => dest.SenderLastName, opt => opt.MapFrom(src => (string)src["sender_last_name"]))
                .ForMember(dest => dest.PackType, opt => opt.MapFrom(src => (int)src["fk_pack"]))
                .ForMember(dest => dest.PackPoints, opt => opt.MapFrom(src => (int)src["points"]))
                .ForMember(dest => dest.IsRecycled, opt => opt.MapFrom(src => (bool)src["is_pack_recycled"]))
                .ForMember(dest => dest.RecipientId, opt => opt.MapFrom(src => (Guid)src["fk_recipient"]))
                .ForMember(dest => dest.RecipientFirstName, opt => opt.MapFrom(src => (string)src["recipient_first_name"]))
                .ForMember(dest => dest.RecipientLastName, opt => opt.MapFrom(src => (string)src["recipient_last_name"]))
                .ForMember(dest => dest.RecipientEmail, opt => opt.MapFrom(src => (string)src["recipient_last_name"]))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => (decimal)src["price"]))
                .ForMember(dest => dest.DestCity, opt => opt.MapFrom(src => (string)src["destination_city"]))
                .ForMember(dest => dest.DestStreet, opt => opt.MapFrom(src => (string)src["destination_street"]))
                .ForMember(dest => dest.DestPostalCode, opt => opt.MapFrom(src => (string)src["destination_postal_code"]))
                .ForMember(dest => dest.DestCountry, opt => opt.MapFrom(src => (string)src["destination_country"]))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => (DateTime)src["created_date"]))
                .ForMember(dest => dest.ConfirmedDate, opt => opt.MapFrom(src => src["confirmed_date"] != DBNull.Value ? (DateTime?)src["confirmed_date"] : null))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src["fk_employee"] != DBNull.Value ? (Guid?)src["fk_employee"] : null))
                .ForMember(dest => dest.ReceivedDate, opt => opt.MapFrom(src => src["received_date"] != DBNull.Value ? (DateTime?)src["received_date"] : null))
                .ForMember(dest => dest.PaidDate, opt => opt.MapFrom(src => src["paid_date"] != DBNull.Value ? (DateTime?)src["paid_date"] : null))
                ;

            this.CreateMap<tblPack, PackBO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.pk_pack))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.points))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price))
                ;

            this.CreateMap<DataRow, MovementBO>()
                .ForMember(dest => dest.ParcelId, opt => opt.MapFrom(src => (int)src["fk_parcel"]))
                .ForMember(dest => dest.PackName, opt => opt.MapFrom(src => (string)src["name"]))
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => (int)src["points"]))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => (DateTime)src["created_date"]))
                ;

            this.CreateMap<tblPayment, PaymentBO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.pk_payment))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.fk_user))
                .ForMember(dest => dest.ParcelId, opt => opt.MapFrom(src => src.fk_parcel))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.amount))
                .ForMember(dest => dest.Success, opt => opt.MapFrom(src => src.success))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.created_date))
                .ForMember(dest => dest.ResultDate, opt => opt.MapFrom(src => src.result_date))
                ;

        }
    }
}