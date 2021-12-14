using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class PhoneBookContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = @"server=localhost; port=33062; database=test; user=root; password=123456; Persist Security Info=False; Connect Timeout=300";
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Phone> Phones { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map entities to tables  
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Phone>().ToTable("Phones");
            // Configure Primary Keys  
            modelBuilder.Entity<User>().HasKey(x => x.UserId).HasName("PK_Users");
            modelBuilder.Entity<Person>().HasKey(x => x.Id).HasName("PK_Persons");
            modelBuilder.Entity<Phone>().HasKey(x => x.Id).HasName("PK_Phones");

            // Configure columns  
            modelBuilder.Entity<User>().Property(x => x.UserId).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<User>().Property(x => x.FirstName).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<User>().Property(x => x.LastName).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnType("nvarchar(100)").IsRequired();

            modelBuilder.Entity<Person>().Property(x => x.Id).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Person>().Property(x => x.FirstName).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<Person>().Property(x => x.LastName).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<Person>().Property(x => x.Company).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<Person>().Property(x => x.UserId).HasColumnType("int").IsRequired();

            modelBuilder.Entity<Phone>().Property(x => x.Id).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Phone>().Property(x => x.PersonId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Phone>().Property(x => x.PhoneNumber).HasColumnType("nvarchar(15)").IsRequired();
        }
    }

}
