using codean.analisador.leitorarquivo;
using codean.analisador.modelodados;
using codean.analisador.terminais;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace codean.analisador.tests
{
    public class GitLogTest
    {
        private readonly AutoMocker _mock;
        private readonly Mock<ICommandTerminal> _terminal;

        public GitLogTest()
        {
            _mock = new AutoMocker();
            _terminal = _mock.GetMock<ICommandTerminal>();
        }

        [Fact]
        public void Deve_Informar_Arquivo_Nao_Criado_Quando_Nao_For_Bem_Sucedido_A_Criacao_Do_Arquivo_De_Dados()
        {
            var arquivoDadosGitLog = GitLog.GerarArquivoDosCommitsPorPeriodo(_terminal.Object, (gitlog) =>
            {
                return ArquivoDadosGitLog.New(gitlog, @"Teste");
            });

            arquivoDadosGitLog.IsCreatedFile().Should().BeFalse();
        }

        [Fact]
        public void Deve_Informar_Que_Nao_Existe_Repositorio_Git_Para_Extrair_Log()
        {
            var arquivoDadosGitLog = GitLog.GerarArquivoDosCommitsPorPeriodo(_terminal.Object, (gitlog) =>
            {
                return gitlog
                .AddPathRepositorioGit(@"c:\temp")
                .GerarArquivoDadosCommit(new PathFileForAnalysis());
            });

            arquivoDadosGitLog.IsCreatedFile().Should().BeFalse();
        }
    }
}
