using Microsoft.EntityFrameworkCore;
using Santex.League.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Santex.League.Repository
{
    public partial class LeagueDbContext : DbContext
    {
        public LeagueDbContext(DbContextOptions<LeagueDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Competition>().ToTable(nameof(Competition));
            modelBuilder.Entity<Team>().ToTable(nameof(Team));
            modelBuilder.Entity<Player>().ToTable(nameof(Player));

            modelBuilder.Entity<Competition>()
                .HasKey(e => new { e.Id });

            modelBuilder.Entity<Competition>()
                .HasMany(x => x.Teams)
                .WithOne(x => x.Competition)
                .HasForeignKey(x => x.CompetitionId);

            modelBuilder.Entity<Team>()
               .HasKey(e => new { e.Id });

            modelBuilder.Entity<Team>()
                .HasMany(x => x.Players)
                .WithOne(x => x.Team)
                .HasForeignKey(x => x.TeamId);

            modelBuilder.Entity<Player>()
                .HasKey(e => new { e.Id });

        }


        public virtual DbSet<Competition> Competition { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Team> Team { get; set; }

    }
}
