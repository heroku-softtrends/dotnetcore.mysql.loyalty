using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loyalty.Models
{
    public class Login
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You cannot use a blank password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserID { get; set; }

    }
}
