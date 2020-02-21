using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace beltExam.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}


        [Required]
        [Display(Name="Name")]
        public string Name {get;set;}


        [Required]
        [EmailAddress]
        public string Email {get;set;}


        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",ErrorMessage="Password must be at least 8 digits long and include at least one number,one alpha and one special char.")]
        // [RegularExpression(@"^(?=.*\d).{8,20}$",ErrorMessage="Password must be at least 8 digits long and include at least one numeric digit.")]
        public string Password {get;set;}

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [NotMapped]
        public string ConfirmPassword {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;


        // NAVIGATION PROPERTY
        // for 1 : M
        public List<FunThing> FunThingsCreated {get;set;}
        // for M : M
        public List<Participant> FunThingsAttending {get;set;}

    }
}