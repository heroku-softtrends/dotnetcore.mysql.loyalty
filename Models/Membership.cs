using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loyalty.Models
{
    public class Membership
    {
        public string MembershipID { get; set; }

        public string UserID { get; set; }

        [Required(ErrorMessage = "Member ID is required")]
        public string MemberID { get; set; }

        [Required(ErrorMessage = "Loyalty Card No is required")]
        public string LoyaltyCardNo { get; set; }
        [Required(ErrorMessage = "Expiration Date is required")]
        public DateTime ExpirationDate { get; set; }
        [Required(ErrorMessage = "Member Site URL is required")]
        public string MemberSiteURL { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
