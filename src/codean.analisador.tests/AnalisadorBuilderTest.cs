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
    public class AnalisadorBuilderTest
    {
        private readonly AutoMocker _mock;
        private readonly Mock<ICommandTerminal> _terminal;

        public AnalisadorBuilderTest()
        {
            _mock = new AutoMocker();
            _terminal = _mock.GetMock<ICommandTerminal>();
            _terminal.Setup(x => x.IsCreatedFile(It.IsAny<string>())).Returns(true);
            _terminal.Setup(x => x.ExistsDirectory(It.IsAny<string>())).Returns(true);
        }


        [Fact]
        public void Deve_Criar_Um_Analisador()
        {
            _terminal.Setup(x => x.CreateStream(It.IsAny<string>())).Returns(GerarArquivoGitLog());

            var builder = Analisador.Instance(_terminal.Object).AnalizarArquivo(new PathFileForAnalysis());
            var analisador = builder.Construir();
            analisador.Should().BeAssignableTo<Analisador>();
        }

        private static FileGitLog GerarArquivoGitLog()
        {
            return new Faker<FileGitLog>("pt_BR").CustomInstantiator(f =>
            {
                return new FileGitLogMock(new StringBuilder()
                .AppendLine("--e342b8e--2022-10-11--dependabot[bot]")
                .AppendLine(";")
                .AppendLine(@"1	1	src/CSharp/CodeCracker/Design/StaticConstructorExceptionCodeFixProvider.cs"));
            }).Generate();
        }
    }
}
