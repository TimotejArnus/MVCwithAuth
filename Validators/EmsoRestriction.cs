using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NapredniObrazec.Validators
{
    public class EmsoRestriction : ValidationAttribute
    {
        private int[] mnozitelji = new[] { 7, 6, 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
       
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int vsota = 0;

            if (value == null)
            {
                return new ValidationResult("Napaka pri vnosu emsa");
            }

            string emso = value.ToString();

            if (emso.Length == 13)
            {

                for (int i = 0; i < value.ToString().Length - 1; i++)
                {
                    vsota += int.Parse(emso[i].ToString()) * mnozitelji[i];
                }

                if (11 - (vsota % 11) == int.Parse(emso[12].ToString()))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("EmsoNiVeljaven");
                }

                
            }

            return new ValidationResult("Napaka pri vnosu emsa");
        }
    }
}
