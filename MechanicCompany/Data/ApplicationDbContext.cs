using System;
using System.Collections.Generic;
using System.Text;
using MechanicCompany.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MechanicCompany.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Mechanic> Mechanics { get; set; }
        public virtual DbSet<RepairRecord> RepairRecords { get; set; }
        public virtual DbSet<RepairPart> RepairParts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>()
                .HasDiscriminator<int>("UserType")
                .HasValue<IdentityUser>(0)
                .HasValue<ApplicationUser>(1);

            builder.Entity<RepairRecord>()
                .HasKey(s => s.Id);

            builder.Entity<Car>()
                .HasKey(g => g.Id);

            builder.Entity<Mechanic>()
                .HasKey(g => g.Id);

            builder.Entity<ApplicationUser>()
                .HasMany(c => c.Cars)
                .WithOne(e => e.ApplicationUser)
                .HasForeignKey(f => f.ApplicationUserId);

            builder.Entity<Car>()
                .HasMany(c => c.RepairRecords)
                .WithOne(e => e.Car)
                .HasForeignKey(f => f.CarId);

            builder.Entity<Mechanic>()
                .HasMany(c => c.RepairRecords)
                .WithOne(e => e.Mechanic)
                .HasForeignKey(f => f.MechanicId);

            builder.Entity<RepairRecord>()
                .HasMany(c => c.RepairPart)
                .WithOne(e => e.RepairRecord)
                .HasForeignKey(f => f.RepairRecordId);
        }
    }
}
