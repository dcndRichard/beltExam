using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace beltExam.Models
{
    [NotMapped]
    public class Login
    {
        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string LoginEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string LoginPassword { get; set; }
    }
}