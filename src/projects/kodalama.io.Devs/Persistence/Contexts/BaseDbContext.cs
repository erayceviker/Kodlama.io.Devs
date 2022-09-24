using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration;

        DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        DbSet<Technology> Technologies { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(p =>
            {
                p.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                p.Property(p => p.Id).HasColumnName("Id");
                p.Property(p => p.Name).HasColumnName("Name");
                p.HasMany(x => x.Technologies);
            });

            modelBuilder.Entity<Technology>(x =>
            {
                x.ToTable("Technologies").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("Id");
                x.Property(p => p.Name).HasColumnName("Name");
                x.HasOne(p => p.ProgrammingLanguage);
            });

            ProgrammingLanguage[] programmingLanguageSeeds = { new(1, "Python"), new(2, "CSharp"), new(3, "Java") };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageSeeds);

            Technology[] technologyEntitySeeds =
            {
                new(1, 1, "Vue"),
                new(2,1,"React"),
                new(3,2,"WPF"),
                new(4,2,"ASP.NET"),
                new(5,3,"Spring"),
                new(6,3,"JSP")
            };
            modelBuilder.Entity<Technology>().HasData(technologyEntitySeeds);

        }
    }
}
