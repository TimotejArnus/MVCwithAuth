using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NapredniObrazec.Models
{

    public class Car
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Polje je zahtevano")]
        
        public string Proizvajalec { get; set; }
        [Required(ErrorMessage = "Polje je zahtevano")]
        
        public string model { get; set; }
        [Required(ErrorMessage = "Polje je zahtevano")]
       
        public int kilometri { get; set; }

        [Required(ErrorMessage = "Polje je zahtevano")]
        
        public string Registracija { get; set; }

        public ICollection<Car> Categories { get; set; }


    }
}
