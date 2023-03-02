using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestT.Models
{
    public class TestContext : DbContext
    {
        public DbSet<CompanyTest> test { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-9HUR6QM\SQLEXPRESS;Database=TESTCOM;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
