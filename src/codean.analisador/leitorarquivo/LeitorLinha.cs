using System.Numerics;
using codean.analisador.modelodados;

namespace codean.analisador.leitorarquivo
{
    public class LeitorLinha
    {
        FileGitLog fileGitLog;
        private OrganizadorCommits _organizador;

        private LeitorLinha(FileGitLog fileGitLog)
        {
            this.fileGitLog = fileGitLog;
            _organizador = OrganizadorCommits.New();
        }

        internal static LeitorLinha New(FileGitLog fileGitLog) => new LeitorLinha(fileGitLog);

        internal void ProximaLinha(Action<OrganizadorCommits> organizadorCommits, Action<List<Commit>> fimLeitura)
        {
            foreach (var linha in fileGitLog.ReadLine)
                _organizador.AddLinha(linha, organizadorCommits);

            fimLeitura(_organizador.Commits.Value);
            _organizador.Dispose();
        }
    }
}
