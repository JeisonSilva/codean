using codean.analisador.leitorarquivo;
using codean.analisador.modelodados;
using FluentAssertions;

namespace codean.analisador.tests
{
    public class GitLogTest
    {
        [Fact]
        public void Deve_Gerar_Arquivo_GitLog_Para_Analise_De_Dados()
        {   
            var arquivoDadosGitLog = GitLog.GerarArquivoDosCommitsPorPeriodo((gitlog) => 
            {
                return gitlog.GerarArquivoDadosCommit();
            });

            arquivoDadosGitLog.ArquivoCriado().Should().BeTrue();
        }

        [Fact]
        public void Deve_Informar_Arquivo_Nao_Criado_Quando_Nao_For_Bem_Sucedido_A_Criacao_Do_Arquivo_De_Dados()
        {
            var arquivoDadosGitLog = GitLog.GerarArquivoDosCommitsPorPeriodo((gitlog) =>
            {
                return ArquivoDadosGitLog.New(@"Teste");
            });

            arquivoDadosGitLog.ArquivoCriado().Should().BeFalse();
        }
    }
}
