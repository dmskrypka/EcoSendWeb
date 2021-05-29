using System;
using System.ComponentModel.DataAnnotations;

namespace EcoSendWeb.Models.View.Account
{
    public class UserUploadVM
    {
        public Guid UserId { get; set; }

        public Guid UploadId { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Count")]
        public int UploadCount { get; set; }

        public DateTime UploadDate { get; set; }

        [Display(Name = "Upload date")]
        public string StrUploadDate
        {
            get => this.UploadDate.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}