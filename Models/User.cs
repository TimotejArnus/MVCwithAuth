using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NapredniObrazec.Validators;

namespace NapredniObrazec.Models
{
    public class User : IdentityUser
    {
        //[ScaffoldColumn(false)]
        //public int Id { get; set; }

        // 1. korak
        //[RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Uporabite samo crke")]
        [Required(ErrorMessage = "potrebno je vnesti Name")]
        public string Name { get; set; }

        //[RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Uporabite samo crke")]
        [Required(ErrorMessage = "potrebno je vnesti Name")]

        public string LastName { get; set; }


        //[DataType(DataType.DateTime, ErrorMessage = "Napacen datum")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[Required(ErrorMessage = "potrebno je vnesti datum rojstva")]
        //public DateTime DateOfBirth { get; set; }


        //[UIHint("Int32")]
        //public int Age { get; set; }    // TODO: Po datumu

        //[MinLength(13, ErrorMessage = "Emso mora biti dolg 13 stevilk")]  // TEST 
        //[MaxLength(13, ErrorMessage = "Emso mora biti dolg 13 stevilk")]  // TEST 
        //[StringLength(13)]
        //[RegularExpression("([1-9][0-9]*)", ErrorMessage = "Vnesite samo stevila")]
        //[Required(ErrorMessage = "potrebno je vnesti EMSO")]
        //[EmsoRestriction]
        //public string EMSO { get; set; }

        // 2. korak
       
        //public string Address { get; set; }
       
        //[UIHint("Int32")]
        //public int PostNumber { get; set; }

       

        //public string Post { get; set; }
       
        //public string Country { get; set; }

        // 3. korak 

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "The email address is required")]
        //[EmailAddress(ErrorMessage = "Napacen email Address")]
        [UIHint("String")]
        public string Email { get; set; }

       


    }
}
