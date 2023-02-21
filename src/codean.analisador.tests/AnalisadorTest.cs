using codean.analisador.analizadores;
using codean.analisador.modelodados;
using FluentAssertions;

namespace codean.analisador.tests
{
    public class AnalisadorTest
    {
        [Fact]
        public void Deve_Contruir_Commits_Com_Base_Em_Um_Arquivo_Git_Estruturado()
        {
            var analisador = new Analisador(@"./logfile.txt");
            analisador.Commits.Value.Count().Should().Be(1520);
        }

        [Fact]
        public void Deve_Retornar_Quantidade_Alteracoes_Por_Arquivo_Top_3 ()
        {
            var analisador = new Analisador(@"./logfile.txt");
            var result = analisador.ProcessarTotalAlteracoesPorArquivo().ExibirEmOrderDecrescenteOsTop(Top.Top3);
            result.Count().Should().Be(3);
        }

        [Fact]
        public void Deve_Retornar_Quantidade_Alteracoes_Por_Arquivo_Top_10()
        {
            var analisador = new Analisador(@"./logfile.txt");
            var result = analisador.ProcessarTotalAlteracoesPorArquivo().ExibirEmOrderDecrescenteOsTop(Top.Top10);
            result.Count().Should().Be(10);
        }

        [Fact]
        public void Deve_Retornar_Quantidade_Alteracoes_Por_Arquivo_Filtrando_POr_Extencao()
        {
            var analisador = new Analisador(@"./logfile.txt");
            var result = analisador.ProcessarTotalAlteracoesPorExtencao(ExtencaoArquivo.Cs).ExibirEmOrderDecrescenteOsTop(Top.Top3);
            result.Should().Contain(x => x.Nome.Contains(".cs"));
        }
    }
}
