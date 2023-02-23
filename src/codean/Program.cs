// See https://aka.ms/new-console-template for more information

using codean.analisador.analizadores;
using codean.analisador.modelodados;
using codean.analisador.terminais;

Console.Clear();
Console.WriteLine("Se estiver pronto presione o botão Enter, mas lembre de executar o procedimento a cima!");
Console.ReadKey();


try
{
    var result = Analisador
                .Instance(PowerShellTerminal.Create())
                .AnalizarArquivo(new PathFileForAnalysis())
                .Construir()
                .ProcessarTotalAlteracoesPorArquivo();
    
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