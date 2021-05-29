using System;

namespace EcoSendWeb.Models.BO.Parcel
{
    public class PaymentBO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int ParcelId { get; set; }
        public decimal Amount { get; set; }
        public bool Success { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ResultDate { get; set; }
    }
}