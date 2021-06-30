using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NapredniObrazec.Models
{
    [NotMapped]
    public class UserWithPassword : User
    {
        //[RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d][A-Za-z\d!@#$%^&*()_+]{7,19}$", ErrorMessage = "Vnesene geslo ni ustrezno")]
        [Required(ErrorMessage = "Potrebno je vnesti geslo")]
        [DataType(DataType.Password)]
        [DisplayName("Re-enter Password")]
        [Compare("Password2", ErrorMessage = "Passwords must match")]
        [UIHint("String")]
        public string Password1 { get; set; }

        [Required(ErrorMessage = "Potrebno je vnesti geslo")]
        [DataType(DataType.Password)]
        [DisplayName("Re-enter Password")]
        [Compare("Password1", ErrorMessage = "Passwords must match")]
        [UIHint("String")]
        public string Password2 { get; set; }
    }
}
