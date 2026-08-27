using Example.Data.Mappings;
using ExampleMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Data
{
    public partial class ExampleMSContext : DbContext
    {

        public ExampleMSContext()
        {
        }

        public ExampleMSContext(DbContextOptions<ExampleMSContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;Database=ExampleDb;Trusted_Connection=True;TrustServerCertificate=True");
            //base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<ExampleData> Example{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfiguration( new ExampleMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
