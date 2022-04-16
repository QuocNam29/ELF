using System;
using System.ComponentModel.DataAnnotations;


namespace ELF.Models
{
    public class DateValidationAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime todayDate = Convert.ToDateTime(value);
            return todayDate <= DateTime.Now;
        }
    }
}