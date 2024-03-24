using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ContactBookDbContext : DbContext
    {
        // db collections
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ContactBookDbNPD211;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>().HasData(new Contact[]
            {
                new Contact() { Id = 1, Name = "Vova", Age = 30, Surname = "Pupkin", Phone = "+3807575828", IsMale = true },
                new Contact() { Id = 2, Name = "Marusia", Age = 25, Surname = "Shishik", Phone = "+380771244", IsMale = false },
                new Contact() { Id = 3, Name = "Olga", Age = 33, Surname = "Shelesh", Phone = "+38067285792", IsMale = false }
            });
        }
    }
}
