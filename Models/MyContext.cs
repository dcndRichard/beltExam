using Microsoft.EntityFrameworkCore;

namespace beltExam.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        public DbSet<User> Users {get; set;}
        public DbSet<FunThing> FunThings {get; set;}
        public DbSet<Participant> Participants {get; set;}
    }
}