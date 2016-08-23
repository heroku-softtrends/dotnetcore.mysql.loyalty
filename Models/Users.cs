using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loyalty.Models
{
    public class Users
    {

        public string UserID { get; set; }


        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateofBirth { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Email ID is required")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public Byte IsActive { get; set; }
    }
}
