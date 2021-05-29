using System;

namespace EcoSendWeb.Models.BO.Account
{
    public class UploadInfo
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public int PartNumberCount { get; set; }
        public string PartNumbers { get; set; }
        public DateTime UploadDate { get; set; }
    }
}