using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFCoreOwnedTypeInheritence
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class OwnedTypesContext : DbContext
    {
        public DbSet<House> Houses { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=OwnedTypesInheritence;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<House>()
                .OwnsOne(e => e.Address);
            
            base.OnModelCreating(modelBuilder);
        }
    }

    public class Address
    {
        [MaxLength(25)]  public string Label { get; set; }
        [MaxLength(500)] public string StreetAddress1 { get; set; }
        [MaxLength(200)] public string City { get; set; }
        [MaxLength(2)]   public string State { get; set; }
        [MaxLength(5)]   public string Zip { get; set; }
    }

    public class ContactAddress : Address
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
    
    public class Contact
    {
        public int Id { get; set; }
        [MaxLength(100)] public string FirstName { get; set; }
        [MaxLength(200)] public string LastName { get; set; }
        public List<ContactAddress> Addresses { get; set; } = new List<ContactAddress>();
    }

    public class House
    {
        public int Id { get; set; }
        public Address Address { get; set; }
        public string OwnerName { get; set; }
    }

}
