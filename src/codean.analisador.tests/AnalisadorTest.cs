using Bogus;
using codean.analisador.analizadores;
using codean.analisador.leitorarquivo;
using codean.analisador.modelodados;
using codean.analisador.terminais;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using System.Text;

namespace codean.analisador.tests
{
    public class AnalisadorTest
    {
        private AutoMocker _mock;
        private readonly Mock<ICommandTerminal> _terminal;

        public AnalisadorTest()
        {
            _mock = new AutoMocker();
            _terminal = _mock.GetMock<ICommandTerminal>();
            _terminal.Setup(x => x.IsCreatedFile(It.IsAny<string>())).Returns(true);
            _terminal.Setup(x => x.ExistsDirectory(It.IsAny<string>())).Returns(true);
        }

        [Fact]
        public void Deve_Contruir_Commits_Com_Base_Em_Um_Arquivo_Git_Estruturado()
        {
            _terminal.Setup(x => x.CreateStream(It.IsAny<string>())).Returns(GerarArquivoGitLog());

            var analisador = Analisador
                .Instance(_terminal.Object)
                .AnalizarArquivo(new PathFileForAnalysis()).Construir();

            analisador.Commits.Value.Count.Should().Be(1);
            analisador.Commits.Value[0].Arquivos.Count.Should().Be(1);
        }

        [Fact]
        public void Deve_Retornar_Quantidade_Alteracoes_Por_Arquivo_Top_3 ()
        {
            _terminal.Setup(x => x.CreateStream(It.IsAny<string>())).Returns(GerarArquivoGitLogTop3());

            var analisador = Analisador
                .Instance(_terminal.Object).AnalizarArquivo(new PathFileForAnalysis()).Construir();

            var result = analisador.ProcessarTotalAlteracoesPorArquivo().ExibirEmOrderDecrescenteOsTop(Top.Top3);
            result.Count().Should().Be(3);
        }

        [Fact]
        public void Deve_Retornar_Quantidade_Alteracoes_Por_Arquivo_Top_10()
        {
            _terminal.Setup(x => x.CreateStream(It.IsAny<string>())).Returns(GerarArquivoGitLogTop10());

            var analisador = Analisador
                .Instance(_terminal.Object).AnalizarArquivo(new PathFileForAnalysis()).Construir();
            var result = analisador.ProcessarTotalAlteracoesPorArquivo().ExibirEmOrderDecrescenteOsTop(Top.Top10);

            result.Count().Should().Be(10);
        }

        [Fact]
        public void Deve_Retornar_Quantidade_Alteracoes_Por_Arquivo_Filtrando_POr_Extencao()
        {
            _terminal.Setup(x => x.CreateStream(It.IsAny<string>())).Returns(GerarArquivoGitLogTop10ComCsproj());
            var analisador = Analisador.Instance(_terminal.Object).AnalizarArquivo(new PathFileForAnalysis()).Construir();
            var result = analisador.ProcessarTotalAlteracoesPorExtencao(ExtencaoArquivo.Cs).ExibirEmOrderDecrescenteOsTop(Top.Top3);
            result.Should().Contain(x => x.Nome.Contains(".cs"));
            result.Count().Should().Be(3);
        }
        
        private static IFileGitLog GerarArquivoGitLog()
        {
            return new Faker<IFileGitLog>("pt_BR").CustomInstantiator(f =>
            {
                return new FileGitLogMock(new StringBuilder()
                .AppendLine("--e342b8e--2022-10-11--dependabot[bot]")
                .AppendLine(";")
                .AppendLine(@"1	1	src/CSharp/CodeCracker/Design/StaticConstructorExceptionCodeFixProvider.cs"));
            }).Generate();
        }

        private static IFileGitLog GerarArquivoGitLogTop3()
        {
            return new Faker<IFileGitLog>("pt_BR").CustomInstantiator(f =>
            {
                return new FileGitLogMock(new StringBuilder()
                .AppendLine("--e342b8e--2022-10-11--dependabot[bot]")
                .AppendLine(";")
                .AppendLine(@"1	20	src/CSharp/CodeCracker/Design/StaticConstructorExceptionCodeFixProvider.cs")
                .AppendLine(";")
                .AppendLine(@"1	5	src/CSharp/CodeCracker/Design/TesteArquivo1.cs")
                .AppendLine(";")
                .AppendLine(@"1	2	src/CSharp/CodeCracker/Design/TesteArquivo2.cs")
                .AppendLine(";")
                .AppendLine(@"1	10	src/CSharp/CodeCracker/Design/TesteArquivo3.cs"));
        }).Generate();
        }

