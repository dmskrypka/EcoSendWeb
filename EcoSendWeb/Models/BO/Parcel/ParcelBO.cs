using System;

namespace EcoSendWeb.Models.BO.Parcel
{
    public class ParcelBO
    {
        public int Id { get; set; }
        public Guid SenderId { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderLastName { get; set; }

        public int PackType { get; set; }
        public int PackPoints { get; set; }
        public bool IsRecycled { get; set; }

        public Guid RecipientId { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string RecipientEmail { get; set; }

        public decimal Price { get; set; }
        public string DestCity { get; set; }
        public string DestStreet { get; set; }
        public string DestPostalCode { get; set; }
        public string DestCountry { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? PaidDate { get; set; }

        public Guid? EmployeeId { get; set; }

    }
}