// See https://aka.ms/new-console-template for more information

using codean.analisador.analizadores;
using codean.analisador.modelodados;

Console.WriteLine("Siga os seguintes passos!");
Console.WriteLine("Entre pelo terminal em algum repositório que gostaria de fazer a análise");
Console.WriteLine(@"Execute o comando: git log --all --numstat --date=short --pretty=format:'--%h--%ad--%aN' --no-renames > c:\temp\logfile.log");
Console.WriteLine("Caso tenha vontade de determinar um periodo de corte utilize depois de --no-renames --after=<YYYY-MM-DD> substituindo por uma data no formato apontado");
Console.WriteLine();
Console.WriteLine("Se estiver pronto presione o botão Enter, mas lembre de executar o procedimento a cima!");
Console.ReadKey();


try
{
    var analisador = new Analisador(path: @"c:\codean\logfile.log");
    var result = analisador.ProcessarTotalAlteracoesPorArquivo();
    var table = new ConsoleTables.ConsoleTable("Arquivo", "Total de alterações");

    foreach (var ranking in result.ExibirEmOrderDecrescenteOsTop(Top.Top10))
        table.AddRow(ranking.Nome, ranking.Total);

    table.Write();
}
catch (FileNotFoundException e)
{

    Console.WriteLine(e.Message);
}

Console.ReadKey();