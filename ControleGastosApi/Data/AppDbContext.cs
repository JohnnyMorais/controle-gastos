using Microsoft.EntityFrameworkCore;
using ControleGastosApi.Models;
 
namespace ControleGastosApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
 
        public DbSet<Person> People { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
 
            // Configuração explícita da deleção em cascata (Cascade Delete)
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Person)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}