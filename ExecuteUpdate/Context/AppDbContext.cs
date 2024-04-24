using ExecuteUpdate.Models;
using Microsoft.EntityFrameworkCore;

namespace ExecuteUpdate.Context
{
    public class AppDbContext : DbContext
    {
        readonly string con = "server=localhost;initial catalog=AlunosDb;uid=root;pwd=Ro123459";
        public DbSet<Aluno> Alunos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(con, ServerVersion.AutoDetect(con))
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }
    }
}