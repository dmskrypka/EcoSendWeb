using System;

namespace EcoSendWeb.Models.BO.Account
{
    public class UserUpload
    {
        public Guid UserId { get; set; }

        public Guid UploadId { get; set; }

        public string Email { get; set; }

        public int UploadCount { get; set; }

        public DateTime UploadDate { get; set; }
    }
}