using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Entities.Entities;

namespace DataAccess.Context
{
    public class SumoDbContext : DbContext
    {
        public SumoDbContext(): base("name = SumoDbConnectionString")
        {
            
        }

        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupTeam> GroupTeams { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here..
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}