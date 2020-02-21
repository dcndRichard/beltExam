using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace beltExam.Models
{
    [NotMapped]
    public class IndexViewModel
    {
        public User NewRegister { get; set; }
        public Login NewLogin { get; set; }
    }
}