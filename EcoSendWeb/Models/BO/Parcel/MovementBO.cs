using System;

namespace EcoSendWeb.Models.BO.Parcel
{
    public class MovementBO
    {
        public int ParcelId { get; set; }
        public string PackName { get; set; }
        public int Points { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}