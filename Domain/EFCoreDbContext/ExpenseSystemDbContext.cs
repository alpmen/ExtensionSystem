using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EFCoreDbContext
{
    public class ExpenseSystemDbContext : DbContext
    {
        public ExpenseSystemDbContext(DbContextOptions<ExpenseSystemDbContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "server=.\\SQLEXPRESS;database=ExpenseTracking;integrated security=true;TrustServerCertificate=True";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<ConsumerExpense> ConsumerExpenses { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