        private static IFileGitLog GerarArquivoGitLogTop10()
        {
            return new Faker<IFileGitLog>("pt_BR").CustomInstantiator(f =>
            {
                return new FileGitLogMock(new StringBuilder()
                .AppendLine("--e342b8e--2022-10-11--dependabot[bot]")
                .AppendLine(";")
                .AppendLine(@"1	20	src/CSharp/CodeCracker/Design/StaticConstructorExceptionCodeFixProvider.cs")
                .AppendLine(";")
                .AppendLine(@"1	5	src/CSharp/CodeCracker/Design/TesteArquivo1.cs")
                .AppendLine(";")
                .AppendLine(@"1	2	src/CSharp/CodeCracker/Design/TesteArquivo2.cs")
                .AppendLine(";")
                .AppendLine(@"1	10	src/CSharp/CodeCracker/Design/TesteArquivo3.cs")
                .AppendLine(";")
                .AppendLine(@"1	10	src/CSharp/CodeCracker/Design/TesteArquivo4.cs")
                .AppendLine(";")
                .AppendLine(@"1	4	src/CSharp/CodeCracker/Design/TesteArquivo5.cs")
                .AppendLine(";")
                .AppendLine(@"1	1	src/CSharp/CodeCracker/Design/TesteArquivo6.cs")
                .AppendLine(";")
                .AppendLine(@"10	10	src/CSharp/CodeCracker/Design/TesteArquivo7.cs")
                .AppendLine(";")
                .AppendLine(@"1	11	src/CSharp/CodeCracker/Design/TesteArquivo8.cs")
                .AppendLine(";")
                .AppendLine(@"1	10	src/CSharp/CodeCracker/Design/TesteArquivo9.cs")
                .AppendLine(";")
                .AppendLine(@"1	100	src/CSharp/CodeCracker/Design/Top10.cs"));
            }).Generate();
        }

        private static IFileGitLog GerarArquivoGitLogTop10ComCsproj()
        {
            return new Faker<IFileGitLog>("pt_BR").CustomInstantiator(f =>
            {
                return new FileGitLogMock(new StringBuilder()
                .AppendLine("--e342b8e--2022-10-11--dependabot[bot]")
                .AppendLine(";")
                .AppendLine(@"1	20	src/CSharp/CodeCracker/Design/StaticConstructorExceptionCodeFixProvider.cs")
                .AppendLine(";")
                .AppendLine(@"1	5	src/CSharp/CodeCracker/Design/TesteArquivo1.csproj")
                .AppendLine(";")
                .AppendLine(@"1	2	src/CSharp/CodeCracker/Design/TesteArquivo2.cs")
                .AppendLine(";")
                .AppendLine(@"1	10	src/CSharp/CodeCracker/Design/TesteArquivo3.cs")
                .AppendLine(";")
                .AppendLine(@"1	10	src/CSharp/CodeCracker/Design/TesteArquivo4.cs")
                .AppendLine(";")
                .AppendLine(@"1	4	src/CSharp/CodeCracker/Design/TesteArquivo5.csproj")
                .AppendLine(";")
                .AppendLine(@"1	1	src/CSharp/CodeCracker/Design/TesteArquivo6.cs")
                .AppendLine(";")
                .AppendLine(@"10	10	src/CSharp/CodeCracker/Design/TesteArquivo7.cs")
                .AppendLine(";")
                .AppendLine(@"1	11	src/CSharp/CodeCracker/Design/TesteArquivo8.csproj")
                .AppendLine(";")
                .AppendLine(@"1	10	src/CSharp/CodeCracker/Design/TesteArquivo9.cs")
                .AppendLine(";")
                .AppendLine(@"1	100	src/CSharp/CodeCracker/Design/Top10.cs"));
            }).Generate();
        }
    }
}
