using Microsoft.EntityFrameworkCore;
using Practica_programada_4.Models;

using Microsoft.EntityFrameworkCore;
using System.IO;
using Practica_programada_4.Models;

namespace Practica_programada_4
{
    public class AppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TitleTag> TitlesTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "data", "books.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            // Nombre de tabla TitlesTags (Fluent API)
            model.Entity<TitleTag>().ToTable("TitlesTags");

            // Orden de columnas en Title
            model.Entity<Title>().Property(t => t.TitleId).HasColumnOrder(0);
            model.Entity<Title>().Property(t => t.AuthorId).HasColumnOrder(1);
            model.Entity<Title>().Property(t => t.TitleName).HasColumnOrder(2);
        }
    }
}
