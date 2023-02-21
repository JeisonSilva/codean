using codean.analisador.leitorarquivo;
using System.Management.Automation;

namespace codean.analisador.modelodados
{
    public class GitLog
    {
        private PowerShell powerShell;
        private string _pathRepositorio;

        private GitLog(PowerShell powerShell)
        {
            this.powerShell = powerShell;
        }

        public static ArquivoDadosGitLog GerarArquivoDosCommitsPorPeriodo(Func<GitLog, ArquivoDadosGitLog> gitlog)
        {
            using var p = PowerShell.Create();
            return gitlog(new GitLog(p));
        }

        public GitLog AddPathRepositorioGit(string path)
        {
            _pathRepositorio = path;
            return this;
        }

        public ArquivoDadosGitLog GerarArquivoDadosCommit()
        {
            if (!DiretorioTempExiste(@"c:\temp\"))
                CriarDiertorioTemp(@"c:\temp\");
            else
                LimparPasta(@"c:\temp\");

            

            string comando = "git log --all --numstat --date=short --pretty=format:'--%h--%ad--%aN' --no-renames > c:\\temp\\logfile.log";
            if (!string.IsNullOrEmpty(this._pathRepositorio))
                powerShell.AddScript($"cd {this._pathRepositorio}");

            powerShell.AddScript(comando);
            powerShell.Invoke();

            return new ArquivoDadosGitLog(@"c:\temp\logfile.log");
        }

        private void LimparPasta(string pach)
            => Directory.Delete(pach, true);

        private void CriarDiertorioTemp(string path)
            => Directory.CreateDirectory(path);

        private bool DiretorioTempExiste(string path)
            => Directory.Exists(path);
    }
}
