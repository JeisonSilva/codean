using codean.analisador.modelodados;

namespace codean.analisador.leitorarquivo
{
    public class ArquivoDadosGitLog
    {
        private string path;
        private readonly GitLog gitLog;

        public ArquivoDadosGitLog(modelodados.GitLog gitLog, string path)
        {
            this.path = path;
            this.gitLog = gitLog;
        }

        public static ArquivoDadosGitLog New(GitLog gitLog,string path)
        {
            return new ArquivoDadosGitLog(gitLog, path);
        }

        public bool IsCreatedFile()
            => gitLog.IsCreatedFile(path);

        public void Open(Action<LeitorLinha> action)
        {
            if (IsCreatedFile())
            {
                action(LeitorLinha.New(gitLog.OpenRead(path)));
            }
            else
            {
                throw new FileNotFoundException("Arquivo não encontrado! Execute o procedimento novamente");
            }

        }
    }
}
