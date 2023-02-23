using codean.analisador.leitorarquivo;
using codean.analisador.terminais;
using System.Management.Automation;

namespace codean.analisador.modelodados
{
    public class GitLog
    {
        private ICommandTerminal commandTerminal;
        private string _pathRepositorio;

        private GitLog(ICommandTerminal commandTerminal)
        {
            _pathRepositorio = string.Empty;
            this.commandTerminal = commandTerminal;
        }

        public static ArquivoDadosGitLog GerarArquivoDosCommitsPorPeriodo(ICommandTerminal commandTerminal, Func<GitLog, ArquivoDadosGitLog> gitlog)
        {
            return gitlog(new GitLog(commandTerminal));
        }

        public GitLog AddPathRepositorioGit(string path)
        {
            _pathRepositorio = path;
            return this;
        }

        public ArquivoDadosGitLog GerarArquivoDadosCommit(PathFileForAnalysis path)
        {

            LimparPasta(path);
            string comando = $"git log --all --numstat --date=short --pretty=format:'--%h--%ad--%aN' --no-renames > {(string)path}";
            if (!string.IsNullOrEmpty(this._pathRepositorio))
                commandTerminal.AddScript($"cd {this._pathRepositorio}");

            commandTerminal.AddScript(comando);
            commandTerminal.Invoke();

            return new ArquivoDadosGitLog(this, path);
        }

        private void LimparPasta(string pach)
            => commandTerminal.DeleteFile(pach);

        private void CriarDiertorioTemp(string path)
            => commandTerminal.DeleteDirectory(path);

        private bool DiretorioTempExiste(string path)
            => commandTerminal.ExistsDirectory(path);

        internal bool IsCreatedFile(string path)
            => commandTerminal.IsCreatedFile(path);

        internal FileGitLog OpenRead(string path)
            => commandTerminal.CreateStream(path);
    }
}
