using System.ComponentModel.DataAnnotations;
using System;

namespace beltExam.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantId {get;set;} //primary key

        public int UserId {get;set;} //forign key

        public int FunThingId {get;set;} //forign key

        public bool isGoing {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;


        // NAVIGATION PROPERTIES
        
        // for M : M
        public User Attendant {get;set;}
        // for M : M
        public FunThing FunThing {get;set;}

    }
}