using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace beltExam.Models
{
    public class FunThing
    {
        [Key]
        public int FunThingId {get;set;}

        [Required]
        [Display(Name="Title")]
        [MinLength(2)]
        public string Title {get;set;}

        [Required]
        [Display(Name="Duration")]
        [MinLength(1)]
        public int Duration {get;set;}

        public string hourMin {get;set;}

        [Required]
        [Display(Name="Description")]
        [MinLength(2)]
        [DataType(DataType.Text)]
        public string Desc {get;set;}

        [FutureDate]     
        [Required(ErrorMessage="Date should be in the future.")]
        // [DataType(DataType.Date)]
        public DateTime Date {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;



        // Foriegn Key for  1 : M
        public int UserId {get;set;}


        
        // NAVIGATION PROPERTY


        // for 1 : M
        public User FunThingCreator {get;set;}
        // for  M : M
        public List<Participant> Participants {get;set;}

    }
}