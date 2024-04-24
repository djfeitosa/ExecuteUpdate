using ExecuteUpdate.Context;
using Microsoft.EntityFrameworkCore;

while (true)
{
    Console.WriteLine("\nIniciando o processamento...");
    Console.WriteLine("\nSelecione a opção : ");
    Console.WriteLine("(1) - Popular as tabelas");
    Console.WriteLine("(2) - Atualizar sem ExecuteUpdate");
    Console.WriteLine("(3) - Atualizar com ExecuteUpdate");
    Console.WriteLine("(4) - Atualizar com ExecuteUpdate e filtro");
    Console.WriteLine("(5) - Atualizações e ChangeTracker");
    Console.WriteLine("(9) - Encerra");

    int opcao = Convert.ToInt32(Console.ReadLine());

    if (opcao == 1)
    {
        PopulaTabelas();
    }
    else if (opcao == 2)
    {
        AtualizacaoPadrao();
    }
    else if (opcao == 3)
    {
        AtualizacaoComExecuteUpdate();
    }
    else if ((opcao == 4))
    {
        AtualizacaoComExecuteUpdateFiltro();
    }
    else if (opcao == 5)
    {
        AtualizacaoChangeTracker();
    }
    else if (opcao == 9)
    {
        break;
    }
    Console.WriteLine("Fim ...");
    Console.ReadKey();
}

static void AtualizacaoPadrao()
{
    using var context = new AppDbContext();

    Console.WriteLine("\nAtualizando alunos..\n");
    var alunos = context.Alunos.ToList();

    foreach (var aluno in alunos)
    {
        aluno.Nome = "XXXXXXXX";
        aluno.DataMatricula = DateTime.UtcNow;
    }
    context.SaveChanges();

    var registros = context.Alunos.Count();
    Console.WriteLine($"Foram atualizados {registros} da tabela\n");
}

static void AtualizacaoComExecuteUpdate()
{
    using var context = new AppDbContext();

    var atualizados = context.Alunos
        .ExecuteUpdate(b => b.SetProperty(g => g.Nome, "YYYYYYYY")
                             .SetProperty(g => g.DataMatricula, DateTime.UtcNow));

    Console.WriteLine($"{atualizados} alunos foram atualizados da tabela\n");
}

static void AtualizacaoComExecuteUpdateFiltro()
{
    using var context = new AppDbContext();

    var atualizados = context.Alunos.Where(a => a.Ativo)
        .ExecuteUpdate(b => b.SetProperty(g => g.Nome, "ZZZZZZZZ")
                             .SetProperty(g => g.DataMatricula, DateTime.UtcNow));

    Console.WriteLine($"{atualizados} alunos foram atualizados da tabela\n");
}

static void AtualizacaoChangeTracker()
{
    using var context = new AppDbContext();

    var aluno = context.Alunos.FirstOrDefault();
    Console.WriteLine($"\nIdade do aluno : {aluno?.Idade}\n");

    Console.WriteLine($"\nAtualizando a idade do aluno com ExecuteUpdate para 25\n");

    context.Alunos.ExecuteUpdate(b => b.SetProperty(g => g.Idade, 25));

    Console.WriteLine($"\nAtualizando a idade do aluno somando 2 anos\n");

    aluno.Idade += 2;
    context.SaveChanges();

    Console.WriteLine($"\nIdade atual do aluno : {aluno.Idade}\n");
}

static void PopulaTabelas()
{
    using var context = new AppDbContext();
    try
    {
        Console.WriteLine("\nPopulando as tabelas ...");
        SeedDatabase.PopulaDb(context);

        var alunos = context.Alunos.Count();

        Console.WriteLine($"\nExistem {alunos} alunos");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao criar ou acessar o bando de dados : " + ex.ToString());
    }
}
