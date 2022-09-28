using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Entities;
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
        DbSet<User> Users { get; set; }
        DbSet<OperationClaim> OperationClaims { get; set; }
        DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }

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

            modelBuilder.Entity<User>(x =>
            {
                x.ToTable("Users").HasKey(u => u.Id);
                x.Property(u => u.Id).HasColumnName("Id");
                x.Property(u => u.FirstName).HasColumnName("FirstName");
                x.Property(u => u.LastName).HasColumnName("LastName");
                x.Property(u => u.Email).HasColumnName("Email");
                x.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt");
                x.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
                x.Property(u => u.Status).HasColumnName("Status");
                x.HasMany(u => u.UserOperationClaims);
                x.HasMany(u => u.RefreshTokens);
            });

            modelBuilder.Entity<OperationClaim>(x=>
            {
                x.ToTable("OperationClaims").HasKey(o => o.Id);
                x.Property(o => o.Id).HasColumnName("Id");
                x.Property(o => o.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<UserOperationClaim>(x =>
            {
                x.ToTable("UserOperationClaims").HasKey(u => u.Id);
                x.Property(u=>u.Id).HasColumnName("Id");
                x.Property(u => u.UserId).HasColumnName("UserId");
                x.Property(u => u.OperationClaimId).HasColumnName("OperationClaimId");
                x.HasOne(o => o.OperationClaim);
                x.HasOne(o => o.User);
            });

            modelBuilder.Entity<RefreshToken>(x =>
            {
                x.ToTable("RefreshTokens").HasKey(r => r.Id);
                x.Property(r => r.Id).HasColumnName("Id");
                x.Property(r => r.UserId).HasColumnName("UserId");
                x.Property(r => r.Token).HasColumnName("Token");
                x.Property(r => r.Expires).HasColumnName("Expires");
                x.Property(r => r.Created).HasColumnName("Created");
                x.Property(r => r.CreatedByIp).HasColumnName("CreatedByIp");
                x.Property(r => r.Revoked).HasColumnName("Revoked");
                x.Property(r => r.RevokedByIp).HasColumnName("RevokedByIp");
                x.Property(r => r.ReplacedByToken).HasColumnName("ReplacedByToken");
                x.Property(r => r.ReasonRevoked).HasColumnName("ReasonRevoked");
                x.HasOne(r => r.User);
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
