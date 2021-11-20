using BoutiqueAPI.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiqueAPI.Data
{
    public class LibraryDbContext : IdentityDbContext
    {
        public DbSet<BoutiqueEntity> Boutiques { get; set; }
        public DbSet<ClothesEntity> Clothes { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            :base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<BoutiqueEntity>().ToTable("Boutiques");
            modelBuilder.Entity<BoutiqueEntity>().Property(b => b.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<BoutiqueEntity>().HasMany(b => b.Clothes).WithOne(c => c.Boutique);

            modelBuilder.Entity<ClothesEntity>().ToTable("Clothes");
            modelBuilder.Entity<ClothesEntity>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ClothesEntity>().HasOne(c => c.Boutique).WithMany(b => b.Clothes);
        }
        //dotnet tool install --global dotnet-ef
        //dotnet ef migrations add InitialCreate
        //dotnet ef database update
    }
}
