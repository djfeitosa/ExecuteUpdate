using ExecuteUpdate.Models;

namespace ExecuteUpdate.Context
{
    public class SeedDatabase
    {
        public static void PopulaDb(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Random random = new();

            context.Alunos.AddRange(Enumerable.Range(1, 500).Select(i =>
            {
                return new Aluno
                {
                    Nome = $"{nameof(Aluno.Nome)}-{i}",
                    Email = $"{nameof(Aluno.Email)}-{i}",
                    Idade = random.Next(15, 20),
                    Ativo = i % 2 == 0,
                    DataMatricula = DateTime.UtcNow
                };
            }));
            context.SaveChanges();
        }
    }
}