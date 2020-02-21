using System;
using System.ComponentModel.DataAnnotations;

namespace beltExam.Models
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value < DateTime.Now)
            {
            return new ValidationResult("Must be in the future");
            }
            return ValidationResult.Success;
        }
    }
}